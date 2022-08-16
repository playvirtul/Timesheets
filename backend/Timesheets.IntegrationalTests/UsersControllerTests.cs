using AutoFixture;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
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
            var password = "Qwerty123";
            var code = fixture.Create<string>();

            var newUser = new CreateUserRequest
            {
                Email = fixture.Create<string>() + "@gmail.com",
                Password = password,
                ConfirmPassword = password
            };

            await DbContext.TelegramInvitations
                .AddAsync(new Entities.TelegramInvitation
                {
                    Id = fixture.Create<int>(),
                    UserName = fixture.Create<string>(),
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>(),
                    Role = fixture.Create<Role>(),
                    Code = code
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/users?code={code}", newUser);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task LoginUser_ShouldLoginUser()
        {
            // arrange
            var fixture = new Fixture();
            var email = fixture.Create<string>() + "@gmail.com";
            var password = "Qwerty123";
            var passwordHash = new Password(password).Hash();

            await DbContext.Users
                .AddAsync(new Entities.User
                {
                    Id = fixture.Create<int>(),
                    Email = email,
                    PasswordHash = passwordHash
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var loginInfo = new CreateLoginRequest
            {
                Email = email,
                Password = password
            };

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/users/token", loginInfo);

            // assert
            response.EnsureSuccessStatusCode();
        }
    }
}