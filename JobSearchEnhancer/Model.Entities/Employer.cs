using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    /// <summary>
    ///     Contain the details of an employer
    /// </summary>
    //[Table("Employers")]
    public class Employer
    {
        [Key]
        public int Id { get; set; }
        //public int JobMineId { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        //public string UnitName1 { get; set; }
        //public string UnitName2 { get; set; }
        //public string WebSite { get; set; }
        //public string Description { get; set; }
        public virtual List<Job> Jobs { get; set; }

        public Employer()
        {
            Jobs = new List<Job>();
        }

        public string IdString
        {
            get { return Id.ToString("D8"); }
        }
    }
}