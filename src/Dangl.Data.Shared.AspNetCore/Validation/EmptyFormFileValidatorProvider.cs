using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Dangl.Data.Shared.AspNetCore.Validation
{
    /// <summary>
    /// This validates that <see cref="IFormFile"/>s have a length greater than zero bytes
    /// </summary>
    public class EmptyFormFileValidatorProvider : IModelValidatorProvider
    {
        /// <summary>
        /// Attaches the validator if there is none yet present and the
        /// parameter is assignable to an <see cref="IFormFile"/>
        /// </summary>
        /// <param name="context"></param>
        public void CreateValidators(ModelValidatorProviderContext context)
        {
            if (typeof(IFormFile).IsAssignableFrom(context.ModelMetadata.ModelType))
            {
                context.Results.Add(new ValidatorItem
                {
                    IsReusable = true,
                    Validator = new EmptyFormFileValidator()
                });
            }
        }
    }
}
