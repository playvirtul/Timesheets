using Microsoft.AspNetCore.Mvc.Testing;
using Respawn;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Timesheets.API;
using Timesheets.DataAccess.Postgre;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;

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
            DbAdapter = DbAdapter.Postgres
        };

        private readonly string _connectionString;

        public BaseControllerTests()
        {
            var application = new WebApplicationFactory<Program>();

            var configuration = application.Server.Services.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString(nameof(TimesheetsDbContext));

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;

            Client = application.CreateClient();
        }

        protected HttpClient Client { get; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await _checkpoint.Reset(conn);
            }
        }
    }
}