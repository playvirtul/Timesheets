namespace Timesheets.WorkTimeReporter
{
    public class TelegramReporterWorker : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private readonly ILogger<TelegramReporterWorker> _logger;

        public TelegramReporterWorker(
            IServiceProvider serviceProvider,
            ILogger<TelegramReporterWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}