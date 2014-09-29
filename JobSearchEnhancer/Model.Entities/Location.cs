using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    //[Table("Locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string FullAddress { get; set; }
        public string Region { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public virtual List<Job> Jobs { get; set; }

        public Location()
        {
            Jobs = new List<Job>();
        }
    }
}