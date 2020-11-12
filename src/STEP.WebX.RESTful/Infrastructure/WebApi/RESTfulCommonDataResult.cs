using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace STEP.WebX.RESTful.WebApi
{
    /// <summary>
    /// Represents a common data result of RESTful Web API.
    /// </summary>
    public class RESTfulCommonDataResult : RESTfulResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ret", Order = 11)]
#if NETCOREAPP2_X
#else
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("ret")]
#endif
        public bool ReturnValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data", Order = 12, NullValueHandling = NullValueHandling.Ignore)]
#if NETCOREAPP2_X
#else
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("data")]
#endif
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RESTfulCommonDataResult()
            : this(true, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        public RESTfulCommonDataResult(bool ret)
            : this(ret, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="data"></param>
        public RESTfulCommonDataResult(bool ret, object data)
            : base()
        {
            ReturnValue = ret;
            Data = data;
        }
    }
}
