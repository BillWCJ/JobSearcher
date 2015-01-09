using Model.Entities.SearchDictionary;

namespace Data.EF.JseDb.Repository
{
    internal class WordRepo : BaseRepository<Word>
    {
        public WordRepo(JseDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}