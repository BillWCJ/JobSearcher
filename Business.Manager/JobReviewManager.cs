using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Utility;
using Data.EF.JseDb;
using Model.Entities.RateMyCoopJob;

namespace Business.Manager
{
    public class JobReviewManager
    {
        public static IEnumerable<JobReview> GetJobReview(int jobId, int employerId)
        {
            var jobreviews = new List<JobReview>();
            using (var db = new JseDbContext())
            {
                foreach (var linker in db.JobLinkers.Include(j => j.Job).Include(j => j.JobReview).Where(j => j.Job.Id == jobId))
                {
                    jobreviews.Add(linker.JobReview);
                }

                if (!jobreviews.Any())
                {
                    jobreviews = GetJobReview(db, employerId);
                }
            }
            return jobreviews;
        }

        private static List<JobReview> GetJobReview(JseDbContext db, int employerId)
        {
            var jobreviews = new List<JobReview>();
            foreach (var linker in db.EmployerLinkers.Include(j => j.Employer).Include(j => j.EmployerReview.JobReviews).Where(j => j.Employer.Id == employerId))
            {
                jobreviews.AddRange(linker.EmployerReview.JobReviews);
            }
            return jobreviews;
        }

        public static IEnumerable<JobReview> GetJobReview(string employerName, string jobTitle)
        {
            var returnlist = new List<JobReview>();
            using (var db = new JseDbContext())
            {
                var list = new List<JobReview>();
                int employerNameTolerance = 0, jobTitelTolerance = 0;
                while (!list.Any())
                {
                    bool anyEmployerMatch = false, anyJobMatch = false;
                    foreach (EmployerReview employer in db.EmployerReviews.Include(e => e.JobReviews))
                    {
                        if (IsSimilar(employerName, employer.Name, employerNameTolerance))
                        {
                            anyEmployerMatch = true;
                            IEnumerable<JobReview> jobs = employer.JobReviews.Where(job => IsSimilar(jobTitle, job.Title, jobTitelTolerance));
                            if (jobs.Any())
                                anyJobMatch = true;
                            list.AddRange(jobs);
                        }
                    }
                    if (!anyEmployerMatch)
                        employerNameTolerance = employerNameTolerance < 2 ? employerNameTolerance++ : employerNameTolerance;
                    if (anyEmployerMatch && !anyJobMatch)
                        jobTitelTolerance = jobTitelTolerance < 2 ? jobTitelTolerance++ : jobTitelTolerance;
                }
                returnlist = list.Select(jobReview => db.JobReviews.Include(j => j.EmployerReview).Include(j => j.JobRatings).FirstOrDefault(j => j.JobReviewId == jobReview.JobReviewId)).ToList();
            }
            return returnlist;
        }

        private static bool IsSimilar(string toBeMatchedName, string name, int tolerance = 0)
        {
            string[] splits = toBeMatchedName.Split(' ', ',', '.', '-');
            int numSplitMatched = splits.Count(split => name.IndexOf(split, StringComparison.OrdinalIgnoreCase) > 0);
            return numSplitMatched >= (splits.Length - tolerance);
        }

        public static List<EmployerReview> GetEmployerReview(string employerName)
        {
            var returnlist = new List<EmployerReview>();
            if (!employerName.IsNullSpaceOrEmpty())
            {
                using (var db = new JseDbContext())
                {
                    for (int tolerance = 0; !returnlist.Any() && tolerance < 5; tolerance++)
                    {
                        foreach (EmployerReview employerReview in db.EmployerReviews.Include(e => e.JobReviews))
                        {
                            if (IsSimilar(employerName, employerReview.Name, tolerance))
                                returnlist.Add(employerReview);
                        }
                    }
                }
            }
            return returnlist;
        }
    }
}