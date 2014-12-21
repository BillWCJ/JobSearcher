using Model.Entities;

namespace Data.EF.JseDb.Interface
{
    public interface ILocationRepo : IBaseRepository<JobLocation>
    {
        JobLocation GetByJobId(int jobId);
        JobLocation GetByEmployerId(int employerId);
    }
}