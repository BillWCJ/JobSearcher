using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Definition
{
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
        public const int MaxNumberOfLevels = 3;//6;

        public static Dictionary<LevelEnum, string> LevelName = new Dictionary<LevelEnum, string>
        {
            {LevelEnum.Junior, LevelEnum.Junior.ToString()},
            {LevelEnum.Intermediate, LevelEnum.Intermediate.ToString()},
            {LevelEnum.Senior, LevelEnum.Senior.ToString()},
            //{LevelEnum.Bachelor, LevelEnum.Bachelor.ToString()},
            //{LevelEnum.Master, LevelEnum.Master.ToString()},
            //{LevelEnum.PhD, LevelEnum.PhD.ToString()},
        };

        public static readonly string[] LevelNames = Enum.GetNames(typeof(LevelEnum));
    }
}
