using System;
using System.Collections.Generic;
using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class MasterSeeder
    {
        public static void SeedAll(Action<string> messageCallBack, UserAccount account, string term, string appsAvail, bool seedCoopRating = false, bool linkJobReviews = false, bool seedLocation = false, string selectLocation = null)
        {
            try
            {
                messageCallBack("Starting Database Seeding...");
                JobMineInfoSeeder jobMineInfoSeeder = new JobMineInfoSeeder(account);
                jobMineInfoSeeder.SeedDb(messageCallBack, term, appsAvail);

                if (seedCoopRating)
                    RateMyCoopJobSeeder.SeedDb(messageCallBack);
                if(linkJobReviews)
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