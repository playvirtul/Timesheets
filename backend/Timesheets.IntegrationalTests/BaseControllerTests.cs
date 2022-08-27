using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Timesheets.API;
using Timesheets.DataAccess.Postgre;
using Timesheets.Domain.Auth;
using Xunit;
using Xunit.Abstractions;

namespace Timesheets.IntegrationalTests
{
    [Collection("Database")]
    public abstract class BaseControllerTests
    {
        public BaseControllerTests(ITestOutputHelper outputHelper)
        {
            var app = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");

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

        protected async Task SignInChief(int userId = 1)
        {
            await Task.Run(() =>
            {
                var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.KEY)
                      .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.NameIdentifier, userId)
                      .AddClaim(ClaimTypes.Role, Role.Chief)
                      .WithVerifySignature(true)
                      .Encode();

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    JwtBearerDefaults.AuthenticationScheme,
                    token);
            });
        }
    }

    [CollectionDefinition("Database")]
    public class DatabaseCollection : IClassFixture<DatabaseFixture>
    {
    }
}