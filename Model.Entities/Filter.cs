using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Model.Definition;
using Newtonsoft.Json;

namespace Model.Entities
{
    public class Filter
    {

        public Filter()
        {
            StringSearchTargets = new List<StringSearchTarget>();
            StringSearchValues = new List<string>();
            DisciplinesSearchTargets = new List<DisciplineEnum>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAntiFilter { get; set; }
        public int PointValue { get; set; }
        public FilterCategory Category { get; set; }

        public ICollection<StringSearchTarget> StringSearchTargets { get; set; }

        public ICollection<string> StringSearchValues { get; set; }
        public bool MatchCase { get; set; }

        public ICollection<DisciplineEnum> DisciplinesSearchTargets { get; set; }

        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

        public ValueSearchTarget ValueSearchSelectedItem { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public string StringSearchTargetsSerialized
        {
            get { return JsonConvert.SerializeObject(StringSearchTargets); }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                var data = JsonConvert.DeserializeObject<ICollection<StringSearchTarget>>(value);
                StringSearchTargets = data ?? new List<StringSearchTarget>();
            }
        }

        public string StringSearchValuesSerialized
        {
            get { return JsonConvert.SerializeObject(StringSearchValues); }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                var data = JsonConvert.DeserializeObject<ICollection<string>>(value);
                StringSearchValues = data ?? new List<string>();
            }
        }

        public string DisciplinesSearchTargetsSerialized
        {
            get { return JsonConvert.SerializeObject(DisciplinesSearchTargets); }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                var data = JsonConvert.DeserializeObject<ICollection<DisciplineEnum>>(value);
                DisciplinesSearchTargets = data ?? new List<DisciplineEnum>();
            }
        }

        public override string ToString()
        {
            return Name + " " + Environment.NewLine + Description;
        }
    }
}