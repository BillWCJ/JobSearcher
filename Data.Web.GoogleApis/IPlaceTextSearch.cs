using Model.Entities;
using Model.Entities.JobMine;

namespace Data.Web.GoogleApis
{
    public interface IPlaceTextSearch
    {
        JobLocation GetLocation(Employer employer, string region);
    }
}