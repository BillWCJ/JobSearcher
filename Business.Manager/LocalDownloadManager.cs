﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility;
using Data.Contract.JobMine;
using Data.EF.JseDb;
using Model.Entities.JobMine;
using Newtonsoft.Json;

namespace Business.Manager
{
    public class LocalDownloadManager
    {
        private static JsonSerializerSettings _jsonSerializerSettings;

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

        public static void ExportJob(Action<string> messageCallBack, string pathLocation, int numJobsPerFile)
        {
            numJobsPerFile = numJobsPerFile > 0 ? numJobsPerFile : 100;
            List<int> jobIds;

            using (var db = new JseDbContext())
            {
                jobIds = db.Jobs.Select(x => x.Id).ToList();
            }
            if (!jobIds.Any())
            {
                messageCallBack("Database does not contain any jobs! Jobs not Exported");
                return;
            }
            messageCallBack("Found {0} Jobs, Starting export".FormatString(jobIds.Count));

            long fileCount = (jobIds.Count + numJobsPerFile - 1)/numJobsPerFile;
            var tasks = new Task[fileCount];
            for (var i = 0; i < fileCount; i++)
            {
                var currentFileJobIds = jobIds.GetRange(i*numJobsPerFile, Math.Min(numJobsPerFile, jobIds.Count - i*numJobsPerFile));
                var currentFilePart = i + 1;
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    GetValue(messageCallBack, pathLocation, numJobsPerFile, currentFilePart, currentFileJobIds);
                });
            }
            Task.WaitAll(tasks);
            messageCallBack("Jobs export finished\n");
        }

        private static void GetValue(Action<string> messageCallBack, string pathLocation, int numJobsPerFile, int currentFilePart, List<int> currentPageJobIds)
        {
            using (var db = new JseDbContext())
            {
                _jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var failures = 0;
                var fileName = "{0}JobExportPart{1}.txt".FormatString(pathLocation, currentFilePart);
                try
                {
                    messageCallBack(string.Format("Writing {0} ({1} Jobs Per File)", fileName, numJobsPerFile));
                    using (var writer = new StreamWriter(fileName))
                    {
                        foreach (var jobId in currentPageJobIds)
                        {
                            try
                            {
                                Job job = db.Jobs.Include(j => j.JobLocation).Include(j => j.Employer).Include(j => j.Levels).Include(j => j.Disciplines)
                                    .FirstOrDefault(j => j.Id == jobId);

                                var serializedString = JsonConvert.SerializeObject(job, _jsonSerializerSettings);
                                writer.Write(serializedString);
                            }
                            catch (Exception e)
                            {
                                messageCallBack("Error occured while writing Job (Id={0}) to {1}: {0}".FormatString(jobId, fileName, e.Message));
                                failures++;
                                Trace.WriteLine(e);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    messageCallBack("Error Occurred: {0}".FormatString(e.Message));
                    failures++;
                    Trace.WriteLine(e);
                }
                finally
                {
                    messageCallBack("Writing to {0} finished with {1} failures".FormatString(fileName, failures));
                }
            }
        }
    }
}