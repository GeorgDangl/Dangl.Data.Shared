using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dangl.Data.Shared.Tests
{
    public static class ApiErrorTests
    {
        public class Untyped
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

            [Fact]
            public void CanDeserializeEmptyApiErrorFromJson()
            {
                var jsonError = "{}";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Null(deserializedApiError.Errors);
            }

            [Fact]
            public void CanDeserializeSingleValueApiErrorFromJson()
            {
                var jsonError = "{ \"Errors\": { \"MyError\": [ \"Error\" ] } }";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Single(deserializedApiError.Errors);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("MyError", deserializedApiError.Errors.First().Key);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("Error", deserializedApiError.Errors.First().Value.First());
            }

            [Fact]
            public void CanDeserializeMultipleValueApiErrorFromJson()
            {
                var jsonError = "{ \"Errors\": {"
                    + "\"FirstError\": [ \"Single Error\" ],"
                    + "\"SecondError\": [ \"One\", \"Two\" ]"
                                + "} }";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Equal(2, deserializedApiError.Errors.Count);
                var firstError = deserializedApiError.Errors
                    .Where(e => e.Key == "FirstError")
                    .Select(e => e.Value)
                    .FirstOrDefault();
                Assert.NotNull(firstError);
                Assert.Single(firstError);
                Assert.Contains("Single Error", firstError);
                var secondError = deserializedApiError.Errors
                    .Where(e => e.Key == "SecondError")
                    .Select(e => e.Value)
                    .FirstOrDefault();
                Assert.NotNull(secondError);
                Assert.Equal(2, secondError.Length);
                Assert.Contains("One", secondError);
                Assert.Contains("Two", secondError);
            }
        }

        public class Typed
        {
            [Fact]
            public void SetsSingleStringAsErrorMessage()
            {
                var apiError = new ApiError<int>("Fatal Error", 0);
                Assert.Equal(1, apiError.Errors.Count);
                Assert.Equal("Message", apiError.Errors.First().Key);
                Assert.Single(apiError.Errors.First().Value);
                Assert.Equal("Fatal Error", apiError.Errors.First().Value[0]);
            }

            [Fact]
            public void SetsDictionaryAsErrors()
            {
                var dict = new Dictionary<string, string[]>();
                var apiError = new ApiError<int>(dict, 0);
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
                var apiError = new ApiError<int>(dict, 0);
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
                Assert.Throws<ArgumentNullException>("message", () => new ApiError<int>((string)null, 0));
            }

            [Fact]
            public void ArgumentNullExceptionOnEmptyInputString()
            {
                Assert.Throws<ArgumentNullException>("message", () => new ApiError<int>(string.Empty, 0));
            }

            [Fact]
            public void ArgumentNullExceptionOnNullInputFlatDictionary()
            {
                Assert.Throws<ArgumentNullException>("errors", () => new ApiError<int>((IDictionary<string, string>)null, 0));
            }

            [Fact]
            public void ArgumentNullExceptionOnNullInputDictionary()
            {
                Assert.Throws<ArgumentNullException>("errors", () => new ApiError<int>((IDictionary<string, string[]>)null, 0));
            }

            [Fact]
            public void UnknownErrorOnEmptyFlatDictionary()
            {
                var apiError = new ApiError<int>(new Dictionary<string, string>(), 0);
                Assert.Equal(1, apiError.Errors.Count);
                Assert.Equal("Message", apiError.Errors.First().Key);
                Assert.Single(apiError.Errors.First().Value);
                Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
            }

            [Fact]
            public void UnknownErrorOnEmptyDictionary()
            {
                var apiError = new ApiError<int>(new Dictionary<string, string[]>(), 0);
                Assert.Equal(1, apiError.Errors.Count);
                Assert.Equal("Message", apiError.Errors.First().Key);
                Assert.Single(apiError.Errors.First().Value);
                Assert.Equal("Unknown Error", apiError.Errors.First().Value[0]);
            }

            [Fact]
            public void CanDeserializeEmptyApiErrorFromJson()
            {
                var jsonError = "{}";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError<int>>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Null(deserializedApiError.Errors);
                Assert.Equal(default, deserializedApiError.Error);
            }

            [Fact]
            public void CanDeserializeSingleValueApiErrorFromJson()
            {
                var jsonError = "{ \"Errors\": { \"MyError\": [ \"Error\" ] } }";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError<int>>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Single(deserializedApiError.Errors);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("MyError", deserializedApiError.Errors.First().Key);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("Error", deserializedApiError.Errors.First().Value.First());
            }

            [Fact]
            public void CanDeserializeMultipleValueApiErrorFromJson()
            {
                var jsonError = "{ \"Errors\": {"
                    + "\"FirstError\": [ \"Single Error\" ],"
                    + "\"SecondError\": [ \"One\", \"Two\" ]"
                                + "} }";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError<int>>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Equal(2, deserializedApiError.Errors.Count);
                var firstError = deserializedApiError.Errors
                    .Where(e => e.Key == "FirstError")
                    .Select(e => e.Value)
                    .FirstOrDefault();
                Assert.NotNull(firstError);
                Assert.Single(firstError);
                Assert.Contains("Single Error", firstError);
                var secondError = deserializedApiError.Errors
                    .Where(e => e.Key == "SecondError")
                    .Select(e => e.Value)
                    .FirstOrDefault();
                Assert.NotNull(secondError);
                Assert.Equal(2, secondError.Length);
                Assert.Contains("One", secondError);
                Assert.Contains("Two", secondError);
            }

            [Fact]
            public void CanDeserializeSingleValueApiErrorFromJson_WithType()
            {
                var jsonError = "{ \"Error\": 3, \"Errors\": { \"MyError\": [ \"Error\" ] } }";
                var deserializedApiError = JsonConvert.DeserializeObject<ApiError<int>>(jsonError);
                Assert.NotNull(deserializedApiError);
                Assert.Equal(3, deserializedApiError.Error);
                Assert.Single(deserializedApiError.Errors);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("MyError", deserializedApiError.Errors.First().Key);
                Assert.Single(deserializedApiError.Errors.First().Value);
                Assert.Equal("Error", deserializedApiError.Errors.First().Value.First());
            }

            [Fact]
            public void SetsError_Primitive()
            {
                var error = 4;
                var apiError = new ApiError<int>(error);
                Assert.Equal(4, apiError.Error);
            }

            [Fact]
            public void SetsError_Object()
            {
                var error = new ErrorType();
                var apiError = new ApiError<ErrorType>(error);
                Assert.Equal(error, apiError.Error);
            }

            private class ErrorType { }
        }
    }
}
