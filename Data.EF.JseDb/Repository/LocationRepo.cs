using System;
using Data.EF.ClusterDB.Interface;
using Model.Entities;

namespace Data.EF.JseDb.Repository
{
    public class LocationRepo : BaseRepository<Location>, ILocationRepo
    {
        public LocationRepo(JseDbContext dbContext) : base(dbContext)
        {
        }

        public Location GetByJobId(int jobId)
        {
            throw new NotImplementedException();
        }

        public Location GetByEmployerId(int employerId)
        {
            throw new NotImplementedException();
        }
    }
}