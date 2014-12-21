using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Data.EF.ClusterDB.Interface;
using Model.Entities;

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
                return DbSet.SingleOrDefault(u => u.JobId == jobId);
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