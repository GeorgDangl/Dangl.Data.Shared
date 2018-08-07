using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dangl.Data.Shared.AspNetCore
{
    /// <summary>
    /// This middleware adds support for clients that send their request bodies compressed
    /// </summary>
    public class CompressedRequestMiddleware
    {
        private readonly RequestDelegate next;
        private const string ContentEncodingHeader = "Content-Encoding";
        private const string ContentEncodingGzip = "gzip";
        private const string ContentEncodingDeflate = "deflate";

        /// <summary>
        /// This middleware adds support for clients that send their request bodies compressed
        /// </summary>
        public CompressedRequestMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// This middleware adds support for clients that send their request bodies compressed
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(ContentEncodingHeader)
                && (string.Equals(context.Request.Headers[ContentEncodingHeader].ToString(), ContentEncodingGzip, StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(context.Request.Headers[ContentEncodingHeader].ToString(), ContentEncodingDeflate, StringComparison.InvariantCultureIgnoreCase)))
            {
                var contentEncoding = context.Request.Headers[ContentEncodingHeader];
                var decompressedStream =
                    string.Equals(contentEncoding.ToString(), ContentEncodingGzip, StringComparison.InvariantCultureIgnoreCase)
                    ? (Stream)new GZipStream(context.Request.Body, CompressionMode.Decompress, true)
                    : (Stream)new DeflateStream(context.Request.Body, CompressionMode.Decompress, true);
                context.Request.Body = decompressedStream;

            }
            await next(context);
        }
    }
}
