using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dangl.Data.Shared.Tests
{
    public class ApiErrorTests
    {
        [Fact]
        public void SetsSingleStringAsErrorMessage()
        {
            var apiError = new ApiError("Fatal Error");
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Fatal Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void SetsDictionaryAsErrors()
        {
            var dict = new Dictionary<string, string[]>();
            var apiError = new ApiError(dict);
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
            var apiError = new ApiError(dict);
            Assert.Equal(2, apiError.Errors.Count);
            Assert.Equal("FirstError", apiError.Errors.First().Key);
            Assert.Equal("SecondError", apiError.Errors.Last().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Single(apiError.Errors.Last().Value);
            Assert.Equal("FirstMessage", apiError.Errors.First().Value[0]);
            Assert.Equal("SecondMessage", apiError.Errors.Last().Value[0]);
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputString()
        {
            Assert.Throws<ArgumentNullException>("message", () => new ApiError((string)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnEmptyInputString()
        {
            Assert.Throws<ArgumentNullException>("message", () => new ApiError(string.Empty));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputFlatDictionary()
        {
            Assert.Throws<ArgumentNullException>("errors", () => new ApiError((IDictionary<string, string>)null));
        }

        [Fact]
        public void ArgumentNullExceptionOnNullInputDictionary()
        {
            Assert.Throws<ArgumentNullException>("errors", () => new ApiError((IDictionary<string, string[]>)null));
        }

        [Fact]
        public void UnknownErrorOnEmptyFlatDictionary()
        {
            var apiError = new ApiError(new Dictionary<string, string>());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }

        [Fact]
        public void UnknownErrorOnEmptyDictionary()
        {
            var apiError = new ApiError(new Dictionary<string, string[]>());
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Equal("Message", apiError.Errors.First().Key);
            Assert.Single(apiError.Errors.First().Value);
            Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
        }
    }
}
