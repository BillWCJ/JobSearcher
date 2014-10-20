using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    class Word
    {
        public int WordId { get; set; }
        public string DictionaryWord { get; set; }
        public string Description { get; set; }
        public string Catergory { get; set; }
        public bool IsNoun { get { return Catergory.Contains("n"); } }
        
    }
}
