using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace STEP.WebX.Extensions.RESTfulSecurity
{
    using RESTful.Exceptions;

    internal static class HttpContextHotlinkingValidatorExtensions
    {
        public static void ValidateHotlinking(this HttpContext context, params string[] whitelist)
        {
            if (whitelist != null && whitelist.Any())
            {
                string referrer = null;
                if (context.Request.Headers.TryGetValue(HeaderNames.Referer, out referrer) ||
                    context.Request.Headers.TryGetValue("Referrer", out referrer))
                {
                    if (whitelist.Any(e => new Regex(e, RegexOptions.IgnoreCase | RegexOptions.Compiled).IsMatch(referrer)))
                    {
                        return;
                    }
                }
            }

            throw new Unauthorized401HotlinkingException();
        }
    }
}
