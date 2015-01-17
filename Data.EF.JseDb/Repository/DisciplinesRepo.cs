using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Data.Contract.JseDb.Interface;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Repository
{
    public class DisciplinesRepo : BaseRepository<Disciplines>, IDisciplinesRepo
    {
        public DisciplinesRepo(JseDbContext dbContext) : base(dbContext)
        {
        }

        public Disciplines GetByJobId(int jobId)
        {
            try
            {
                return null; //DbSet.SingleOrDefault(u => u.Job.Id == jobId);
            }
            catch (Exception e)
            {
                string msg = e.GetType() + " : " + e.Message + " at " + GetType().GetMethods() + " : id =" + jobId;
                Trace.WriteLine(msg);
                throw new DataException(msg, e);
            }
        }
    }
}