using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Data.Contract.GoogleApis.Interface;
using Data.EF.JseDb;
using Data.Web.GoogleApis;
using Model.Entities;
using Model.Entities.JobMine;

namespace Business.DataBaseSeeder
{
    public class GoogleLocationSeeder
    {
        public GoogleLocationSeeder(UserAccount account)
        {
            Account = account;
        }

        private UserAccount Account { get; set; }

        public void SeedDb(string selectLocation = null)
        {
            using (var db = new JseDbContext())
            {
                IList<JobLocation> notSetLocations = (from l in db.Locations where l.Longitude == null && l.Latitude == null && l.FullAddress == null select l).ToList();
                IPlaceTextSearch locationSearcher = new GoogleRepo(new List<string> {Account.GoogleApisBrowserKey}).LocationRepo;
                selectLocation = selectLocation == null? null : selectLocation.ToLower();

                foreach (JobLocation location in notSetLocations)
                {
                    try
                    {
                        if(selectLocation != null && !location.Region.ToLower().Contains(selectLocation))
                            continue;

                        Job job = db.Jobs.Include(j => j.Employer).FirstOrDefault(j => j.JobLocation.Id == location.Id);
                        Employer employer = db.Employers.FirstOrDefault(e => e.Id == job.Employer.Id);

                        JobLocation completedJobLocation = locationSearcher.GetLocation(employer, location.Region);
                        if (completedJobLocation != null)
                        {
                            location.FullAddress = completedJobLocation.FullAddress;
                            location.Longitude = completedJobLocation.Longitude;
                            location.Latitude = completedJobLocation.Latitude;

                            db.Locations.Attach(location);
                            DbEntityEntry<JobLocation> locationEntry = db.Entry(location);
                            locationEntry.Property(e => e.FullAddress).IsModified = true;
                            locationEntry.Property(e => e.Latitude).IsModified = true;
                            locationEntry.Property(e => e.Longitude).IsModified = true;
                        }

                        //Because Entity Framework is retarded or i am, EF thinking that everything is change even though only location is changed
                        DbEntityEntry<Job> jobEntry = db.Entry(job);
                        jobEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Disciplines> disciplineEntry = db.Entry(job.Disciplines);
                        disciplineEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Levels> levelsEntry = db.Entry(job.Levels);
                        levelsEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Employer> employerEntry = db.Entry(employer);
                        employerEntry.State = EntityState.Unchanged;

                        Trace.TraceInformation(db.SaveChanges() + "Changes");
                    }
                    catch (Exception e)
                    {
                        Trace.TraceWarning(e.ToString()); //ignore error
                    }
                }
            }
        }
    }
}