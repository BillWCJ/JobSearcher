using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Definition
{
    /// <summary>
    /// Enum for the different Levels for a job
    /// </summary>
    /// <remarks>BA, MA and PHD are current disabled</remarks>
    public enum LevelEnum : byte
    {
        Junior,
        Intermediate,
        Senior,
        //Bachelor,
        //Master,
        //PhD
    };
    public partial class GlobalDef
    {
        /// <summary>
        /// Maximum number of levels a job is at
        /// </summary>
        /// <remarks>BA, MA and PHD are current disabled, which is why this number is 3 (would have been 6)</remarks>
        public const int MaxNumberOfLevels = 3;//6;

        /// <summary>
        /// Dictionary of the different level names using their LevelEnum as Key
        /// </summary>
        /// <remarks>BA, MA and PHD are current disabled</remarks>
        public static Dictionary<LevelEnum, string> LevelName = new Dictionary<LevelEnum, string>
        {
            {LevelEnum.Junior, LevelEnum.Junior.ToString()},
            {LevelEnum.Intermediate, LevelEnum.Intermediate.ToString()},
            {LevelEnum.Senior, LevelEnum.Senior.ToString()},
            //{LevelEnum.Bachelor, LevelEnum.Bachelor.ToString()},
            //{LevelEnum.Master, LevelEnum.Master.ToString()},
            //{LevelEnum.PhD, LevelEnum.PhD.ToString()},
        };

        /// <summary>
        /// List of all level names that is ordered from Junior to Intermediate to...
        /// </summary>
        /// <remarks>BA, MA and PHD are current disabled</remarks>
        public static readonly string[] LevelNames = Enum.GetNames(typeof(LevelEnum));
    }
}
