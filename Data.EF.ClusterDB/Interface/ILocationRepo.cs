using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    internal interface ILocationRepo : IBaseRepository<Location>
    {
        Location GetByJobId(int jobId);
        Location GetByEmployerId(int employerId);
    }
}