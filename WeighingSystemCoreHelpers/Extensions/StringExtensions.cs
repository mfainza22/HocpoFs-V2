using System.Collections;
using System.Collections.Generic;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class StringExtensions
    {
        public static string Parameterize(this string value)
        {
            string result = "@";
            result = "@" + value;
            return result;
        }

        public static string EnHash(this string value)
        {
            if (value.Length == 0) return "";

            string result = "#";
            result += value;
            return result;
        }

        public static string ToCapitalizeString(this string text)
        {
            if (text.Length == 0) return "";

            return System.Char.ToUpperInvariant(text[0]) + text.Substring(1).ToLower();
        }

        public static string ToAcceptableHTMLId(this string text)
        {
            if (text.Length == 0) return "";
            text = text.Replace(".", "_");
            text = text.Replace(" ", "_");
            return text;
        }

        public static bool IsNull(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }

        public static string DefaultIfEmpty(this string obj, string defaultVal = "")
        {
            if (string.IsNullOrEmpty(obj))
            {
                return defaultVal;
            }

            return obj;
        }
    }
}