using Dangl.Data.Shared.AspNetCore.Authorization;
using System;
using System.Linq;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Authorization
{
    public class BaseClaimBasedAuthorizationRequirementTests
    {
        [Fact]
        public void ArgumentNullExceptionForNullClaimNames()
        {
            Assert.Throws<ArgumentNullException>("claimNames", () => new BaseClaimBasedAuthorizationRequirement(null));
        }

        [Fact]
        public void ArgumentExceptionForEmptyClaimNames()
        {
            Assert.Throws<ArgumentException>(() => new BaseClaimBasedAuthorizationRequirement(new string[] { }));
        }

        [Fact]
        public void ReturnsCorrectClaimNames_WithOnlyOne()
        {
            var requirement = new BaseClaimBasedAuthorizationRequirement(new[] { "myClaimName" });
            Assert.Equal("myClaimName", requirement.ClaimNames.Single());
        }

        [Fact]
        public void ReturnsCorrectClaimNames_WithMultiple()
        {
            var requirement = new BaseClaimBasedAuthorizationRequirement(new[] { "myClaimName", "client_myClaimName" });
            Assert.Equal("myClaimName", requirement.ClaimNames.First());
            Assert.Equal("client_myClaimName", requirement.ClaimNames.Last());
        }
    }
}
