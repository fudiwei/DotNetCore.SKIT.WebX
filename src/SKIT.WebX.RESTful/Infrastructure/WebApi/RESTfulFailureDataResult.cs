using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SKIT.WebX.RESTful.WebApi
{
    /// <summary>
    /// Represents a common data with message result of RESTful Web API.
    /// </summary>
    public class RESTfulFailureDataResult : RESTfulResult
    {
        private readonly KeyValuePair<string, object>[] _exts;

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
        [JsonIgnore]
#if NETCOREAPP2_X
#else
        [System.Text.Json.Serialization.JsonIgnore]
#endif
        public string ReturnFailure { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data", Order = 12, NullValueHandling = NullValueHandling.Ignore)]
#if NETCOREAPP2_X
#else
        [System.Text.Json.Serialization.JsonPropertyNameAttribute("data")]
#endif
        public object Data
        {
            get
            {
                IDictionary<string, object> data = new Dictionary<string, object>()
                {
                    { "failure", ReturnFailure }
                };

                if (_exts != null && _exts.Any())
                {
                    for (int i = 0, len = _exts.Length; i < len; i++)
                    {
                        KeyValuePair<string, object> item = _exts[i];
                        data[item.Key] = item.Value as object;
                    }
                }

                return data;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        public RESTfulFailureDataResult(bool ret, string failure)
            : base()
        {
            ReturnValue = ret;
            ReturnFailure = failure;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="failure"></param>
        /// <param name="exts"></param>
        public RESTfulFailureDataResult(bool ret, string failure, params KeyValuePair<string, object>[] exts)
            : this(ret, failure)
        {
            if (exts != null && exts.Any(e => string.IsNullOrEmpty(e.Key)))
                throw new ArgumentException("Some key of the element is null or empty.", nameof(exts));

            _exts = exts;
        }
    }
}
