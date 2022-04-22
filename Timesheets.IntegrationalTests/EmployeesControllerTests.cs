using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API;
using Timesheets.API.Contracts;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.IntegrationalTests
{
    public class EmployeesControllerTests
    {
        private HttpClient _client;

        public EmployeesControllerTests()
        {
            var application = new WebApplicationFactory<Program>();

            _client = application.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnEmployees()
        {
            // arrange

            // act
            var response = await _client.GetAsync("api/v1/employees");

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
            var response = await _client.PostAsJsonAsync("api/v1/employees", employee);

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
            var response = await _client.PostAsJsonAsync("api/v1/employees", employee);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}