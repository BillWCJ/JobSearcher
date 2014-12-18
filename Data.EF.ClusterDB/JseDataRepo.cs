using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EF.ClusterDB.Interface;
using Data.EF.ClusterDB.Repository;

namespace Data.EF.ClusterDB
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
