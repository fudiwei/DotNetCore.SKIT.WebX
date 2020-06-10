using System;
using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace STEP.WebX.Grpc
{
    /// <summary>
    /// Settings used to configure service instances.
    /// </summary>
    public sealed class WebXGrpcServiceSettings
    {
        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="AuthenticationOptions"/>.
        /// </summary>
        public Action<AuthenticationOptions> SetupAuthenticationOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="AuthorizationOptions"/>.
        /// </summary>
        public Action<AuthorizationOptions> SetupAuthorizationOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to setup the provided <see cref="GrpcServiceOptions"/>.
        /// </summary>
        public Action<GrpcServiceOptions> SetupGrpcServiceOptions { get; set; } = (options) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="AuthenticationBuilder"/>.
        /// </summary>
        public Action<AuthenticationBuilder> ConfigureAuthenticationBuilder { get; set; } = (builder) => { };

        /// <summary>
        /// Gets or sets an <see cref="System.Action"/> to configure the provided <see cref="IGrpcServerBuilder"/>.
        /// </summary>
        public Action<IGrpcServerBuilder> ConfigureGrpcServerBuilder { get; set; } = (builder) => { };

        internal WebXGrpcServiceSettings()
        {
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    using STEP.WebX.Grpc;

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionGrpcExtensions
    {
        /// <summary>
        /// Adds gRPC services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddWebXGrpc(this IServiceCollection services)
        {
            return AddWebXGrpc(services, (settings) => { });
        }

        /// <summary>
        /// Adds gRPC services to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settingsConfigure"></param>
        public static IServiceCollection AddWebXGrpc(this IServiceCollection services, Action<WebXGrpcServiceSettings> settingsConfigure)
        {
            if (settingsConfigure == null)
                throw new ArgumentNullException(nameof(settingsConfigure));

            WebXGrpcServiceSettings settings = new WebXGrpcServiceSettings();
            settingsConfigure.Invoke(settings);

            // Inject HttpClientFactory
            {
                services.AddHttpClient();
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

            // Inject Grpc
            {
                IGrpcServerBuilder builder = services.AddGrpc(options =>
                {
                    options.MaxReceiveMessageSize = Constants.MAX_GRPC_MESSAGE_SIZE;
                    options.MaxSendMessageSize = Constants.MAX_GRPC_MESSAGE_SIZE;

                    settings.SetupGrpcServiceOptions.Invoke(options);
                });
                settings.ConfigureGrpcServerBuilder.Invoke(builder);
            }

            return services;
        }
    }
}
