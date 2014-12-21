using System;
using System.Collections.Generic;
using Data.EF.JseDb.Interface;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Repository
{
    internal class JobRepo : BaseRepository<Job>, IJobRepo
    {
        public JobRepo(JseDbContext dbContext) : base(dbContext)
        {
        }

        public List<Job> GetJobsByEmployerId(int employerId)
        {
            throw new NotImplementedException();
        }

        public List<Job> GetJobsByLocationId(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}