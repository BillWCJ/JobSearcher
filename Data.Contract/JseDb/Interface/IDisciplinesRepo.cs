using Model.Entities.JobMine;

namespace Data.Contract.JseDb.Interface
{
    public interface IDisciplinesRepo : IBaseRepository<Disciplines>
    {
        Disciplines GetByJobId(int jobId);
    }
}