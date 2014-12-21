using Data.EF.JseDb;
using Data.IO.Local;
using Data.Web.GoogleApis;
using Data.Web.JobMine;

namespace Data.Entities
{
    public interface IRepoCollection
    {
        IJseDataRepo JseDataRepo { get; set; }

        IJseLocalRepo JseLocalRepo { get; set; }

        IGoogleRepo GoogleRepo { get; set; }

        IJobMineRepo JobMineRepo { get; set; }
    }
}