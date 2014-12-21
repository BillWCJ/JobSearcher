using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.JobMine;

namespace Model.Entities.SearchDictionary
{
    public class SearchDictionary
    {
        [Key, ForeignKey("Job")]
        public int JobId { get; set; }

        public virtual Job Job { get; set; }

        [Key, ForeignKey("Word")]
        public int WordId { get; set; }

        public virtual Word Word { get; set; }

        public int JobTitleCount { get; set; }
        public int CommentCount { get; set; }
        public int DescriptionCount { get; set; }
    }
}