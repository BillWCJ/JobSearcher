using System;
using Model.Entities;

namespace Data.Web.JobMine
{
    public class JobMineRepo : IJobMineRepo
    {
        public JobMineRepo(string username, string password) : this()
        {
            Client = Login.GetJobMineLoggedInWebClient(username, password);
        }

        public JobMineRepo(UserAccount account) : this()
        {
            Client = Login.GetJobMineLoggedInWebClient(account.Username, account.Password);
        }

        private JobMineRepo()
        {
            if (Client == null)
                throw new ArgumentException("Unable to Login");
            JobInquiry = new JobInquiry(Client);
            JobDetail = new JobDetail(Client);
        }

        private CookieEnabledWebClient Client { get; set; }

        public JobInquiry JobInquiry { get; private set; }

        public JobDetail JobDetail { get; private set; }
    }
}