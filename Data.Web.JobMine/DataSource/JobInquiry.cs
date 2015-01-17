using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Data.Contract.JobMine.Interface;
using HtmlAgilityPack;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.Web;

namespace Data.Web.JobMine.DataSource
{
    //public class JobInquiry : IJobInquiry
    //{

    //    static CookieEnabledWebClient Client { get; set; }
    //    public JobInquiry(CookieEnabledWebClient client)
    //    {
    //        Client = client;
    //    }
    //    /// <summary>
    //    ///     Get all the JobOverViews from inquiry page
    //    /// </summary>
    //    public IEnumerable<JobOverView> GetJobOverViews(string term, string jobStatus) { return JobInquiryHelpper.GetJobInquiryPageObject(Client, term, jobStatus, GetJobOverView); }

    //    public IEnumerable<string> GetJobIds(string term, string jobStatus) { return JobInquiryHelpper.GetJobInquiryPageObject(Client, term, jobStatus, GetJobId); }

    //    private static JobOverView GetJobOverView(HtmlNode row, int count)
    //    {
    //        string jobId = GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count);
    //        var jobOverView = new JobOverView
    //        {
    //            JobTitle = " ", //GetConvertedNodeInnerHtml(row, ColumnPath.JobTitle, count),
    //            Employer = new Employer
    //            {
    //                Name = GetConvertedNodeInnerHtml(row, ColumnPath.EmployerName, count),
    //                UnitName = GetConvertedNodeInnerHtml(row, ColumnPath.UnitName, count)
    //            },
    //            JobLocation = new JobLocation
    //            {
    //                Region = GetConvertedNodeInnerHtml(row, ColumnPath.Region, count)
    //            },
    //            Id = Convert.ToInt32(jobId),
    //        };
    //        if (Utility.IsCorrectJobId(jobId) && Utility.IsJobOverViewCompleted(jobOverView))
    //            return jobOverView;
    //        return null;
    //    }

    //    private static string GetJobId(HtmlNode row, int count) { return GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count); }

    //    private static string GetConvertedNodeInnerHtml(HtmlNode row, string path, int count)
    //    {
    //        return
    //            row.SelectSingleNode(path + count + "']")
    //                .InnerHtml.Replace("&nbsp;", " ")
    //                .Replace("<br />", "\n")
    //                .Replace("&amp;", "&");
    //    }
    //}

    public class JobInquiry : IJobInquiry
    {
        /// <summary>
        ///     Because each real row are accompany by a row of empty text and first row is the title row, this is why the first
        ///     row index starts at 3
        /// </summary>
        private const int FirstRowIndex = 3;

        /// <summary>
        ///     Constant value that indicate it is the first time searching the job inquiry page, which means some extra operation
        ///     needs to be completed
        /// </summary>
        private const int FirstSearch = -1;

        public JobInquiry(ICookieEnabledWebClient client)
        {
            Client = client;
        }

        private static ICookieEnabledWebClient Client { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="term"></param>
        /// <param name="jobStatus"></param>
        /// <returns></returns>
        public IEnumerable<JobOverView> GetJobOverViews(string term, string jobStatus)
        {
            int numPages = FirstSearch;
            string iCAction = IcAction.Search;
            string iCsid = "";
            int iCStateNum = 1;
            var jobOverViews = new Queue<JobOverView>();

            for (int currentPageNum = 1; (numPages == FirstSearch) || currentPageNum <= numPages; currentPageNum++)
            {
                Trace.TraceInformation("GetJobOverViews Parsing Page " + currentPageNum);
                var doc = new HtmlDocument();
                SetInquiryData(Client, numPages, ref iCAction, ref iCsid, ref iCStateNum);

                string jobinfo = GetJobinfo(Client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(jobinfo);

                if (iCAction == IcAction.Search)
                    numPages = GetNumberOfPages(doc);

                GetCurrentPageJobOverViews(doc, jobOverViews);
            }
            return jobOverViews;
        }

        public IEnumerable<string> GetJobIds(string term, string jobStatus)
        {
            int numPages = FirstSearch;
            string iCAction = IcAction.Search;
            string iCsid = "";
            int iCStateNum = 1;
            var jobIds = new Queue<string>();

            for (int currentPageNum = 1; currentPageNum <= numPages || numPages == FirstSearch; currentPageNum++)
            {
                var doc = new HtmlDocument();
                SetInquiryData(Client, numPages, ref iCAction, ref iCsid, ref iCStateNum);

                string jobinfo = GetJobinfo(Client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(jobinfo);

                if (iCAction == IcAction.Search)
                    numPages = GetNumberOfPages(doc);

                GetCurrentPageJobIDs(doc, jobIds);
            }
            return jobIds;
        }

        /// <summary>
        ///     Return the JobInquiryData Navevaluecollection for post operation to JobMine JobInquiryPage
        /// </summary>
        public static NameValueCollection JobInquiryData(string iCStateNum, string iCAction, string iCsid, string term,
            string jobStatus = "POST", string location = "", string discipline1 = "", string discipline2 = "", string discipline3 = "")
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
                {"UW_CO_JOBSRCH_UW_CO_JOB_TITLE", ""},
                {"UW_CO_JOBSRCH_UW_CO_EMPLYR_NAME", ""},
                {"UW_CO_JOBSRCH_UW_CO_LOCATION", location},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP1", discipline1},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP2", discipline2},
                {"UW_CO_JOBSRCH_UW_CO_ADV_DISCP3", discipline3},
                {"UW_CO_JOBSRCH_UW_CO_JS_JOBSTATUS", jobStatus}
            };
            return searchData;
        }

        private static void SetInquiryData(ICookieEnabledWebClient client, int numPages, ref string iCAction, ref string iCsid, ref int iCStateNum)
        {
            var doc = new HtmlDocument();
            if (iCAction != IcAction.Down && numPages != FirstSearch)
                iCAction = IcAction.Down;

            if (iCAction == IcAction.Search)
            {
                string srcUrl = GetIframeSrcUrl(client);
                string downloadString = client.DownloadString(srcUrl);
                doc.LoadHtml(downloadString);
                iCsid = GetIcsid(doc);
                iCStateNum = GetIcStateNum(doc);
            }
            else
                iCStateNum++;
        }

        private static IEnumerable<JobOverView> GetCurrentPageJobOverViews(HtmlDocument doc,
            Queue<JobOverView> jobOverViews)
        {
            HtmlNode thisTableNode = GetTableNode(doc);
            for (int childIndex = FirstRowIndex, count = 0; (childIndex < thisTableNode.ChildNodes.Count); childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name == "tr")
                {
                    JobOverView thisJobOverView = GetJobOverView(row, count);
                    jobOverViews.Enqueue(thisJobOverView);
                    count++;
                }
            }
            return jobOverViews;
        }

        private static void GetCurrentPageJobIDs(HtmlDocument doc, Queue<string> jobIds)
        {
            HtmlNode thisTableNode = GetTableNode(doc);
            for (int childIndex = FirstRowIndex, count = 0; childIndex < thisTableNode.ChildNodes.Count; childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name != "tr") continue;
                string thisJobId = GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count);
                if (!IsCorrectJobId(thisJobId)) continue;
                jobIds.Enqueue(thisJobId);
                count++;
            }
        }

        private static string GetJobinfo(ICookieEnabledWebClient client, string iCAction, string term, string iCsid,
            int iCStateNum, string jobStatus)
        {
            const string url = JobMineDef.JobInquiryUrlShortpsc, method = "POST";
            NameValueCollection jobInquiryData = JobInquiryData(iCStateNum.ToString(CultureInfo.InvariantCulture), iCAction, iCsid, term, jobStatus);
            byte[] values = client.UploadValues(url, method, jobInquiryData);
            return Encoding.UTF8.GetString(values);
        }

        private static int GetNumberOfPages(HtmlDocument doc)
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

        public static bool IsJobOverViewCompleted(JobOverView jov)
        {
            return jov.Id > 0 && !string.IsNullOrEmpty(jov.JobTitle) &&
                   !string.IsNullOrEmpty(jov.Employer.Name) && !string.IsNullOrEmpty(jov.JobLocation.Region);
        }

        private static HtmlNode GetTableNode(HtmlDocument doc)
        {
            return
                doc.DocumentNode.SelectSingleNode(
                    "/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]");
        }

        private static JobOverView GetJobOverView(HtmlNode row, int count)
        {
            string jobId = GetConvertedNodeInnerHtml(row, ColumnPath.JobId, count);
            if (IsCorrectJobId(jobId))
                return new JobOverView
                {
                    JobTitle = " ", //GetConvertedNodeInnerHtml(row, ColumnPath.JobTitle, count),
                    Employer = new Employer
                    {
                        Name = GetConvertedNodeInnerHtml(row, ColumnPath.EmployerName, count),
                        UnitName = GetConvertedNodeInnerHtml(row, ColumnPath.UnitName, count)
                    },
                    JobLocation = new JobLocation {Region = GetConvertedNodeInnerHtml(row, ColumnPath.Region, count)},
                    Id = Convert.ToInt32(jobId),
                };
            return new JobOverView();
        }

        private static string GetConvertedNodeInnerHtml(HtmlNode row, string path, int count)
        {
            return
                row.SelectSingleNode(path + count + "']")
                    .InnerHtml.Replace("&nbsp;", " ")
                    .Replace("<br />", "\n")
                    .Replace("&amp;", "&");
        }

        /// <summary>
        ///     Get the Iframe Url of the job search result table
        /// </summary>
        /// <param name="client">Loggedin WebClient</param>
        /// <returns>Iframe URL</returns>
        private static string GetIframeSrcUrl(ICookieEnabledWebClient client)
        {
            //var doc = new HtmlDocument();
            //string inquirypagehtml = client.DownloadString(JobMineDef.JobInquiryUrlpsp);
            //doc.LoadHtml(inquirypagehtml);
            //string src =
            //    doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[1]/iframe[1]").Attributes["src"].Value;
            //src = doc.DocumentNode.SelectSingleNode("//iframe[@id='ptifrmtgtframe']").Attributes["src"].Value;
            return
                @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&amp;FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&amp;IsFolder=false&amp;IgnoreParamTempl=FolderPath%2cIsFolder&amp;PortalActualURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentProvider=WORK&amp;PortalCRefLabel=Job%20Inquiry&amp;PortalRegistryName=EMPLOYEE&amp;PortalServletURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fSS%2f&amp;PortalURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2f&amp;PortalHostNode=WORK&amp;NoCrumbs=yes&amp;PortalKeyStruct=yes";
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

        public struct ColumnPath
        {
            public const string JobId = "//span[@id='UW_CO_JOBRES_VW_UW_CO_JOB_ID$";
            public const string JobTitle = "//span[@id='UW_CO_JOBTITLE_HL$";
            public const string EmployerName = "//span[@id='UW_CO_JOBRES_VW_UW_CO_PARENT_NAME$";
            public const string Region = "//span[@id='UW_CO_JOBRES_VW_UW_CO_WORK_LOCATN$";
            public const string UnitName = "//span[@id='UW_CO_JOBRES_VW_UW_CO_EMPLYR_NAME1$";
        }
    }
}