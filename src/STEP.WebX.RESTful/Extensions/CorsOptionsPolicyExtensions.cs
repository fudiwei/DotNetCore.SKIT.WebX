using System;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// 
    /// </summary>
    public static class CorsOptionsPolicyExtensions
    {
        /// <summary>
        /// Adds a new policy and sets it as the default.
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static CorsOptions AddDefaultPolicy(this CorsOptions opts)
        {
            Action<CorsPolicyBuilder> configurePolicy = p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            opts.AddDefaultPolicy(configurePolicy);
            opts.AddPolicy("default", configurePolicy);
            return opts;
        }
    }
}
