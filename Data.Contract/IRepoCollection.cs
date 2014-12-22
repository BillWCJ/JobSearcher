using Data.Contract.GoogleApis;
using Data.Contract.JobMine;
using Data.Contract.JseDb;
using Data.Contract.Local;

namespace Data.Contract
{
    public interface IRepoCollection
    {
        IJseDataRepo JseDataRepo { get; set; }

        IJseLocalRepo JseLocalRepo { get; set; }

        IGoogleRepo GoogleRepo { get; set; }

        IJobMineRepo JobMineRepo { get; set; }
    }
}