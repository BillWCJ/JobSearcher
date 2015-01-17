using System;

namespace Model.Entities.RateMyCoopJob
{
    public class JobRating
    {
        public int JobRatingId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public string Date { get; set; }
        public string Salary { get; set; }
        public virtual JobReview JobReview { get; set; }

        public override string ToString()
        {
            string msg = null;
            msg += "Rating: " + Rating + "|" + "Date: " + Date + "|" + Salary + Environment.NewLine;
            msg += Comment + Environment.NewLine;
            return msg;
        }
    }
}
