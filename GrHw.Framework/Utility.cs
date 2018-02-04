using System;

namespace GrHw.Framework
{
    public static class Utility
    {
        public static DateTime AsDate(this string dateString)
        {
            DateTime dt;
            if (DateTime.TryParse(dateString, out dt))
            {
                return dt;
            }
            return DateTime.MinValue;
        }
    }
}