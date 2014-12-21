using Business.DataBaseSeeder;
using Model.Entities;

namespace DevelopmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        private static void Seeders(string term, string appsAvail, UserAccount account)
        {
            new JobMineInfoSeeder(account).SeedDb(term, appsAvail);
            RateMyCoopJobSeeder.SeedDb();
            new GoogleLocationSeeder(account).SeedDb();
        }
    }
}