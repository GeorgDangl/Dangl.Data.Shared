using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// Data transfer class to convey api errors. This is a extended class to support
    /// initialization from Asp.Net Core and Asp.Net Identity errors
    /// </summary>
    public class AspNetCoreApiError : ApiError
    {
        /// <summary>
        /// Outputs an error in the form of { "Message": "Error" }
        /// </summary>
        /// <param name="message"></param>
        public AspNetCoreApiError(string message)
            : base(message)
        { }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        public AspNetCoreApiError(IDictionary<string, string> errors)
            : base(errors)
        { }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        public AspNetCoreApiError(IDictionary<string, string[]> errors)
            : base(errors)
        { }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="modelState"></param>
        public AspNetCoreApiError(ModelStateDictionary modelState)
            : base(modelState?.ToDictionary(errorEntry => errorEntry.Key,
                errorEntry => errorEntry.Value.Errors.Select(error => error.ErrorMessage).ToArray()) ?? throw new ArgumentNullException(nameof(modelState)))
        { }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="identityErrors"></param>
        public AspNetCoreApiError(IEnumerable<IdentityError> identityErrors)
            : base(identityErrors?.ToDictionary(error => error.Code, error => new[] { error.Description }) ?? throw new ArgumentNullException(nameof(identityErrors)))
        { }
    }
}
