


using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class LongExtensions
    {
        public static bool IsNullOrZero(this Int64 value)
        {
            if (value == 0) { return true; }
            return false;
        }

        public static bool IsNullOrZero(this Nullable<Int64> value)
        {
            if (value == null) { return true; }
            else if (value == 0) { return true; }

            return false;
        }
    }

}