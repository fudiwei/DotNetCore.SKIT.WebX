using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace STEP.WebX.GrpcSample.Services.Background
{
    class CalcService : BackgroundSchedulerService
    {
        private readonly Calculator.CalculatorClient _calculatorClient;

        public override string DisplayName => "Calc";

        protected override TimeSpan Interval => TimeSpan.FromSeconds(1);
        
        public CalcService(ILoggerFactory loggerFactory, Calculator.CalculatorClient calculatorClient) 
            : base(loggerFactory)
        {
            _calculatorClient = calculatorClient;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var random = new Random();

            var sumRequest = new SumRequest()
            {
                NumA = random.Next(0, 99999),
                NumB = random.Next(0, 99999),
            };
            var sumResponse = await _calculatorClient.SumAsync(sumRequest);

            Logger.LogInformation($"Calc: {sumRequest.NumA} + {sumRequest.NumB} = {sumResponse.Value}.");
        }
    }
}
