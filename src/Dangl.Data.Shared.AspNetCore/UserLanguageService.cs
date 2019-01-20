using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This service provides the locale for the current Http request
    /// </summary>
    public class UserLanguageService : IUserLanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<string> _availableLanguages;
        private readonly string _languageCookieName;

        /// <summary>
        /// This service provides the locale for the current Http request
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="languageCookieName">This is the name of the cookie from which locales are tried to be read from</param>
        /// <param name="availableLanguages"></param>
        public UserLanguageService(IHttpContextAccessor httpContextAccessor,
            string languageCookieName,
            IEnumerable<string> availableLanguages)
        {
            _httpContextAccessor = httpContextAccessor;
            _availableLanguages = availableLanguages
                .Select(l => l.ToLowerInvariant())
                .ToList();
            _languageCookieName = languageCookieName;
        }

        /// <summary>
        /// This will return the locale for the current Http request. If a cookie with a locale is set,
        /// then this will be used. Otherwise, the Accept-Language header is parsed and the first configured
        /// locale is returned
        /// </summary>
        /// <returns></returns>
        public string GetUserLocale()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                return _availableLanguages[0];
            }

            var cookieLocale = request.Cookies[_languageCookieName];
            if (!string.IsNullOrWhiteSpace(cookieLocale)
                && _availableLanguages.Any(al => al.Equals(cookieLocale, StringComparison.OrdinalIgnoreCase)))
            {
                return cookieLocale;
            }

            var userLocales = request.Headers[Microsoft.Net.Http.Headers.HeaderNames.AcceptLanguage].ToString();
            var userAcceptLanguage = GetAcceptLanguageFromHeaderOrNull(userLocales);

            if (!string.IsNullOrWhiteSpace(userAcceptLanguage))
            {
                return userAcceptLanguage;
            }

            return _availableLanguages[0];
        }

        /// <summary>
        /// This parsed the Accept-Language header value and returns the first result
        /// in the header that is also present in the local availableLocales variable
        /// </summary>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public string GetAcceptLanguageFromHeaderOrNull(string headerValue)
        {
            if (headerValue == null)
            {
                return null;
            }
            try
            {
                var clientLanguages = (headerValue)
                    .Split(',')
                    .Select(StringWithQualityHeaderValue.Parse)
                    .OrderByDescending(language => language.Quality.GetValueOrDefault(1))
                    .Select(language => language.Value)
                    .Select(languageCode =>
                    {
                        if (languageCode.Contains("-"))
                        {
                            return languageCode.Split('-').First();
                        }

                        return languageCode;
                    })
                    .Select(languageCode => languageCode.ToLowerInvariant())
                    .Distinct()
                    .Where(languageCode => !string.IsNullOrWhiteSpace(languageCode) && languageCode.Trim() != "*");
                return clientLanguages
                    .FirstOrDefault(clientLanguage => _availableLanguages.Contains(clientLanguage));
            }
            catch
            {
                return null;
            }
        }
    }
}
