using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.RateMyCoopJob
{
    public class EmployerReview
    {
        public EmployerReview()
        {
            Jobs = Jobs ?? new List<JobReview>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployerId { get; set; }
        public string Name { get; set; }
        public virtual List<JobReview> Jobs { get; set; } 
    }
}
