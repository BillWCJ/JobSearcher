using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Model.Definition;

namespace Model.Entities
{
    /// <summary>
    ///     Entity that specify the siscipline(s) the job is target for
    /// </summary>
    public class Disciplines
    {
        public Disciplines()
        {
        }

        /// <summary>
        ///     Initalize a instance of Disciplines using a string that contains all the disciplines
        /// </summary>
        /// <param name="data">Optional Jobmine Disciplines section string in job detail</param>
        public Disciplines(string data = " ")
        {
            byte currentDisciplineIndex = 0;
            foreach (KeyValuePair<byte, string> name in GlobalDef.DisciplinesNames.Where(name => name.Value != null && data.Contains(name.Value)))
                this[currentDisciplineIndex++] = name.Key;
        }

        /// <summary>
        ///     Key of the Job that contain this discipline
        /// </summary>
        [Key, ForeignKey("Job")]
        public int JobId { get; set; }

        public byte Discipline1 { get; set; }
        public byte Discipline2 { get; set; }
        public byte Discipline3 { get; set; }
        public byte Discipline4 { get; set; }
        public byte Discipline5 { get; set; }

        /// <summary>
        ///     Instance of Job entity that contain this discipline
        /// </summary>
        public virtual Job Job { get; set; }

        /// <summary>
        ///     Get or Set the discipline with the given index
        /// </summary>
        /// <param name="index">index that indicate discipline #1 to #(max)</param>
        /// <returns>Get: discipline number in byte | Set: void</returns>
        public byte this[int index]
        {
            get
            {
                switch (index)
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
                switch (index)
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

        /// <summary>
        ///     Overriden ToString method that return discipline string that is reader friendly
        /// </summary>
        /// <returns>all the disicipline in their respective JobMine names</returns>
        public override string ToString()
        {
            string toString = String.Empty;
            try
            {
                for (byte i = 0; i < GlobalDef.MaxNumberOfDisciplinesPerJob; i++)
                {
                    byte key = this[i];
                    if (key != (byte) DisciplineEnum.UnAssigned &&  GlobalDef.DisciplinesNames.ContainsKey(key))
                    {
                        string disciplineName = GlobalDef.DisciplinesNames[key];
                        toString += disciplineName + ", ";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType(), GetType(), e.Message);
            }
            return UtilityMethods.UtilityMethods.TrimEndCommaAndSpace(toString);
        }
    }
}