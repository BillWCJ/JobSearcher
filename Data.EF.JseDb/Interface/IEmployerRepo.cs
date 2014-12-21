using System.Collections.Generic;
using Model.Entities.JobMine;

namespace Data.EF.JseDb.Interface
{
    public interface IEmployerRepo : IBaseRepository<Employer>
    {
        Employer GetByJobId(int jobId);
        IList<Employer> GetByLocationId(int locationId);
    }
}