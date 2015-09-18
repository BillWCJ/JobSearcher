using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.Utility;
using Data.EF.JseDb;
using Data.Web.RateMyCoopJob;
using Model.Definition;
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

        public static void SeedDb(Action<string> messageCallBack)
        {
            messageCallBack("Now downloading job reviews...");
            List = RateMyCoopJob.GetListofJobs();
            // TODO Always Delete db before running console app
            // TODO Implement DropDB
            int numReview = List.Count(), currentReviewNum = 0;
            messageCallBack("Found {0} job reviews. please wait around {1} minutes for download to complete".FormatString(numReview, (numReview+59)/60));
            using (var db = new JseDbContext())
            {
                foreach (JobReview jReview in List)
                {
                    messageCallBack(CommonDef.CurrentStatus + "Seeding {0}/{1} job review".FormatString(++currentReviewNum, numReview));
                    RateMyCoopJob.PopulateRatingsField(jReview);
                    jReview.EmployerReview = db.EmployerReviews.Find(jReview.EmployerReview.EmployerId) ?? jReview.EmployerReview;
                    db.JobReviews.AddOrUpdate(jReview);
                    db.SaveChanges();
                }

                messageCallBack(CommonDef.CurrentStatus);
                messageCallBack("Done Downloading Job Reviews");
            }
        }
    }
}