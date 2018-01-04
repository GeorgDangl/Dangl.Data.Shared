using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dangl.Data.Shared.Validation;
using Xunit;

namespace Dangl.Data.Shared.Tests.Validation
{
    public class AbsoluteUriAttributeTests
    {
        [Fact]
        public void ReturnsErrorWhenPropertyIsNoString()
        {
            var objectToValidate = new ClassWithIntegerAttribute { Property = 4 };
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
        public void ReturnsNoErrorWhenPropertyIsNull()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = null };
            var validationContext = new ValidationContext(objectToValidate)
            {
                MemberName = nameof(ClassWithIntegerAttribute.Property)
            };
            var validationResults = new List<ValidationResult>();
            var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
            Assert.True(validationResult);
            Assert.False(validationResults.Any());
        }

        [Fact]
        public void ReturnsErrorWhenPropertyIsEmpty()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = string.Empty };
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
        public void ReturnsErrorWhenPropertyIsNoUrl()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = "Hello world!" };
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
        public void ReturnsErrorWhenPropertyIsRelativeUrlWithStartingSlash()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = "/docs/intro.html" };
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
        public void ReturnsErrorWhenPropertyIsRelativeUrl()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = "docs/intro.html" };
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
        public void ReturnsOkWhenPropertyIsAbsoluteWithProtocol()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = "https://www.example.com/docs/intro.html" };
            var validationContext = new ValidationContext(objectToValidate)
            {
                MemberName = nameof(ClassWithIntegerAttribute.Property)
            };
            var validationResults = new List<ValidationResult>();
            var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
            Assert.True(validationResult);
            Assert.False(validationResults.Any());
        }

        [Fact]
        public void ReturnsErrorWhenPropertyIsAbsoluteWithoutProtocol()
        {
            var objectToValidate = new ClassWithStringAttribute { Property = "www.example.com/docs/intro.html" };
            var validationContext = new ValidationContext(objectToValidate)
            {
                MemberName = nameof(ClassWithIntegerAttribute.Property)
            };
            var validationResults = new List<ValidationResult>();
            var validationResult = Validator.TryValidateObject(objectToValidate, validationContext, validationResults, true);
            Assert.False(validationResult);
            Assert.True(validationResults.Any());
        }

        public class ClassWithStringAttribute
        {
            [AbsoluteUri]
            public string Property { get; set; }
        }

        public class ClassWithIntegerAttribute
        {
            [AbsoluteUri]
            public int Property { get; set; }
        }
    }
}
