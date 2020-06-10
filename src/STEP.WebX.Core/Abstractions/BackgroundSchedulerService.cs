using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace STEP.WebX
{
    /// <summary>
    /// Defines methods for objects that are managed by the host.
    /// </summary>
    public abstract class BackgroundSchedulerService : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _stoppingTokenSource = new CancellationTokenSource();
        private Task _executingTask = Task.CompletedTask;
        private bool _disposed = false;

        /// <summary>
        /// Gets display name of this service.
        /// </summary>
        public virtual string DisplayName { get; } = "Worker";

        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the time to delay before the first execution.
        /// </summary>
        protected virtual TimeSpan Delay { get; } = TimeSpan.Zero;

        /// <summary>
        /// Gets the interval between two executions.
        /// </summary>
        protected virtual TimeSpan Interval { get; } = TimeSpan.Zero;

        /// <summary>
        /// 
        /// </summary>
        protected BackgroundSchedulerService(ILoggerFactory loggerFactory)
        {
            DisplayName = GetType().Name + "Worker";
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region Implements Microsoft.Extensions.Hosting.IHostedService
        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = RunEndlessLoopingAsync(_stoppingTokenSource.Token);

            if (!_executingTask.IsCompleted)
                await Task.CompletedTask;
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        private async Task RunEndlessLoopingAsync(CancellationToken stoppingToken)
        {
            Thread.Sleep(0);
            await Task.Delay(Delay, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation(new EventId(0, "BEGIN"), "Background hosted service is running with the schedule.");

                try
                {
                    await ExecuteAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException && stoppingToken.IsCancellationRequested)
                        continue;

                    await HandleExceptionAsync(ex, stoppingToken);
                }
                finally
                {
                    Thread.Sleep(0);
                    await Task.Delay(Interval);
                }

                Logger.LogInformation(new EventId(1, "END"), "Background hosted service has run completely.");
            }
        }
        #endregion

        /// <summary>
        /// This method will be executed repeatly in an endless loop when the application host is ready.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// This method will be executed only if an exception occurs.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual Task HandleExceptionAsync(Exception ex, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.LogError(ex, "An exception occured in the backgroud hosted service.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_stoppingTokenSource.Token.CanBeCanceled)
                        _stoppingTokenSource.Cancel();

                    _stoppingTokenSource.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
