using Data.EF.JseDb.Interface;
using Data.EF.JseDb.Repository;

namespace Data.EF.JseDb
{
    public class JseDataRepo : IJseDataRepo
    {
        public JseDataRepo()
        {
            DbContext = new JseDbContext();
            DisciplinesRepo = new DisciplinesRepo(DbContext);
            EmployerRepo = new EmployerRepo(DbContext);
            JobRepo = new JobRepo(DbContext);
            LevelsRepo = new LevelsRepo(DbContext);
            LocationRepo = new LocationRepo(DbContext);
        }

        public JseDbContext DbContext { get; set; }
        public IDisciplinesRepo DisciplinesRepo { get; private set; }
        public IEmployerRepo EmployerRepo { get; private set; }
        public IJobRepo JobRepo { get; private set; }
        public ILevelsRepo LevelsRepo { get; private set; }
        public ILocationRepo LocationRepo { get; private set; }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}