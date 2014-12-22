using Data.Contract.GoogleApis.Interface;

namespace Data.Contract.GoogleApis
{
    public interface IGoogleRepo
    {
        IPlaceTextSearch LocationRepo { get; set; }
    }
}