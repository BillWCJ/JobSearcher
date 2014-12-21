using Model.Entities.JobMine;

namespace Data.EF.JseDb.Interface
{
    public interface ILevelsRepo : IBaseRepository<Levels>
    {
        Levels GetByJobId(int jobId);
    }
}