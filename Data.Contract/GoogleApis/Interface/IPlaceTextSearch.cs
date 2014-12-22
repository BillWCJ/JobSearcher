using Model.Entities;
using Model.Entities.JobMine;

namespace Data.Contract.GoogleApis.Interface
{
    public interface IPlaceTextSearch
    {
        JobLocation GetLocation(Employer employer, string region);
    }
}