using System;
using System.Security.Claims;

namespace Realtor.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            Claim claim = principal.FindFirst("sub");

            return claim?.Value;
        }
    }
}
