using System;
using System.Collections.Generic;
using Data.Web.JobMine;
using GlobalVariable;
using Model.Entities;

namespace TextDownload
{
    public class JobMine
    {
        public static void DownLoadJobs(string username, string password, string term, string jobStatus, string filePath)
        {
            GVar.Account = new UserAccount { Username = username, Password = password };

            var client = new CookieEnabledWebClient();
            Queue<string> jobIDs;

            Console.WriteLine("Loggedin : {0}", Login.LoginToJobMine(client).ToString());
            jobIDs = JobInquiry.GetJobIDs(client, term, jobStatus);
            int numjob = jobIDs.Count;
            Console.WriteLine("Total Number of Jobs Found: {0}", numjob);
            JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, client, fileLocation: filePath, numJobsPerFile: 100);
        }
    }
}
