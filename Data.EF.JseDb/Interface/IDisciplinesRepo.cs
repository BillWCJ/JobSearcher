using Model.Entities.JobMine;

namespace Data.EF.JseDb.Interface
{
    public interface IDisciplinesRepo : IBaseRepository<Disciplines>
    {
        Disciplines GetByJobId(int jobId);
    }
}