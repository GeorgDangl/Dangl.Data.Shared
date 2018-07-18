using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This middleware transforms incoming Http HEAD request
    /// internally to Http GET and ensures that a null stream is
    /// being sent back.
    /// </summary>
    public class HttpHeadRequestMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// This middleware transforms incoming Http HEAD request
        /// internally to Http GET and ensures that a null stream is
        /// being sent back.
        /// </summary>
        public HttpHeadRequestMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// This middleware transforms incoming Http HEAD request
        /// internally to Http GET and ensures that a null stream is
        /// being sent back.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            var isHead = HttpMethods.IsHead(context.Request.Method);
            if (isHead)
            {
                context.Request.Method = HttpMethods.Get;
                context.Response.Body = Stream.Null;
            }

            await _next(context);

            if (isHead)
            {
                context.Request.Method = HttpMethods.Head;
            }
        }
    }
}
