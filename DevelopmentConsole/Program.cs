using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;
using Business.DataBaseSeeder;
using Business.Manager;
using Data.EF.JseDb;
using Data.IO.Local;
using Model.Definition;
using Model.Entities.JobMine;

namespace DevelopmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new JseDbContext())
            {

            }
            foreach (var msg in MasterSeeder.SeedAll("1155", JobStatus.Posted, new JseLocalRepo().GetAccount(), true, false))
            {
                Console.WriteLine(msg);
            }
            Console.ReadKey();
        }
    }
}