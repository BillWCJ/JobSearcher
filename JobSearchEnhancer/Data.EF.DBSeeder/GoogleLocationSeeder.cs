using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.ClusterDB;
using Data.Web.GoogleApis;
using Model.Entities;

namespace Data.EF.DBSeeder
{
    public class GoogleLocationSeeder
    {
        UserAccount Account { get; set; }

        public GoogleLocationSeeder(UserAccount account)
        {
            Account = account;
        }

        public void SeedDb()
        {
            using (var db = new ClusterRepository())
            {
                IList<Location> locations = (from l in db.Locations select l).ToList();
                foreach (Location location in locations)
                {
                    try
                    {
                        if (location.AlreadySet)
                            continue;
                        Job job = db.Jobs.Include(j => j.Employer).FirstOrDefault(j => j.Location.Id == location.Id);
                        if (job == null) continue;

                        Employer employer = db.Employers.FirstOrDefault(e => e.Id == job.Employer.Id);
                        if (employer == null) continue;

                        Location completedLocation = new PlaceTextSearch(Account).GetLocation(employer, location.Region);
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

                        //Because Entity Frameworkd is retarded or i am retard, EF thinking that everything is change even though only location is changed
                        DbEntityEntry<Job> jobEntry = db.Entry(job);
                        jobEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Disciplines> disciplineEntry = db.Entry(job.Disciplines);
                        disciplineEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Levels> levelsEntry = db.Entry(job.Levels);
                        levelsEntry.State = EntityState.Unchanged;
                        DbEntityEntry<Employer> employerEntry = db.Entry(employer);
                        employerEntry.State = EntityState.Unchanged;

                        Console.WriteLine(db.SaveChanges() + "Changes");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e + "\n" + e.Message + "\n" + e.InnerException + "\n" + e.InnerException.Message);
                    }
                }
            }
        }
    }
}
