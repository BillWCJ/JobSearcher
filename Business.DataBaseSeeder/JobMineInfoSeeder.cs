using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utility;
using Data.Contract.JobMine;
using Data.Contract.JseDb;
using Data.EF.JseDb;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.DataBaseSeeder
{
    internal struct JobMineInfoSeederProgressInfo
    {
        public int NumJobFound, NumJobSeeded, NumJobUpdated, NumJobRemoved;
        public bool IsAllJobFound, IsAllJobRetrived;
        public ConcurrentQueue<JobOverView> JobOverViews;
        public ConcurrentQueue<Job> Jobs;
        public ConcurrentQueue<JobOverView> ToBeUpdatedJobOverViews;
        public readonly HashSet<int> DbJobIds;
        public List<int> CurrentJobIds, RemovedJobIds;
        public readonly object DbLock;

        public JobMineInfoSeederProgressInfo(IJseDataRepo repo)
        {
            DbJobIds = new HashSet<int>(repo.JobRepo.GetJobIds());
            NumJobFound = 0;
            NumJobSeeded = 0;
            NumJobUpdated = 0;
            NumJobRemoved = 0;
            IsAllJobRetrived = false;
            IsAllJobFound = false;
            JobOverViews = new ConcurrentQueue<JobOverView>();
            Jobs = new ConcurrentQueue<Job>();
            CurrentJobIds = new List<int>();
            RemovedJobIds = new List<int>();
            ToBeUpdatedJobOverViews = new ConcurrentQueue<JobOverView>();
            DbLock = new object();
        }
    }

    public class JobMineInfoSeeder
    {
        public JobMineInfoSeeder(UserAccount account, IJobMineRepo jobMineRepo)
        {
            Account = account;
            _jobMineRepo = jobMineRepo;
        }

        private readonly IJobMineRepo _jobMineRepo;
        private UserAccount Account { get; set; }

        public void SeedDb(Action<string> messageCallBack, IJseDataRepo repo, string term, string appsAvail, int numberOfJobsToSeed = int.MaxValue)
        {
            messageCallBack("Now downloading job posting information...");
            var progressInfo = new JobMineInfoSeederProgressInfo(repo);

            Action<JobMineInfoSeederProgressInfo> currentSeedingProgressUpdate =
                info =>
                    messageCallBack(CommonDef.CurrentStatus +
                                    "Found {0} jobs in total. {1} new jobs Added; {2} updated; {3} removed".FormatString(info.NumJobFound, info.NumJobSeeded, info.NumJobUpdated, info.NumJobRemoved));

            messageCallBack("Searching For Jobs");
            foreach (var jov in _jobMineRepo.JobInquiry.GetJobOverViews(term, appsAvail).Take(numberOfJobsToSeed))
            {
                if (jov != null)
                {
                    progressInfo.JobOverViews.Enqueue(jov);
                    progressInfo.CurrentJobIds.Add(jov.Id);
                    progressInfo.NumJobFound++;
                }
                else
                {
                    break;
                }

                currentSeedingProgressUpdate(progressInfo);
            }
            progressInfo.IsAllJobFound = true;
            progressInfo.RemovedJobIds = progressInfo.DbJobIds.Except(progressInfo.CurrentJobIds).ToList();



            var getJobDetailTasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                var getJobDetailTask = Task.Factory.StartNew(() =>
                {
                    GetJobDetails(ref progressInfo, currentSeedingProgressUpdate);
                }, Task.Factory.CancellationToken);
                getJobDetailTasks.Add(getJobDetailTask);
            }

            var updateJovToDbTask = Task.Factory.StartNew(() =>
            {
                UpdateJovToDb(ref progressInfo, repo, currentSeedingProgressUpdate);
            }, Task.Factory.CancellationToken);

            var seedJobToDbTask = Task.Factory.StartNew(() =>
            {
                SeedJobToDb(repo, ref progressInfo, currentSeedingProgressUpdate);
            }, Task.Factory.CancellationToken);

            var removeJobFromDbTask = Task.Factory.StartNew(() =>
            {
                RemoveJobFromDb(messageCallBack, repo, ref progressInfo, currentSeedingProgressUpdate);
            }, Task.Factory.CancellationToken);

            Task.WaitAll(getJobDetailTasks.ToArray());
            progressInfo.IsAllJobRetrived = true;

            updateJovToDbTask.Wait();
            seedJobToDbTask.Wait();
            removeJobFromDbTask.Wait();

            messageCallBack(CommonDef.CurrentStatus);
            messageCallBack("Finished downloading job posting data");
        }

        private static void RemoveJobFromDb(Action<string> messageCallBack, IJseDataRepo repo, ref JobMineInfoSeederProgressInfo progressInfo, Action<JobMineInfoSeederProgressInfo> currentSeedingProgressUpdate)
        {
            while (!progressInfo.IsAllJobFound)
                Thread.Sleep(1000);

            foreach (int jobId in progressInfo.RemovedJobIds)
            {
                try
                {
                    lock (progressInfo.DbLock)
                    {
                        repo.JobRepo.Delete(jobId);
                    }
                    progressInfo.NumJobRemoved++;
                }
                catch (Exception e)
                {
                    messageCallBack("Error while removing job {0} : {1}".FormatString(jobId, e.Message));
                }
                currentSeedingProgressUpdate(progressInfo);
            }
        }

        private static void SeedJobToDb(IJseDataRepo db, ref JobMineInfoSeederProgressInfo progressInfo, Action<JobMineInfoSeederProgressInfo> currentSeedingProgressUpdate)
        {
            while (!progressInfo.IsAllJobRetrived || progressInfo.Jobs.Any())
            {
                Job job = null;
                progressInfo.Jobs.TryDequeue(out job);

                if (job != null)
                {
                    lock (progressInfo.DbLock)
                    {
                        db.JobRepo.SeedJobAndRelatedEntities(job);
                    }
                    progressInfo.NumJobSeeded++;
                    currentSeedingProgressUpdate(progressInfo);
                }
            }
        }

        private static void UpdateJovToDb(ref JobMineInfoSeederProgressInfo progressInfo, IJseDataRepo db, Action<JobMineInfoSeederProgressInfo> currentSeedingProgressUpdate)
        {
            while (!progressInfo.IsAllJobRetrived || progressInfo.ToBeUpdatedJobOverViews.Any())
            {
                JobOverView jov;
                progressInfo.ToBeUpdatedJobOverViews.TryDequeue(out jov);
                if (jov != null)
                {
                    lock (progressInfo.DbLock)
                    {
                        db.JobRepo.UpdateWithJov(jov);
                    }
                    progressInfo.NumJobUpdated++;
                    currentSeedingProgressUpdate(progressInfo);
                }
            }
        }

        private void GetJobDetails(ref JobMineInfoSeederProgressInfo progressInfo, Action<JobMineInfoSeederProgressInfo> currentSeedingProgressUpdate)
        {
            while (!progressInfo.IsAllJobFound || progressInfo.JobOverViews.Any())
            {
                if (progressInfo.JobOverViews.Any())
                {
                    JobOverView jov = null;
                    progressInfo.JobOverViews.TryDequeue(out jov);

                    if (jov != null)
                    {
                        if (progressInfo.DbJobIds.Contains(jov.Id))
                        {
                            progressInfo.ToBeUpdatedJobOverViews.Enqueue(jov);
                        }
                        else
                        {
                            var job = _jobMineRepo.JobDetail.GetJob(jov);
                            progressInfo.Jobs.Enqueue(job);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
                currentSeedingProgressUpdate(progressInfo);
            }
        }

        public static void SeedJobAndRelatedEntities(Job job, IJseDbContext db)
        {
            Employer existingEmployer = db.Employers.FirstOrDefault(e => e.Name == job.Employer.Name && e.UnitName == job.Employer.UnitName);
            if (existingEmployer != null)
            {
                foreach (Job existingJob in existingEmployer.Jobs)
                {
                    if (existingJob.JobLocation.Region == job.JobLocation.Region)
                    {
                        job.JobLocation = existingJob.JobLocation;
                        break;
                    }
                }
                job.Employer = existingEmployer;
            }

            db.Jobs.Add(job);
            db.SaveChanges();
        }
    }
}