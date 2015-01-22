using System.Collections.Generic;
using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class MasterSeeder
    {
        public static IEnumerable<string> SeedAll(string term, string appsAvail, UserAccount account, bool seedCoopRating = true, bool seedLocation = true, string selectLocation = null)
        {
            foreach (var msg in new JobMineInfoSeeder(account).SeedDb(term, appsAvail))
                yield return msg;

            if (seedCoopRating)
            {
                RateMyCoopJobSeeder.SeedDb();
                RateMyCoopJobLinker.SeedDb();
            }
            if (seedLocation)
                new GoogleLocationSeeder(account).SeedDb(selectLocation);
        }
    }
}