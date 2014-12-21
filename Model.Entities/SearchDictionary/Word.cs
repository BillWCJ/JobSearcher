using System.ComponentModel.DataAnnotations;

namespace Model.Entities.SearchDictionary
{
    public class Word
    {
        [Key]
        public int WordId { get; set; }
        public string DictionaryWord { get; set; }
        public string Description { get; set; }
        public string Catergory { get; set; }

        public bool IsNoun
        {
            get
            {
                return Catergory.Contains("n");
            }
        }
    }
}