using Data.Contract.JseDb.Interface;

namespace Data.Contract.JseDb
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
