using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace STEP.WebX.Sample.Services.Background
{
    class ClockingService : BackgroundSchedulerService
    {
        public override string DisplayName => "Clocking";

        protected override TimeSpan Interval => TimeSpan.FromMinutes(1);
        
        public ClockingService(ILoggerFactory loggerFactory) 
            : base(loggerFactory)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.LogInformation("Now is: " + DateTimeOffset.Now);
            await Task.CompletedTask;
        }
    }
}
