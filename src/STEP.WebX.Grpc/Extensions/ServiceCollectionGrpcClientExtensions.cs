﻿using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Grpc.Core;
using Grpc.Net.ClientFactory;

namespace STEP.WebX.Grpc
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionGrpcClientExtensions
    {
        /// <summary>
        /// Adds gRPC clients to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        public static IHttpClientBuilder AddGrpcClient<TClient, TOptions>(this IServiceCollection services)
            where TClient : ClientBase
            where TOptions : class, IGrpcClientOptions, IOptions<TOptions>, new()
        {
            return AddGrpcClient<TClient, TOptions>(services, (provider, options) => { });
        }

        /// <summary>
        /// Adds gRPC clients to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureClient"></param>
        public static IHttpClientBuilder AddGrpcClient<TClient, TOptions>(this IServiceCollection services, Action<GrpcClientFactoryOptions> configureClient)
            where TClient : ClientBase
            where TOptions : class, IGrpcClientOptions, IOptions<TOptions>, new()
        {
            return AddGrpcClient<TClient, TOptions>(services, (provider, options) => configureClient?.Invoke(options));
        }

        /// <summary>
        /// Adds gRPC clients to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureClient"></param>
        public static IHttpClientBuilder AddGrpcClient<TClient, TOptions>(this IServiceCollection services, Action<IServiceProvider, GrpcClientFactoryOptions> configureClient)
            where TClient : ClientBase
            where TOptions : class, IGrpcClientOptions, IOptions<TOptions>, new()
        {
            return services.AddGrpcClient<TClient>((provider, options) =>
            {
                TOptions gRpcClientOptions = provider.GetRequiredService<IOptions<TOptions>>().Value;

                if (gRpcClientOptions.BaseAddress == null)
                    throw new ArgumentException("The base address of gRPC client cannot be empty.");

                options.Address = gRpcClientOptions.BaseAddress;
                options.ChannelOptionsActions.Add(channel =>
                {
                    channel.LoggerFactory = provider.GetRequiredService<ILoggerFactory>();
                    channel.MaxSendMessageSize = gRpcClientOptions.MaxChannelSendMessageSize;
                    channel.MaxReceiveMessageSize = gRpcClientOptions.MaxChannelReceiveMessageSize;
                    channel.ThrowOperationCanceledOnCancellation = true;
                    channel.DisposeHttpClient = true;

                    if (!Uri.UriSchemeHttps.Equals(options.Address.Scheme, StringComparison.InvariantCultureIgnoreCase))
                        channel.Credentials = ChannelCredentials.Insecure;
                });

                configureClient?.Invoke(provider, options);
            }).ConfigurePrimaryHttpMessageHandler(provider =>
            {
                TOptions gRpcClientOptions = provider.GetRequiredService<IOptions<TOptions>>().Value;

                HttpClientHandler handler = new HttpClientHandler();

                if (gRpcClientOptions.IgnoreCertificateErrors)
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                return handler;
            }).EnableCallContextPropagation(options =>
            {
                options.SuppressContextNotFoundErrors = true;
            });
        }
    }
}
