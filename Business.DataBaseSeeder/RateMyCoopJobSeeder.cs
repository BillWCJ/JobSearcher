using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Data.EF.ClusterDB;
using Data.Web.RateMyCoopJob;
using Model.Entities.RateMyCoopJob;

namespace Business.DataBaseSeeder
{
    public static class RateMyCoopJobSeeder
    {
        private static IEnumerable<JobReview> List { get; set; }

        static  RateMyCoopJobSeeder()
        {
            List = new List<JobReview>();
        }

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
                foreach (var jReview in List)
                {
                    RateMyCoopJob.PopulateRatingsField(jReview);

                    jReview.Employer = db.EmployerReviews.Find(jReview.Employer.EmployerId) ?? jReview.Employer;

                    db.JobReviews.AddOrUpdate(jReview);
                    db.SaveChanges();
                }

                Console.WriteLine("Done");
            }
        }
    }
}
