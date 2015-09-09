using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.JseDb;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.DataBaseSeeder
{
    public static class DatabaseCleaner
    {
        public static void DeleteJobs(Action<String> messageCallBack)
        {
            using (var db = new JseDbContext())
            {
                //remove job linker
                messageCallBack("Starting to remove JobLinker");
                foreach (JobLinker jobLinker in db.JobLinkers)
                {
                    db.JobLinkers.Remove(jobLinker);
                    db.SaveChanges();
                }
                messageCallBack("Remove JobLinker complete");
                messageCallBack("Starting to remove Jobs");
                foreach (Job job in db.Jobs)
                {
                    db.Jobs.Remove(job);
                    db.SaveChanges();
                }
                messageCallBack("Remove Jobs complete");
                messageCallBack("Starting to remove employers");
                foreach (Employer employer in db.Employers)
                {
                    db.Employers.Remove(employer);
                    db.SaveChanges();
                }
                messageCallBack("Remove Jobs employers");
                messageCallBack("Starting to remove Disciplines");
                foreach (Disciplines disciplines in db.Disciplines)
                {
                    db.Disciplines.Remove(disciplines);
                    db.SaveChanges();
                }
                messageCallBack("Remove Jobs Disciplines");
                messageCallBack("Starting to remove Levels");
                foreach (Levels levels in db.Levels)
                {
                    db.Levels.Remove(levels);
                    db.SaveChanges();
                }
                messageCallBack("Remove Jobs Levels");
                messageCallBack("Starting to remove Locations");
                foreach (JobLocation location in db.Locations)
                {
                    db.Locations.Remove(location);
                    db.SaveChanges();
                }
                messageCallBack("Remove Jobs Locations");
            }
        }
    }
}
