using Microsoft.AspNetCore.Builder;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This middleware transforms incoming Http HEAD request
    /// internally to Http GET and ensures that a null stream is
    /// being sent back. It should be called before adding the Mvc middleware.
    /// </summary>
    public static class HttpHeadRequestMiddlewareExtensions
    {
        /// <summary>
        /// This middleware transforms incoming Http HEAD request
        /// internally to Http GET and ensures that a null stream is
        /// being sent back. It should be called before adding the Mvc middleware.
        /// </summary>
        public static IApplicationBuilder UseHttpHeadToGetTransform(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpHeadRequestMiddleware>();
        }
    }
}
