using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;


namespace HaynyBatista.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Email");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetPhoneNumber(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PhoneNumber");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetMoneySaverUserID(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("HaynyUsuarioId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}