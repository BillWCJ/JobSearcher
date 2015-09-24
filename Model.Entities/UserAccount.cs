using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Definition;

namespace Model.Entities
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public string JobMineUsername { get; set; }
        [NotMapped]
        public string JobMinePassword { get; set; }
        public string JobStatus { get; set; }
        public string Term { get; set; }

        //Not Used
        public string GoogleApisServerKey { get; set; }
        public string GoogleApisBrowserKey { get; set; }
        public string FilePath { get; set; }
    }
}