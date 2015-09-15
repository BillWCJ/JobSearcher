using System;
using Data.Contract.JseDb.Interface;
using Model.Entities;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Repository
{
    public class LocationRepo : BaseRepository<JobLocation>, ILocationRepo
    {
        public LocationRepo(JseDbContext dbContext) : base(dbContext)
        {
        }

        public JobLocation GetByJobId(int jobId)
        {
            throw new NotImplementedException();
        }

        public JobLocation GetByEmployerId(int employerId)
        {
            throw new NotImplementedException();
        }
    }
}