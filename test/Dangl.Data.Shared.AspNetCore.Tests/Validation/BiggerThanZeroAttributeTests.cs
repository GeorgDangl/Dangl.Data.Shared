using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dangl.Data.Shared.AspNetCore.Validation;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests.Validation
{
    public class BiggerThanZeroAttributeTests
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
    }
}
