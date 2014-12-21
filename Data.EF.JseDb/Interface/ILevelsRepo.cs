using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    public interface ILevelsRepo : IBaseRepository<Levels>
    {
        Levels GetByJobId(int jobId);
    }
}