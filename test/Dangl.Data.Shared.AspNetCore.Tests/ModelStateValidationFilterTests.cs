using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace Dangl.Data.Shared.AspNetCore.Tests
{
    public class ModelStateValidationFilterTests
    {
        private readonly ModelStateValidationFilter _filter = new ModelStateValidationFilter();

        [Fact]
        public void SetsNoResultForValidModelState()
        {
            var actionExcecutingContext = GetActionExecutingContext();
            _filter.OnActionExecuting(actionExcecutingContext);
            Assert.Null(actionExcecutingContext.Result);
        }

        [Fact]
        public void SetsBadRequestObjectResultForInvalidModelState()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Name", "Name property is required");
            var actionExcecutingContext = GetActionExecutingContext(modelState);
            _filter.OnActionExecuting(actionExcecutingContext);
            Assert.NotNull(actionExcecutingContext.Result);
            Assert.IsType<BadRequestObjectResult>(actionExcecutingContext.Result);
        }

        [Fact]
        public void ReturnsApiErrorInBodyOfBadRequestForInvalidModelState()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Name", "Name property is required");
            var actionExcecutingContext = GetActionExecutingContext(modelState);
            _filter.OnActionExecuting(actionExcecutingContext);
            Assert.NotNull(actionExcecutingContext.Result);
            Assert.IsType<BadRequestObjectResult>(actionExcecutingContext.Result);
            var result = actionExcecutingContext.Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ApiError>(result.Value);
            Assert.IsType<AspNetCoreApiError>(result.Value);
            var apiError = result.Value as AspNetCoreApiError;
            Assert.NotNull(apiError);
            Assert.Equal(1, apiError.Errors.Count);
            Assert.Contains(apiError.Errors, e => e.Key == "Name");
            Assert.Contains(apiError.Errors, e => e.Value.Contains("Name property is required"));
        }

        [Fact]
        public void DoesNotThrowIfContextNull()
        {
            _filter.OnActionExecuting(null);
        }

        [Fact]
        public void SetResultToBadRequestResultWhenModelStateIsInvalid()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Name", "Name property is required");
            var actionExcecutingContext = GetActionExecutingContext(modelState);
            _filter.OnActionExecuting(actionExcecutingContext);
            Assert.NotNull(actionExcecutingContext.Result);
            Assert.IsType<BadRequestObjectResult>(actionExcecutingContext.Result);
        }

        private ActionExecutingContext GetActionExecutingContext(ModelStateDictionary modelState = null)
        {
            modelState = modelState ?? new ModelStateDictionary();
            var actionContext = new ActionContext(
                new Mock<HttpContext>().Object,
                new Mock<RouteData>().Object,
                new Mock<ActionDescriptor>().Object,
                modelState);
            var actionExcecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new object());
            return actionExcecutingContext;
        }
    }
}
