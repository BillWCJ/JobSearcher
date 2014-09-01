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
