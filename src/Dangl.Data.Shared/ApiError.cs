using System;
using System.Collections.Generic;

namespace Dangl.Data.Shared
{
    public class ApiError
    {
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

        public ApiError(IDictionary<string, string[]> errors)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
            SetUnknownErrorIfNoErrorsSet();
        }

        protected void SetUnknownErrorIfNoErrorsSet()
        {
            if (Errors.Count == 0)
            {
                Errors.Add("Message", new[] { "Unknown Error" });
            }
        }

        public IDictionary<string, string[]> Errors { get; protected set; }
    }
}
