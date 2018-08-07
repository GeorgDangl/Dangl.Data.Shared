using Microsoft.AspNetCore.Builder;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This middleware adds support for clients that send their request bodies compressed
    /// </summary>
    public static class CompressedRequestMiddlewareExtensions
    {
        /// <summary>
        /// This middleware adds support for clients that send their request bodies compressed
        /// </summary>
        public static IApplicationBuilder UseClientCompressionSupport(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CompressedRequestMiddleware>();
        }
    }
}
