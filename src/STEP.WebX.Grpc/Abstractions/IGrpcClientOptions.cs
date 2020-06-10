using System;
using Microsoft.Extensions.Options;

namespace STEP.WebX.Grpc
{
    /// <summary>
    /// An options interface for configuring a gRPC client.
    /// The class must impletement <see cref="IOptions{TOptions}"/> which impletements this also.
    /// </summary>
    public interface IGrpcClientOptions
    {
        /// <summary>
        /// Gets or sets the address to use when making gRPC calls.
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to ignore or not to ignore all the certificate errors. 
        /// REF: https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-a-grpc-service-with-an-untrustedinvalid-certificate
        /// </summary>
        bool IgnoreCertificateErrors { get; set; }

        /// <summary>
        /// Gets or sets the maximum message size in bytes that can be sent from the client..
        /// </summary>
        int? MaxChannelSendMessageSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum message size in bytes that can be sent from the client..
        /// </summary>
        int? MaxChannelReceiveMessageSize { get; set; }
    }
}
