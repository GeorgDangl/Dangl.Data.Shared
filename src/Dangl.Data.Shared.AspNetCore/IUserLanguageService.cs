namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This interface should provide the language for the current Http request
    /// </summary>
    public interface IUserLanguageService
    {
        /// <summary>
        /// The should return the language / locale for the current Http request
        /// </summary>
        /// <returns></returns>
        string GetUserLocale();
    }
}
