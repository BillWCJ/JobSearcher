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
            Initalize();
            var client = new CookieEnabledWebClient();
            const string term = "1149";

            if (!ContentExtraction.LoginToJobmine(client))
                Console.WriteLine("NotLoggedIn");

            string iCStateNum, iCSID;
            Queue<string> jobIDs = GetJobIDs((int)GVar.ICActionEnum.Search, client, term, iCSID, iCStateNum);
            for (int i = 0; i < 2; i++)
            {
                jobIDs.Concat(GetJobIDs((int)GVar.ICActionEnum.Down, client, term, iCSID, iCStateNum));
            }
        }

        private static Queue<string> GetJobIDs(int iCAction, CookieEnabledWebClient client, string term, string ICSID, string iCStateNum)
        {
            Queue<string> jobIDs = new Queue<string>();
            HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string srcUrl = GetIframeSrcUrl(client);
            doc.LoadHtml(client.DownloadString(srcUrl));
            var s = GetJobinfo(client, doc, iCAction, term);
            doc.LoadHtml(s);
            HtmlNode thisTableNode = doc.DocumentNode.SelectSingleNode("/page[1]/field[1]/tr[29]/td[2]/div[1]/table[1]/tr[2]/td[1]/table[1]/tr[1]/td[1]/table[1]");
            for (int i = 3, count = 0; (i < thisTableNode.ChildNodes.Count); i++)
            {
                HtmlNode row = thisTableNode.ChildNodes[i];
                if (row.Name == "tr")
                {
                    string thisJobID = row.SelectSingleNode("//span[@id='UW_CO_JOBRES_VW_UW_CO_JOB_ID$" + count + "']").InnerHtml;
                    if (ContentExtraction.IsCorrectJobID(thisJobID))
                    {
                        jobIDs.Enqueue(thisJobID);
                        count++;
                    }
                }
            }
            return jobIDs;
        }

        private static string GetJobinfo(CookieEnabledWebClient client, HtmlDocument doc, int iCAction, string term)
        {
            const string url = GVar.JobInquiryUrlShortpsc, method = "POST";
            string iCSID = GetICSID(doc), iCStateNum = GetICStateNum(doc);
            return Encoding.UTF8.GetString(client.UploadValues(url, method, JobSearchData(iCStateNum, iCAction, iCSID, term)));
        }
        private static string GetICStateNum(HtmlDocument doc)
        {
            string iCStateNum;
            iCStateNum = doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/input[3]").Attributes["value"].Value;
            //iCStateNum = doc.DocumentNode.SelectSingleNode("//input[@id='ICStateNum']").Attributes["value"].Value;
            return iCStateNum;
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
        public static NameValueCollection JobSearchData(string iCStateNum, int iCAction, string iCSID, string term)
        {
            return JobSearchData(iCStateNum, iCAction, iCSID, term, null, null);
        }
        public static NameValueCollection JobSearchData(string iCStateNum, int iCAction, string iCSID, string term, string[] disciplines, string location)
        {
            var searchData = new NameValueCollection
            {
                {"ICAJAX","1"},
                {"ICNAVTYPEDROPDOWN","1"},
                {"ICType","Panel"},
                {"ICElementNum","0"},
                {"ICStateNum",iCStateNum},
                {"ICAction",GVar.ICAction[iCAction]},
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
