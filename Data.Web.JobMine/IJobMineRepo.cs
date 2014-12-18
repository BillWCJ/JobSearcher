namespace Data.Web.JobMine
{
    public interface IJobMineRepo
    {
        JobInquiry JobInquiry { get; }
        JobDetail JobDetail { get; }
    }
}