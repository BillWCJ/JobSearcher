﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.Contract.JseDb;
using Data.Contract.JseDb.Interface;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Repository
{
    internal class EmployerRepo : BaseRepository<Employer>, IEmployerRepo
    {
        public EmployerRepo(IJseDbContext dbContext) : base(dbContext)
        {
        }

        public Employer GetByJobId(int jobId)
        {
            Employer employer = null;
            try
            {
                employer = (from e in DbContext.Employers join j in DbContext.Jobs on e.Id equals j.Employer.Id where j.Id == jobId select e).FirstOrDefault();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            return employer;
        }

        public IList<Employer> GetByLocationId(int locationId)
        {
            IList<Employer> employer = null;
            try
            {
                employer = DbSet.Where(e => e.Jobs.Any(j => j.JobLocation.Id == locationId)).ToList();
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            return employer;
        }
    }
}