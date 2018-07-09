using Dangl.Data.Shared.AspNetCore.Authorization;
using System;
using System.Linq;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Authorization
{
    // TODO DELETE
    /*
    public class ClaimBasedAuthorizationRequirementTests
    {
        [Fact]
        public void ArgumentNullExceptionForNullClaimNames()
        {
            Assert.Throws<ArgumentNullException>("claimNames", () => new ClaimBasedAuthorizationRequirement(null));
        }

        [Fact]
        public void ArgumentExceptionForEmptyClaimNames()
        {
            Assert.Throws<ArgumentException>(() => new ClaimBasedAuthorizationRequirement(new string[] { }));
        }

        [Fact]
        public void ReturnsCorrectClaimNames_WithOnlyOne()
        {
            var requirement = new ClaimBasedAuthorizationRequirement(new[] { "myClaimName" });
            Assert.Equal("myClaimName", requirement.ClaimNames.Single());
        }

        [Fact]
        public void ReturnsCorrectClaimNames_WithMultiple()
        {
            var requirement = new ClaimBasedAuthorizationRequirement(new[] { "myClaimName", "client_myClaimName" });
            Assert.Equal("myClaimName", requirement.ClaimNames.First());
            Assert.Equal("client_myClaimName", requirement.ClaimNames.Last());
        }
    }
    */
}
