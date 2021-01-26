using System;

namespace Dangl.Data.Shared
{
    /// <summary>
    /// A <see cref="RepositoryResult"/> returns data from the Repository layer
    /// </summary>
    public class RepositoryResult<TResult, TError>
    {
        private const string UnknownError = "Unknown Error";

        private RepositoryResult() { }

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The result of the operation
        /// </summary>
        public TResult Value { get; private set; }

        /// <summary>
        /// The operation error, if error occurred, null if no error occurred
        /// </summary>
        public TError Error { get; private set; }

        /// <summary>
        /// The message describing the operation failure, The message describing the operation failure, null if no error occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates an unsuccessful result
        /// </summary>
        /// <param name="error">Typed error data</param>
        /// <param name="errorMessage">An explanatory message to describe the error</param>
        /// <exception cref="ArgumentNullException">When <paramref name="error"/> is null</exception>
        /// <returns>The object representing the unsuccessful result</returns>
        public static RepositoryResult<TResult, TError> Fail(TError error, string errorMessage = null)
        {
            if (error == null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            return new RepositoryResult<TResult, TError>
            {
                IsSuccess = false,
                Error = error,
                ErrorMessage = errorMessage ?? UnknownError
            };
        }

        /// <summary>
        /// Creates a successful result
        /// </summary>
        /// <param name="value">The value to return when the result is successful</param>
        /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null</exception>
        /// <returns>The object representing the successful result</returns>
        public static RepositoryResult<TResult, TError> Success(TResult value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new RepositoryResult<TResult, TError>
            {
                IsSuccess = true,
                Value = value
            };
        }

        /// <summary>
        /// Converts a value to a successful result
        /// </summary>
        /// <param name="value">The value to convert</param>
        public static implicit operator RepositoryResult<TResult, TError>(TResult value) =>
            Success(value);
    }

    /// <summary>
    /// A <see cref="RepositoryResult"/> returns data from the Repository layer
    /// </summary>
    public class RepositoryResult<TResult>
    {
        private const string UnknownError = "Unknown Error";

        private RepositoryResult() { }

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The result of the operation
        /// </summary>
        public TResult Value { get; private set; }

        /// <summary>
        /// The message describing the operation failure, null if no error occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates an unsuccessful result
        /// </summary>
        /// <param name="errorMessage">An explanatory message to describe the error</param>
        /// <returns>The object representing the unsuccessful result</returns>
        public static RepositoryResult<TResult> Fail(string errorMessage) =>
            new RepositoryResult<TResult>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };

        /// <summary>
        /// Creates an unsuccessful result with an unknown error
        /// </summary>
        /// <returns>The object representing the unsuccessful result</returns>
        public static RepositoryResult<TResult> Fail() =>
            new RepositoryResult<TResult>
            {
                IsSuccess = false,
                ErrorMessage = UnknownError
            };

        /// <summary>
        /// Creates a successful result
        /// </summary>
        /// <param name="value">The value to return when the result is successful</param>
        /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null</exception>
        /// <returns>The object representing the successful result</returns>
        public static RepositoryResult<TResult> Success(TResult value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new RepositoryResult<TResult>
            {
                IsSuccess = true,
                Value = value
            };
        }

        /// <summary>
        /// Converts a value to a successful result
        /// </summary>
        /// <param name="value">The value to convert</param>
        public static implicit operator RepositoryResult<TResult>(TResult value) =>
            Success(value);
    }

    /// <summary>
    /// A <see cref="RepositoryResult"/> returns data from the Repository layer
    /// </summary>
    public class RepositoryResult
    {
        private const string UnknownError = "Unknown Error";

        private RepositoryResult() { }

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The message describing the operation failure, null if no error occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates an unsuccessful result
        /// </summary>
        /// <param name="errorMessage">An explanatory message to describe the error</param>
        /// <returns>The object representing the unsuccessful result</returns>
        public static RepositoryResult Fail(string errorMessage) =>
            new RepositoryResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };

        /// <summary>
        /// Creates an unsuccessful result with an unknown error
        /// </summary>
        /// <returns>A object representing the fail result</returns>
        public static RepositoryResult Fail() =>
            new RepositoryResult
            {
                IsSuccess = false,
                ErrorMessage = UnknownError
            };

        /// <summary>
        /// Creates a successful result
        /// </summary>
        /// <returns>The object representing the successful result</returns>
        public static RepositoryResult Success() =>
            new RepositoryResult
            {
                IsSuccess = true
            };
    }
}
