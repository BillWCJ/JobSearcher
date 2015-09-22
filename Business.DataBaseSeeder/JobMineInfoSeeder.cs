using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Utility;
using Data.EF.JseDb;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.DataBaseSeeder
{
    public class JobMineInfoSeeder
    {
        public JobMineInfoSeeder(UserAccount account)
        {
            Account = account;
        }

        private UserAccount Account { get; set; }

        public void SeedDb(Action<string> messageCallBack, string term, string appsAvail, int numberOfJobsToSeed = int.MaxValue)
        {
            messageCallBack("Now downloading job posting information...");
            using (var db = new JseDbContext())
            {
                messageCallBack("Searching For Jobs");
                int numJobSeeded = 0, numJobUpdated = 0, numJobRemoved = 0;
                JobMineRepo jobMineRepo;
                IEnumerable<JobOverView> jobOverViews = new List<JobOverView>();
                try
                {
                    jobMineRepo = new JobMineRepo(Account);
                    jobOverViews = jobMineRepo.JobInquiry.GetJobOverViews(term, appsAvail).Take(numberOfJobsToSeed);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while getting job list, Please make sure all information has been entered correctly", e);
                }

                var numJob = jobOverViews.Count();
                messageCallBack("Found {0} jobs. please wait around {1} minutes for download to complete".FormatString(numJob, (numJob + 59) / 60));

                var dbJobIds = new HashSet<int>(db.Jobs.Select(j => j.Id));
                var currentJobIds = jobOverViews.Select(j => j.Id);
                var removedJobs = dbJobIds.Except(currentJobIds);

                foreach (JobOverView jov in jobOverViews)
                {
                    try
                    {
                        if (dbJobIds.Contains(jov.Id))
                        {
                            var job = db.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer).FirstOrDefault(x => x.Id == jov.Id);
                            UpdateJob(job, jov, db);
                            numJobUpdated++;
                        }
                        else
                        {
                            var job = jobMineRepo.JobDetail.GetJob(jov);
                            SeedJobAndRelatedEntities(job, db);
                            numJobSeeded++;
                        }
                        messageCallBack(CommonDef.CurrentStatus + "Number of job seeded: {0}; updated: {1}; removed {2}".FormatString(numJobSeeded, numJobUpdated, numJobRemoved));
                    }
                    catch (Exception e)
                    {
                        messageCallBack("Error while seeding or updating job {0} : {1}".FormatString(jov.Id, e.Message));
                    }
                }

                foreach (int jobId in removedJobs)
                {
                    try
                    {
                        var job = db.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer).FirstOrDefault(x => x.Id == jobId);
                        db.Jobs.Remove(job);
                        db.SaveChanges();
                        numJobRemoved++;
                    }
                    catch (Exception e)
                    {
                        messageCallBack("Error while removing job {0} : {1}".FormatString(jobId, e.Message));
                    }
                    messageCallBack(CommonDef.CurrentStatus + "Number of job seeded: {0}; updated: {1}; removed {2}".FormatString(numJobSeeded, numJobUpdated, numJobRemoved));
                }
                messageCallBack(CommonDef.CurrentStatus);
                messageCallBack("Finished downloading job posting data");
            }
        }

        public static void UpdateJob(Job job, JobOverView jov, JseDbContext db)
        {
            job.NumberOfApplied = jov.NumberOfApplied;
            job.AlreadyApplied = jov.AlreadyApplied;
            job.OnShortList = jov.OnShortList;

            db.Jobs.Attach(job);
            var entry = db.Entry(job);
            entry.Property(e => e.NumberOfApplied).IsModified = true;
            entry.Property(e => e.AlreadyApplied).IsModified = true;
            entry.Property(e => e.OnShortList).IsModified = true;
            //entry.Property(e => e.Disciplines).IsModified = false;
            //entry.Property(e => e.Levels).IsModified = false;
            //entry.Property(e => e.Employer).IsModified = false;
            //entry.Property(e => e.JobLocation).IsModified = false;
            //entry.Property(e => e.LocalShortList).IsModified = false;
            db.SaveChanges();
        }

        public static void SeedJobAndRelatedEntities(Job job, JseDbContext db)
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