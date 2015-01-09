using Model.Entities.RateMyCoopJob;

namespace Data.EF.JseDb.Repository
{
    internal class JobRatingRepo : BaseRepository<JobRating>
    {
        public JobRatingRepo(JseDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}