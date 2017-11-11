using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dangl.Data.Shared.Tests
{
    public class RepositoryResultTests
    {
        public class GenericTests
        {
            [Fact]
            public void ReturnsSuccessOnSuccess()
            {
                var result = RepositoryResult<string>.Success("Hello");
                Assert.True(result.IsSuccess);
                Assert.Null(result.ErrorMessage);
                Assert.Equal("Hello", result.Value);
            }

            [Fact]
            public void ReturnsPrimitiveType()
            {
                var result = RepositoryResult<int>.Success(5);
                Assert.Equal(5, result.Value);
            }

            [Fact]
            public void ReturnsDefaultForNonNullableTypeOnFail()
            {
                var resultInt = RepositoryResult<int>.Fail();
                Assert.Equal(default, resultInt.Value);
                var resultDouble = RepositoryResult<double>.Fail();
                Assert.Equal(default, resultDouble.Value);
                var resultDateTime = RepositoryResult<DateTime>.Fail();
                Assert.Equal(default, resultDateTime.Value);
                var resultGuid = RepositoryResult<Guid>.Fail();
                Assert.Equal(default, resultGuid.Value);
            }
            
            [Fact]
            public void ArgumentNullExceptionOnNullValueForSuccess()
            {
                Assert.Throws<ArgumentNullException>("value", () => RepositoryResult<string>.Success(null));
            }

            [Fact]
            public void ReturnsNoSuccessOnFail()
            {
                var result = RepositoryResult<string>.Fail("Error");
                Assert.False(result.IsSuccess);
                Assert.Equal("Error", result.ErrorMessage);
                Assert.Null(result.Value);
            }

            [Fact]
            public void ReturnsUnknownErrorOnNoError()
            {
                var result = RepositoryResult<string>.Fail();
                Assert.False(result.IsSuccess);
                Assert.Equal("Unknown Error", result.ErrorMessage);
                Assert.Null(result.Value);
            }

            [Fact]
            public void ReturnsCorrectValueOnSuccess()
            {
                var value = new object();
                var result = RepositoryResult<object>.Success(value);
                Assert.Equal(value, result.Value);
            }
        }

        public class RegularTests
        {
            [Fact]
            public void ReturnsSuccessOnSuccess()
            {
                var result = RepositoryResult.Success();
                Assert.True(result.IsSuccess);
                Assert.Null(result.ErrorMessage);
            }

            [Fact]
            public void ReturnsNoSuccessOnFail()
            {
                var result = RepositoryResult.Fail("Error");
                Assert.False(result.IsSuccess);
                Assert.Equal("Error", result.ErrorMessage);
            }

            [Fact]
            public void ReturnsUnknownErrorOnNoError()
            {
                var result = RepositoryResult.Fail();
                Assert.False(result.IsSuccess);
                Assert.Equal("Unknown Error", result.ErrorMessage);
            }
        }
    }
}
