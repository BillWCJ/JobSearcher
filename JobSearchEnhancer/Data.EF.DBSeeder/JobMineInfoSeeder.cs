﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Web.JobMine;
using GlobalVariable;
using Model.Entities;

namespace Data.EF.ClusterDB
{
    public class JobMineInfoSeeder
    {
        public static void SeedDb(string username, string password, string term, string appsAvail, uint numberOfJobsToSeed = 4294967295)
        {
            using (var db = new ClusterRepository())
            {
                uint count = 0;
                GVar.Account = new UserAccount { Username = username, Password = password };
                CookieEnabledWebClient client = Login.NewJobMineLoggedInWebClient();
                Queue<JobOverView> jobOverViews = JobInquiry.GetJobOverViews(client, term, appsAvail);
                foreach (JobOverView jov in jobOverViews)
                {
                    if(db.Jobs.Any(j => j.Id == jov.Id))
                        continue;

                    Job job = JobDetail.GetJob(client, jov);

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
                    Console.WriteLine("Added" + count + "jobs.");
                    count++;
                    if (count >= numberOfJobsToSeed)
                        break;
                }
            }
        }
    }
}