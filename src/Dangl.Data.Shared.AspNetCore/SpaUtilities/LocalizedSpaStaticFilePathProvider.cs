namespace Dangl.Data.Shared.AspNetCore.SpaUtilities
{
    /// <summary>
    /// This class builds file paths for localized SPAs, meaning SPAs
    /// that are available in different locales in different subfolders.
    /// </summary>
    public class LocalizedSpaStaticFilePathProvider
    {
        private readonly IUserLanguageService _userLanguageService;
        private readonly string _distFolder;

        /// <summary>
        /// This class builds file paths for localized SPAs, meaning SPAs
        /// that are available in different locales in different subfolders.
        /// </summary>
        public LocalizedSpaStaticFilePathProvider(IUserLanguageService userLanguageService,
            string distFolder)
        {
            _userLanguageService = userLanguageService;
            _distFolder = distFolder;
        }

        /// <summary>
        /// This returns the path to the file for the current users locale
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public string GetRequestPath(string subpath)
        {
            var userLocale = _userLanguageService.GetUserLocale();
            var spaFilePath = "/" + _distFolder.TrimStart('/').TrimEnd('/') + "/" + userLocale + "/" + subpath.TrimStart('/');
            return spaFilePath;
        }
    }
}
