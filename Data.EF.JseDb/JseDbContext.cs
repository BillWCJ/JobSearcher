using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Data.EF.JseDb.Configuration;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;
using Model.Entities.SearchDictionary;

namespace Data.EF.JseDb
{
    public class JseDbContext : DbContext, IJseDbContext
    {
        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<Employer> Employers { get; set; }
        public IDbSet<JobLocation> Locations { get; set; }
        public IDbSet<Levels> Levels { get; set; }
        public IDbSet<Disciplines> Disciplines { get; set; }
        public IDbSet<JobReview> JobReviews { get; set; }
        public IDbSet<EmployerReview> EmployerReviews { get; set; }
        public IDbSet<JobRating> JobRatings { get; set; }
        public IDbSet<Word> Words { get; set; }
        public IDbSet<SearchDictionary> SearchDictionaries { get; set; }
        public IDbSet<LocationOfInterest> LocationOfInterests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new JobLocationConfiguration());
            modelBuilder.Configurations.Add(new LocationOfInterestConfiguration());
        }
    }
}