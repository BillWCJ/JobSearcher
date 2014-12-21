using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Data.EF.ClusterDB;
using Data.EF.JseDb;
using Data.Web.GoogleApis;
using Model.Entities;

namespace Business.DataBaseSeeder
{
    public class GoogleLocationSeeder
    {
        public GoogleLocationSeeder(UserAccount account)
        {
            Account = account;
        }

        private UserAccount Account { get; set; }

        public void SeedDb()
        {
            using (var db = new JseDbContext())
            {
                IList<Location> notSetLocations = (from l in db.Locations where l.AlreadySet == false select l).ToList();
                PlaceTextSearch locationSearcher = new GoogleRepo(new List<string> {Account.GoogleApisBrowserKey}).LocationRepo;
                foreach (Location location in notSetLocations)
                {
                    try
                    {
                        Job job = db.Jobs.Include(j => j.Employer).FirstOrDefault(j => j.Location.Id == location.Id);
                        Employer employer = db.Employers.FirstOrDefault(e => e.Id == job.Employer.Id);

                        Location completedLocation = locationSearcher.GetLocation(employer, location.Region);
                        if (completedLocation != null)
                        {
                            location.FullAddress = completedLocation.FullAddress;
                            location.Longitude = completedLocation.Longitude;
                            location.Latitude = completedLocation.Latitude;

                            db.Locations.Attach(location);
                            DbEntityEntry<Location> locationEntry = db.Entry(location);
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