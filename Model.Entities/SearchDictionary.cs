using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    class SearchDictionary
    {
        public int JobId { get; set; }
        public int WordId { get; set; }
        public int JobTitleCount { get; set; }
        public int CommentCount { get; set; }
        public int DescriptionCount { get; set; }
    }
}
