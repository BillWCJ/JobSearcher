using Data.Contract.JseDb.Interface;
using Model.Entities.RateMyCoopJob;

namespace Data.EF.JseDb.Repository
{
    internal class EmployerReviewRepo : BaseRepository<EmployerReview>, IBaseRepository<EmployerReview>
    {
        public EmployerReviewRepo(JseDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}