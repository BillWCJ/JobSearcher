using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utility;
using Data.EF.JseDb;
using Data.Web.JobMine;
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
                int numJobSeeded = 0, numJobUpdated = 0;
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

                foreach (JobOverView jov in jobOverViews)
                {
                    var job = db.Jobs.FirstOrDefault(x => x.Id == jov.Id);

                    if (job == null)
                    {
                        job = jobMineRepo.JobDetail.GetJob(jov);
                        SeedJobAndRelatedEntities(job, db);
                        //messageCallBack("Job Seeded: " + ++numJobSeeded);
                    }
                    else
                    {
                        UpdateJob(job, jov, db);
                        //messageCallBack("Job Updated: " + ++numJobUpdated);
                    }
                }
                messageCallBack("Finished downloading job posting data");
            }
        }

        public static void UpdateJob(Job job, JobOverView jov, JseDbContext db)
        {
            job.NumberOfApplied = jov.NumberOfApplied;
            job.AlreadyApplied = jov.AlreadyApplied;
            job.OnShortList = jov.OnShortList;
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