using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Timesheets.DataAccess.Postgre;
using Timesheets.Domain;
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
        public async Task Create_ShouldCreateEmployee()
        {
            // arrange
            var fixture = new Fixture();

            var employee = new NewEmployee
            {
                FirstName = fixture.Create<string>(),
                LastName = fixture.Create<string>(),
                Position = fixture.Create<Position>()
            };

            // act
            var response = await Client.PostAsJsonAsync("api/v1/employees", employee);

            // assert
            response.EnsureSuccessStatusCode();

            var employeeId = await response.Content.ReadFromJsonAsync<int>();

            Assert.NotEqual(default(int), employeeId);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("     ", "     ")]
        [InlineData(null, null)]
        public async Task Create_InvalidEmployeeName_ShouldReturnBadRequest(string invalidFirstName, string invalidLastName)
        {
            var fixture = new Fixture();

            // arrange
            var employee = new NewEmployee
            {
                FirstName = invalidFirstName,
                LastName = invalidLastName,
                Position = fixture.Create<Position>()
            };

            // act
            var response = await Client.PostAsJsonAsync("api/v1/employees", employee);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateSalary()
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

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
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
    }
}