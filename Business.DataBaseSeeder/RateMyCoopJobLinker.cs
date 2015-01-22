using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.EF.JseDb;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace Business.DataBaseSeeder
{
    public static class RateMyCoopJobLinker
    {
        public static void SeedDb()
        {
            using (var db = new JseDbContext())
            {
                LinkEmployersAndJobs(db);
                Console.WriteLine("Saving Changes");
                db.SaveChanges();
                Console.WriteLine("Done");
            }
        }

        public static void ClearDb()
        {
            using (var db = new JseDbContext())
            {
                foreach (var linker in db.EmployerLinkers)
                    db.EmployerLinkers.Remove(linker);
                db.SaveChanges();

                foreach (var linker in db.JobLinkers)
                    db.JobLinkers.Remove(linker);
                db.SaveChanges();
            }
        }

        private static void LinkEmployersAndJobs(JseDbContext db)
        {
            int count = 0;
            var eReviewDict = new Dictionary<string, List<EmployerReview>>();

            Console.WriteLine("Formatting Job Review");
            foreach (EmployerReview eReview in db.EmployerReviews.Include(e => e.JobReviews))
            {
                if (!eReviewDict.ContainsKey(eReview.Name.ToLower()))
                    eReviewDict.Add(eReview.Name.ToLower(), new List<EmployerReview>{eReview});
                else
                    eReviewDict[eReview.Name.ToLower()].Add(eReview);
            }

            Console.WriteLine("Linking Employer & Job Review");
            foreach (Employer employer in db.Employers.Include(e => e.Jobs))
            {
                if (eReviewDict.ContainsKey(employer.Name.ToLower()))
                {
                    var eReview = eReviewDict[employer.Name.ToLower()];

                    AddEmployerLinker(eReview, employer, db);

                    LinkJobs(eReview, employer, db);
                }
                Console.WriteLine("Linking employer " + count++);
            }
        }

        private static void LinkJobs(IEnumerable<EmployerReview> eReviews, Employer employer, JseDbContext db)
        {
            foreach (var eReview in eReviews)
            {
                foreach (JobReview jReview in eReview.JobReviews)
                {
                    foreach (Job job in employer.Jobs)
                    {
                        if (jReview.Title.ToLower() == job.JobTitle.ToLower())
                            AddJobLinker(jReview, job, db);
                    }
                }
            }
        }

        private static void AddJobLinker(JobReview jReview, Job job, JseDbContext db)
        {
            Console.WriteLine("Matched Job: " + job.JobTitle);
            var joblinker = new JobLinker
            {
                Data = "Exact",
                JobReview = jReview,
                Job = job
            };
            db.JobLinkers.Add((joblinker));
        }

        private static void AddEmployerLinker(IEnumerable<EmployerReview> eReviews, Employer employer, JseDbContext db)
        {
            Console.WriteLine("Matched Employer: " + employer.Name);
            foreach (var eReview in eReviews)
            {
                var employerLinker = new EmployerLinker
                {
                    Data = "Exact",
                    Employer = employer,
                    EmployerReview = eReview
                };
                db.EmployerLinkers.Add(employerLinker);
            }
        }
    }
}