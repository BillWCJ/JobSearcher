using System.Collections.Generic;
using Data.Contract.JobMine;

namespace Business.Manager
{
    public class LocalDownloadManager
    {
        public LocalDownloadManager(IJobMineRepo jobMineRepo)
        {
            JobMineRepo = jobMineRepo;
        }
        private IJobMineRepo JobMineRepo { get; set; }

        public IEnumerable<string> DownLoadJobs(string username, string password, string term, string jobStatus, string filePath)
        {
            var jobIDs = (Queue<string>) JobMineRepo.JobInquiry.GetJobIds(term, jobStatus);
            yield return string.Format("Total Number of Jobs Found: {0}", jobIDs.Count);
            foreach (string msg in JobMineRepo.JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, filePath))
                yield return msg;
            yield return "Finished\n";
        }
    }
}