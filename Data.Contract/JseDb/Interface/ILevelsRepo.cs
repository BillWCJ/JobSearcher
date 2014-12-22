using Model.Entities.JobMine;

namespace Data.Contract.JseDb.Interface
{
    public interface ILevelsRepo : IBaseRepository<Levels>
    {
        Levels GetByJobId(int jobId);
    }
}