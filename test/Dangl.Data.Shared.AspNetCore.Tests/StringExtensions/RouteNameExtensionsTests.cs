using Dangl.Data.Shared.AspNetCore.StringExtensions;
using System;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.StringExtensions
{
    public static class RouteNameExtensionsTests
    {
        public class WithoutAsyncSuffix
        {
            [Fact]
            public void ArgumentNullExceptionForNullInput()
            {
                Assert.Throws<ArgumentNullException>(() => RouteNameExtensions.WithoutAsyncSuffix(null));
            }

            [Fact]
            public void ReturnsInputForNoAsyncSuffix()
            {
                var input = "Test";
                var result = input.WithoutAsyncSuffix();
                Assert.Equal(input, result);
            }

            [Theory]
            [InlineData("TestAsync")]
            [InlineData("Testasync")]
            [InlineData("TestASYNC")]
            public void ReturnsInputWithoutAsyncSuffix(string input)
            {
                var result = input.WithoutAsyncSuffix();
                Assert.Equal("Test", result);
            }
        }

        public class WithoutControllerSuffix
        {
            [Fact]
            public void ArgumentNullExceptionForNullInput()
            {
                Assert.Throws<ArgumentNullException>(() => RouteNameExtensions.WithoutControllerSuffix(null));
            }

            [Fact]
            public void ReturnsInputForNoAsyncSuffix()
            {
                var input = "Test";
                var result = input.WithoutControllerSuffix();
                Assert.Equal(input, result);
            }

            [Theory]
            [InlineData("TestController")]
            [InlineData("Testcontroller")]
            [InlineData("TestCONTROLLER")]
            public void ReturnsInputWithoutAsyncSuffix(string input)
            {
                var result = input.WithoutControllerSuffix();
                Assert.Equal("Test", result);
            }
        }
    }
}
