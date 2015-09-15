using System.ComponentModel.DataAnnotations;
using Model.Entities.JobMine;

namespace Model.Entities.RateMyCoopJob
{
    public class JobLinker
    {
        [Key]
        public int Id { get; set; }
        public virtual Job Job { get; set; }
        public virtual JobReview JobReview { get; set; }
        public string Data { get; set; }
    }
}
