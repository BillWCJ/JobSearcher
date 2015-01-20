using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace Model.Entities
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
