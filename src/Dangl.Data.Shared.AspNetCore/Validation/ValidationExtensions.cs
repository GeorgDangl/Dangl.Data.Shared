using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dangl.Data.Shared.AspNetCore.Validation
{
    /// <summary>
    /// Extensions for validation
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// This leads to parameters of type <see cref="IFormFile"/> that are empty, meaning
        /// when they have a body length of zero bytes, to return an invalid ModelState
        /// </summary>
        /// <param name="mvcOptions"></param>
        /// <returns></returns>
        public static MvcOptions AddEmptyFormFileValidator(this MvcOptions mvcOptions)
        {
            mvcOptions.ModelValidatorProviders.Add(new EmptyFormFileValidatorProvider());
            return mvcOptions;
        }
    }
}
