using System;
using Data.Web.JobMine.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
