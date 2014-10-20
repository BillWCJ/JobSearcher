using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    interface IJobRepo : IBaseRepository<Job>
    {
        List<Job> GetJobsByEmployerId(int employerId);
        List<Job> GetJobsByLocationId(int locationId);
    }
}
