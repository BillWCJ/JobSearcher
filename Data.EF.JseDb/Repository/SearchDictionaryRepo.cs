using Model.Entities.SearchDictionary;

namespace Data.EF.JseDb.Repository
{
    internal class SearchDictionaryRepo : BaseRepository<SearchDictionary>
    {
        public SearchDictionaryRepo(JseDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
