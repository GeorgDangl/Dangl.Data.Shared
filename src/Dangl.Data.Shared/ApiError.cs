using System;
using System.Collections.Generic;

namespace Dangl.Data.Shared
{
    /// <summary>
    /// Data transfer class to convey api errors
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// This parameterless constructor does not set up any errors.
        /// </summary>
        public ApiError() { }

        /// <summary>
        /// Outputs an error in the form of { "Message": "Error" }
        /// </summary>
        /// <param name="message"></param>
        public ApiError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            Errors = new Dictionary<string, string[]>
            {
                { "Message", new[] { message } }
            };
            SetUnknownErrorIfNoErrorsSet();
        }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        public ApiError(IDictionary<string, string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }
            var dict = new Dictionary<string, string[]>();
            foreach (var entry in errors)
            {
                dict.Add(entry.Key, new[] {entry.Value});
            }
            Errors = dict;
            SetUnknownErrorIfNoErrorsSet();
        }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        public ApiError(IDictionary<string, string[]> errors)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
            SetUnknownErrorIfNoErrorsSet();
        }

        /// <summary>
        /// If there are no errors, a default "Unknown Error" is created
        /// </summary>
        protected void SetUnknownErrorIfNoErrorsSet()
        {
            if (Errors.Count == 0)
            {
                Errors.Add("Message", new[] { "Unknown Error" });
            }
        }

        /// <summary>
        /// This dictionary contains a set of all errors and their messages
        /// </summary>
        public IDictionary<string, string[]> Errors { get; set; }
    }

    /// <summary>
    /// Data transfer class to convey api errors with a typed error result
    /// </summary>
    public class ApiError<TError> : ApiError
    {
        // For Json deserialization
        private ApiError() : base() { }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiError(TError error)
            : base()
        {
            Error = error;
        }

        /// <summary>
        /// Outputs an error in the form of { "Message": "Error" }
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public ApiError(string message, TError error)
            : base(message)
        {
            Error = error;
        }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="error"></param>
        public ApiError(IDictionary<string, string> errors, TError error)
            : base(errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }
            var dict = new Dictionary<string, string[]>();
            foreach (var entry in errors)
            {
                dict.Add(entry.Key, new[] { entry.Value });
            }
            Errors = dict;
            SetUnknownErrorIfNoErrorsSet();
            Error = error;
        }

        /// <summary>
        /// Outputs all errors
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="error"></param>
        public ApiError(IDictionary<string, string[]> errors, TError error)
            : base(errors)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
            SetUnknownErrorIfNoErrorsSet();
            Error = error;
        }

        /// <summary>
        /// The operation error
        /// </summary>
        public TError Error { get; set; }
    }
}
