using System;
using System.Linq;
using Business.Account;
using Business.DataBaseSeeder;
using Data.EF.ClusterDB;
using Model.Definition;
using Model.Entities;

namespace DevelopmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string term = "1149";
            const string appsAvail = JobStatus.AppsAvail;
            Seeders(term, appsAvail, new AccountManager().Account);

            //SeedOneFakeJob();
            var jsedb = new JseDataRepo();
            Console.WriteLine(jsedb.JobRepo.GetById(1));
            Console.ReadKey();
        }

        private static void SeedOneFakeJob()
        {
            using (var db = new JseDbContext())
            {
                Job job = new Job {JobTitle = "abc", Id = 1, Employer = new Employer {Name = "sds"}, Location = new Location {Region = "lol"}};
                job.Location.Jobs.Add(job);
                job.Employer.Jobs.Add(job);

                db.Jobs.Add(job);
                db.SaveChanges();
            }
        }

        private static void Seeders(string term, string appsAvail, UserAccount account)
        {
            new JobMineInfoSeeder(account).SeedDb(term, appsAvail);
            new GoogleLocationSeeder(account).SeedDb();
        }
    }
}