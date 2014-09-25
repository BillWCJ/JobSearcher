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
using Jobs;
using GlobalVariable;
using System.Xml;
using System.Windows.Forms;
using System.Web;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using JobMine;


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

            Console.WriteLine("Loggedin : {0}", JobMine.Login.LoginToJobmine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadJobsFromWebToLocal(client, jobIDs, filePath, 100);
        }


        private static void Initalize()
        {
            GVar.Account = new AccountInfo();
        }

        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            File.WriteAllText(GVar.FilePath + htmlFileName, client.DownloadString(downloadUrl));
            Console.WriteLine("{0} - Opening: {1}", JobMine.Login.IsLoggedInToJobmine(client), htmlFileName);
            Process.Start(GVar.FilePath + htmlFileName);
        }
    }
}
