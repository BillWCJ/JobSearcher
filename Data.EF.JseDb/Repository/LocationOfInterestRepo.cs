using Model.Entities;

namespace Data.EF.JseDb.Repository
{
    internal class LocationOfInterestRepo : BaseRepository<LocationOfInterest>
    {
        public LocationOfInterestRepo(JseDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}