using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.Entities.RateMyCoopJob
{
    public class JobReview
    {
        public JobReview()
        {
            JobRatings = JobRatings ?? new List<JobRating>();
        }

        public virtual EmployerReview EmployerReview { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobReviewId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string AverageSalary { get; set; }
        public string SalaryRange { get; set; }
        public int NumberOfReviews { get; set; }
        public int Popularity { get; set; }
        public string JobDescription { get; set; }
        public int AverageRating { get; set; }
        public virtual List<JobRating> JobRatings { get; private set; }

        public override string ToString()
        {
            string msg = null;
            msg += EmployerReview.ToString();
            msg += "Title: " + Title + Environment.NewLine;
            msg += "Location: " + Location + Environment.NewLine;
            msg += "AverageSalary: " + AverageSalary + Environment.NewLine;
            msg += "SalaryRange: " + SalaryRange + Environment.NewLine;
            msg += "NumberOfReviews: " + NumberOfReviews + Environment.NewLine;
            msg += "Popularity: " + Popularity + Environment.NewLine;
            msg += "JobDescription: " + JobDescription + Environment.NewLine;
            msg += "AverageRating: " + AverageRating + Environment.NewLine;
            foreach (JobRating rating in JobRatings)
                msg += Environment.NewLine + rating;
            return msg;
        }
    }
}
