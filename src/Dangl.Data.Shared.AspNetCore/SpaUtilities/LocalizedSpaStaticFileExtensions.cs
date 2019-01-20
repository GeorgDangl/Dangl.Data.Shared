using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="defaultFile"></param>
        public static void UseLocalizedSpaStaticFiles(this IApplicationBuilder applicationBuilder, string defaultFile)
        {
            applicationBuilder.Use((context, next) =>
            {
                // In this part of the pipeline, the request path is altered to point to a localized SPA asset
                var spaFilePathProvider = context.RequestServices.GetRequiredService<LocalizedSpaStaticFilePathProvider>();
                context.Request.Path = spaFilePathProvider.GetRequestPath("/" + defaultFile.TrimStart('/'));
                return next();
            });

            applicationBuilder.UseStaticFiles();
        }
    }
}
