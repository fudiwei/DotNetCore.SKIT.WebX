using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SKIT.WebX
{
    /// <summary>
    /// 
    /// </summary>
    public static class HeaderDictionaryStaticExtensions
    {
        const string SEPARATOR = ", ";

        /// <summary>
        /// Gets all the values associated with the specified key.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool TryGetValues(this IHeaderDictionary headers, string name, out string[] values)
        {
            values = null;

            if (headers.Keys.Contains(name, StringComparer.InvariantCultureIgnoreCase))
            {
                values = headers
                    .Where(h => string.Equals(name, h.Key, StringComparison.InvariantCultureIgnoreCase))
                    .Select(h => string.Join(SEPARATOR, h.Value.ToArray()))
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .ToArray();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the first value associated with the specified key.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetValue(this IHeaderDictionary headers, string name, out string value)
        {
            value = null;

            if (TryGetValues(headers, name, out string[] values))
            {
                value = values.FirstOrDefault();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts <see cref="IHeaderDictionary"/> to a <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToDictionary(this IHeaderDictionary headers)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            
            foreach (string key in headers.Keys)
            {
                if (TryGetValues(headers, key, out string[] values))
                {
                    dict.Add(key, string.Join(SEPARATOR, values));
                }
            }

            return dict;
        }
    }
}
