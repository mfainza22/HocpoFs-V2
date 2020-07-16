


using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToFormattedWt(this decimal value)
        {
            return value.ToString("#,##0");
        }

        public static string ToFormattedWt(this decimal? value)
        {
            return value.HasValue ? value.Value.ToString("#,##0") : "0";
        }

        public static bool IsNullOrZero(this Nullable<Decimal> value)
        {
            if (value == null) { return true; }
            else if (value == 0) { return true; }

            return false;
        }
    }

}