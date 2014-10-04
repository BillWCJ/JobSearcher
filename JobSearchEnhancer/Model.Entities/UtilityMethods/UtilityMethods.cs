using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.UtilityMethods
{
    public static class UtilityMethods
    {
        public static string TrimEndCommaAndSpace(string toString)
        {
            return toString.TrimEnd(',', ' ');
        }
    }
}
