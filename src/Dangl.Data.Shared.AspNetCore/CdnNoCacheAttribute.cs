using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This attribute should be used for API responses on Cloudflare CDN. It sets the 'Cache-Control' header to 'no-store, no-cache, no-transform'.
    /// Cloudflare will not automatically compress responses for unknown content types, and will also not automatically pass through compression.
    /// For example, returning mime type 'application/octet-stream' without an appropriate 'no-cache, no-transform' entry in 'Cache-Control' will
    /// make Cloudflare always return the response uncompressed, even if the original server did compress it.
    /// </summary>
    public class CdnNoCacheAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// This attribute should be used for API responses on Cloudflare CDN. It sets the 'Cache-Control' header to 'no-store, no-cache, no-transform'.
        /// Cloudflare will not automatically compress responses for unknown content types, and will also not automatically pass through compression.
        /// For example, returning mime type 'application/octet-stream' without an appropriate 'no-cache, no-transform' entry in 'Cache-Control' will
        /// make Cloudflare always return the response uncompressed, even if the original server did compress it.
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers[HeaderNames.CacheControl] = "no-store, no-cache, no-transform";
        }
    }
}
