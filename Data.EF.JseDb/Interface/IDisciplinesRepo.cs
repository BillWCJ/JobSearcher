using Model.Entities;
using Model.Entities.JobMine;

namespace Data.EF.ClusterDB.Interface
{
    public interface IDisciplinesRepo : IBaseRepository<Disciplines>
    {
        Disciplines GetByJobId(int jobId);
    }
}