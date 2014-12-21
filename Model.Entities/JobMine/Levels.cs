using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Definition;

namespace Model.Entities.JobMine
{
    /// <summary>
    ///     Entity that specify the level(s) the job is target for
    /// </summary>
    public class Levels
    {
        public Levels()
        {
        }

        /// <summary>
        ///     Initalize a new instance of level entity
        /// </summary>
        /// <param name="levelString"></param>
        public Levels(string levelString = " ")
        {
            try
            {
                for (int i = 0; i < GlobalDef.MaxNumberOfLevels; i++)
                    this[i] = levelString.Contains(GlobalDef.LevelNames[i]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        ///     Id of the Job that contain this level entity
        /// </summary>
        [Key, ForeignKey("Job")]
        public int JobId { get; set; }

        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }
        //public bool IsBachelor { get; set; }
        //public bool IsMaster { get; set; }
        //public bool IsPhD { get; set; }

        /// <summary>
        ///     Instance of Job entity that contain this level entity
        /// </summary>
        public virtual Job Job { get; set; }

        /// <summary>
        ///     Get or Set the level with the given index
        /// </summary>
        /// <param name="index">index that indicate level #1 to #(max)</param>
        /// <returns>Get: discipline number in byte | Set: void</returns>
        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return IsJunior;
                    case 1:
                        return IsIntermediate;
                    case 2:
                        return IsSenior;
                    default:
                        return false;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        IsJunior = value;
                        break;
                    case 1:
                        IsIntermediate = value;
                        break;
                    case 2:
                        IsSenior = value;
                        break;
                }
            }
        }

        /// <summary>
        ///     Get if the Job contain this Level instance is at the level with the given index
        /// </summary>
        /// <param name="levelEnum">index as LevelEnum</param>
        public bool IsLevel(LevelEnum levelEnum)
        {
            return IsLevel((int) levelEnum);
        }

        /// <summary>
        ///     Get if the Job contain this Level instance is at the level with the given index
        /// </summary>
        /// <param name="index">index of the level from Junior to Intermediate to ...</param>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when the index is out of the ranges</exception>
        public bool IsLevel(int index)
        {
            if (index < 0 || index >= GlobalDef.MaxNumberOfLevels)
                throw new ArgumentOutOfRangeException();
            return this[index];
        }

        /// <summary>
        ///     Overriden ToString method that convert the levels into reader friendly string
        /// </summary>
        /// <returns>Reader friendly string</returns>
        public override string ToString()
        {
            string toString = string.Empty;

            for (int i = 0; i < GlobalDef.MaxNumberOfLevels; i++)
                if (this[i])
                    toString += GlobalDef.LevelNames[i] + ", ";
            return toString.TrimEndCommaAndSpace();
        }
    }
}