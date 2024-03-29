﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Common.Utility;
using Model.Definition;

namespace Model.Entities.JobMine
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
            foreach (var name in Enum.GetValues(typeof(DisciplineEnum)).Cast<DisciplineEnum>().Where(name => name.GetDescription() != null && data.Contains(name.GetDescription())))
                this[currentDisciplineIndex++] = (byte)name;
        }

        /// <summary>
        ///     Key of the Job that contain this discipline
        /// </summary>
        [Key]
        public int Id { get; set; }
        public byte Discipline1 { get; set; }
        public byte Discipline2 { get; set; }
        public byte Discipline3 { get; set; }
        public byte Discipline4 { get; set; }
        public byte Discipline5 { get; set; }

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

        public bool ContainDiscipline(DisciplineEnum discipline)
        {
            try
            {
                for (byte i = 0; i < GlobalDef.MaxNumberOfDisciplinesPerJob; i++)
                {
                    if (this[i] == (byte)discipline)
                        return true;
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
            }
            return false;
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
                    IEnumerable<DisciplineEnum> disciplineEnums = Enum.GetValues(typeof(DisciplineEnum)).Cast<DisciplineEnum>();
                    if (key != (byte)DisciplineEnum.UnAssigned && disciplineEnums.Contains((DisciplineEnum)key))
                    {
                        string disciplineName = ((DisciplineEnum)key).GetDescription();
                        toString += disciplineName + ", ";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType(), GetType(), e.Message);
            }
            return toString.TrimEndCommaAndSpace();
        }
    }
}