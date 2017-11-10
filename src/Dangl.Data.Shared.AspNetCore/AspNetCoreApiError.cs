using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dangl.Data.Shared.AspNetCore
{
    public class AspNetCoreApiError : ApiError
    {
        public AspNetCoreApiError(string message)
            : base(message)
        { }

        public AspNetCoreApiError(IDictionary<string, string> errors)
            : base(errors)
        { }

        public AspNetCoreApiError(IDictionary<string, string[]> errors)
            : base(errors)
        { }

        public AspNetCoreApiError(ModelStateDictionary modelState)
            : base(modelState?.ToDictionary(errorEntry => errorEntry.Key,
                errorEntry => errorEntry.Value.Errors.Select(error => error.ErrorMessage).ToArray()) ?? throw new ArgumentNullException(nameof(modelState)))
        { }

        public AspNetCoreApiError(IEnumerable<IdentityError> identityErrors)
            : base(identityErrors?.ToDictionary(error => error.Code, error => new[] { error.Description }) ?? throw new ArgumentNullException(nameof(identityErrors)))
        { }
    }
}
