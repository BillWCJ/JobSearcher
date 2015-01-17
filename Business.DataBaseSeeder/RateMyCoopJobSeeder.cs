using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Data.EF.JseDb;
using Data.Web.RateMyCoopJob;
using Model.Entities.RateMyCoopJob;

namespace Business.DataBaseSeeder
{
    public static class RateMyCoopJobSeeder
    {
        static RateMyCoopJobSeeder()
        {
            List = new List<JobReview>();
        }

        private static IEnumerable<JobReview> List { get; set; }

        public static void SeedDb()
        {
            List = RateMyCoopJob.GetListofJobs();
            // TODO Always Delete db before running console app
            // TODO Implement DropDB
            using (var db = new JseDbContext())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Now seeding db...");
                Console.ResetColor();
                foreach (JobReview jReview in List)
                {
                    RateMyCoopJob.PopulateRatingsField(jReview);

                    jReview.EmployerReview = db.EmployerReviews.Find(jReview.EmployerReview.EmployerId) ?? jReview.EmployerReview;

                    db.JobReviews.AddOrUpdate(jReview);
                    db.SaveChanges();
                }

                Console.WriteLine("Done");
            }
        }
    }
}