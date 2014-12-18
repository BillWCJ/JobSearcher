using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.RateMyCoopJob
{
    public class JobReview
    {
        public JobReview()
        {
            JobRatings = JobRatings ?? new List<JobRating>();
        }

        public virtual EmployerReview Employer { get; set; }

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
    }
}
