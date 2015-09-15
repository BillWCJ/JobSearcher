using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities.JobMine
{
    /// <summary>
    ///     Entity that contains the location information
    /// </summary>
    public class JobLocation
    {
        public JobLocation()
        {
            Jobs = new List<Job>();
        }

        [Key]
        public int Id { get; set; }

        public string FullAddress { get; set; }
        public string Region { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public virtual List<Job> Jobs { get; set; }

        public bool AlreadySet
        {
            get
            {
                return !string.IsNullOrEmpty(FullAddress) && Longitude != null && Latitude != null;
            }
        }
    }
}