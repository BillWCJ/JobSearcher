using System;
using System.Collections.Generic;
using Data.EF.JseDb;
using Data.Web.JobMine;
using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class MasterSeeder
    {
        public static void SeedAll(Action<string> messageCallBack, UserAccount account, bool seedCoopRating = false, bool linkJobReviews = false, bool seedLocation = false, string selectLocation = null)
        {
            messageCallBack("Starting Seeding Process");
            try
            {
                var repo = new JseDataRepo(new JseDbContext());
                messageCallBack("Connected to Local Database...");
                JobMineInfoSeeder jobMineInfoSeeder = new JobMineInfoSeeder(account, new JobMineRepo(account));
                messageCallBack("Connected to Jobmine Data Source...");
                messageCallBack("Starting to Search and seed Jobmine postings...");
                jobMineInfoSeeder.SeedDb(messageCallBack, repo, account.Term, account.JobStatus);

                if (seedCoopRating)
                    RateMyCoopJobSeeder.SeedDb(messageCallBack);
                if (linkJobReviews)
                    RateMyCoopJobLinker.SeedDb();
                if (seedLocation)
                    new GoogleLocationSeeder(account).SeedDb(selectLocation);
                messageCallBack("Seeding completed");
            }
            catch (Exception e)
            {
                messageCallBack(e.Message);
            }
            messageCallBack("Exiting Seeding Process");
        }
    }
}