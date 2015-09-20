using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.IO.Local;
using Data.Web.JobMine;
using Model.Definition;
using Model.Entities.JobMine;

namespace Business.Manager
{
    public class JobMineCommunicator
    {
        public static bool AddToShortList(Job job)
        {
            var account = new JseLocalRepo().GetAccount();
            var jobmineRepo = new JobMineRepo(account);
            return jobmineRepo.JobInquiry.AddJobToShortList(job.Id, "1161", JobStatus.Approved, job.JobTitle, job.Employer.Name);
        }
    }
}
