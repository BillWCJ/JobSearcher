using System;
using System.Collections.Generic;
using Data.EF.ClusterDB.Interface;
using Model.Entities;

namespace Data.EF.ClusterDB.Repository
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