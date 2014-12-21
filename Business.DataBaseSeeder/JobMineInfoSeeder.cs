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

        public void SeedDb(string term, string appsAvail, int numberOfJobsToSeed = int.MaxValue)
        {
            using (var db = new JseDbContext())
            {
                var jobMineRepo = new JobMineRepo(Account);
                IEnumerable<JobOverView> jobOverViews = jobMineRepo.JobInquiry.GetJobOverViews(term, appsAvail).Take(numberOfJobsToSeed);
                foreach (JobOverView jov in jobOverViews)
                {
                    if (db.Jobs.Any(j => j.Id == jov.Id))
                        continue;

                    Job job = jobMineRepo.JobDetail.GetJob(jov);

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
    }
}