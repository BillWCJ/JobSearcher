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
            string data = string.Empty;
            WebClientCookieEnabled client = ContentExtraction.SetUpWebClientCookieEnabled();
            data = client.DownloadString(GVar.JobSearchUrl);
            File.WriteAllText(GVar.LocationFilePath + "ConfirmLogIn.html", data);
            Process.Start(GVar.LocationFilePath + "ConfirmLogIn.html");

            StreamReader sr = new StreamReader(GVar.LocationFilePath + "JobList.txt");
            StreamWriter op = new StreamWriter(GVar.LocationFilePath + "Jobs.txt");

            op.Write("Extract Time:" + DateTime.Now.ToString("h:mm:ss tt"));
            while (!sr.EndOfStream) {
                string jobnum = sr.ReadLine();
                string url = GVar.JobDetailBaseUrl + jobnum;
                data = client.DownloadString(url);
                op.Write(ContentExtraction.returninfo(ContentExtraction.ExtractJobInfo(data, url)));
            }

            sr.Close();
            op.Close();
            Process.Start(GVar.LocationFilePath + "Jobs.txt");

        }
    }
}
