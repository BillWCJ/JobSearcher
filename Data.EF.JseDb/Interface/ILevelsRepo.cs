using Model.Entities;
using Model.Entities.JobMine;

namespace Data.EF.ClusterDB.Interface
{
    public interface ILevelsRepo : IBaseRepository<Levels>
    {
        Levels GetByJobId(int jobId);
    }
}