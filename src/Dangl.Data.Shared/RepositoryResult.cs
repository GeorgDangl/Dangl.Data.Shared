﻿namespace Dangl.Data.Shared
{
    /// <summary>
    /// A <see cref="RepositoryResult"/> returns data from the Repository layer
    /// </summary>
    public class RepositoryResult<T>
    {
        private RepositoryResult() { }

        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The result of the operation
        /// </summary>
        public T Result { get; private set; }

        /// <summary>
        /// Null if no error occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates an unsuccessfull result
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static RepositoryResult<T> Fail(string errorMessage)
        {
            return new RepositoryResult<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        /// <summary>
        /// Creates a successfull result
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RepositoryResult<T> Success(T value)
        {
            return new RepositoryResult<T>
            {
                IsSuccess = true,
                Result = value
            };
        }
    }

    /// <summary>
    /// A <see cref="RepositoryResult"/> returns data from the Repository layer
    /// </summary>
    public class RepositoryResult
    {
        private RepositoryResult() { }
        
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; private set; }
        
        /// <summary>
        /// Null if no error occurred
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Creates an unsuccessfull result
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static RepositoryResult Fail(string errorMessage)
        {
            return new RepositoryResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        /// <summary>
        /// Creates a successfull result
        /// </summary>
        /// <returns></returns>
        public static RepositoryResult Success()
        {
            return new RepositoryResult
            {
                IsSuccess = true
            };
        }
    }
}