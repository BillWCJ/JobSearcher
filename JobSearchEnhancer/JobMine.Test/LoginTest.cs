using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClientExtension;
using JobMine;

namespace JobMine.Test
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void ConfirmLogin_ShouldReturnTrue_WhenExecute()
        {
            Assert.IsTrue(Login.LoginToJobmine(new CookieEnabledWebClient()));
        }
    }
}
