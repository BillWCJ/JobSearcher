using System.Collections.Generic;
using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    public interface IEmployerRepo : IBaseRepository<Employer>
    {
        Employer GetByJobId(int jobId);
        List<Employer> GetByLocationId(int locationId);
    }
}