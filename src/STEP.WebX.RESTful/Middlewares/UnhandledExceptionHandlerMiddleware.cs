using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace STEP.WebX.RESTful.Middlewares
{
    using Exceptions;
    using WebApi;

    /// <summary>
    /// Defines a method to generator log string when there is exception.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    public delegate string ExceptionLogGenerator(HttpContext context, Exception ex);

    /// <summary>
    /// 
    /// </summary>
    internal class UnhandledExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ExceptionLogGenerator _logGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="logGenerator"></param>
        public UnhandledExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ExceptionLogGenerator logGenerator)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(GetType());
            _logGenerator = logGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (RESTfulException ex)
            {
                if (ex.Code >= 50000)
                {
                    if (_logger.IsEnabled(LogLevel.Error))
                        _logger.LogError(_logGenerator.Invoke(context, ex));
                }
                else
                {
                    if (_logger.IsEnabled(LogLevel.Warning))
                        _logger.LogWarning(_logGenerator.Invoke(context, ex));
                }

                await WriteResponseAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning(_logGenerator.Invoke(context, ex));

                await WriteResponseAsync(context, new BadRequest400InvalidParameterException(ex.ValidationAttribute.ErrorMessageResourceName));
            }
#if NETCOREAPP2_X || NETCOREAPP3_X
            catch (Microsoft.AspNetCore.Server.Kestrel.Core.BadHttpRequestException ex)
#else
            catch (BadHttpRequestException ex)
#endif
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning(_logGenerator.Invoke(context, ex));

                if (ex.Message != null && ex.Message.Contains("payload"))
                    await WriteResponseAsync(context, new BadRequest400PayloadTooLargeException());
                else
                    await WriteResponseAsync(context, new BadRequest400DataParsingException());
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    if (context.RequestAborted.IsCancellationRequested)
                    {
                        return;
                    }
                }

                if (_logger.IsEnabled(LogLevel.Critical))
                    _logger.LogCritical(_logGenerator.Invoke(context, ex));

                await WriteResponseAsync(context, new InternalServerError500FatalException());
            }
        }

        private async Task WriteResponseAsync(HttpContext context, RESTfulException ex)
        {
            HttpResponse response = context.Response;

            if (!response.HasStarted)
            {
                try
                {
                    RESTfulResult result = new RESTfulExceptionResult(ex);
                    response.StatusCode = result.HttpStatusCode;
                    response.ContentType = "application/json; charset=utf-8";
                    await response.WriteAsync(result.ToString(), context.RequestAborted);
                }
                catch (OperationCanceledException)
                {
                    if (!context.RequestAborted.IsCancellationRequested)
                        throw;
                }
            }
        }
    }
}

namespace STEP.WebX.RESTful
{
    using Middlewares;

    /// <summary>
    /// 
    /// </summary>
    public static class UnhandledExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Wrap exceptions, so all exceptions thrown are converted to <see cref="STEP.WebX.RESTful.WebApi.IRESTfulResult"/> automagically.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnhandledExceptionHandler(this IApplicationBuilder builder)
        {
            ExceptionLogGenerator logGenerator = (HttpContext context, Exception ex) => 
            {
                context.Request.Headers.TryGetValues(HeaderNames.Referer, out string[] referrers);
                context.Request.Headers.TryGetValues(HeaderNames.UserAgent, out string[] userAgents);
                string clientIp = context.Request.GetClientIp();
                string headers = JsonHelper.Serialize(context.Request.Headers.ToDictionary());
                string body = string.Empty;
                string fatal = JsonHelper.Serialize(new
                {
                    Type = ex.GetType().FullName,
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    HResult = ex.HResult,
                    Data = ex.Data,
                    InnerException = (ex.InnerException == null) ? null : new
                    {
                        Type = ex.InnerException.GetType().FullName,
                        Message = ex.InnerException.Message,
                        Source = ex.InnerException.Source,
                        StackTrace = ex.InnerException.StackTrace,
                        HResult = ex.InnerException.HResult,
                        Data = ex.InnerException.Data
                    }
                });

                try
                {
                    if (!HttpMethod.Get.Method.Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase) &&
                        !HttpMethod.Head.Method.Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase) &&
                        !HttpMethod.Options.Method.Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase) &&
                        !HttpMethod.Trace.Method.Equals(context.Request.Method, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!(context.Request?.ContentType?.StartsWith("multipart/") ?? false) && 
                            context.Request?.Body != null)
                        {
                            body = context.Request.ReadBodyAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        }
                    }
                }
                catch (Exception e)
                {
                    body = string.Concat("(READ BODY FAILED: ", e.Message, ")");
                }

                StringBuilder logBuilder = new StringBuilder();
                logBuilder.AppendFormat("   Base: {0} \r\n", context.Request.PathBase);
                logBuilder.AppendFormat("   Path: {0} \r\n", context.Request.Path);
                logBuilder.AppendFormat("  Query: {0} \r\n", context.Request.QueryString);
                logBuilder.AppendFormat(" Method: {0} \r\n", context.Request.Method);
                logBuilder.AppendFormat("Referer: {0} \r\n", string.Join("; ", referrers ?? new string[0]));
                logBuilder.AppendFormat("   From: {0} \r\n", clientIp);
                logBuilder.AppendFormat("  Agent: {0} \r\n", string.Join("; ", userAgents ?? new string[0]));
                logBuilder.AppendFormat("Headers: {0} \r\n", headers);
                logBuilder.AppendFormat("   Body: {0} \r\n", body);
                logBuilder.AppendFormat("  Fatal: {0} \r\n", fatal);

                return logBuilder.ToString();
            };

            return UseUnhandledExceptionHandler(builder, logGenerator);
        }

        /// <summary>
        /// Wrap exceptions, so all exceptions thrown are converted to <see cref="STEP.WebX.RESTful.WebApi.IRESTfulResult"/> automagically.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="logGenerator"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnhandledExceptionHandler(this IApplicationBuilder builder, ExceptionLogGenerator logGenerator)
        {
            if (logGenerator == null)
                throw new ArgumentNullException(nameof(logGenerator));

            return builder.UseMiddleware<UnhandledExceptionHandlerMiddleware>(logGenerator);
        }
    }
}