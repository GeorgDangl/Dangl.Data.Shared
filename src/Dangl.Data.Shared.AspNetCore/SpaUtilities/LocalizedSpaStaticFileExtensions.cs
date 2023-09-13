using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;

namespace Dangl.Data.Shared.AspNetCore.SpaUtilities
{
    /// <summary>
    /// Extensions to work with localized SPAs
    /// </summary>
    public static class LocalizedSpaStaticFileExtensions
    {
        /// <summary>
        /// This configures the necessary services for servicing localized SPAs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="languageCookieName">The cookie under which user locale can be saved</param>
        /// <param name="availableLocales">The available locales of the SPA</param>
        /// <param name="spaRootPath">The root path under which the SPA resides, e.g. "/dist"</param>
        public static void AddLocalizedSpaStaticFiles(this IServiceCollection services,
            string languageCookieName,
            string[] availableLocales,
            string spaRootPath)
        {
            services.AddTransient<IUserLanguageService>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return new UserLanguageService(httpContextAccessor, languageCookieName, availableLocales);
            });
            services.AddTransient<LocalizedSpaStaticFilePathProvider>(serviceProvider =>
            {
                var userLanguageService = serviceProvider.GetRequiredService<IUserLanguageService>();
                return new LocalizedSpaStaticFilePathProvider(userLanguageService, spaRootPath);
            });
        }

        /// <summary>
        /// This configures the localized SPA serving in the app pipeline. It should be called as
        /// last step in the config, since it will serve the SPA index file for all requests.
        /// For requests, it also tries to determine if a localized file exists, and if so, serves
        /// it directly. This makes accessing relative files in SPAs possible, e.g. accessing
        /// '/assets/logo.png' will serve the localized file '/dist/en/assets/logo.png' if it exists.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="defaultFile"></param>
        public static void UseLocalizedSpaStaticFiles(this IApplicationBuilder applicationBuilder, string defaultFile)
        {
            applicationBuilder.Use((context, next) =>
            {
                // In this part of the pipeline, the request path is altered to point to a localized SPA asset
                var spaFilePathProvider = context.RequestServices.GetRequiredService<LocalizedSpaStaticFilePathProvider>();
                var isEmptyOrDefaultFileRequest = !context.Request.Path.HasValue
                || string.IsNullOrWhiteSpace(context.Request.Path.Value)
                    || context.Request.Path.Value == "/"
                    || context.Request.Path.Value.EndsWith(defaultFile, StringComparison.InvariantCultureIgnoreCase);

                if (!isEmptyOrDefaultFileRequest)
                {
                    // We're checking if the file actually exists, because it could be a relative
                    // file request e.g. to an asset or a script file
                    var localRequestPath = spaFilePathProvider.GetRequestPath("/" + context.Request.Path.ToString());
                    var webHostEnvironment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                    var requestedFileInfo = webHostEnvironment.WebRootFileProvider.GetFileInfo(localRequestPath);
                    if (requestedFileInfo.Exists)
                    {
                        context.Request.Path = localRequestPath;
                        return next();
                    }
                }

                context.Request.Path = spaFilePathProvider.GetRequestPath("/" + defaultFile.TrimStart('/'));
                return next();
            });

            applicationBuilder.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    if (ctx.Context.Request.Path.ToString().EndsWith("/" + defaultFile.TrimStart('/'), StringComparison.InvariantCultureIgnoreCase))
                    {
                        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "no-store";
                    }
                }
            });
        }
    }
}
