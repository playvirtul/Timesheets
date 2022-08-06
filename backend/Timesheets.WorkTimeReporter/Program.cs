using Microsoft.EntityFrameworkCore;
using Timesheets.DataAccess.Postgre;
using Timesheets.WorkTimeReporter;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TelegramReporterWorker>();

        services.AddDbContext<TimesheetsDbContext>(
                options =>
                {
                    options.UseNpgsql(hostContext.Configuration.GetConnectionString(nameof(TimesheetsDbContext)));
                });

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<DataAccessMappingProfile>();
        });
    })
    .Build();

await host.RunAsync();