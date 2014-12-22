using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using Model.Entities;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;
using Model.Entities.SearchDictionary;

namespace Data.EF.JseDb
{
    public interface IJseDbContext
    {
        IDbSet<Job> Jobs { get; set; }
        IDbSet<Employer> Employers { get; set; }
        IDbSet<JobLocation> Locations { get; set; }
        IDbSet<Levels> Levels { get; set; }
        IDbSet<Disciplines> Disciplines { get; set; }
        IDbSet<JobReview> JobReviews { get; set; }
        IDbSet<EmployerReview> EmployerReviews { get; set; }
        IDbSet<JobRating> JobRatings { get; set; }
        IDbSet<Word> Words { get; set; }
        IDbSet<SearchDictionary> SearchDictionaries { get; set; }
        IDbSet<LocationOfInterest> LocationOfInterests { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        //DbSet Set() where TEntity : class;
        DbSet Set(Type entityType);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        //DbEntityEntry Entry(TEntity entity) where TEntity : class;
        DbEntityEntry Entry(object entity);
        void Dispose();
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
    }
}