using Data.Contract.JobMine.Interface;

namespace Data.Contract.JobMine
{
    public interface IJobMineRepo
    {
        IJobInquiry JobInquiry { get; }
        IJobDetail JobDetail { get; }
    }
}