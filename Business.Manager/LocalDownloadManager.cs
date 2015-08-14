using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common.Utility;
using Data.Contract.JobMine;
using Data.EF.JseDb;
using Newtonsoft.Json;

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
            yield return string.Format("Total Number of Jobs Found: {0}\n", jobIDs.Count);
            foreach (var msg in JobMineRepo.JobDetail.DownLoadAndWriteJobsToLocal(jobIDs, filePath))
                yield return msg;
            yield return "Finished\n";
        }

        public static void ExportJob(Action<string> messageCallBack, string fileLocation, uint numJobsPerFile)
        {
            using (var db = new JseDbContext())
            {
                var jobIds = new Queue<int>(db.Jobs.Select(x => x.Id));
                var jobCount = jobIds.Count;
                if (jobCount >= 0)
                {
                    messageCallBack("Found {0} Jobs, Starting export".Format(jobCount));
                }
                else
                {
                    messageCallBack("Database does not contain any jobs! Jobs not Exported");
                }

                var success = true;
                for (var currentFilePart = 1;; currentFilePart++)
                {
                    messageCallBack(string.Format("Writing JobExportPart {0} ({1} Jobs Per File)\n", currentFilePart, numJobsPerFile));
                    try
                    {
                        var writer = new StreamWriter(fileLocation + ("JobExportPart" + currentFilePart + ".txt"));
                        for (uint currentFileJobCount = 0; currentFileJobCount < numJobsPerFile && jobIds.Count > 0; currentFileJobCount++)
                        {
                            var currentJobId = jobIds.Dequeue();
                            var job = db.Jobs.Find(currentJobId);
                            writer.Write(JsonConvert.ToString(job));
                        }
                        writer.Close();
                    }
                    catch (Exception e)
                    {
                        success = false;
                        Trace.WriteLine(e);
                    }
                    messageCallBack(string.Format("Writing JobDetailPart" + currentFilePart + " {0}\n", success ? "Succeeded" : "Failed"));
                }
            }
        }
    }
}