using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Dangl.Data.Shared.Validation
{
    /// <summary>
    /// This is a <see cref="ValidationAttribute"/> that can only be applied to integer or long properties and ensures they have a value bigger than zero.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public sealed class BiggerThanZeroAttribute : ValidationAttribute
    {
        /// <summary>
        /// Will return true, this validation attribute requires a validation context to access to class that is defining the property.
        /// </summary>
        public override bool RequiresValidationContext => true;

        /// <summary>
        /// Will return an error if the attribute is either not an integer / long or is smaller or equal to zero.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationProperty = validationContext.ObjectInstance.GetType().GetTypeInfo()
                .DeclaredProperties
                .Single(property => property.Name == validationContext.MemberName)
                .GetValue(validationContext.ObjectInstance);

            var isInteger = validationProperty is int;
            if (isInteger)
            {
                return ValidateInteger(validationProperty, validationContext);
            }

            var isLong = validationProperty is long;
            if (isLong)
            {
                return ValidateLong(validationProperty, validationContext);
            }

            return new ValidationResult($"{validationContext.MemberName} must be an integer or a long");
        }

        private ValidationResult ValidateInteger(object validationProperty, ValidationContext validationContext)
        {
            var integerValue = (int)validationProperty;
            var isBiggerThanZero = integerValue > 0;
            return isBiggerThanZero
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.MemberName} must be bigger than zero.");
        }

        private ValidationResult ValidateLong(object validationProperty, ValidationContext validationContext)
        {
            var longValue = (long)validationProperty;
            var isBiggerThanZero = longValue > 0;
            return isBiggerThanZero
                ? ValidationResult.Success
                : new ValidationResult($"{validationContext.MemberName} must be bigger than zero.");
        }
    }
}
