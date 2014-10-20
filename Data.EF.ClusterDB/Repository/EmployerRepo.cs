using System;
using System.Collections.Generic;
using Data.EF.ClusterDB.Interface;
using Model.Entities;

namespace Data.EF.ClusterDB.Repository
{
    internal class EmployerRepo : BaseRepository<Employer>, IEmployerRepo
    {
        public EmployerRepo(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public Employer GetByJobId(int jobId)
        {
            throw new NotImplementedException();
        }

        public List<Employer> GetByLocationId(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}