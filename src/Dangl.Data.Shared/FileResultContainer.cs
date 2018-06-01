using System.IO;

namespace Dangl.Data.Shared
{
    /// <summary>
    /// This is a Dto class for file results
    /// </summary>
    public class FileResultContainer
    {
        /// <summary>
        /// Initializes all properties
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        public FileResultContainer(Stream stream, string fileName, string mimeType)
        {
            Stream = stream;
            FileName = fileName;
            MimeType = mimeType;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileResultContainer()
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
