using System;
using Data.IO.Local;
using Data.Web.JobMine;
using Data.Web.JobMine.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Definition;
using Model.Entities.Web;

namespace Test.All
{
    [TestClass]
    public class Development
    {
        [TestMethod]
        public void DevelopmentTest1()
        {
            bool result = Login.LoginToJobMine(new CookieEnabledWebClient(), "w52jiang", "badpassword");
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void DevelopmentTest2()
        {
            var jobmineRepo = new JobMineRepo(new JseLocalRepo().GetAccount());
            var result = jobmineRepo.JobInquiry.AddJobToShortList(00273242, "1161", JobStatus.Approved, null, "Apple");
        }
    }
}
