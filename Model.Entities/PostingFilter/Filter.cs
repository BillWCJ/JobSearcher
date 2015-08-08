using System;
using System.Collections.Generic;
using Model.Definition;

namespace Model.Entities.PostingFilter
{
    public class StringSearchTargetData
    {
        public IList<StringSearchTarget> Targets;
        public IList<string> Values;
        public bool MatchCase;
    }
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FilterCategory Category { get; set; }
        public StringSearchTargetData StringSearchTargetData { get; set; }

        public override string ToString()
        {
            return Name + Environment.NewLine + Description;
        }
    }
}