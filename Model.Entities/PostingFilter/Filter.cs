using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Definition;

namespace Model.Entities.PostingFilter
{
    public class Filter
    {
        public Filter()
        {
            StringSearchTargets = new List<StringSearchTarget>();
            StringSearchValues = new List<string>();
            DisciplinesSearchTarget = new List<DisciplineEnum>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FilterCategory Category { get; set; }
        public IList<StringSearchTarget> StringSearchTargets { get; set; }
        public IList<string> StringSearchValues { get; set; }
        public bool MatchCase { get; set; }
        public List<DisciplineEnum> DisciplinesSearchTarget { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

        public override string ToString()
        {
            return Name + " " + Environment.NewLine + Description;
        }
    }
}