

using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimValue)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Value == claimValue);

            return (claim != null) ? claim.Value : string.Empty;
        }
    }

}