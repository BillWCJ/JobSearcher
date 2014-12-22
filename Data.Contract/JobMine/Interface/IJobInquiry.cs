using System.Collections.Generic;
using Model.Entities.JobMine;

namespace Data.Contract.JobMine.Interface
{
    public interface IJobInquiry
    {
        IEnumerable<JobOverView> GetJobOverViews(string term, string jobStatus);
        IEnumerable<string> GetJobIds(string term, string jobStatus);
    }
}