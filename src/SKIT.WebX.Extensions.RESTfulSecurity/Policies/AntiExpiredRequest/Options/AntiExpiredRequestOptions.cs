using System;
using Microsoft.AspNetCore.Http;

namespace SKIT.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// Provides programmatic configuration for blocking expired request.
    /// </summary>
    public class AntiExpiredRequestOptions
    {
        internal const int DEFAULT_EXPIRATION_LIMIT = 900;

        /// <summary>
        /// Gets or sets the time (seconds) difference between the server and the client in allowable range.
        /// </summary>
        public int ExpirationLimit { get; set; } = DEFAULT_EXPIRATION_LIMIT;

        /// <summary>
        /// Gets or sets an method to decide on whether to skip validator.
        /// </summary>
        public Func<HttpContext, bool> Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal AntiExpiredRequestOptions()
        {
        }
    }
}
