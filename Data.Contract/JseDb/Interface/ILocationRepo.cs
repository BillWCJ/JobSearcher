using Model.Entities;
using Model.Entities.JobMine;

namespace Data.Contract.JseDb.Interface
{
    public interface ILocationRepo : IBaseRepository<JobLocation>
    {
        JobLocation GetByJobId(int jobId);
        JobLocation GetByEmployerId(int employerId);
    }
}