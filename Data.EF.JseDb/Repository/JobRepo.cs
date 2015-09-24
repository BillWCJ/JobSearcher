using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Data.Contract.JseDb;
using Data.Contract.JseDb.Interface;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Repository
{
    internal class JobRepo : BaseRepository<Job>, IJobRepo
    {
        public JobRepo(IJseDbContext dbContext) : base(dbContext)
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

        public IEnumerable<int> GetJobIds()
        {
            return DbContext.Jobs.Select(j => j.Id);
        }

        public bool UpdateWithJov(JobOverView jov)
        {
            Job job = DbContext.Jobs.Include(j => j.Levels).Include(j => j.Disciplines).Include(j => j.JobLocation).Include(j => j.Employer).FirstOrDefault(x => x.Id == jov.Id);

            job.NumberOfApplied = jov.NumberOfApplied;
            job.AlreadyApplied = jov.AlreadyApplied;
            job.OnShortList = jov.OnShortList;

            DbContext.Jobs.Attach(job);
            DbEntityEntry<Job> entry = DbContext.Entry<Job>(job);
            entry.Property(e => e.NumberOfApplied).IsModified = true;
            entry.Property(e => e.AlreadyApplied).IsModified = true;
            entry.Property(e => e.OnShortList).IsModified = true;
            //entry.Property(e => e.Disciplines).IsModified = false;
            //entry.Property(e => e.Levels).IsModified = false;
            //entry.Property(e => e.Employer).IsModified = false;
            //entry.Property(e => e.JobLocation).IsModified = false;
            //entry.Property(e => e.LocalShortList).IsModified = false;
            DbContext.SaveChanges();

            return true;
        }

        public bool SeedJobAndRelatedEntities(Job job)
        {
            Employer existingEmployer = DbContext.Employers.FirstOrDefault(e => e.Name == job.Employer.Name && e.UnitName == job.Employer.UnitName);
            if (existingEmployer != null)
            {
                foreach (Job existingJob in existingEmployer.Jobs)
                {
                    if (existingJob.JobLocation.Region == job.JobLocation.Region)
                    {
                        job.JobLocation = existingJob.JobLocation;
                        break;
                    }
                }
                job.Employer = existingEmployer;
            }

            DbContext.Jobs.Add(job);
            DbContext.SaveChanges();
            return true;
        }
    }
}