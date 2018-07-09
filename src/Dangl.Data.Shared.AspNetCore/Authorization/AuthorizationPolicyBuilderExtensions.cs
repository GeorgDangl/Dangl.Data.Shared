using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Dangl.Data.Shared.AspNetCore.Authorization
{
    /// <summary>
    /// Extension class to set up policies
    /// </summary>
    public static class AuthorizationPolicyBuilderExtensions
    {
        /// <summary>
        /// This configures a policy that checks for required claims and ensures their value
        /// is either "true" or a date in the future until it is valid.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="services"></param>
        /// <param name="claimNames"></param>
        /// <returns></returns>
        public static AuthorizationPolicyBuilder AddClaimsValueAuthorization(this AuthorizationPolicyBuilder policy,
            IServiceCollection services,
            params string[] claimNames)
        {
            services.AddSingleton<IAuthorizationHandler, ClaimBasedAuthorizationRequirementHandler>();

            return policy
                .RequireAuthenticatedUser()
                .AddRequirements(new ClaimBasedAuthorizationRequirement(claimNames));
        }
    }
}
