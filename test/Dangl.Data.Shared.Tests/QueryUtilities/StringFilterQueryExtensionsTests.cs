using Dangl.Data.Shared.QueryUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dangl.Data.Shared.Tests.QueryUtilities
{
    public class StringFilterQueryExtensionsTests
    {
        [Fact]
        public void ArgumentNullExceptionForNullQueryable()
        {
            var queryable = new List<string>().AsQueryable();
            var filter = "hello world";
            Assert.Throws<ArgumentNullException>("queryable", () => StringFilterQueryExtensions
                .Filter<object>(null, filter, x => y => y != null, true));
        }

        [Fact]
        public void ArgumentNullExceptionForNullExpression()
        {
            var queryable = new List<string>().AsQueryable();
            var filter = "hello world";
            Assert.Throws<ArgumentNullException>("filterExpression", () => queryable
                .Filter(filter, null, true));
        }

        [Fact]
        public void DoesNotThrowForNullFilter()
        {
            var queryable = new List<string>().AsQueryable();
            var actual = queryable.Filter(null, x => y => y != null, true);
        }

        [Fact]
        public void FiltersWithContainsMethod()
        {
            var queryable = new List<string>
            {
                "Hello World",
                "How are you?",
                "What a nice day!"
            }
            .AsQueryable();

            var actual = queryable.Filter("Hello", filter => word => word.Contains(filter), transformFilterToLowercase: false);
            Assert.Single(actual);
        }

        [Fact]
        public void FiltersWithContainsMethod_TransformedToLowercase()
        {
            var queryable = new List<string>
            {
                "hello World",
                "How are you?",
                "What a nice day!"
            }
            .AsQueryable();

            var actual = queryable.Filter("Hello", filter => word => word.Contains(filter), transformFilterToLowercase: true);
            Assert.Single(actual);
        }

        [Fact]
        public void FiltersWithContainsMethod_NotTransformedToLowercase()
        {
            var queryable = new List<string>
            {
                "hello World",
                "How are you?",
                "What a nice day!"
            }
            .AsQueryable();

            var actual = queryable.Filter("Hello", filter => word => word.Contains(filter), transformFilterToLowercase: false);
            Assert.Empty(actual);
        }
    }
}
