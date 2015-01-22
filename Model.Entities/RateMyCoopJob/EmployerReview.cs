using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.RateMyCoopJob
{
    public class EmployerReview
    {
        public EmployerReview()
        {
            JobReviews = JobReviews ?? new List<JobReview>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployerId { get; set; }
        public string Name { get; set; }
        public virtual List<JobReview> JobReviews { get; set; }

        public override string ToString()
        {
            return "EmployerName: " + Name + Environment.NewLine;
        }
    }
}
