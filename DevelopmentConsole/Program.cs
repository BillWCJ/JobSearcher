using System;
using System.Linq;
using Data.EF.JseDb;
using Model.Entities.JobMine;

namespace DevelopmentConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var jseDb = new JseDataRepo();
            int count = 0;
            Console.WriteLine(jseDb.JobRepo.GetAll().ToList().Count);
            foreach (Job job in jseDb.JobRepo.GetAll().ToList())
            {
                if (job.JobDescription.IndexOf("solidworks", StringComparison.OrdinalIgnoreCase) > 0)
                    count++;
                Console.WriteLine(count + " " + job.IdString);
            }
            Console.ReadKey();
        }
    }
}