using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System;
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
    public class ProjectsControllerTests : BaseControllerTests
    {
        public ProjectsControllerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

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
            var random = new Random();
            var projectId = 0;
            var employeeId = 0;

            using (var scope = Application.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TimesheetsDbContext>();

                var project = dbContext.Projects
                    .Add(new Entities.Project { Title = fixture.Create<string>() });

                var employee = dbContext.Employees
                    .Add(new Entities.Employee
                    {
                        FirstName = fixture.Create<string>(),
                        LastName = fixture.Create<string>(),
                        Position = fixture.Create<Position>()
                    });

                await dbContext.SaveChangesAsync();

                projectId = project.Entity.Id;
                employeeId = employee.Entity.Id;
            }

            var workTime = new NewWorkTime
            {
                Hours = random.Next(1, 25),
                Date = DateTime.Now.AddDays(random.Next(-7, 0))
            };

            var url = $"api/v1/projects/{projectId}/workTime?employeeId={employeeId}";

            // act
            var response = await Client.PostAsJsonAsync(url, workTime);

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