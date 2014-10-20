using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    public interface IDisciplinesRepo : IBaseRepository<Disciplines>
    {
        Disciplines GetByJobId(int jobId);
    }
}