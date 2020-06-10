using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace STEP.WebX.RESTful.WebApi
{
    using Exceptions;

    /// <summary>
    /// Represents a basic result of RESTful Web API.
    /// </summary>
    public class RESTfulResult : ActionResult, IRESTfulResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonIgnore]
#endif
        public int HttpStatusCode { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("errcode", Order = 1)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("errcode")]
#endif
        public virtual int ErrorCode { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("errmsg", Order = 2, NullValueHandling = NullValueHandling.Include)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("errmsg")]
#endif
        public virtual string ErrorMessage { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        protected RESTfulResult()
            : this(StatusCodes.Status200OK, ErrorCodes.OK, "ok")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="errCode"></param>
        /// <param name="errMsg"></param>
        public RESTfulResult(int statusCode, int errCode, string errMsg)
        {
            HttpStatusCode = statusCode;
            ErrorCode = errCode;
            ErrorMessage = errMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            ExecuteResultAsync(context).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;

            if (!response.HasStarted)
            {
                try
                {
                    response.StatusCode = HttpStatusCode;
                    response.ContentType = "application/json; charset=utf-8";
                    await new JsonResult(this).ExecuteResultAsync(context);
                }
                catch (OperationCanceledException)
                {
                    if (!context.HttpContext.RequestAborted.IsCancellationRequested)
                        throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonHelper.Serialize(this);
        }
    }
}
