using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Diagnostics;
using Data.EF.DBSeeder;
using Data.Web.GoogleApis;
using Model.Definition;
using Model.Entities;
using GlobalVariable;
using System.Xml;
using Data.Web.JobMine;
using Data.EF.ClusterDB;
using Data.IO.Local;

namespace Presentation.Console.Developer
{
    class Program
    {
        static void Main(string[] args)
        {
            string term = "1151";
            string appsAvail = JobStatus.AppsAvail;
            var client = new CookieEnabledWebClient();
            Seeders(term, appsAvail);



            //DownloadJobsDetailsForAllUser();
            //DownLoadJobs("w52jiang", "Ss332640747:)","1149", GVar.JobStatus.AppsAvail, @"C:\Users\BillWenChao\Desktop\");
        }

        private static void Seeders(string term, string appsAvail)
        {
            UserSetting.GetAccount();
            JobMineInfoSeeder.SeedDb(GVar.Account.Username, GVar.Account.Password, term, appsAvail);
            GoogleLocationSeeder.SeedDb();
        }
        public static void DownLoadJobs(string term, string jobStatus, string filePath)
        {
            System.Console.WriteLine("Enter JobMine UserName");
            string username = System.Console.ReadLine();
            System.Console.WriteLine("Enter Password");
            string password = System.Console.ReadLine();
            GVar.Account = new UserAccount { Username = username, Password = password };

            var client = new CookieEnabledWebClient();
            Queue<string> jobIDs;

            System.Console.WriteLine("Loggedin : {0}", Login.LoginToJobMine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            System.Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, client, fileLocation: filePath, numJobsPerFile: 100);
        }
        private static void DownloadJobsDetailsForAllUser()
        {
            System.Console.WriteLine("Enter The Term (eg 1149)");
            string term = System.Console.ReadLine();
            System.Console.WriteLine("Enter JobStatus (one of the following option: {0},{1},{2},{3})", JobStatus.Approved,
                JobStatus.AppsAvail, JobStatus.Cancelled, JobStatus.Posted);
            System.Console.WriteLine("They are Approved, AppsAvail, Cancelled, and Posted respectively");
            string jobStatus = System.Console.ReadLine();
            System.Console.WriteLine(@"Please Enter the File Path (eg. C:\Users\BillWenChao\Desktop\  - have to end with '\')");
            string filePath = @"C:\Users\BillWenChao\Desktop\";
            filePath = System.Console.ReadLine();
            DownLoadJobs(term, jobStatus, filePath);

        }

        private static void DownloadWriteOpenHtmlData(CookieEnabledWebClient client, string downloadUrl, string htmlFileName)
        {
            File.WriteAllText(GVar.FilePath + htmlFileName, client.DownloadString(downloadUrl));
            System.Console.WriteLine("{0} - Opening: {1}", Login.LoginToJobMine(client), htmlFileName);
            Process.Start(GVar.FilePath + htmlFileName);
        }
    }
}
