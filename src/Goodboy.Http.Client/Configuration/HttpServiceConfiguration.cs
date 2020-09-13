using System;

namespace Goodboy.Http.Client.Configuration
{
    public class HttpServiceConfiguration
    {
        /// <summary>
        /// Gets or Sets external service base url.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets external http service requests' timeout.
        /// </summary>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(60);
    }
}