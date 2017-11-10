using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dangl.Data.Shared.AspNetCore.Tests
{
    public class AspNetCoreApiErrorTests
    {
        [Fact]
        public void SetsSingleStringAsErrorMessage()
        {
            var apiError = new AspNetCoreApiError("Fatal Error");
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Fatal Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void SetsDictionaryAsErrors()
        {
            var dict = new Dictionary<string, string[]>();
            var apiError = new AspNetCoreApiError(dict);
            Assert.Equal(dict, apiError.Errors);
        }

        [Fact]
        public void ExpandsDictionaryAsErrorsWithSingleItemArrays()
        {
            var dict = new Dictionary<string, string>
            {
                {"FirstError", "FirstMessage"},
                {"SecondError", "SecondMessage"}
            };
            var apiError = new AspNetCoreApiError(dict);
            Assert.Equal(2, apiError.Errors.Count);
            Assert.Equal("FirstError", apiError.Errors.First().Key);
            Assert.Equal("SecondError", apiError.Errors.Last().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Single(apiError.Errors.Last().Value);
            Assert.Equal("FirstMessage", apiError.Errors.First().Value[0]);
            Assert.Equal("SecondMessage", apiError.Errors.Last().Value[0]);
        }

        [Fact]
        public void SetsDataFromModelState()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("First Error", "First Message");
            modelState.AddModelError("Second Error", "Second Message");
            var apiError = new AspNetCoreApiError(modelState);
            Assert.Equal(2, apiError.Errors.Count);
            Assert.Equal("First Error", apiError.Errors.First().Key);
            Assert.Equal("Second Error", apiError.Errors.Last().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Single(apiError.Errors.Last().Value);
            Assert.Equal("First Message", apiError.Errors.First().Value[0]);
            Assert.Equal("Second Message", apiError.Errors.Last().Value[0]);
        }

        [Fact]
        public void SetsDataFromIdentityErrors()
        {
            var identityErrors = new List<IdentityError>
            {
                new IdentityError {Code = "First Code", Description = "First Description"},
                new IdentityError {Code = "Second Code", Description = "Second Description"}
            };
            var apiError = new AspNetCoreApiError(identityErrors);
            Assert.Equal(2, apiError.Errors.Count);
            Assert.Equal("First Code", apiError.Errors.First().Key);
            Assert.Equal("Second Code", apiError.Errors.Last().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Single(apiError.Errors.Last().Value);
            Assert.Equal("First Description", apiError.Errors.First().Value[0]);
            Assert.Equal("Second Description", apiError.Errors.Last().Value[0]);
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputString()
        {
            Assert.Throws<ArgumentNullException>("message", () => new AspNetCoreApiError((string)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnEmptyInputString()
        {
            Assert.Throws<ArgumentNullException>("message", () => new AspNetCoreApiError(string.Empty));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputFlatDictionary()
        {
            Assert.Throws<ArgumentNullException>("errors", () => new AspNetCoreApiError((IDictionary<string, string>)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputDictionary()
        {
            Assert.Throws<ArgumentNullException>("errors", () => new AspNetCoreApiError((IDictionary<string, string[]>)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputModelStateDictionary()
        {
            Assert.Throws<ArgumentNullException>("modelState", () => new AspNetCoreApiError((ModelStateDictionary)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputIdentityErrors()
        {
            Assert.Throws<ArgumentNullException>("identityErrors", () => new AspNetCoreApiError((IEnumerable<IdentityError>)null));
        }

        [Fact]
        public void UnknownErrorOnEmptyFlatDictionary()
        {
            var apiError = new AspNetCoreApiError(new Dictionary<string, string>());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void UnknownErrorOnEmptyDictionary()
        {
            var apiError = new AspNetCoreApiError(new Dictionary<string, string[]>());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void UnknownErrorOnEmptyIdentityErrors()
        {
            var apiError = new AspNetCoreApiError(new List<IdentityError>());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void UnknownErrorOnEmptyModelStateDictionary()
        {
            var apiError = new AspNetCoreApiError(new ModelStateDictionary());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }
    }
}
