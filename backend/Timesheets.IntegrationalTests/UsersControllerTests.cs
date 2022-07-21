﻿using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.DataAccess.Postgre;
using Timesheets.Domain;
using Timesheets.Domain.Auth;
using Xunit;
using Xunit.Abstractions;
using Entities = Timesheets.DataAccess.Postgre.Entities;

namespace Timesheets.IntegrationalTests
{
    public class UsersControllerTests : BaseControllerTests
    {
        public UsersControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUserAndEmployee()
        {
            // arrange
            var fixture = new Fixture();
            var password = fixture.Create<string>();
            var code = fixture.Create<string>();

            var newUser = new NewUser
            {
                Email = fixture.Create<string>() + "@gmail.com",
                Password = password,
                ConfirmPassword = password,
                Role = fixture.Create<Role>()
            };

            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                dbContext.Invitations
                    .Add(new Entities.Invitation
                    {
                        Id = fixture.Create<int>(),
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>(),
                        Code = code
                    });

                await dbContext.SaveChangesAsync();
            }

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/users?code={code}", newUser);

            // assert
            response.EnsureSuccessStatusCode();
        }
    }
}