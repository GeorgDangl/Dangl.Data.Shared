using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dangl.Data.Shared.AspNetCore.Authorization
{
    // TODO UPDATE README
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
    // TODO DELETE AND RENAME FILE
    /*
    /// <summary>
    /// This configures a policy that checks for required claims and ensures their value
    /// is either "true" or a date in the future until it is valid.
    /// </summary>
    public class ClaimBasedAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// The list of names of claims that should be checked, any single one found will
        /// result in the requirement being met
        /// </summary>
        public IReadOnlyList<string> ClaimNames { get; }

        /// <summary>
        /// The list of names of claims that should be checked, any single one found will
        /// result in the requirement being met
        /// </summary>
        /// <param name="claimNames"></param>
        public ClaimBasedAuthorizationRequirement(params string[] claimNames)
        {
            ClaimNames = claimNames?
                .Distinct()
                .ToList()
                .AsReadOnly()
                ?? throw new ArgumentNullException(nameof(claimNames));
            if (!ClaimNames.Any())
            {
                throw new ArgumentException($"Please provide at least one value for {nameof(claimNames)}");
            }
        }
    }
    */
}
