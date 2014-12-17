using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Model.Entities.RateMyCoopJob
{
    public class JobRating
    {
        public JobRating()
        {
            Ratings = Ratings ?? new List<Rating>();
        }

        public EmployerRating Employer { get; set; }
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string SalaryRange { get; set; }
        public int Reviews { get; set; }
        public int Popularity { get; set; }
        public string JobDescription { get; set; }
        public int Rating { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
