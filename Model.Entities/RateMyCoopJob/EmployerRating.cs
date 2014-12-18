
using System.Collections.Generic;

namespace Model.Entities.RateMyCoopJob
{
    public class EmployerRating
    {
        public int EmployerId { get; set; }
        public string Name { get; set; }
        public List<JobRating> Jobs { get; set; } 
    }
}
