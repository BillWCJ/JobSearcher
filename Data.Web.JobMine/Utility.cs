using System.IO;
using System.Text.RegularExpressions;
using Model.Entities;

namespace Data.Web.JobMine
{
    public static class Utility
    {
        public static bool IsCorrectJobId(string jobId)
        {
            var regex = new Regex("[0-9]{8,8}");
            return regex.IsMatch(jobId);
        }

        public static bool IsJobOverViewCompleted(JobOverView jov)
        {
            return jov.Id > 0 && !string.IsNullOrEmpty(jov.JobTitle) &&
                   !string.IsNullOrEmpty(jov.Employer.Name) && !string.IsNullOrEmpty(jov.Location.Region);
        }
    }
}