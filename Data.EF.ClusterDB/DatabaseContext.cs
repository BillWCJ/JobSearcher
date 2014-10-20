using System.Data.Entity;
using Data.EF.ClusterDB.Configuration;
using Model.Entities;

namespace Data.EF.ClusterDB
{
    public class DatabaseContext : DbContext
    {
        private const string DefaultConnectionString = "JobSearchEnhancerDatabase";
        private const bool LazyLoadingEnabled = false;

        public DatabaseContext(string connectionString)
            : base(connectionString ?? DefaultConnectionString)
        {
        }

        public DatabaseContext() : this(DefaultConnectionString)
        {
        }

        public DatabaseContext(bool lazyLoadingEnabled) : this()
        {
            Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
        }

        public IDbSet<Job> Jobs { get; set; }
        public IDbSet<Employer> Employers { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<Levels> Levels { get; set; }
        public IDbSet<Disciplines> Disciplines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LocationConfiguration());
        }
    }
}