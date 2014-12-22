using System;
using Data.Contract.JobMine;
using Data.Contract.JobMine.Interface;
using Model.Entities;
using Model.Entities.Web;

namespace Data.Web.JobMine
{
    public class JobMineRepo : IJobMineRepo
    {
        public JobMineRepo(string username, string password)
        {
            Client = Login.GetJobMineLoggedInWebClient(username, password);
            if (Client == null)
                throw new ArgumentException("Unable to Login");
            JobInquiry = new JobInquiry(Client);
            JobDetail = new JobDetail(Client);
        }

        public JobMineRepo(UserAccount account)
            : this(account.JobMineUsername, account.JobMinePassword)
        {
        }

        private CookieEnabledWebClient Client { get; set; }

        public IJobInquiry JobInquiry { get; private set; }

        public IJobDetail JobDetail { get; private set; }
    }
}