using Microsoft.AspNetCore.Builder;

namespace Dangl.Data.Shared.AspNetCore.SpaUtilities
{
    /// <summary>
    /// Extensions for working with SPAs
    /// </summary>
    public static class SpaStaticFileMiddlewareExtensions
    {
        /// <summary>
        /// This will configure static files and SPA support. If a request hits and no matching
        /// file is found, it will return the index file for the SPA, e.g. index.html.
        /// This must be called as the last step in the pipeline, as it will redirect all
        /// requests that don't match an existing file to the SPA index file.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pathToIndex">Must be an absolute path relative to wwwroot, e.g. /dist/index.html</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSpaStaticFile(this IApplicationBuilder app, string pathToIndex)
        {
            app.UseStaticFiles();
            // This serves SPA files from disk,
            // e.g. from wwwroot/dist/index.html
            // if no file has been returned yet
            app.Use((context, next) =>
            {
                context.Request.Path = pathToIndex;
                return next();
            });
            app.UseStaticFiles();
            return app;
        }
    }
}
