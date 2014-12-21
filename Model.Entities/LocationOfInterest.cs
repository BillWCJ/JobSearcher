using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class LocationOfInterest
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string Region { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}