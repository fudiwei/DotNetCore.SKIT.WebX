using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using STEP.WebX.Grpc;

namespace STEP.WebX.GrpcSample.Options
{
    public class CalculatorGrpcClientOptions : IOptions<CalculatorGrpcClientOptions>, IGrpcClientOptions
    {
        public CalculatorGrpcClientOptions Value => this;

        public Uri BaseAddress { get; set; } = new Uri("https://localhost:5001");

        public bool IgnoreCertificateErrors { get; set; }

        public int? MaxChannelSendMessageSize { get; set; }

        public int? MaxChannelReceiveMessageSize { get; set; }
    }
}
