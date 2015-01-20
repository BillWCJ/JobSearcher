using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.JobMine;
using Model.Entities.RateMyCoopJob;

namespace Model.Entities
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
