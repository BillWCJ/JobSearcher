using Data.Contract.JseDb.Interface;
using Model.Entities.RateMyCoopJob;

namespace Data.EF.JseDb.Repository
{
    internal class JobReviewRepo : BaseRepository<JobReview>, IBaseRepository<JobReview>
    {
        public JobReviewRepo(JseDbContext dbContext) : base(dbContext)
        {
        }
    }
}