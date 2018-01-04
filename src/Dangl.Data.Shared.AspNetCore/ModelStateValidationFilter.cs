using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// If the ModelState of the context is invalid, a <see cref="BadRequestObjectResult"/>
    /// is returned with an <see cref="AspNetCoreApiError"/> body that contains error information. For valid states,
    /// no action is executed.
    /// </summary>
    public class ModelStateValidationFilter : IActionFilter
    {
        /// <summary>
        /// If the ModelState of the context is invalid, a <see cref="BadRequestObjectResult"/>
        /// is returned with an <see cref="AspNetCoreApiError"/> body that contains error information. For valid states,
        /// no action is executed.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context?.ModelState?.IsValid == false)
            {
                var apiErrorResult = new AspNetCoreApiError(context.ModelState);
                context.Result = new BadRequestObjectResult(apiErrorResult);
            }
        }

        /// <summary>
        /// This is a no-op
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not doing anything after the action has executed
        }
    }
}
