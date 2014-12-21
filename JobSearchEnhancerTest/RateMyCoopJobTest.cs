using System.Collections.Generic;
using Data.Web.RateMyCoopJob;
using Model.Entities.RateMyCoopJob;
using Xunit;

namespace JobSearchEnhancerTest
{
    public class RateMyCoopJobTest
    {
        private IEnumerable<JobReview> list;

        [Fact]
        public void GetJobsTest()
        {
            list = RateMyCoopJob.GetListofJobs();
            Assert.NotEmpty(list);
        }
    }
}
