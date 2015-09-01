using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static bool IsNullSpaceOrEmpty(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == "")
                return true;
            return false;
        }

        public static string FormatString(this string value, params object[] args)
        {
            return String.Format(value, args);
        }
    }
}
