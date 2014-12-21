using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    public interface ILocationRepo : IBaseRepository<Location>
    {
        Location GetByJobId(int jobId);
        Location GetByEmployerId(int employerId);
    }
}