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
            try
            {
                var repo = new JseDataRepo(new JseDbContext());
                messageCallBack("Starting Database Seeding...");
                JobMineInfoSeeder jobMineInfoSeeder = new JobMineInfoSeeder(account, new JobMineRepo(account));
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
        }
    }
}