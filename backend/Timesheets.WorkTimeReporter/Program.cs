using Microsoft.EntityFrameworkCore;
using Timesheets.BusinessLogic;
using Timesheets.DataAccess.Postgre;
using Timesheets.DataAccess.Postgre.Repositories;
using Timesheets.Domain.Interfaces;
using Timesheets.TelegramApiClient;
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

        services.AddScoped<ITelegramUsersService, TelegramUsersService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ISalariesService, SalariesService>();

        services.AddScoped<ITelegramUsersRepository, TelegramUsersRepository>();
        services.AddScoped<IInvitationsRepository, InvitationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IWorkTimesRepository, WorkTimesRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<ISalariesRepository, SalariesRepository>();
        services.AddScoped<IWorkTimesRepository, WorkTimesRepository>();

        services.AddScoped<ITelegramApiClient, TelegramApiClient>(x =>
        {
            var token = hostContext.Configuration.GetSection(nameof(BotConfiguration)).Get<BotConfiguration>().BotToken;

            return new TelegramApiClient(token);
        });
    })
    .Build();

await host.RunAsync();