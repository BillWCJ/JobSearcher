using System;
using System.Collections.Generic;
using System.Linq;
using Business.Manager;
using Model.Entities;
using Xunit;

namespace JobSearchEnhancerTest
{
    //TODO To use xunit, you'll need to install the xunit runner from extention tools
    public class JobManagerTest
    {
        private List<Job> Jobs;

        public JobManagerTest()
        {
            GetStubbedData();
        }
 
        [Fact]
        public void CheckTermTest()
        {
            var job = Jobs.FirstOrDefault(j => j.Id.Equals(1));
            var term = JobManager.GetTermDuration(job);
            Assert.Equal(TermType.Eight, term);

            job = Jobs.FirstOrDefault(j => j.Id.Equals(2));
            term = JobManager.GetTermDuration(job);
            Assert.Equal(TermType.Not_Specified, term);
        }

        private void GetStubbedData()
        {
            Jobs = new List<Job>
            {
                new Job
                {
                    Id = 1,
                    JobTitle = "Fibreline Process",
                    Comment = "8-month work term required.   " +
                              "You must be able to do a sequence change to accommodate " +
                              "an eight month work term in order to apply to this position.",
                    JobDescription = "The successful candidate will work on site at the mill " +
                                     "location primarily under the direction of a Process Engineer " +
                                     "or Process Specialist"
                },

                new Job
                {
                    Id = 2,
                    JobTitle = "Engineering",
                    Comment = " ",
                    JobDescription = "Product Engineering is a key engineering position with company-wide exposure."
                },

                new Job
                {
                    Id = 3,
                    JobTitle = "Junior Developer",
                    Comment = "Doesnt have term duration details",
                    JobDescription = "If you are interested you can work for 4 or 8 months"
                },

                new Job
                {
                    Id = 4,
                    JobTitle = "Senior Java Developer",
                    Comment = "This is a 4 month work term",
                    JobDescription = ""
                }
            };
        }
    }
}
