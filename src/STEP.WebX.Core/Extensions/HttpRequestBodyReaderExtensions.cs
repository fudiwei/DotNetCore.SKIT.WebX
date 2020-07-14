using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace STEP.WebX
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestBodyReaderExtensions
    {
        /// <summary>
        /// Asynchronously reads the bytes from the stream of the body for this request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBodyAsByteArrayAsync(this HttpRequest request, int bufferSize = 81920)
        {
            const string ITEM_KEY = "_STEPWEBX.body.bytes";

            if (!(request.HttpContext.Items.TryGetValue(ITEM_KEY, out object val) && val is byte[]))
            {
                val = new byte[0];

                request.EnableBuffering();

                if (request.Body.CanSeek)
                {
                    request.Body.Seek(0, SeekOrigin.Begin);
                }

                if (request.Body.CanRead)
                {
                    using (Stream stream = new MemoryStream())
                    {
                        await request.Body.CopyToAsync(stream, bufferSize, request.HttpContext.RequestAborted);
                        stream.Seek(0, SeekOrigin.Begin);

                        byte[] tmp = new byte[stream.Length];
                        await stream.ReadAsync(tmp, 0, tmp.Length, request.HttpContext.RequestAborted);

                        val = tmp;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Can not read the stream of current request' s body.");
                }

                if (request.Body.CanSeek)
                {
                    request.Body.Seek(0, SeekOrigin.Begin);
                }

                request.HttpContext.Items[ITEM_KEY] = val;
            }

            return (byte[])val;
        }

        /// <summary>
        /// Asynchronously reads the string from the stream of the body for this request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<string> ReadBodyAsStringAsync(this HttpRequest request)
        {
            return await ReadBodyAsStringAsync(request, Encoding.UTF8);
        }

        /// <summary>
        /// Asynchronously reads the string from the stream of the body for this request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> ReadBodyAsStringAsync(this HttpRequest request, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            const string ITEM_KEY = "_STEPWEBX.body.string";

            if (!(request.HttpContext.Items.TryGetValue(ITEM_KEY, out object val) && val is string))
            {
                val = encoding.GetString(await request.ReadBodyAsByteArrayAsync());

                request.HttpContext.Items[ITEM_KEY] = val;
            }

            return (string)val;
        }

        /// <summary>
        /// Asynchronously reads the JSON from the stream of the body for this request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<JToken> ReadBodyAsJsonAsync(this HttpRequest request)
        {
            return await ReadBodyAsJsonAsync(request, Encoding.UTF8);
        }

        /// <summary>
        /// Asynchronously reads the JSON from the stream of the body for this request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<JToken> ReadBodyAsJsonAsync(this HttpRequest request, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            const string ITEM_KEY = "_STEPWEBX.body.jtoken";

            if (!(request.HttpContext.Items.TryGetValue(ITEM_KEY, out object val) && val is JToken))
            {
                try
                {
                    string raw = await request.ReadBodyAsStringAsync();
                    if (string.IsNullOrEmpty(raw))
                        raw = "{}";
                    val = JToken.Parse(raw);

                    request.HttpContext.Items[ITEM_KEY] = val;
                }
                catch (JsonException)
                {
                    throw;
                }
            }

            return ((JToken)val).DeepClone();
        }
    }
}
