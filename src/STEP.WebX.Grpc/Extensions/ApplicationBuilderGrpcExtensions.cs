using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace STEP.WebX.Grpc
{
    /// <summary>
    /// Settings used to configure application instances.
    /// </summary>
    public sealed class WebXGrpcApplicationSettings
    {
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to be executed before UseEndpoints.
        /// </summary>
        public Action<IApplicationBuilder> ExecuteBeforeUseEndpoints { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        public Action<IEndpointRouteBuilder> ConfigureEndpointRouteBuilder { get; set; } = (builder) => { };

        internal WebXGrpcApplicationSettings()
        { 
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    using STEP.WebX.Grpc;

    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderGrpcExtensions
    {
        /// <summary>
        /// Adds a map of incoming requests to the specified TGrpcService type.
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseWebXGrpx(this IApplicationBuilder app)
        {
            return UseWebXGrpx(app, (settings) => { });
        }

        /// <summary>
        /// Adds a map of incoming requests to the specified TGrpcService type.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="settingsConfigure"></param>
        public static IApplicationBuilder UseWebXGrpx(this IApplicationBuilder app, Action<WebXGrpcApplicationSettings> settingsConfigure)
        {
            if (settingsConfigure == null)
                throw new ArgumentNullException(nameof(settingsConfigure));

            WebXGrpcApplicationSettings settings = new WebXGrpcApplicationSettings();
            settingsConfigure.Invoke(settings);

            // Use Routing
            {
                app = app.UseRouting();
            }

            // Use Endpoints
            {
                settings.ExecuteBeforeUseEndpoints(app);

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcServices();

                    settings.ConfigureEndpointRouteBuilder.Invoke(endpoints);
                });
            }

            return app;
        }
    }
}
