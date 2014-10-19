using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    /// <summary>
    ///     Entity that contains the details of an employer
    /// </summary>
    public class Employer
    {
        /// <summary>
        ///     Initalize a new instance of Employer Entity
        /// </summary>
        public Employer()
        {
            Jobs = new List<Job>();
        }

        [Key]
        public int Id { get; set; }

        //public int JobMineId { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        //public string UnitName1 { get; set; }
        //public string UnitName2 { get; set; }
        //public string WebSite { get; set; }
        //public string Description { get; set; }
        /// <summary>
        ///     All the Jobs that have this Employer
        /// </summary>
        public virtual List<Job> Jobs { get; set; }

        /// <summary>
        ///     Get the fully formated Id string
        /// </summary>
        /// <example>Id = 1234567 -> IdString = 01234567</example>
        public string IdString
        {
            get { return Id.ToString("D8"); }
        }
    }
}