using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// If the invoked controller action has one or more parameters of type <see cref="IFormFile"/> with
    /// a <see cref="RequiredAttribute"/>, this filter validates that they have a value bound and are not null.
    /// If they are null, a <see cref="BadRequestObjectResult"/> with an <see cref="ApiError"/> is returned.
    /// For valid invocations, no action is executed.
    /// </summary>
    public class RequiredFormFileValidationFilter : IActionFilter
    {
        /// <summary>
        /// If the invoked controller action has one or more parameters of type <see cref="IFormFile"/> with
        /// a <see cref="RequiredAttribute"/>, this filter validates that they have a value bound and are not null.
        /// If they are null, a <see cref="BadRequestObjectResult"/> with an <see cref="ApiError"/> is returned.
        /// For valid invocations, no action is executed.
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requiredFormFileParameters = context
                .ActionDescriptor
                .Parameters
                .Where(p => p.ParameterType == typeof(IFormFile))
                .OfType<ControllerParameterDescriptor>()
                .Where(c => c.ParameterInfo.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)))
                .ToList();

            var missingFormFileParameterNames = requiredFormFileParameters
                .Where(r => context.ActionArguments.All(arg => arg.Key != r.Name))
                .Select(r => r.Name)
                .ToList();

            if (missingFormFileParameterNames.Any())
            {
                var errors = new Dictionary<string, string[]>();
                var fileErrorMessages = missingFormFileParameterNames
                    .Select(n => $"Missing required form file \"{n}\"")
                    .ToArray();
                errors.Add("Missing file", fileErrorMessages);
                var apiErrorResult = new ApiError(errors);
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
