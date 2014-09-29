using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Diagnostics;
using Data.Web.GoogleApis;
using Model.Entities;
using GlobalVariable;
using System.Xml;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using Data.Web.JobMine;
using Data.EF.ClusterDB;
using Data.IO.Local;

namespace ConsoleApplication 
{
    public class ConsoleApplication
    {
        static void Main(string[] args)
        {
            UserSetting.GetAccount();
            string term = "1149";
            string appsAvail = GVar.JobStatus.AppsAvail;
            var client = new CookieEnabledWebClient();
            using (var db = new ClusterRepository())
            {
                foreach (Job job in db.Jobs)
                {
                    db.Entry(job).Reference(j => j.Employer).Load();
                    db.Entry(job).Reference(j => j.Location).Load();
                    PlaceTextSearch.SetLocationIfIncomplete(client, job.Employer, job.Location);
                }
            }
            //JobMineInitalSeeder.SeedDb(GVar.Account.Username, GVar.Account.Password, term, appsAvail);
            //DownloadJobsDetailsForAllUser();
            //TestDownload("w52jiang", "Ss332640747:)","1149", GVar.JobStatus.AppsAvail, @"C:\Users\BillWenChao\Desktop\");
        }



        private static void DownloadJobsDetailsForAllUser()
        {
            Console.WriteLine("Enter The Term (eg 1149)");
            string term = Console.ReadLine();
            Console.WriteLine("Enter JobStatus (one of the following option: {0},{1},{2},{3})", GVar.JobStatus.Approved,
                GVar.JobStatus.AppsAvail, GVar.JobStatus.Cancelled, GVar.JobStatus.Posted);
            Console.WriteLine("They are Approved, AppsAvail, Cancelled, and Posted respectively");
            string jobStatus = Console.ReadLine();
            Console.WriteLine(@"Please Enter the File Path (eg. C:\Users\BillWenChao\Desktop\  - have to end with '\')");
            string filePath = @"C:\Users\BillWenChao\Desktop\";
            filePath = Console.ReadLine();
            TestDownload(term, jobStatus, filePath);

        }
        private static void TestDownload(string username, string password, string term, string jobStatus, string filePath)
        {
            GVar.Account = new UserAccount { Username = username, Password = password };

            var client = new CookieEnabledWebClient();
            Queue<string> jobIDs;

            Console.WriteLine("Loggedin : {0}", Login.LoginToJobmine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadJobsFromWebToLocal(client, jobIDs, filePath, 100);
        }
        private static void TestDownload(string term, string jobStatus, string filePath)
        {
            Console.WriteLine("Enter JobMine UserName");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();
            GVar.Account = new UserAccount {Username = username, Password = password};

            var client = new CookieEnabledWebClient();
            Queue<string> jobIDs;

            Console.WriteLine("Loggedin : {0}", Login.LoginToJobmine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadJobsFromWebToLocal(client, jobIDs, filePath, 100);
        }

        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            File.WriteAllText(GVar.FilePath + htmlFileName, client.DownloadString(downloadUrl));
            Console.WriteLine("{0} - Opening: {1}", Login.IsLoggedInToJobmine(client), htmlFileName);
            Process.Start(GVar.FilePath + htmlFileName);
        }
    }
}
