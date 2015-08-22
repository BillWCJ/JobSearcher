using System.Collections.Generic;
using System.Linq;
using Business.Manager;
using Model.Definition;
using Model.Entities.JobMine;
using Xunit;

namespace Test.Common
{
    //TODO To use xunit, you'll need to install the xunit runner from extention tools
    public class JobManagerTest
    {
        private List<Job> _jobs;

        public JobManagerTest()
        {
            GetStubbedData();
        }
 
        [Fact]
        public void CheckTermTest()
        {
            var job = _jobs.FirstOrDefault(j => j.Id.Equals(1));
            var term = JobManager.GetTermDuration(job);
            Assert.Equal(TermType.Eight, term);

            job = _jobs.FirstOrDefault(j => j.Id.Equals(2));
            term = JobManager.GetTermDuration(job);
            Assert.Equal(TermType.Unknown, term);

            job = _jobs.FirstOrDefault(j => j.Id.Equals(6));
            term = JobManager.GetTermDuration(job);
            Assert.Equal(TermType.Both, term);
        }

        private void GetStubbedData()
        {
            _jobs = new List<Job>
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
                },

                new Job
                {
                    Id = 5,
                    JobTitle = ".Net Developer",
                    Comment = "This is an 8 month work term. A 4 month term will not be considered",
                    JobDescription = "Work in a team of Asp.Net developers"
                },

                new Job
                {
                    Id = 6,
                    JobTitle = "Systems analyst",
                    Comment = "This is work is for 4 months. An 8 month work term is also considered",
                    JobDescription = "Work hard play hard"
                }
            };
        }
    }
}
