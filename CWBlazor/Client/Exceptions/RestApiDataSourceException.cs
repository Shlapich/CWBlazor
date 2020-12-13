using System;
using System.Net.Http;

namespace CWBlazor.Client.Exceptions
{
    /// <summary>
    /// The <see cref="RestApiDataSourceException"/> is thrown when server responded with error on web api call.
    /// </summary>
    public class RestApiDataSourceException : HttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiDataSourceException"/> class
        /// with it's message set to empty string.
        /// </summary>
        public RestApiDataSourceException()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiDataSourceException"/> class
        /// with it's message set to <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message text.</param>
        public RestApiDataSourceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestApiDataSourceException"/> class
        /// with it's message set to <paramref name="message"/> and specified inner exception.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="inner">Inner exception.</param>
        public RestApiDataSourceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
