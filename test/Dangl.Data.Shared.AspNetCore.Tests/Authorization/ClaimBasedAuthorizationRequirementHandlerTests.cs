using Dangl.Data.Shared.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Authorization
{
    public class ClaimBasedAuthorizationRequirementHandlerTests
    {
        private readonly ClaimBasedAuthorizationRequirementHandler _handler = new ClaimBasedAuthorizationRequirementHandler(null);
        private AuthorizationHandlerContext _context;
        private readonly ClaimBasedAuthorizationRequirement _requirement = new ClaimBasedAuthorizationRequirement(_requiredClaimName, _optionalPrefix + _requiredClaimName);

        private static readonly string _requiredClaimName = "required_claim_name";
        private static readonly string _optionalPrefix = "client_";

        private bool _returnUserIsAuthenticated = true;
        private readonly List<Claim> _userClaimsToReturn = new List<Claim> { new Claim("sub", "user_id_value") };

        private async Task HandleAsync()
        {
            var claimsPrincipal = new ClaimsPrincipal();

            if (_returnUserIsAuthenticated)
            {
                var claimsIdentity = new ClaimsIdentity(_userClaimsToReturn, "test_auth");
                claimsPrincipal.AddIdentity(claimsIdentity);
            }
            else
            {
                var claimsIdentity = new ClaimsIdentity(new List<Claim>());
                claimsPrincipal.AddIdentity(claimsIdentity);
            }

            _context = new AuthorizationHandlerContext(new[] { _requirement }, claimsPrincipal, null);

            await _handler.HandleAsync(_context);
        }

        [Fact]
        public async Task SetsUpUserAsIsAuthenticated()
        {
            await HandleAsync();
            Assert.True(_context.User.Identity.IsAuthenticated);
        }

        [Fact]
        public async Task SetUpUserIsNotAuthenticated()
        {
            _returnUserIsAuthenticated = false;
            await HandleAsync();
            Assert.False(_context.User.Identity.IsAuthenticated);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUnauthenticatedUser()
        {
            _returnUserIsAuthenticated = false;
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithoutClaims()
        {
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueFalse()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, "false"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueBeforeNow()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, "2018-04-30T17:48:40+00:00"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueTrue()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, "true"));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFuture()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, DateTime.UtcNow.AddHours(1).ToString("o")));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFutureWrappedInQuotes()
        {
            var yearToUse = $"{DateTime.Now.Year + 1:0000}";
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, $"\"{yearToUse}-08-08T09:02:15.5732531Z\""));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFutureWithExistingValue()
        {
            var yearToUse = $"{DateTime.Now.Year + 1:0000}";
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, $"{yearToUse}-08-08T09:02:15.5732531Z"));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueThatIsNotDate()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, "something_else"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueThatIsEmpty()
        {
            _userClaimsToReturn.Add(new Claim(_requiredClaimName, ""));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        // ******
        // With prefix below

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueFalseWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, "false"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueBeforeNowWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, "2018-04-30T17:48:40+00:00"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueTrueWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, "true"));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFutureWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, DateTime.UtcNow.AddHours(1).ToString("o")));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFutureWrappedInQuotesWithClaimPrefix()
        {
            var yearToUse = $"{DateTime.Now.Year + 1:0000}";
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, $"\"{yearToUse}-08-08T09:02:15.5732531Z\""));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesSuccessForUserWithClaimValueInFutureWithExistingValueWithClaimPrefix()
        {
            var yearToUse = $"{DateTime.Now.Year + 1:0000}";
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, $"{yearToUse}-08-08T09:02:15.5732531Z"));
            await HandleAsync();
            Assert.True(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueThatIsNotDateWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, "something_else"));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }

        [Fact]
        public async Task IndicatesNoSuccessForUserWithClaimValueThatIsEmptyWithClaimPrefix()
        {
            _userClaimsToReturn.Add(new Claim(_optionalPrefix + _requiredClaimName, ""));
            await HandleAsync();
            Assert.False(_context.HasSucceeded);
        }
    }
}
