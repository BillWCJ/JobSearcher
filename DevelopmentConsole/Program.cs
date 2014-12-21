using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Business.Account;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.IO.Local;
using Data.Web.JobMine;
using Data.Web.RateMyCoopJob;
using Model.Definition;
using Model.Entities;
using Model.Entities.RateMyCoopJob;

namespace DevelopmentConsole
{
    class Program
    {
        static void Main(string[] args)
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
