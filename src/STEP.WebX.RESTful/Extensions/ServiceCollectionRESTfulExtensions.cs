using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// Settings used to configure service instances.
    /// </summary>
    public sealed class WebXRESTfulServiceSettings
    {
#if !NETCORE_2_X
        /// <summary>
        /// 
        /// </summary>
        public enum JsonSerializers
        { 
            /// <summary>
            /// Newtonsoft.Json (Json.NET)
            /// </summary>
            NewtonsoftJson = 0,

            /// <summary>
            /// System.Text.Json
            /// </summary>
            SystemTextJson = 1
        }

        /// <summary>
        /// Gets or sets which JSON serializer will to be used (default: NewtonsoftJson).
        /// </summary>
        public JsonSerializers JsonSerializer { get; set; } = JsonSerializers.NewtonsoftJson;
#endif

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="CorsOptions"/>.
        /// </summary>
        public Action<CorsOptions> SetupCorsOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="ForwardedHeadersOptions"/>.
        /// </summary>
        public Action<ForwardedHeadersOptions> SetupForwardedHeadersOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="AuthenticationOptions"/>.
        /// </summary>
        public Action<AuthenticationOptions> SetupAuthenticationOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="AuthorizationOptions"/>.
        /// </summary>
        public Action<AuthorizationOptions> SetupAuthorizationOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="MvcOptions"/>.
        /// </summary>
        public Action<MvcOptions> SetupMvcOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IHealthChecksBuilder"/>.
        /// </summary>
        public Action<IHealthChecksBuilder> ConfigureHealthChecksBuilder { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="AuthenticationBuilder"/>.
        /// </summary>
        public Action<AuthenticationBuilder> ConfigureAuthenticationBuilder { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IMvcBuilder"/>.
        /// </summary>
        public Action<IMvcBuilder> ConfigureMvcBuilder { get; set; } = (builder) => { };

        internal WebXRESTfulServiceSettings()
        { 
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    using STEP.WebX.RESTful;
    using STEP.WebX.RESTful.WebApi;
    using STEP.WebX.RESTful.Paging;

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionRESTfulExtensions
    {
        /// <summary>
        /// Adds RESTful Web API services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddWebXRESTful(this IServiceCollection services)
        {
            return AddWebXRESTful(services, (settings) => { });
        }

        /// <summary>
        /// Adds RESTful Web API services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settingsConfigure"></param>
        public static IServiceCollection AddWebXRESTful(this IServiceCollection services, Action<WebXRESTfulServiceSettings> settingsConfigure)
        {
            if (settingsConfigure == null)
                throw new ArgumentNullException(nameof(settingsConfigure));

            WebXRESTfulServiceSettings settings = new WebXRESTfulServiceSettings();
            settingsConfigure.Invoke(settings);

            // Inject IHttpContextAccessor
            {
                services.AddHttpContextAccessor();
            }

            // Inject HttpClientFactory
            {
                services.AddHttpClient();
            }

            // Inject CORS
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy();

                    settings.SetupCorsOptions.Invoke(options);
                });
            }

            // Inject ForwardedHeaders
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.All;
                    options.RequireHeaderSymmetry = false;
                    options.ForwardLimit = 5;

                    settings.SetupForwardedHeadersOptions?.Invoke(options);
                });
            }

            // Inject HealthCheck
            {
                IHealthChecksBuilder builder = services.AddHealthChecks();
                settings.ConfigureHealthChecksBuilder.Invoke(builder);
            }

            // Inject Authentication
            {
                AuthenticationBuilder builder = services.AddAuthentication(settings.SetupAuthenticationOptions);
                settings.ConfigureAuthenticationBuilder.Invoke(builder);
            }

            // Inject Authorization
            {
                services.AddAuthorization(settings.SetupAuthorizationOptions);
            }

            // Inject Mvc
            {
                IMvcBuilder builder = services
#if NETCORE_2_X

                    .AddMvc(options =>
                    {
                        options.ValidateModelState();
                        options.ModelBinderProviders.Insert(0, new PagingQueryModelBinderProvider());
                        options.ModelMetadataDetailsProviders.Insert(0, new RequiredBindingMetadataProvider());

                        settings.SetupMvcOptions.Invoke(options);
                    })
                    .AddJsonOptions(options =>
                    {
                        JsonHelper.Initialize(options.SerializerSettings);
                    });
#else
                    .AddControllers(options =>
                    {
                        options.ValidateModelState();
                        options.ModelBinderProviders.Insert(0, new PagingQueryModelBinderProvider());
                        options.ModelMetadataDetailsProviders.Insert(0, new RequiredBindingMetadataProvider());
                
                        settings.SetupMvcOptions.Invoke(options);
                    })
                    .ConfigureApiBehaviorOptions(options=> 
                    {
                        options.SuppressModelStateInvalidFilter = true;
                        options.SuppressMapClientErrors = true;
                    })
                    .AddJsonOptions(options =>
                    {
                        JsonHelper.Initialize(options.JsonSerializerOptions);
                    });

                if (settings.JsonSerializer == WebXRESTfulServiceSettings.JsonSerializers.NewtonsoftJson)
                {
                    builder.AddNewtonsoftJson(options =>
                    {
                        JsonHelper.Initialize(options.SerializerSettings);
                    });
                }   
#endif
                settings.ConfigureMvcBuilder.Invoke(builder);
            }

            return services;
        }
    }
}
