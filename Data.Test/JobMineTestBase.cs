using System.Net;
using Data.Contract.JobMine;
using Data.Web.JobMine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities.Web;
using Moq;

namespace Data.Test
{
    [TestClass]
    public class JobMineTestBase
    {
        protected Mock<ICookieEnabledWebClient> Client { get; set; }
        protected IJobMineRepo JobMineRepo { get; set; }
        protected Mock<CookieContainer> CookieContainer { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Client = new Mock<ICookieEnabledWebClient>();
            CookieContainer = new Mock<CookieContainer>();
            Client.Setup(c => c.CookieContainer).Returns(CookieContainer.Object);
            JobMineRepo = new JobMineRepo(Client.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }
    }
}