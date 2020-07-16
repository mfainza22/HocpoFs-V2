


using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class BooleanExtensions
    {
        public static bool IsNull(this Nullable<Boolean> value)
        {
            if (value.HasValue) return false;
            return true;
        }

        public static string ToString(this bool value, BooleanText booleanText)
        {
            MatchCollection matches = Regex.Matches(booleanText.ToString(), "[A-Z][a-z]+");
            return value.ToString(matches[0].Value, matches[1].Value);
            // return Convert.ToString(value).ToString().ToLower();
        }

        public static string ToString(this bool value, string trueValue, string falseValue)
        {
            return (value) ? trueValue : falseValue;
        }

        public enum BooleanText
        {
            TrueOrFalse,
            YesOrNo,
            EnabledDisabled
        }

    }

}