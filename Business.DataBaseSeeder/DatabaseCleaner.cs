using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utility;
using Data.EF.JseDb;
using Model.Definition;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace Business.DataBaseSeeder
{
    public static class DatabaseCleaner
    {
        public static void DeleteJobs(Action<String> messageCallBack)
        {
            using (var db = new JseDbContext())
            {
                var repo = new JseDataRepo(db);
                //remove job linker
                messageCallBack("Starting to remove JobLinker");
                foreach (JobLinker jobLinker in db.JobLinkers)
                {
                    db.JobLinkers.Remove(jobLinker);
                    db.SaveChanges();
                }
                messageCallBack("Remove JobLinker complete");
                messageCallBack("Starting to remove Jobs");
                var jobIds = repo.JobRepo.GetJobIds();
                int numJobDeleted = 0, totalNumJob = jobIds.Count();
                foreach (var jobId in jobIds)
                {
                    repo.JobRepo.Delete(jobId);
                    repo.SaveChanges();
                    messageCallBack(CommonDef.CurrentStatus + "Deleted: {0}/{1} jobs".FormatString(++numJobDeleted, totalNumJob));
                }
                messageCallBack("Remove Jobs complete");
                messageCallBack("Starting to remove employers");
                var employers = db.Employers.ToList();
                foreach (Employer employer in employers)
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
