using System;
using Microsoft.AspNetCore.Http;

namespace STEP.WebX.Extensions.RESTfulSecurity
{
    /// <summary>
    /// Provides programmatic configuration for blocking hotlinking.
    /// </summary>
    public class AntiHotlinkingOptions
    {
        /// <summary>
        /// Gets or sets an array which indicates routes, to decide on whether to skip validator (support regular expressions).
        /// </summary>
        public string[] WhiteList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal AntiHotlinkingOptions()
        {
        }
    }
}
