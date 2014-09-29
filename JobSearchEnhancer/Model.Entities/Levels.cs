using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Definition;

namespace Model.Entities
{
    /// <summary>
    ///     Specify the level(s) the job is target for
    /// </summary>
    public class Levels
    {
        public Levels()
        {
        }

        public Levels(string levelString) : this()
        {
            try
            {
                for (int i = 0; i < GlobalDef.MaxNumberOfLevels; i++)
                {
                    this[i] = IsLevel(GlobalDef.LevelNames[i], levelString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Key, ForeignKey("Job")]
        public int Id { get; set; }
        public bool IsJunior { get; set; }
        public bool IsIntermediate { get; set; }
        public bool IsSenior { get; set; }

        //public bool IsBachelor { get; set; }
        //public bool IsMaster { get; set; }
        //public bool IsPhD { get; set; }
        public virtual Job Job { get; set; }

        //public Dictionary<int, bool> Level = new Dictionary<int, bool>
        //{
        //    {0, IsJunior},
        //    {1, IsIntermediate},
        //    {2, IsSenior},
        //    //{3, IsBachelor},
        //    //{4, IsMaster},
        //    //{5, IsPhD}
        //};
        public bool this[int i]
        {
            get
            {
                switch (i)
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
                switch (i)
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

        public bool IsLevel(LevelEnum levelEnum)
        {
            return IsLevel((int) levelEnum);
        }

        public bool IsLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= GlobalDef.MaxNumberOfLevels)
                throw new ArgumentOutOfRangeException();
            return this[levelIndex];
        }

        public static bool IsLevel(string level, string levelString)
        {
            return levelString.IndexOf(level, StringComparison.InvariantCulture) > -1;
        }

        public override string ToString()
        {
            string toString = string.Empty;

            for (int i = 0; i < GlobalDef.MaxNumberOfLevels; i++)
                if (this[i])
                    toString += GlobalDef.LevelNames[i] + ", ";
            return toString.TrimEnd(',', ' ');
        }
    }
}