using System;
using Microsoft.AspNetCore.Http;

namespace STEP.WebX.Extensions.RESTfulSecurity
{
    using RESTful.Exceptions;

    internal static class HttpContextTimestampValidatorExtensions
    {
        public static void ValidateRequestTimestamp(this HttpContext context, int expiresIn)
        {
            if (expiresIn < 0)
                throw new ArgumentOutOfRangeException(nameof(expiresIn));

            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            string clientTimestampStr = context.Request.Query["timestamp"];

            if (string.IsNullOrEmpty(clientTimestampStr))
                throw new RequestTimeout408LackOfTimestampException();
            if (!long.TryParse(clientTimestampStr, out long clientTimestamp))
                throw new TimeoutInvalidTimestampException();
            if (Math.Abs(clientTimestamp - now) > expiresIn)
                throw new RequestTimeout408TimeoutOrExpiredException();
        }
    }
}
