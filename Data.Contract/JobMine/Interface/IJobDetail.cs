using System.Collections.Generic;
using Model.Entities.JobMine;

namespace Data.Contract.JobMine.Interface
{
    public interface IJobDetail
    {
        Job GetJob(JobOverView jov);
        IEnumerable<string> DownLoadAndWriteJobsToLocal(Queue<string> jobIDs, string fileLocation, uint numJobsPerFile = 100);
    }
}