using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GlobalVariable;
using System.Globalization;
using Model.Definition;

namespace Model.Entities
{
    public class Disciplines
    {
        [Key, ForeignKey("Job")]
        public int Id { get; set; }
        public byte Discipline1 { get; set; }
        public byte Discipline2 { get; set; }
        public byte Discipline3 { get; set; }
        public byte Discipline4 { get; set; }
        public byte Discipline5 { get; set; }
        public virtual Job Job { get; set; }

        //public Dictionary<int, byte> Discipline = new Dictionary<int, byte>
        //{
        //    {0,Discipline1},
        //    {1,Discipline2},
        //    {2,Discipline3},
        //    {3,Discipline4},
        //    {4,Discipline5}
        //};

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Discipline1;
                    case 1:
                        return Discipline2;
                    case 2:
                        return Discipline3;
                    case 3:
                        return Discipline4;
                    case 4:
                        return Discipline5;
                    default:
                        return (byte) DisciplineEnum.UnAssigned;
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        Discipline1 = value;
                        break;
                    case 1:
                        Discipline2 = value;
                        break;
                    case 2:
                        Discipline3 = value;
                        break;
                    case 3:
                        Discipline4 = value;
                        break;
                    case 4:
                        Discipline5 = value;
                        break;
                }
            }
        }

        public Disciplines()
        {
        }
        public Disciplines(string data) : this()
        {
            byte disciplineCurrentIndex = 0;
            foreach (var disciplinesName in GlobalDef.DisciplinesNames.Where(disciplinesName => data.IndexOf(disciplinesName.Value, StringComparison.InvariantCulture) > -1))
            {
                this[disciplineCurrentIndex++] = disciplinesName.Key;
            }
        }

        public override string ToString()
        {
            string toString = String.Empty;
            try
            {
                for (byte i = 0; i < GlobalDef.MaxNumberOfDisciplinesPerJob; i++)
                {
                    byte key = this[i];
                    if ( key != (byte) DisciplineEnum.UnAssigned)
                    toString += GlobalDef.DisciplinesNames[key] + ", ";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType(), GetType(), e.Message);
            }
            return toString.TrimEnd(',', ' ');
        }
    }
}