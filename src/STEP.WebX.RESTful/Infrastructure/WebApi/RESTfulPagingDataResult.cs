using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace STEP.WebX.RESTful.WebApi
{
    /// <summary>
    /// Represents a paging data result of RESTful Web API.
    /// </summary>
    public class RESTfulPagingDataResult : RESTfulResult
    {
        /// <summary>
        /// 
        /// </summary>
        public sealed class PagingData
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("offset", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("offset")]
#endif
            public object Offset { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("page", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("page")]
#endif
            public int? Page { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("limit", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("limit")]
#endif
            public int? Limit { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("items", Order = 4)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("items")]
#endif
            public IEnumerable<object> Items { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("total_count", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("total_count")]
#endif
            public int? TotalCount { get; set; }

            /// <summary>
            /// 
            /// </summary>
            internal PagingData()
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ret", Order = 11)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("ret")]
#endif
        public bool ReturnValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data", Order = 12)]
#if !NETCORE_2_X
        [System.Text.Json.Serialization.JsonPropertyName("data")]
#endif
        public PagingData Data { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        public RESTfulPagingDataResult(bool ret)
            : base()
        {
            ReturnValue = ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="items"></param>
        public RESTfulPagingDataResult(bool ret, IEnumerable<object> items)
            : this(ret)
        {
            Data = new PagingData()
            {
                Items = items.ToArray()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        public RESTfulPagingDataResult(bool ret, IEnumerable<object> items, int totalCount)
            : this(ret, items)
        {
            if (totalCount < 0 || totalCount < items.Count())
                throw new ArgumentOutOfRangeException(nameof(totalCount));

            Data.TotalCount = totalCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        public RESTfulPagingDataResult(bool ret, int page, int limit, IEnumerable<object> items)
            : this(ret, items)
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));
            if (limit < 0)
                throw new ArgumentOutOfRangeException(nameof(limit));

            Data.Page = page;
            Data.Limit = limit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        public RESTfulPagingDataResult(bool ret, int page, int limit, IEnumerable<object> items, int totalCount)
            : this(ret, page, limit, items)
        {
            if (totalCount < 0 || totalCount < items.Count())
                throw new ArgumentOutOfRangeException(nameof(totalCount));

            Data.TotalCount = totalCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        public RESTfulPagingDataResult(bool ret, string offset, int limit, IEnumerable<object> items)
            : this(ret, items)
        {
            if (limit < 0)
                throw new ArgumentOutOfRangeException(nameof(limit));

            Data.Offset = offset ?? string.Empty;
            Data.Limit = limit;
            Data.Items = items.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="items"></param>
        /// <param name="totalCount"></param>
        public RESTfulPagingDataResult(bool ret, string offset, int limit, IEnumerable<object> items, int totalCount)
            : this(ret, offset, limit, items)
        {
            if (totalCount < 0 || totalCount < items.Count())
                throw new ArgumentOutOfRangeException(nameof(totalCount));

            Data.TotalCount = totalCount;
        }
    }
}
