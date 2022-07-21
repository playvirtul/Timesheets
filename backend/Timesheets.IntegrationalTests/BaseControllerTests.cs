using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Timesheets.API;
using Timesheets.DataAccess.Postgre;
using Xunit;
using Xunit.Abstractions;

namespace Timesheets.IntegrationalTests
{
    public abstract class BaseControllerTests : IAsyncLifetime
    {
        private static readonly Checkpoint _checkpoint = new Checkpoint()
        {
            SchemasToInclude = new[]
            {
                "public"
            },
            TablesToIgnore = new[]
            {
                new Table("__EFMigrationsHistory")
            },
            DbAdapter = DbAdapter.Postgres
        };

        private readonly string _connectionString;

        public BaseControllerTests(ITestOutputHelper outputHelper)
        {
            Application = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        configurationBuilder.AddUserSecrets<BaseControllerTests>();
                    });
                    builder.ConfigureServices((context, services) =>
                    {
                        var descriptor = services.SingleOrDefault(
                            x => x.ServiceType == typeof(DbContextOptions<TimesheetsDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<TimesheetsDbContext>(
                            options =>
                            {
                                var connection = context.Configuration.GetConnectionString(nameof(TimesheetsDbContext));
                                options.UseNpgsql(connection);
                                options.EnableSensitiveDataLogging();
                                options.EnableDetailedErrors();
                            });
                    });
                });

            var configuration = Application.Server.Services.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString(nameof(TimesheetsDbContext));

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;

            Client = Application.CreateDefaultClient(new LoggingHandler(outputHelper));
        }

        protected HttpClient Client { get; }

        protected WebApplicationFactory<Startup> Application { get; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        // тесты падают скорее всего из-за этой проблемы
        public async Task DisposeAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await _checkpoint.Reset(conn);
            }

            await Task.Delay(200);
        }
    }
}