using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// If the ModelState of the context is invalid, a <see cref="BadRequestObjectResult"/>
    /// is returned with an <see cref="AspNetCoreApiError"/> body that contains error information. For valid states,
    /// no action is executed.
    /// </summary>
    public class ModelStateValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// If the ModelState of the context is invalid, a <see cref="BadRequestObjectResult"/>
        /// is returned with an <see cref="AspNetCoreApiError"/> body that contains error information. For valid states,
        /// no action is executed.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public ModelStateValidationFilter(ILoggerFactory loggerFactory = null)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<ModelStateValidationFilter>();
        }

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
                _logger.LogInformation("Send BadRequest response due to invalid ModelState");
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
