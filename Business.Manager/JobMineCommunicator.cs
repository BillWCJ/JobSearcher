using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.JseDb;
using Data.IO.Local;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.Manager
{
    public class JobMineCommunicator
    {
        public static bool AddToShortList(Job job, UserAccount userAccount)
        {
            try
            {
                var jobmineRepo = new JobMineRepo(userAccount);
                var addSuccess = jobmineRepo.JobInquiry.AddJobToShortList(job.Id, userAccount.Term, userAccount.JobStatus, job.JobTitle, job.Employer.Name);
                if (addSuccess)
                {
                    var repo = new JseDataRepo(new JseDbContext());
                    job.OnShortList = true;
                    repo.JobRepo.UpdateWithJov(job as JobOverView);
                }
                return addSuccess;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
