using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dangl.Data.Shared.AspNetCore
{
    public class AspNetCoreApiError : ApiError
    {
        public AspNetCoreApiError(ModelStateDictionary modelState)
        {
            Errors = modelState.ToDictionary(errorEntry => errorEntry.Key,
                errorEntry => errorEntry.Value.Errors.Select(error => error.ErrorMessage).ToArray());
        }

        public AspNetCoreApiError(IEnumerable<IdentityError> identityErrors)
        {
            Errors = identityErrors.ToDictionary(error => error.Code, error => new[] { error.Description });
        }

        public AspNetCoreApiError(string message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "Message", new[] { message } }
            };
        }

        public override Dictionary<string, string[]> Errors { get; }
    }
}
