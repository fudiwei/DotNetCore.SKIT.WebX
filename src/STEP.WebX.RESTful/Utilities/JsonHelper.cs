using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace STEP.WebX.RESTful
{
    internal static class JsonHelper
    {
        public static JsonSerializerSettings DefaultNewtonsoftJsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.None,
                    ContractResolver = new DefaultContractResolver()
                };
            }
        }

        public static void Initialize(JsonSerializerSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            JsonSerializerSettings defaultSettings = DefaultNewtonsoftJsonSerializerSettings;
            foreach (PropertyInfo property in settings.GetType().GetRuntimeProperties().Where(e => e.CanRead && e.CanWrite))
            {
                property.SetValue(settings, property.GetValue(defaultSettings));
            }
        }

#if NETCOREAPP2_X
#else
        public static System.Text.Json.JsonSerializerOptions DefaultSystemTextJsonSerializerSettings
        {
            get
            {
                System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    IgnoreNullValues = true,
                    PropertyNamingPolicy = null,
                    PropertyNameCaseInsensitive = true
                };
                return options;
            }
        }

        public static void Initialize(System.Text.Json.JsonSerializerOptions settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
        
            System.Text.Json.JsonSerializerOptions defaultSettings = DefaultSystemTextJsonSerializerSettings;
            foreach (PropertyInfo property in settings.GetType().GetRuntimeProperties().Where(e => e.CanRead && e.CanWrite))
            {
                property.SetValue(settings, property.GetValue(defaultSettings));
            }
        }
#endif

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, DefaultNewtonsoftJsonSerializerSettings);

            // TODO: There are so many bugs in System.Text.Json!
            //#if NETCOREAPP2_X
            //            return JsonConvert.SerializeObject(obj, DefaultNewtonsoftJsonSerializerSettings);
            //#else
            //            return System.Text.Json.JsonSerializer.Serialize(obj, DefaultSystemTextJsonSerializerSettings);
            //#endif
        }
    }
}
