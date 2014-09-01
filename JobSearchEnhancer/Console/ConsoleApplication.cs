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
    class ConsoleApplication
    {
        static void Main(string[] args) {

            CookieEnabledWebClient client = ContentExtraction.SetUpCookieEnabledWebClientForLogIn();
            DownloadWriteOpenHtmlData(client, GVar.MenuUrl, "ConfirmLogIn.html");

            CookieEnabledWebClient searchClient = SetUpCookieEnabledWebClientForJobSearch(client);
            DownloadWriteOpenHtmlData(searchClient, GVar.JobSearchUrl, "ConfirmJobSearch.html");

            GetJobsFromWeb(client);
            Console.Write("Application Finished Execution");
            Console.ReadKey();
        }

        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            string data = string.Empty;
            data = client.DownloadString(downloadUrl);
            File.WriteAllText(GVar.LocationFilePath + htmlFileName, data);
            Process.Start(GVar.LocationFilePath + htmlFileName);
        }


        public static CookieEnabledWebClient SetUpCookieEnabledWebClientForJobSearch(CookieEnabledWebClient oldClient)
        {
            CookieEnabledWebClient client = new CookieEnabledWebClient();
            client.setcookie(oldClient.getcookie());
            client.BaseAddress = GVar.JobInquiryUrl;
            NameValueCollection searchData = SetJobSearchData();
            client.UploadValues(GVar.JobInquiryUrl, "POST", searchData);
            client.Headers.Add("user-agent", GVar.UserAgent);
            return client;
        }

        public static NameValueCollection SetJobSearchData()
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
            searchData.Add("ICSID", "IFqDm7vwVA8WwyH25e3/XDSBemEAM4e1lLbMx96hq48=");
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

        private static void GetJobsFromWeb(CookieEnabledWebClient client)
        {
            string info = string.Empty;
            StreamReader reader = StreamReader.Null;
            StreamWriter writer = StreamWriter.Null;

            try
            {
                reader = new StreamReader(GVar.LocationFilePath + "JobList.txt");
                writer = new StreamWriter(GVar.LocationFilePath + "Jobs.txt");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("!Error-FileNotFoundException_In_GetJobsFromWeb: {0}\n", e);
            }
            catch (IOException e)
            {
                Console.WriteLine("!Error-IOException_In_GetJobsFromWeb: {0}\n", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(),e);
            }

            try
            {
                writer.Write("Extract Time:" + DateTime.Now.ToString("h:mm:ss tt"));
                while (!reader.EndOfStream)
                {
                    string jobnum = reader.ReadLine();
                    string url = GVar.JobDetailBaseUrl + jobnum;
                    info = client.DownloadString(url);
                    writer.Write(ContentExtraction.returninfo(ContentExtraction.ExtractJobInfo(info, url)));
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine("!Error-{0}_In_GetJobsFromWeb: {1}\n", e.ToString(),e);
            }
            

            if (reader != StreamReader.Null)
            {
                reader.Close();   
            }
            if (writer != StreamWriter.Null)
            {
                writer.Close();
                Process.Start(GVar.LocationFilePath + "Jobs.txt");
            }
        }
    }
}
