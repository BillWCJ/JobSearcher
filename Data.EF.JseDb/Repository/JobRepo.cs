using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Contract.JseDb.Interface;
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

        public Job GetFullJob(int id)
        {
            return DbContext.Jobs.Include(j => j.JobLocation).Include(j => j.Employer).FirstOrDefault(j => j.Id == id);
        }
    }
}