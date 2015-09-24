using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Business.DataBaseSeeder;
using Data.Contract.JobMine;
using Data.Contract.JseDb;
using Data.IO.Local;
using Data.Web.JobMine;
using Data.Web.JobMine.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.Web;
using Moq;

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

        [TestMethod]
        public void DevelopmentTest3()
        {
            //arrange
            var fakeJob = new Job
            {
                Id = 1
            };
            var ua = new UserAccount {};
            var jobMineRepoMock = new Mock<IJobMineRepo>();
            var jseRepoMock = new Mock<IJseDataRepo>();
            var jmis = new JobMineInfoSeeder(ua, jobMineRepoMock.Object);
            jobMineRepoMock.Setup(j => j.JobInquiry.GetJobOverViews(It.IsAny<string>(), It.IsAny<string>())).Returns(GetJov);
            jseRepoMock.Setup(j => j.JobRepo.GetJobIds()).Returns(new List<int>() {99,100000});
            jobMineRepoMock.Setup(j => j.JobDetail.GetJob(It.IsAny<JobOverView>())).Returns<JobOverView>(j =>
            {
                Thread.Sleep(500);
                return fakeJob;
            });

            //act
            jmis.SeedDb(Console.WriteLine, jseRepoMock.Object, "", "", 100);

            //assert
            jseRepoMock.Verify(j => j.JobRepo.Delete(It.IsAny<int>()), Times.Exactly(1));
            jseRepoMock.Verify(j => j.JobRepo.SeedJobAndRelatedEntities(It.IsAny<Job>()), Times.Exactly(99));
            jseRepoMock.Verify(j => j.JobRepo.UpdateWithJov(It.IsAny<JobOverView>()), Times.Exactly(1));
            jobMineRepoMock.Verify(j => j.JobDetail.GetJob(It.IsAny<JobOverView>()), Times.Exactly(99));
        }

        private static IEnumerable<JobOverView> GetJov()
        {
            for (int i = 1; i < 200; i++)
            {
                Thread.Sleep(500);
                yield return new JobOverView
                {
                    Id = i,
                };
            }
            yield return null;
        }
    }
}
