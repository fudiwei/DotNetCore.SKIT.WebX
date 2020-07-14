using System;
using Microsoft.AspNetCore.Http;

namespace STEP.WebX
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestStaticExtensions
    {
        /// <summary>
        /// Get the client IP address for this request (supports X-Forwarded-For / X-Original-Forwarded-For).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpRequest request)
        {
            request.Headers.TryGetValue("X-Original-Forwarded-For", out string clientIp);

            if (string.IsNullOrEmpty(clientIp))
            {
                request.Headers.TryGetValue("X-Forwarded-For", out clientIp);
            }

            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }

            return clientIp ?? string.Empty;
        }

        /// <summary>
        /// Get a unique identifier to represent this request for calling chain tracing (supports X-Request-Id).
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestId(this HttpRequest request)
        {
            request.Headers.TryGetValue("X-Request-Id", out string requestId);

            if (string.IsNullOrEmpty(requestId))
            {
                requestId = request.HttpContext?.TraceIdentifier;
            }

            return requestId ?? string.Empty;
        }
    }
}
