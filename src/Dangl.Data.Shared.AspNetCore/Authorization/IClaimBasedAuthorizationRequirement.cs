using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Dangl.Data.Shared.AspNetCore.Authorization
{
    /// <summary>
    /// This configures a policy that checks for required claims and ensures their value
    /// is either "true" or a date in the future until it is valid.
    /// </summary>
    public interface IClaimBasedAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// The list of names of claims that should be checked, any single one found will
        /// result in the requirement being met
        /// </summary>
        IReadOnlyList<string> ClaimNames { get; }
    }
}
