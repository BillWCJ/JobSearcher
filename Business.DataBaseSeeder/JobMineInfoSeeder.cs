using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<string> SeedDb(string term, string appsAvail, int numberOfJobsToSeed = int.MaxValue)
        {
            using (var db = new JseDbContext())
            {
                yield return "Searching For Jobs";
                int numJobSeeded = 0, numJobUpdated = 0;
                var jobMineRepo = new JobMineRepo(Account);
                IEnumerable<JobOverView> jobOverViews = jobMineRepo.JobInquiry.GetJobOverViews(term, appsAvail).Take(numberOfJobsToSeed);
                yield return "Number of Job Founded: " + jobOverViews.Count();

                foreach (JobOverView jov in jobOverViews)
                {
                    var job = db.Jobs.FirstOrDefault(x => x.Id == jov.Id);

                    if (job == null)
                    {
                        job = jobMineRepo.JobDetail.GetJob(jov);
                        SeedJobAndRelatedEntities(job, db);
                        yield return "Job Seeded: " + ++numJobSeeded;
                    }
                    else
                    {
                        UpdateJob(job, jov, db);
                        yield return "Job Updated: " + ++numJobUpdated;
                    }
                }
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