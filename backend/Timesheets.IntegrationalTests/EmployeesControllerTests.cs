using AutoFixture;
using System;
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
    public class EmployeesControllerTests : BaseControllerTests
    {
        public EmployeesControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public async Task Get_ShouldReturnEmployees()
        {
            // arrange
            await SignInChief();

            // act
            var response = await Client.GetAsync("api/v1/employees");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SendTelegramInvite_ShouldSendTelegramInvite()
        {
            // arrange
            await SignInChief();

            var fixture = new Fixture();
            var userName = "dsfsdfsdfsaf";

            await DbContext.TelegramUsers.AddAsync(new Entities.TelegramUser
            {
                UserName = userName,
                ChatId = 312433636
            });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var invitationRequest = new CreateInvitationRequest
            {
                UserName = userName,
                FirstName = fixture.Create<string>(),
                LastName = fixture.Create<string>(),
                Position = fixture.Create<Position>(),
                Role = fixture.Create<Role>()
            };

            // act
            var response = await Client
                .PostAsJsonAsync("api/v1/employees/telegramInvitation", invitationRequest);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Save_ShouldCreateSalary()
        {
            // arrange
            var fixture = new Fixture();

            var salary = new CreateSalaryRequest
            {
                Amount = fixture.Create<decimal>(),
                Bonus = fixture.Create<decimal>(),
                SalaryType = fixture.Create<SalaryType>()
            };

            var user = await DbContext.Users
                .AddAsync(new Entities.User
                {
                    Id = fixture.Create<int>(),
                    Email = fixture.Create<string>() + "@gmail.com",
                    PasswordHash = fixture.Create<string>(),
                    Role = fixture.Create<Role>()
                });

            await SignInChief(user.Entity.Id);

            var employee = await DbContext.Employees
                .AddAsync(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var employeeId = employee.Entity.Id;

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/employees/{employeeId}/salary", salary);

            // assert
            response.EnsureSuccessStatusCode();

            var salaryId = await response.Content.ReadFromJsonAsync<int>();

            Assert.NotEqual(default(int), salaryId);
        }

        [Fact]
        public async Task Save_ShouldUpdateSalary()
        {
            // arrange
            var fixture = new Fixture();

            var salary = new CreateSalaryRequest
            {
                Amount = fixture.Create<decimal>(),
                Bonus = fixture.Create<decimal>(),
                SalaryType = fixture.Create<SalaryType>()
            };

            var user = await DbContext.Users
               .AddAsync(new Entities.User
               {
                   Id = fixture.Create<int>(),
                   Email = fixture.Create<string>() + "@gmail.com",
                   PasswordHash = fixture.Create<string>(),
                   Role = fixture.Create<Role>()
               });

            await SignInChief(user.Entity.Id);

            var employee = await DbContext.Employees
                .AddAsync(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.Salaries.AddAsync(new Entities.Salary
            {
                Amount = fixture.Create<decimal>(),
                Bonus = 0,
                SalaryType = fixture.Create<SalaryType>(),
                Employee = employee.Entity
            });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var employeeId = employee.Entity.Id;

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/employees/{employeeId}/salary", salary);

            // assert
            response.EnsureSuccessStatusCode();

            var salaryId = await response.Content.ReadFromJsonAsync<int>();

            Assert.NotEqual(default(int), salaryId);
        }

        [Fact]
        public async Task SalaryCalculation_ShouldReturnAmount()
        {
            // arrange
            var fixture = new Fixture();
            var random = new Random();

            var month = random.Next(1, 13);
            var year = DateTime.Now.Year;

            var user = await DbContext.Users
               .AddAsync(new Entities.User
               {
                   Id = fixture.Create<int>(),
                   Email = fixture.Create<string>() + "@gmail.com",
                   PasswordHash = fixture.Create<string>(),
                   Role = fixture.Create<Role>()
               });

            await SignInChief(user.Entity.Id);

            var employee = await DbContext.Employees
                .AddAsync(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.Salaries.AddAsync(new Entities.Salary
            {
                Amount = fixture.Create<decimal>(),
                Bonus = 0,
                SalaryType = fixture.Create<SalaryType>(),
                Employee = employee.Entity
            });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var employeeId = employee.Entity.Id;

            var url = $"api/v1/employees/{employeeId}/salary-calculation?month={month}&year={year}";

            // act
            var response = await Client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task BindProject_ValidEmployeeIdAndProjectId_ShouldBindProjectWithEmployee()
        {
            var fixture = new Fixture();

            // arrange
            var project = await DbContext.Projects
                .AddAsync(new Entities.Project { Title = fixture.Create<string>() });

            var user = await DbContext.Users
                .AddAsync(new Entities.User
                {
                    Id = fixture.Create<int>(),
                    Email = fixture.Create<string>() + "@gmail.com",
                    PasswordHash = fixture.Create<string>(),
                    Role = fixture.Create<Role>()
                });

            await SignInChief(user.Entity.Id);

            var employee = await DbContext.Employees
                .AddAsync(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var employeeId = employee.Entity.Id;
            var projectId = project.Entity.Id;

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/employees/{employeeId}/project", projectId);

            // assert
            response.EnsureSuccessStatusCode();
        }
    }
}