using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities.JobMine;

namespace Model.Entities
{
    public class JobShortList
    {
        [Key]
        public int Id { get; set; }
        public virtual Job Job { get; set; }
        public string Data { get; set; }
    }
}
