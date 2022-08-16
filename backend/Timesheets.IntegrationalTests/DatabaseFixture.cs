using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System.Threading.Tasks;
using Xunit;

namespace Timesheets.IntegrationalTests
{
    public class DatabaseFixture : IAsyncLifetime
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

        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("IntegrationTestsSettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(typeof(DatabaseFixture).Assembly)
                .Build();

            ConnectionString = builder.GetConnectionString("TimesheetsDbContext");
        }

        public string ConnectionString { get; }

        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                await conn.OpenAsync();

                await _checkpoint.Reset(conn);
            }
        }
    }
}