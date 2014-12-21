using System;
using System.Collections.Generic;
using System.Diagnostics;
using Data.Web.JobMine;

namespace Business.Manager
{
    public class LocalDownloadManager
    {
        public IEnumerable<string> DownLoadJobs(string username, string password, string term, string jobStatus, string filePath)
        {
            JobMineRepo jobMineRepo = null;
            bool isLoggedIn = true;
            try
            {
                jobMineRepo = new JobMineRepo(username, password);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                isLoggedIn = false;
            }
            yield return string.Format("Loggedin : {0}", isLoggedIn);

            if (isLoggedIn)
            {
                var jobIDs = (Queue<string>) jobMineRepo.JobInquiry.GetJobIds(term, jobStatus);
                yield return string.Format("Total Number of Jobs Found: {0}", jobIDs.Count);
                foreach (string msg in jobMineRepo.JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, filePath))
                    yield return msg;
                yield return "Finished\n";
            }
        }
    }
}