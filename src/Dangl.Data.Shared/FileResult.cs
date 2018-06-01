using System.IO;

namespace Dangl.Data.Shared
{
    /// <summary>
    /// This is a Dto class for file results
    /// </summary>
    public class FileResult
    {
        /// <summary>
        /// Initializes all properties
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        public FileResult(Stream stream, string fileName, string mimeType)
        {
            Stream = stream;
            FileName = fileName;
            MimeType = mimeType;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileResult()
        {
        }

        /// <summary>
        /// The stream of the file
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// The name of the file
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The MIME type of the file
        /// </summary>
        public string MimeType { get; }
    }
}
