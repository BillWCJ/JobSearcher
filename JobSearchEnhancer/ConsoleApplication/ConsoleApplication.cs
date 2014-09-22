using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Web.UI.WebControls;
using System.Windows;
using System.Diagnostics;
using WebClientExtension;
using ContentProcess;
using Jobs;
using GlobalVariable;
using System.Xml;
using System.Windows.Forms;
using System.Web;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;


namespace ConsoleApplication 
{
    public class ConsoleApplication
    {
        static void Main(string[] args)
        {
            //Initalize();
            DownloadJobsDetailsForAllUser();
        }

        private static void DownloadJobsDetailsForAllUser()
        {
            Console.WriteLine("Enter JobMine UserName");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            GVar.Account = new AccountInfo(username, password);

            var client = new CookieEnabledWebClient();
            Console.WriteLine("Enter The Term (eg 1149)");
            string term = Console.ReadLine();
            Console.WriteLine("Enter JobStatus (one of the following option: {0},{1},{2},{3})", GVar.JobStatus.Approved,
                GVar.JobStatus.AppsAvail, GVar.JobStatus.Cancelled, GVar.JobStatus.Posted);
            Console.WriteLine("They are Approved, AppsAvail, Cancelled, and Posted respectively");
            string jobStatus = Console.ReadLine();
            Console.WriteLine(@"Please Enter the File Path (eg. C:\Users\BillWenChao\Desktop\  - have to end with '\')");
            string filePath = @"C:\Users\BillWenChao\Desktop\";
            filePath = Console.ReadLine();

            Queue<string> jobIDs;

            Console.WriteLine("Loggedin : {0}", ContentExtraction.LoginToJobmine(client).ToString());
            jobIDs = GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            ContentExtraction.DownLoadJobsFromWebToLocal(client, jobIDs, filePath, 100);
        }

        private static Queue<string> GetJobIDs(CookieEnabledWebClient client, string term, string jobStatus)
        {
            const int firstSearch = -1;
            int numPages = firstSearch;
            string iCAction = GVar.ICAction.Search;
            string iCsid = "";
            int iCStateNum = 1;
            Queue<string> jobIDs= new Queue<string>();
            

            for (int currentPageNum = 1; currentPageNum <= numPages || numPages == firstSearch ; currentPageNum++)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();

                if (iCAction != GVar.ICAction.Down && numPages != firstSearch)
                    iCAction = GVar.ICAction.Down;

                if (iCAction == GVar.ICAction.Search)
                {
                    string srcUrl = GetIframeSrcUrl(client);
                    doc.LoadHtml(client.DownloadString(srcUrl));
                    iCsid = GetICSID(doc);
                    iCStateNum = GetICStateNum(doc);
                }
                else
                {
                    iCStateNum++;
                }

                var s = GetJobinfo(client, iCAction, term, iCsid, iCStateNum, jobStatus);
                doc.LoadHtml(s);

                if (iCAction == GVar.ICAction.Search)
                {
                    numPages = NumPages(doc);
                }

                ConCatJobID(GetCurrentPageJobIDs(doc), jobIDs);
            }
            return jobIDs;
        }

        private static Queue<string> GetCurrentPageJobIDs(HtmlDocument doc)
        {
            Queue<string> jobIdOfCurrentPage = new Queue<string>();
            HtmlNode thisTableNode =
                doc.DocumentNode.SelectSingleNode(
                    "/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]");
            for (int childIndex = 3, count = 0; (childIndex < thisTableNode.ChildNodes.Count); childIndex++)
            {
                HtmlNode row = thisTableNode.ChildNodes[childIndex];
                if (row.Name == "tr")
                {
                    string thisJobId =
                        row.SelectSingleNode("//span[@id='UW_CO_JOBRES_VW_UW_CO_JOB_ID$" + count + "']").InnerHtml;
                    if (ContentExtraction.IsCorrectJobID(thisJobId))
                    {
                        jobIdOfCurrentPage.Enqueue(thisJobId);
                        count++;
                    }
                }
            }
            return jobIdOfCurrentPage;
        }

        private static int NumPages(HtmlDocument doc)
        {
            var currentJobsDisplayString = doc.DocumentNode.SelectNodes("//span[@class='PSGRIDCOUNTER']")[1].InnerHtml;
            //var currentJobsDisplayString = doc.DocumentNode.SelectSingleNode("/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[2]/td[1]/div[1]/span[2]").InnerHtml;
            const string seperator = "of ";
            int numberOfJobs = Convert.ToInt32(currentJobsDisplayString.Substring(currentJobsDisplayString.IndexOf(seperator) + seperator.Length));


            return numberOfJobs/25 + ((numberOfJobs%25 == 0) ? 0 : 1);
        }

        private static void ConCatJobID(Queue<string> temp, Queue<string> jobIDs)
        {
            foreach (string jobId in temp)
            {
                jobIDs.Enqueue(jobId);
            }
        }
        private static string GetJobinfo(CookieEnabledWebClient client, string iCAction, string term, string iCSID, int iCStateNum, string jobStatus)
        {
            const string url = GVar.JobInquiryUrlShortpsc, method = "POST";
            return Encoding.UTF8.GetString(client.UploadValues(url, method, JobSearchData(iCStateNum.ToString(), iCAction, iCSID, term, jobStatus)));
        }
        private static int GetICStateNum(HtmlDocument doc)
        {
            string iCStateNum;
            iCStateNum = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[3]").Attributes["value"].Value;
            //iCStateNum = doc.DocumentNode.SelectSingleNode("//input[@id='ICStateNum']").Attributes["value"].Value;
            return Convert.ToInt32( iCStateNum);
        }
        private static string GetICSID(HtmlDocument doc)
        {
            string iCSID;
            iCSID = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[13]").Attributes["value"].Value;
            //iCSID = doc.DocumentNode.SelectSingleNode("//input[@id='ICSID']").Attributes["value"].Value;
            return iCSID;
        }

        private static string GetIframeSrcUrl(CookieEnabledWebClient client)
        {
            return
                @"https://jobmine.ccol.uwaterloo.ca/psc/SS/EMPLOYEE/WORK/c/UW_CO_STUDENTS.UW_CO_JOBSRCH.GBL?pslnkid=UW_CO_JOBSRCH_LINK&amp;FolderPath=PORTAL_ROOT_OBJECT.UW_CO_JOBSRCH_LINK&amp;IsFolder=false&amp;IgnoreParamTempl=FolderPath%2cIsFolder&amp;PortalActualURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentURL=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2fEMPLOYEE%2fWORK%2fc%2fUW_CO_STUDENTS.UW_CO_JOBSRCH.GBL%3fpslnkid%3dUW_CO_JOBSRCH_LINK&amp;PortalContentProvider=WORK&amp;PortalCRefLabel=Job%20Inquiry&amp;PortalRegistryName=EMPLOYEE&amp;PortalServletURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsp%2fSS%2f&amp;PortalURI=https%3a%2f%2fjobmine.ccol.uwaterloo.ca%2fpsc%2fSS%2f&amp;PortalHostNode=WORK&amp;NoCrumbs=yes&amp;PortalKeyStruct=yes";

            HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(client.DownloadString(GVar.JobInquiryUrlpsp));
            string src;
            src = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[1]/iframe[1]").Attributes["src"].Value;
            //src = doc.DocumentNode.SelectSingleNode("//iframe[@id='ptifrmtgtframe']").Attributes["src"].Value;
            return src;
        }
        private static void Initalize()
        {
            GVar.Account = new AccountInfo();
        }
        private static bool IsValidUrl(string url)
        {
            Uri uri = null;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || null == uri) return false;
            else return true;
        }
        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            File.WriteAllText(GVar.FilePath + htmlFileName, client.DownloadString(downloadUrl));
            Console.WriteLine("{0} - Opening: {1}", ContentExtraction.IsLoggedInToJobmine(client), htmlFileName);
            Process.Start(GVar.FilePath + htmlFileName);
        }
        public static NameValueCollection JobSearchData(string iCStateNum, string iCAction, string iCSID, string term, string jobStatus)
        {
            return JobSearchData(iCStateNum, iCAction, iCSID, term, jobStatus, null, null);
        }
        public static NameValueCollection JobSearchData(string iCStateNum, string iCAction, string iCSID, string term, string jobStatus, string location, string[] disciplines)
        {
            var searchData = new NameValueCollection
            {
                {"ICAJAX","1"},
                {"ICNAVTYPEDROPDOWN","1"},
                {"ICType","Panel"},
                {"ICElementNum","0"},
                {"ICStateNum",iCStateNum},
                {"ICAction", iCAction},
                {"ICXPos","0"},
                {"ICYPos","110"},
                {"ResponsetoDiffFrame","-1"},
                {"TargetFrameName","None"},
                {"ICFocus",""},
                {"ICSaveWarningFilter","0"},
                {"ICChanged","-1"},
                {"ICResubmit","0"},
                {"ICSID",iCSID},
                {"ICModalWidget","0"},
                {"ICZoomGrid","0"},
                {"ICZoomGridRt","0"},
                {"ICModalLongClosed",""},
                {"ICActionPrompt","false"},
                {"ICFind",""},
                {"ICAddCount",""},
                {"UW_CO_JOBSRCH_UW_CO_WT_SESSION",term},
            };
            if (!string.IsNullOrEmpty(jobStatus))
                searchData.Add("UW_CO_JOBSRCH_UW_CO_JS_JOBSTATUS", jobStatus);
            if (!string.IsNullOrEmpty(location))
                searchData.Add("UW_CO_JOBSRCH_UW_CO_LOCATION", location);
            if (disciplines != null)
                for (int i = 1; i <= ((disciplines.Length <= 3)? disciplines.Length : 3); i++)
                    if (!string.IsNullOrEmpty(disciplines[i-1]))
                        searchData.Add("UW_CO_JOBSRCH_UW_CO_ADV_DISCP"+i, disciplines[i-1]);

            return searchData;
        }
        private static string GetICSID(string html)
        {
            try
            {
                int start = html.IndexOf("value=\"", html.IndexOf("id=\"ICSID\""));
                int end = html.IndexOf("\">");
                return html.Substring(start, end - start);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
