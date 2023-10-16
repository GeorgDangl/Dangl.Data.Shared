using System;

namespace Dangl.Data.Shared.AspNetCore.StringExtensions
{
    /// <summary>
    /// Extensions when working with route or action names
    /// </summary>
    public static class RouteNameExtensions
    {
        /// <summary>
        /// Will strip the suffix 'Async' from the input string if it exists, case insensitive
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string WithoutAsyncSuffix(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.EndsWith("Async", StringComparison.InvariantCultureIgnoreCase))
            {
                return input[0..^5];
            }

            return input;
        }

        /// <summary>
        /// Will strip the suffix 'Controller' from the input string if it exists, case insensitive
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string WithoutControllerSuffix(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
            {
                return input[0..^10];
            }

            return input;
        }
    }
}
