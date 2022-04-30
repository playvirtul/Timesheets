using AutoFixture;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Timesheets.API.Contracts;
using Xunit;

namespace Timesheets.IntegrationalTests
{
    public class ProjectsControllerTests : BaseControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnProjects()
        {
            // arrange

            // act
            var response = await Client.GetAsync("api/v1/projects");

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_ShouldCreateWorkTime()
        {
            // arrange
            var fixture = new Fixture();
            var projectId = 1;

            var workTime = new NewWorkTime
            {
                Hours = fixture.Create<int>(),
                Date = fixture.Create<DateTime>()
            };

            // act
            var response = await Client.PostAsJsonAsync($"api/v1/projects/{projectId}/workTime", workTime);

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
            var response = await Client.PostAsJsonAsync("api/v1/projects", project);

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
            var response = await Client.PostAsJsonAsync("api/v1/projects", project);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}