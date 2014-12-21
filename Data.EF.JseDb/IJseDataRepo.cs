using Data.EF.ClusterDB.Interface;

namespace Data.EF.JseDb
{
    public interface IJseDataRepo
    {
        IDisciplinesRepo DisciplinesRepo { get; }
        IEmployerRepo EmployerRepo { get; }
        IJobRepo JobRepo { get; }
        ILevelsRepo LevelsRepo { get; }
        ILocationRepo LocationRepo { get; }
        void SaveChanges();
    }
}
