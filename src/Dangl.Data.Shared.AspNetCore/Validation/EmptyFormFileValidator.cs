using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Dangl.Data.Shared.AspNetCore.Validation
{
    /// <summary>
    /// This validates that <see cref="IFormFile"/>s have a length greater than zero bytes
    /// </summary>
    public class EmptyFormFileValidator : IModelValidator
    {
        /// <summary>
        /// Returns an error if the parameter is an <see cref="IFormFile"/> with a length of zero bytes
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (context.Model is IFormFile formFile
                && formFile.Length == 0)
            {
                return new[]
                {
                    new ModelValidationResult(context.ModelMetadata.Name, "The uploaded file has a length of zero bytes, please provide a non-empty file")
                };
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
