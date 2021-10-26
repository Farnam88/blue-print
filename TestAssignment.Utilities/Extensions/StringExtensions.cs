using System.Globalization;

namespace TestAssignment.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsString(this string str)
        {
            return str is not null && !string.IsNullOrWhiteSpace(str);
        }

        public static string ToNormalize(this string str)
        {
            return str.Trim().ToUpper(CultureInfo.CurrentCulture);
        }

        public static bool HasString(string str)
        {
            return str is not null && !string.IsNullOrWhiteSpace(str);
        }
    }
}