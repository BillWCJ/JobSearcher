using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.EF.ClusterDB;
using Data.Web.JobMine;
using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class JobMineInfoSeeder
    {
        UserAccount Account { get; set; }

        public JobMineInfoSeeder(UserAccount account)
        {
            Account = account;
        }

        public void SeedDb(string term, string appsAvail, int numberOfJobsToSeed = int.MaxValue)
        {
            using (var db = new JseDbContext())
            {
                var jobMineRepo = new JobMineRepo(Account);
                var jobOverViews = jobMineRepo.JobInquiry.GetJobOverViews(term, appsAvail).Take(numberOfJobsToSeed);
                foreach (JobOverView jov in jobOverViews)
                {
                    if(db.Jobs.Any(j => j.Id == jov.Id))
                        continue;

                    Job job = jobMineRepo.JobDetail.GetJob(jov);

                    Employer existingEmployer = db.Employers.FirstOrDefault(e => e.Name == job.Employer.Name && e.UnitName == job.Employer.UnitName);
                    if (existingEmployer != null)
                    {
                        foreach (Job existingJob in existingEmployer.Jobs)
                        {
                            if (existingJob.Location.Region == job.Location.Region)
                            {
                                job.Location = existingJob.Location;
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
