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

                var usersService = scope.ServiceProvider.GetRequiredService<IUsersService>();
                var salariesService = scope.ServiceProvider.GetRequiredService<ISalariesService>();
                var telegramUsersService = scope.ServiceProvider.GetRequiredService<ITelegramUsersService>();
                var telegramApiClient = scope.ServiceProvider.GetRequiredService<ITelegramApiClient>();

                var users = await usersService.Get();

                foreach (var user in users)
                {
                    var report = await salariesService.SalaryCalculation(user.Id, DateTime.Now.Month, DateTime.Now.Year);

                    if (report.IsFailure)
                    {
                        _logger.LogError("{error}", report.Error);
                    }

                    var telegramUser = await telegramUsersService.Get(user.TelegramUserName);

                    if (telegramUser.IsFailure)
                    {
                        _logger.LogError("{error}", telegramUser.Error);
                    }

                    var message = $"Количество рабочих часов за месяц - {report.Value.Hours}" +
                        $"\nЗарплата за рабочие часы - {report.Value.SalaryAmount}";

                    await telegramApiClient.SendTelegramMessage(telegramUser.Value.ChatId, message);
                }

                await Task.Delay(10000);
            }
        }
    }
}