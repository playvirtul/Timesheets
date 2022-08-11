using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Http;
using Timesheets.API;
using Timesheets.DataAccess.Postgre;
using Xunit;
using Xunit.Abstractions;

namespace Timesheets.IntegrationalTests
{
    public abstract class BaseControllerTests : IClassFixture<DatabaseFixture>
    {
        public BaseControllerTests(ITestOutputHelper outputHelper)
        {
            var app = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        configurationBuilder.AddUserSecrets<BaseControllerTests>();
                    });
                });

            Client = app.CreateDefaultClient(new LoggingHandler(outputHelper));
            DbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<TimesheetsDbContext>();
        }

        protected HttpClient Client { get; }

        protected TimesheetsDbContext DbContext { get; }
    }
}