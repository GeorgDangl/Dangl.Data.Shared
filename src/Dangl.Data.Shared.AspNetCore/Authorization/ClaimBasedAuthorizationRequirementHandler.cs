using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dangl.Data.Shared.AspNetCore.Authorization
{
    /// <summary>
    /// This configures a policy that checks for required claims and ensures their value
    /// is either "true" or a date in the future until it is valid.
    /// </summary>
    public class ClaimBasedAuthorizationRequirementHandler : AuthorizationHandler<ClaimBasedAuthorizationRequirement>
    {
        /// <summary>
        /// This checks if at least one of the required claims with a value indicating
        /// validity is present
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                foreach (var claim in context.User.Claims.Where(c => requirement.ClaimNames.Contains(c.Type)))
                {
                    if (ClaimHasValidValue(claim))
                    {
                        context.Succeed(requirement);
                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }

        private bool ClaimHasValidValue(Claim claim)
        {
            if (string.Equals(claim.Value, "true", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (DateTimeOffset.TryParse(claim.Value.Trim('"'), out var claimValidUntil) && claimValidUntil >= DateTimeOffset.UtcNow)
            {
                return true;
            }
            return false;
        }
    }
}
