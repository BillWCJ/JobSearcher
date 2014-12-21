using System.Data.Entity;
using Data.EF.ClusterDB.Configuration;
using Model.Entities;
using Model.Entities.RateMyCoopJob;

namespace Data.EF.ClusterDB
{
    public class JseDbContextInitializer : DropCreateDatabaseIfModelChanges<JseDbContext>
    {
        protected override void Seed(JseDbContext dbContext)
        {
            base.Seed(dbContext);
        }
    }

    public class JseDbContext : DbContext
    {
        private const string DefaultConnectionString = "JobSearchEnhancerDatabase";
        private const bool LazyLoadingEnabled = false;

        public JseDbContext()
        {
        }

        public JseDbContext(bool lazyLoadingEnabled)
            : this()
        {
            Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }

        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<Employer> Employers { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Levels> Levels { get; set; }
        public IDbSet<Disciplines> Disciplines { get; set; }
        public IDbSet<JobReview> JobReviews { get; set; }
        public IDbSet<EmployerReview> EmployerReviews { get; set; }
        public IDbSet<JobRating> JobRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LocationConfiguration());
        }
    }
}