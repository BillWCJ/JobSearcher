using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using GlobalVariable;
using HtmlAgilityPack;
using Model.Entities;

namespace Data.Web.JobMine
{
    public static class JobInquiry
    {
        private const int FirstRowIndex = 3;

        /// <summary>
        ///     Return the JobInquiryData Navevaluecollection for post operation to JobMine JobInquiryPage
        /// </summary>
        /// <param name="iCStateNum"></param>
        /// <param name="iCAction"></param>
        /// <param name="iCsid"></param>
        /// <param name="term"></param>
        /// <param name="jobStatus"></param>
        /// <param name="location"></param>
        /// <param name="disciplines"></param>
        /// <returns></returns>
        public static NameValueCollection JobInquiryData(string iCStateNum, string iCAction, string iCsid, string term,
            string jobStatus, string location = "", string[] disciplines = null)
        {
            var searchData = new NameValueCollection
            {
                {"ICAJAX", "1"},
                {"ICNAVTYPEDROPDOWN", "1"},
                {"ICType", "Panel"},
                {"ICElementNum", "0"},
                {"ICStateNum", iCStateNum},
                {"ICAction", iCAction},
                {"ICXPos", "0"},
                {"ICYPos", "110"},
                {"ResponsetoDiffFrame", "-1"},
                {"TargetFrameName", "None"},
                {"ICFocus", ""},
                {"ICSaveWarningFilter", "0"},
                {"ICChanged", "-1"},
                {"ICResubmit", "0"},
                {"ICSID", iCsid},
                {"ICModalWidget", "0"},
                {"ICZoomGrid", "0"},
                {"ICZoomGridRt", "0"},
                {"ICModalLongClosed", ""},
                {"ICActionPrompt", "false"},
                {"ICFind", ""},
                {"ICAddCount", ""},
                {"UW_CO_JOBSRCH_UW_CO_WT_SESSION", term},
            };
            if (!string.IsNullOrEmpty(jobStatus))
                searchData.Add("UW_CO_JOBSRCH_UW_CO_JS_JOBSTATUS", jobStatus);
            if (!string.IsNullOrEmpty(location))
                searchData.Add("UW_CO_JOBSRCH_UW_CO_LOCATION", location);
            if (disciplines != null)
                for (int i = 1; i <= ((disciplines.Length <= 3) ? disciplines.Length : 3); i++)
                    if (!string.IsNullOrEmpty(disciplines[i - 1]))
                        searchData.Add("UW_CO_JOBSRCH_UW_CO_ADV_DISCP" + i, disciplines[i - 1]);

            return searchData;
        }

        /// <summary>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="term"></param>
        /// <param name="jobStatus"></param>
        /// <returns></returns>
        public static Queue<JobOverView> GetJobOverViews(CookieEnabledWebClient client, string term, string jobStatus)
        {
            const int firstSearch = -1;
            int numPages = firstSearch;
            string iCAction = GVar.IcAction.Search, iCsid = "";
            int iCStateNum = 1;
            var jobOverViews = new Queue<JobOverView>();

            for (int currentPageNum = 1; (numPages == firstSearch) || currentPageNum <= numPages; currentPageNum++)
            {
                var doc = new HtmlDocument();

                if (iCAction != GVar.IcAction.Down && numPages != firstSearch)
                    iCAction = GVar.IcAction.Down;

                if (iCAction == GVar.IcAction.Search)
                {
                    string srcUrl = GetIframeSrcUrl(client);
                    string downloadString = client.DownloadString(srcUrl);
                    doc.LoadHtml(downloadString);
                    iCsid = GetIcsid(doc);
                    iCStateNum = GetIcStateNum(doc);
                }
                else
                    iCStateNum++;

                string jobinfo = GetJobinfo(client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(jobinfo);

                if (iCAction == GVar.IcAction.Search)
                    numPages = NumPages(doc);

                ConCatJobOverView(GetCurrentPageJobOverViews(doc), jobOverViews);
            }
            return jobOverViews;
        }

        private static IEnumerable<JobOverView> GetCurrentPageJobOverViews(HtmlDocument doc)
        {
            var currentPageJobOverViews = new Queue<JobOverView>();
            HtmlNode thisTableNode = GetTableNode(doc);
            for (int childIndex = FirstRowIndex, count = 0; (childIndex < thisTableNode.ChildNodes.Count); childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name == "tr")
                {
                    string thisJobTitle = GetJobTitle(row, count);
                    string thisEmployerName = GetEmployerName(row, count);
                    string thisRegion = GetRegion(row, count);
                    string thisUnitName = GetUnitName(row, count);
                    string thisJobId = GetJobId(row, count);

                    if (!IsCorrectJobId(thisJobId) || string.IsNullOrEmpty(thisJobTitle) ||
                        string.IsNullOrEmpty(thisEmployerName) || string.IsNullOrEmpty(thisRegion)) continue;

                    var thisJobOverView = new JobOverView
                    {
                        JobTitle = thisJobTitle,
                        Employer = new Employer {Name = thisEmployerName, UnitName = thisUnitName},
                        Location = new Location {Region = thisRegion},
                        JobMineId = Convert.ToInt32(thisJobId),
                    };
                    currentPageJobOverViews.Enqueue(thisJobOverView);
                    count++;
                }
            }
            return currentPageJobOverViews;
        }

        public static Queue<string> GetJobIDs(CookieEnabledWebClient client, string term, string jobStatus)
        {
            const int firstSearch = -1;
            int numPages = firstSearch;
            string iCAction = GVar.IcAction.Search;
            string iCsid = "";
            int iCStateNum = 1;
            var jobIDs = new Queue<string>();


            for (int currentPageNum = 1; currentPageNum <= numPages || numPages == firstSearch; currentPageNum++)
            {
                var doc = new HtmlDocument();

                if (iCAction != GVar.IcAction.Down && numPages != firstSearch)
                    iCAction = GVar.IcAction.Down;

                if (iCAction == GVar.IcAction.Search)
                {
                    string srcUrl = GetIframeSrcUrl(client);
                    doc.LoadHtml(client.DownloadString(srcUrl));
                    iCsid = GetIcsid(doc);
                    iCStateNum = GetIcStateNum(doc);
                }
                else
                    iCStateNum++;

                string jobinfo = GetJobinfo(client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(jobinfo);

                if (iCAction == GVar.IcAction.Search)
                    numPages = NumPages(doc);

                ConCatJobId(GetCurrentPageJobIDs(doc), jobIDs);
            }
            return jobIDs;
        }

        private static IEnumerable<string> GetCurrentPageJobIDs(HtmlDocument doc)
        {
            var currentPageJobIDs = new Queue<string>();
            HtmlNode thisTableNode = GetTableNode(doc);
            for (int childIndex = FirstRowIndex, count = 0; childIndex < thisTableNode.ChildNodes.Count; childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name != "tr") continue;
                string thisJobId = GetJobId(row, count);
                if (!IsCorrectJobId(thisJobId)) continue;
                currentPageJobIDs.Enqueue(thisJobId);
                count++;
            }
            return currentPageJobIDs;
        }

        private static void ConCatJobOverView(IEnumerable<JobOverView> temp, Queue<JobOverView> jobIDs)
        {
            foreach (JobOverView jobId in temp)
                jobIDs.Enqueue(jobId);
        }

        private static void ConCatJobId(IEnumerable<string> temp, Queue<string> jobIDs)
        {
            foreach (string jobId in temp)
                jobIDs.Enqueue(jobId);
        }

        private static string GetJobinfo(CookieEnabledWebClient client, string iCAction, string term, string iCsid,
            int iCStateNum, string jobStatus)
        {
            const string url = GVar.JobInquiryUrlShortpsc, method = "POST";
            return
                Encoding.UTF8.GetString(client.UploadValues(url, method,
                    JobInquiryData(iCStateNum.ToString(CultureInfo.InvariantCulture), iCAction, iCsid, term, jobStatus)));
        }

        private static int NumPages(HtmlDocument doc)
        {
            string currentJobsDisplayString =
                doc.DocumentNode.SelectNodes("//span[@class='PSGRIDCOUNTER']")[1].InnerHtml;
            //var currentJobsDisplayString = doc.DocumentNode.SelectSingleNode("/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[2]/td[1]/div[1]/span[2]").InnerHtml;
            const string seperator = "of ";
            int numberOfJobs =
                Convert.ToInt32(
                    currentJobsDisplayString.Substring(
                        currentJobsDisplayString.IndexOf(seperator, StringComparison.Ordinal) + seperator.Length));


            return numberOfJobs/25 + ((numberOfJobs%25 == 0) ? 0 : 1);
        }

        private static HtmlNode GetTableNode(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]");
        }

        private static string GetJobId(HtmlNode row, int count)
        {
            return GetConvertedNodeInnerHtml(row, "//span[@id='UW_CO_JOBRES_VW_UW_CO_JOB_ID$", count);
        }

        private static string GetJobTitle(HtmlNode row, int count)
        {
            return GetConvertedNodeInnerHtml(row, "//span[@id='UW_CO_JOBTITLE_HL$", count);
        }

        private static string GetEmployerName(HtmlNode row, int count)
        {
            return GetConvertedNodeInnerHtml(row, "//span[@id='UW_CO_JOBRES_VW_UW_CO_PARENT_NAME$", count);
        }

        private static string GetRegion(HtmlNode row, int count)
        {
            return GetConvertedNodeInnerHtml(row, "//span[@id='UW_CO_JOBRES_VW_UW_CO_WORK_LOCATN$", count);
        }

        private static string GetUnitName(HtmlNode row, int count)
        {
            return GetConvertedNodeInnerHtml(row, "//span[@id='UW_CO_JOBRES_VW_UW_CO_EMPLYR_NAME1$", count);
        }

        private static string GetConvertedNodeInnerHtml(HtmlNode row, string path, int count)
        {
            return row.SelectSingleNode(path + count + "']")
                .InnerHtml.Replace("&nbsp;", " ")
                .Replace("<br />", "\n")
                .Replace("&amp;", "&"); 
        }

        /// <summary>
        ///     Get the Iframe Url of the job search result table
        /// </summary>
        /// <param name="client">Loggedin WebClient</param>
        /// <returns>Iframe URL</returns>
        private static string GetIframeSrcUrl(CookieEnabledWebClient client)
        {
            return
                @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&amp;FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&amp;IsFolder=false&amp;IgnoreParamTempl=FolderPath%2cIsFolder&amp;PortalActualURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentProvider=WORK&amp;PortalCRefLabel=Job%20Inquiry&amp;PortalRegistryName=EMPLOYEE&amp;PortalServletURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fSS%2f&amp;PortalURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2f&amp;PortalHostNode=WORK&amp;NoCrumbs=yes&amp;PortalKeyStruct=yes";

            var doc = new HtmlDocument();
            doc.LoadHtml(client.DownloadString(GVar.JobInquiryUrlpsp));
            string src;
            src = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[1]/iframe[1]").Attributes["src"].Value;
            //src = doc.DocumentNode.SelectSingleNode("//iframe[@id='ptifrmtgtframe']").Attributes["src"].Value;
            return src;
        }

        /// <summary>
        ///     Get the ICStateNum of the page in Html
        /// </summary>
        /// <param name="doc">Current page in Html</param>
        /// <returns>string ICStateNum</returns>
        /// <remarks>Safe Extraction: DocumentNode.SelectSingleNode("//input[@id='ICStateNum']").Attributes["value"].Value;</remarks>
        private static int GetIcStateNum(HtmlDocument doc)
        {
            string iCStateNum = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[3]").Attributes["value"].Value;
            return Convert.ToInt32(iCStateNum);
        }

        /// <summary>
        ///     Get the ICSID of the page in Html
        /// </summary>
        /// <param name="doc">Current page in Html</param>
        /// <returns>string ICSID</returns>
        /// <remarks>Safe Extraction: DocumentNode.SelectSingleNode("//input[@id='ICSID']").Attributes["value"].Value;</remarks>
        private static string GetIcsid(HtmlDocument doc)
        {
            string iCsid = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[13]").Attributes["value"].Value;
            return iCsid;
        }

        public static bool IsCorrectJobId(string jobId)
        {
            var regex = new Regex("[0-9]{8,8}");
            bool right = regex.IsMatch(jobId);
            return regex.IsMatch(jobId);
        }
    }
}