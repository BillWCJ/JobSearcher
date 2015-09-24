using System;
using Data.Contract.JobMine;
using Data.Contract.JobMine.Interface;
using Data.Web.JobMine.Common;
using Data.Web.JobMine.DataSource;
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
            InitalizeRepos();
        }

        public JobMineRepo(UserAccount account)
            : this(account.JobMineUsername, account.JobMinePassword)
        {
        }

        public JobMineRepo(ICookieEnabledWebClient client)
        {
            if (!Login.IsLoggedIn(client))
                throw new ArgumentException("Not Logged in");
            Client = client;
            JobInquiry = new JobInquiry(Client);
            JobDetail = new JobDetail(Client);
        }

        private ICookieEnabledWebClient Client { get; set; }
        public IJobInquiry JobInquiry { get; private set; }
        public IJobDetail JobDetail { get; private set; }

        private void InitalizeRepos()
        {
            JobInquiry = new JobInquiry(Client);
            JobDetail = new JobDetail(Client);
        }
    }
}