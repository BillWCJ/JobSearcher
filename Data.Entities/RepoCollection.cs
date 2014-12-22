using System.Collections.Generic;
using Data.Contract;
using Data.Contract.GoogleApis;
using Data.Contract.JobMine;
using Data.Contract.JseDb;
using Data.Contract.Local;
using Data.EF.JseDb;
using Data.IO.Local;
using Data.Web.GoogleApis;
using Data.Web.JobMine;
using Model.Entities;

namespace Data.Entities
{
    public class RepoCollection : IRepoCollection
    {
        public RepoCollection(string username, string password, List<string> googleApisKeys)
            : this()
        {
            GoogleRepo = new GoogleRepo(googleApisKeys);
            JobMineRepo = new JobMineRepo(username, password);
        }

        public RepoCollection(UserAccount account)
            : this()
        {
            GoogleRepo = new GoogleRepo(new List<string> {account.GoogleApisBrowserKey});
            JobMineRepo = new JobMineRepo(account.JobMineUsername, account.JobMinePassword);
        }

        private RepoCollection()
        {
            JseDataRepo = new JseDataRepo();
            JseLocalRepo = new JseLocalRepo();
        }

        public IJseDataRepo JseDataRepo { get; set; }
        public IJseLocalRepo JseLocalRepo { get; set; }
        public IGoogleRepo GoogleRepo { get; set; }
        public IJobMineRepo JobMineRepo { get; set; }
    }
}