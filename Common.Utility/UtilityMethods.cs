namespace Model.Entities
{
    public static class UtilityMethods
    {
        public static string TrimEndCommaAndSpace(this string toString)
        {
            return toString.TrimEnd(',', ' ');
        }
    }
}
