using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System;
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

            // act
            var response = await Client.GetAsync("api/v1/employees");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task SendTelegramInvite_ShouldSendTelegramInvite()
        {
            // arrange
            var fixture = new Fixture();

            var telegramEmployeeDetails = new TelegramEmlpoyeeDetails
            {
                TelegramUserName = "testUser",
                FirstName = fixture.Create<string>(),
                LastName = fixture.Create<string>(),
                Position = fixture.Create<Position>()
            };

            // act
            var responce = await Client
                .PostAsJsonAsync("api/v1/employees/telegramInvitation/employeeDetails", telegramEmployeeDetails);

            // assert
            responce.EnsureSuccessStatusCode();
        }

        //[Fact]
        //public async Task Create_ShouldCreateEmployee()
        //{
        //    // arrange
        //    var fixture = new Fixture();

        //    var employee = new TelegramEmlpoyeeDetails
        //    {
        //        FirstName = fixture.Create<string>(),
        //        LastName = fixture.Create<string>(),
        //        Position = fixture.Create<Position>()
        //    };

        //    // act
        //    var response = await Client.PostAsJsonAsync("api/v1/employees", employee);

        //    // assert
        //    response.EnsureSuccessStatusCode();

        //    var employeeId = await response.Content.ReadFromJsonAsync<int>();

        //    Assert.NotEqual(default(int), employeeId);
        //}

        //[Theory]
        //[InlineData("", "")]
        //[InlineData(" ", " ")]
        //[InlineData("     ", "     ")]
        //[InlineData(null, null)]
        //public async Task Create_InvalidEmployeeName_ShouldReturnBadRequest(string invalidFirstName, string invalidLastName)
        //{
        //    var fixture = new Fixture();

        //    // arrange
        //    var employee = new TelegramEmlpoyeeDetails
        //    {
        //        FirstName = invalidFirstName,
        //        LastName = invalidLastName,
        //        Position = fixture.Create<Position>()
        //    };

        //    // act
        //    var response = await Client.PostAsJsonAsync("api/v1/employees", employee);

        //    // assert
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        [Fact]
        public async Task Save_ShouldCreateSalary()
        {
            // arrange
            var fixture = new Fixture();

            var salary = new NewSalary
            {
                Amount = fixture.Create<decimal>(),
                Bonus = fixture.Create<decimal>(),
                SalaryType = fixture.Create<SalaryType>()
            };

            var employeeId = 0;

            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var user = dbContext.Users
                    .Add(new Entities.User
                    {
                        Id = fixture.Create<int>(),
                        Email = fixture.Create<string>() + "@gmail.com",
                        PasswordHash = fixture.Create<string>(),
                        Role = fixture.Create<Role>()
                    });

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
                        Id = user.Entity.Id,
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>()
                    });

                await dbContext.SaveChangesAsync();

                employeeId = employee.Entity.Id;
            }

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

            var salary = new NewSalary
            {
                Amount = fixture.Create<decimal>(),
                Bonus = fixture.Create<decimal>(),
                SalaryType = fixture.Create<SalaryType>()
            };

            var employeeId = 0;

            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var user = dbContext.Users
                   .Add(new Entities.User
                   {
                       Id = fixture.Create<int>(),
                       Email = fixture.Create<string>() + "@gmail.com",
                       PasswordHash = fixture.Create<string>(),
                       Role = fixture.Create<Role>()
                   });

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
                        Id = user.Entity.Id,
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>()
                    });

                dbContext.Salaries.Add(new Entities.Salary
                {
                    Amount = fixture.Create<decimal>(),
                    Bonus = 0,
                    SalaryType = fixture.Create<SalaryType>(),
                    Employee = employee.Entity
                });

                await dbContext.SaveChangesAsync();

                employeeId = employee.Entity.Id;
            }

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

            var employeeId = 0;
            var month = random.Next(1, 13);
            var year = DateTime.Now.Year;

            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var user = dbContext.Users
                   .Add(new Entities.User
                   {
                       Id = fixture.Create<int>(),
                       Email = fixture.Create<string>() + "@gmail.com",
                       PasswordHash = fixture.Create<string>(),
                       Role = fixture.Create<Role>()
                   });

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
                        Id = user.Entity.Id,
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>()
                    });

                dbContext.Salaries.Add(new Entities.Salary
                {
                    Amount = fixture.Create<decimal>(),
                    Bonus = 0,
                    SalaryType = fixture.Create<SalaryType>(),
                    Employee = employee.Entity
                });

                await dbContext.SaveChangesAsync();

                employeeId = employee.Entity.Id;
            }

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
            var employeeId = 0;
            var projectId = 0;

            // arrange
            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var project = dbContext.Projects
                    .Add(new Entities.Project { Title = fixture.Create<string>() });

                var user = dbContext.Users
                    .Add(new Entities.User
                    {
                        Id = fixture.Create<int>(),
                        Email = fixture.Create<string>() + "@gmail.com",
                        PasswordHash = fixture.Create<string>(),
                        Role = fixture.Create<Role>()
                    });

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
                        Id = user.Entity.Id,
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>()
                    });

                await dbContext.SaveChangesAsync();

                employeeId = employee.Entity.Id;
                projectId = project.Entity.Id;
            }

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/employees/{employeeId}/project", projectId);

            // assert
            response.EnsureSuccessStatusCode();
        }
    }
}