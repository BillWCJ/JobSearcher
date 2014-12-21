using System;
using Model.Entities;
using Model.Entities.Web;

namespace Data.Web.JobMine
{
    public class JobMineRepo : IJobMineRepo
    {
        public JobMineRepo(string username, string password) : this()
        {
            Client = Login.GetJobMineLoggedInWebClient(username, password);
            if (Client == null)
                throw new ArgumentException("Unable to Login");
        }

        public JobMineRepo(UserAccount account) : this()
        {
            Client = Login.GetJobMineLoggedInWebClient(account.JobMineUsername, account.JobMinePassword);
            if (Client == null)
                throw new ArgumentException("Unable to Login");
        }

        private JobMineRepo()
        {
            JobInquiry = new JobInquiry(Client);
            JobDetail = new JobDetail(Client);
        }

        private CookieEnabledWebClient Client { get; set; }

        public JobInquiry JobInquiry { get; private set; }

        public JobDetail JobDetail { get; private set; }
    }
}