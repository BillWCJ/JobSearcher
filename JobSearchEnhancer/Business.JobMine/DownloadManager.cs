using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Web.JobMine;
using Model.Entities;

namespace Business.JobMine
{
    public class DownloadManager
    {
        UserAccount Account { get; set; }

        DownloadManager(UserAccount account)
        {
            Account = account;
        }
        public void DownLoadJobs(string username, string password, string term, string jobStatus, string filePath)
        {
            Account = new UserAccount { Username = username, Password = password };

            var client = new CookieEnabledWebClient();
            Queue<string> jobIDs;

            Console.WriteLine((string) "Loggedin : {0}", new Login(Account).LoginToJobMine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, client, Account, fileLocation: filePath, numJobsPerFile: 100);
        }
    }
}
