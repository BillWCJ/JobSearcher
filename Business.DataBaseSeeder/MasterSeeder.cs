using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class MasterSeeder
    {
        public static void SeedAll(string term, string appsAvail, UserAccount account)
        {
            new JobMineInfoSeeder(account).SeedDb(term, appsAvail);
            RateMyCoopJobSeeder.SeedDb();
            new GoogleLocationSeeder(account).SeedDb();
        }
    }
}