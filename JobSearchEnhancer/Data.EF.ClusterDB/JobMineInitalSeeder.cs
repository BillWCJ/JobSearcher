using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Web.JobMine;
using GlobalVariable;
using Model.Entities;

namespace Data.EF.ClusterDB
{
    public class JobMineInitalSeeder
    {
        public static void SeedDb(string username, string password, string term, string appsAvail)
        {
            using (var db = new ClusterRepository())
            {
                GVar.Account = new UserAccount { Username = username, Password = password };
                CookieEnabledWebClient client = Login.NewJobMineLoggedInWebClient();
                Queue<JobOverView> jobOverViews = JobInquiry.GetJobOverViews(client, term, appsAvail);
                foreach (JobOverView jov in jobOverViews)
                {
                    Job job = JobDetail.GetJob(client, jov);
                    Employer existingEmployer =
                        db.Employers.FirstOrDefault(e => e.Name == job.Employer.Name && e.UnitName == job.Employer.UnitName);
                    if (existingEmployer != null)
                        job.Employer = existingEmployer;

                    db.Jobs.Add(job);
                    db.SaveChanges();
                    Console.WriteLine("Added");
                }
            }
        }
    }
}
