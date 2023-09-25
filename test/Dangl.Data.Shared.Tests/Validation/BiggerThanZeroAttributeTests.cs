using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dangl.Data.Shared.Validation;
using Xunit;

namespace Dangl.Data.Shared.Tests.Validation
{
    public static class BiggerThanZeroAttributeTests
    {
        public class IntegerTests
        {
            [Fact]
            public void ReturnsErrorWhenPropertyIsNoInteger()
            {
                var objectToValidate = new ClassWithStringAttribute { Property = "Hello world!" };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithStringAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.False(validationResult);
                Assert.True(validationResults.Any());
            }

            [Fact]
            public void ReturnsErrorWhenPropertyIsZero()
            {
                var objectToValidate = new ClassWithIntegerAttribute { Property = 0 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithIntegerAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.False(validationResult);
                Assert.True(validationResults.Any());
            }

            [Fact]
            public void ReturnsErrorWhenPropertyIsNegative()
            {
                var objectToValidate = new ClassWithIntegerAttribute { Property = -4 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithIntegerAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.False(validationResult);
                Assert.True(validationResults.Any());
            }

            [Fact]
            public void ReturnsOkWhenPropertyIsPositiveInteger()
            {
                var objectToValidate = new ClassWithIntegerAttribute { Property = 2 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithIntegerAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.True(validationResult);
                Assert.False(validationResults.Any());
            }
        }

        public class LongTests
        {
            [Fact]
            public void ReturnsErrorWhenPropertyIsZero()
            {
                var objectToValidate = new ClassWithLongAttribute { Property = 0 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithLongAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.False(validationResult);
                Assert.True(validationResults.Any());
            }

            [Fact]
            public void ReturnsErrorWhenPropertyIsNegative()
            {
                var objectToValidate = new ClassWithLongAttribute { Property = -4 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithLongAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.False(validationResult);
                Assert.True(validationResults.Any());
            }

            [Fact]
            public void ReturnsOkWhenPropertyIsPositiveInteger()
            {
                var objectToValidate = new ClassWithLongAttribute { Property = 2 };
                var validationContext = new ValidationContext(objectToValidate)
                {
                    MemberName = nameof(ClassWithLongAttribute.Property)
                };
                var validationResults = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
                Assert.True(validationResult);
                Assert.False(validationResults.Any());
            }
        }

        public class ClassWithStringAttribute
        {
            [BiggerThanZero]
            public string Property { get; set; }
        }

        public class ClassWithIntegerAttribute
        {
            [BiggerThanZero]
            public int Property { get; set; }
        }

        public class ClassWithLongAttribute
        {
            [BiggerThanZero]
            public long Property { get; set; }
        }
    }
}
