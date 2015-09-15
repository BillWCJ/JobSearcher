using System.ComponentModel.DataAnnotations;
using Model.Entities.JobMine;

namespace Model.Entities.RateMyCoopJob
{
    public class EmployerLinker
    {
        [Key]
        public int Id { get; set; }
        public virtual Employer Employer { get; set; }
        public virtual EmployerReview EmployerReview { get; set; }
        public string Data { get; set; }
    }
}
