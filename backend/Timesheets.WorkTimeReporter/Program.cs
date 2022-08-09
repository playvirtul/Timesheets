using Timesheets.WorkTimeReporter;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<TelegramReporterWorker>();
    })
    .Build();

await host.RunAsync();