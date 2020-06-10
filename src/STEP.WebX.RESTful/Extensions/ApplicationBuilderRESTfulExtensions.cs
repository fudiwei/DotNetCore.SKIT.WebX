using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// Settings used to configure application instances.
    /// </summary>
    public sealed class WebXRESTfulApplicationSettings
    {
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="HealthCheckOptions"/>.
        /// </summary>
        public Action<HealthCheckOptions> SetupHealthCheckOptionsSetup { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="ForwardedHeadersOptions"/>.
        /// </summary>
        public Action<ForwardedHeadersOptions> SetupForwardedHeadersOptions { get; set; } = (options) => { };

#if NETCORE_2_X
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to be executed before UseMvc.
        /// </summary>
        public Action<IApplicationBuilder> ExecuteBeforeUseMvc { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IRouteBuilder"/>.
        /// </summary>
        public Action<IRouteBuilder> ConfigureRouteBuilder { get; set; } = (builder) => { };
#else
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to be executed before UseEndpoints.
        /// </summary>
        public Action<IApplicationBuilder> ExecuteBeforeUseEndpoints { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        public Action<IEndpointRouteBuilder> ConfigureEndpointRouteBuilder { get; set; } = (builder) => { };
#endif

        internal WebXRESTfulApplicationSettings()
        { 
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    using STEP.WebX.RESTful;

    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderRESTfulExtensions
    {
        /// <summary>
        /// Adds a map of incoming requests to the specified TGrpcService type.
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseWebXRESTful(this IApplicationBuilder app)
        {
            return UseWebXRESTful(app, (settings) => { });
        }

        /// <summary>
        /// Adds a map of incoming requests to the specified TGrpcService type.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="settingsConfigure"></param>
        public static IApplicationBuilder UseWebXRESTful(this IApplicationBuilder app, Action<WebXRESTfulApplicationSettings> settingsConfigure)
        {
            if (settingsConfigure == null)
                throw new ArgumentNullException(nameof(settingsConfigure));

            WebXRESTfulApplicationSettings settings = new WebXRESTfulApplicationSettings();
            settingsConfigure.Invoke(settings);

            // Use RESTful Middlewares
            {
                app.UseExceptionWrapper();
                app.UseUnsupportedMediaTypeStatusCodeConverter();
                app.UseUnmatchedRouteHandler();
            }

            // Use HealthCheck
            const string HEALTHCHECK_PATH = "/healthz";
            {
                HealthCheckOptions options = new HealthCheckOptions();
                settings.SetupHealthCheckOptionsSetup.Invoke(options);

                app = app.UseHealthChecks(HEALTHCHECK_PATH, options);
            }

            // Use ForwardedHeaders
            {
                ForwardedHeadersOptions options = new ForwardedHeadersOptions();
                settings.SetupForwardedHeadersOptions.Invoke(options);

                app = app.UseForwardedHeaders(options);
            }

#if NETCORE_2_X
            // Use Mvc & CORS
            {
                app.UseDefaultCors();

                settings.ExecuteBeforeUseMvc.Invoke(app);

                app.UseMvc(settings.ConfigureRouteBuilder);
            }
#else
            // Use Routing & CORS & Endpoints
            {
                app.UseRouting();
                app.UseDefaultCors();

                settings.ExecuteBeforeUseEndpoints.Invoke(app);

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHealthChecks(HEALTHCHECK_PATH);
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllers();

                    settings.ConfigureEndpointRouteBuilder.Invoke(endpoints);
                });
            }
#endif

            return app;
        }
    }
}
