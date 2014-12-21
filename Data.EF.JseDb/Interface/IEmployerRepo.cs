﻿using System.Collections.Generic;
using Model.Entities;

namespace Data.EF.ClusterDB.Interface
{
    public interface IEmployerRepo : IBaseRepository<Employer>
    {
        Employer GetByJobId(int jobId);
        IList<Employer> GetByLocationId(int locationId);
    }
}