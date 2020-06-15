using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// Provides programmatic configuration for the RESTful error handlers.
    /// </summary>
    public sealed class RESTfulErrorOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether using <see cref="Middlewares.UnhandledExceptionHandlerMiddleware"/>.
        /// The default is 'true'.
        /// </summary>
        public bool UnhandledExceptionHandlerEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a function to generate log message when an exception is thrown.
        /// </summary>
        public Middlewares.ExceptionLogGenerator UnhandledExceptionLogGenerator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether using <see cref="Middlewares.UnsupportedMediaTypeHandleriddleware"/>.
        /// The default is 'true'.
        /// </summary>
        public bool UnsupportedMediaTypeHandlerEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether using <see cref="Middlewares.UnmatchedRouteHandlerMiddleware"/>.
        /// The default is 'true'.
        /// </summary>
        public bool UnmatchedRouteHandlerEnabled { get; set; } = true;
    }

    /// <summary>
    /// Settings used to configure application instances.
    /// </summary>
    public sealed class WebXRESTfulApplicationSettings
    {
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="RESTfulErrorOptions"/>.
        /// </summary>
        public Action<RESTfulErrorOptions> SetupRESTfulErrorOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="HealthCheckOptions"/>.
        /// </summary>
        public Action<HealthCheckOptions> SetupHealthCheckOptions { get; set; } = (options) => { };

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
                RESTfulErrorOptions options = new RESTfulErrorOptions();
                settings.SetupRESTfulErrorOptions.Invoke(options);

                if (options.UnhandledExceptionHandlerEnabled)
                {
                    if (options.UnhandledExceptionLogGenerator == null)
                        app.UseUnhandledExceptionHandler();
                    else
                        app.UseUnhandledExceptionHandler(options.UnhandledExceptionLogGenerator);
                }

                if (options.UnsupportedMediaTypeHandlerEnabled)
                {
                    app.UseUnsupportedMediaTypeHandler();
                }

                if (options.UnmatchedRouteHandlerEnabled)
                {
                    app.UseUnmatchedRouteHandler();
                }
            }

            // Use HealthCheck
            const string HEALTHCHECK_PATH = "/healthz";
            {
                HealthCheckOptions options = new HealthCheckOptions();
                settings.SetupHealthCheckOptions.Invoke(options);

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
