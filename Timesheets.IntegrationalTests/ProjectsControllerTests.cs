using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API;
using Timesheets.API.Contracts;
using Xunit;

namespace Timesheets.IntegrationalTests
{
    public class ProjectsControllerTests
    {
        private readonly HttpClient _client;

        public ProjectsControllerTests()
        {
            var application = new WebApplicationFactory<Program>();

            _client = application.CreateClient();
        }

        [Fact]
        public async Task Get_ShouldReturnProjects()
        {
            // arrange

            // act
            var response = await _client.GetAsync("api/v1/projects");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_ShouldCreateProject()
        {
            // arrange
            var fixture = new Fixture();

            var project = new NewProject
            {
                Title = fixture.Create<string>()
            };

            // act
            var response = await _client.PostAsJsonAsync("api/v1/projects", project);

            // assert
            response.EnsureSuccessStatusCode();

            var projectId = await response.Content.ReadFromJsonAsync<int>();

            Assert.NotEqual(default(int), projectId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        [InlineData(null)]
        public async Task Create_InvalidTitle_ShouldReturnBadRequest(string invalidTitle)
        {
            // arrange
            var project = new NewProject
            {
                Title = invalidTitle
            };

            // act
            var response = await _client.PostAsJsonAsync("api/v1/projects", project);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}