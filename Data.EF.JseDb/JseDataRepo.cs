using Data.Contract.JseDb;
using Data.Contract.JseDb.Interface;
using Data.EF.JseDb.Repository;
using Model.Entities;
using Model.Entities.RateMyCoopJob;
using Model.Entities.SearchDictionary;

namespace Data.EF.JseDb
{
    public class JseDataRepo : IJseDataRepo
    {
        public JseDataRepo(JseDbContext dbContext)
        {
            DbContext = dbContext;
            DisciplinesRepo = new DisciplinesRepo(DbContext);
            EmployerRepo = new EmployerRepo(DbContext);
            JobRepo = new JobRepo(DbContext);
            LevelsRepo = new LevelsRepo(DbContext);
            LocationRepo = new LocationRepo(DbContext);
            JobReviewRepo = new BaseRepository<JobReview>(DbContext);
            EmployerReviewRepo = new BaseRepository<EmployerReview>(DbContext);
            JobRatingRepo = new BaseRepository<JobRating>(DbContext);
            //WordRepo = new BaseRepository<Word>(DbContext);
            //SearchDictionaryRepo = new BaseRepository<SearchDictionary>(DbContext);
            //LocationOfInterestRepo = new BaseRepository<LocationOfInterest>(DbContext);
            LocalShortListRepo = new BaseRepository<LocalShortList>(DbContext);
            FilteRepo = new BaseRepository<Filter>(DbContext);
        }

        public IJseDbContext DbContext { get; set; }
        public IDisciplinesRepo DisciplinesRepo { get; private set; }
        public IEmployerRepo EmployerRepo { get; private set; }
        public IJobRepo JobRepo { get; private set; }
        public ILevelsRepo LevelsRepo { get; private set; }
        public ILocationRepo LocationRepo { get; private set; }
        public IBaseRepository<JobReview> JobReviewRepo { get; private set; }
        public IBaseRepository<EmployerReview> EmployerReviewRepo { get; private set; }
        public IBaseRepository<JobRating> JobRatingRepo { get; private set; }
        //public IBaseRepository<Word> WordRepo { get; private set; }
        //public IBaseRepository<SearchDictionary> SearchDictionaryRepo { get; private set; }
        //public IBaseRepository<LocationOfInterest> LocationOfInterestRepo { get; private set; }
        public IBaseRepository<LocalShortList> LocalShortListRepo { get; private set; }
        public IBaseRepository<Filter> FilteRepo { get; private set; } 

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}