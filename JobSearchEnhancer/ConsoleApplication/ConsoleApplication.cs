using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Diagnostics;
using WebClientExtension;
using ContentProcess;
using Jobs;
using GlobalVariable;

namespace ConsoleApplication 
{
    public class ConsoleApplication
    {
        static void Main(string[] args) {
            CookieEnabledWebClient client = ContentExtraction.SetUpClient(GVar.LogInUrl, ContentExtraction.LoginData());
            //DownloadWriteOpenHtmlData(client, GVar.LogInUrl, "ConfirmLogIn.html");
            //DownloadWriteOpenHtmlData(client, GVar.TestJobDetailUrl, "ConfirmJobDetail.html");
            //DownloadWriteOpenHtmlData(client, GVar.JobInquiryUrlShort, "ConfirmLogIn.html");
            //DownloadWriteOpenHtmlData(client, GVar.TestJobDetailUrl, "ConfirmJobDetail.html");

            ContentExtraction.GetJobsFromWeb(client);
            Console.ReadLine();
        }


        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            string data = string.Empty;
            data = client.DownloadString(downloadUrl);
            File.WriteAllText(GVar.LocationFilePath + htmlFileName, data);
            Console.WriteLine("{0} - Opening: {1}", ContentExtraction.ConfirmLogin(client), htmlFileName);
            Process.Start(GVar.LocationFilePath + htmlFileName);
        }

        public static NameValueCollection JobSearchData()
        {
            NameValueCollection searchData = new NameValueCollection();
            searchData.Add("ICAJAX", "1");
            searchData.Add("ICAction", "UW_CO_JOBSRCHDW_UW_CO_DW_SRCHBTN");
            searchData.Add("ICActionPrompt", "false");
            searchData.Add("ICAddCount", "");
            searchData.Add("ICChanged", "-1");
            searchData.Add("ICElementNum", "0");
            searchData.Add("ICFind", "");
            searchData.Add("ICFocus", "");
            searchData.Add("ICModalLongClosed", "");
            searchData.Add("ICModalWidget", "0");
            searchData.Add("ICNAVTYPEDROPDOWN", "1");
            searchData.Add("ICResubmit", "0");
            searchData.Add("ICSID", GetICSID());
            searchData.Add("ICSaveWarningFilter", "0");
            searchData.Add("ICStateNum", "2");
            searchData.Add("ICType", "Panel");
            searchData.Add("ICXPos", "0");
            searchData.Add("ICYPos", "0");
            searchData.Add("ICZoomGrid", "0");
            searchData.Add("ICZoomGridRt", "0");
            searchData.Add("ResponsetoDiffFrame", "-1");
            searchData.Add("TargetFrameName", "None");
            //searchData.Add("UW_CO_JOBSRCH_UW_CO_ADV_DISCP1", "");
            //searchData.Add("UW_CO_JOBSRCH_UW_CO_ADV_DISCP2", "");
            //searchData.Add("UW_CO_JOBSRCH_UW_CO_ADV_DISCP3", "");
            //searchData.Add("UW_CO_JOBSRCH_UW_CO_LOCATION", "");
            searchData.Add("UW_CO_JOBSRCH_UW_CO_WT_SESSION", "1149");
            return searchData;
        }

        private static string GetICSID()
        {
            return "vmm1e4Ya3QVteEx8I0IrqxlXpR5ZcsE5mvd2UQmCcCc=";
        }
    }
}
