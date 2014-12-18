using System;
using System.Collections.Generic;
using Data.Web.JobMine;
using Model.Entities;

namespace Business.Common
{
    public class JobMineManager
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