using System;
using System.ComponentModel.DataAnnotations;

namespace Dangl.Data.Shared.Validation
{
    /// <summary>
    /// This <see cref="ValidationAttribute"/> ensures that a string property represents an absolute uri.
    /// </summary>
    public sealed class AbsoluteUriAttribute : ValidationAttribute
    {
        /// <summary>
        /// Will return true, this validation attribute requires a validation context to access to class that is defining the property.
        /// </summary>
        public override bool RequiresValidationContext => true;

        /// <summary>
        /// Will return an error if the property is not a string or is not an absolute uri
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null; // Null is ok, this should be handled by a [Required] attribute if desired
            }
            if (!(value is string stringValue))
            {
                return new ValidationResult($"{validationContext.MemberName} must be a string");
            }
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult($"\"{stringValue}\" is present but empty or only whitespace");
            }
            if (!Uri.TryCreate(stringValue, UriKind.Absolute, out var outVar))
            {
                return new ValidationResult($"\"{stringValue}\" is not an absolute Uri");
            }
            return null;
        }
    }
}
