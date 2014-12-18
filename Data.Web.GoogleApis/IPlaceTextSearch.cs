using Model.Entities;

namespace Data.Web.GoogleApis
{
    public interface IPlaceTextSearch
    {
        Location GetLocation(Employer employer, string region);
    }
}