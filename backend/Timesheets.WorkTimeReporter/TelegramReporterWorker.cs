using Microsoft.EntityFrameworkCore;
using Timesheets.DataAccess.Postgre;
using Timesheets.Domain.Interfaces;

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

                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var employees = await dbContext.Employees
                    .Include(e => e.User)
                    .AsNoTracking()
                    .ToArrayAsync();

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}