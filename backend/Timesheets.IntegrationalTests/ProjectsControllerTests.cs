using AutoFixture;
using System;
using System.Net;
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

            var project = DbContext.Projects
                .Add(new Entities.Project { Title = fixture.Create<string>() });

            var user = DbContext.Users
               .Add(new Entities.User
               {
                   Id = fixture.Create<int>(),
                   Email = fixture.Create<string>() + "@gmail.com",
                   PasswordHash = fixture.Create<string>(),
                   Role = fixture.Create<Role>()
               });

            var employee = DbContext.Employees
                .Add(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            employee.Entity.Projects.Add(project.Entity);

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var projectId = project.Entity.Id;
            var employeeId = employee.Entity.Id;

            var workTime = new CreateWorkTimeRequest
            {
                Hours = random.Next(WorkTime.MIN_WORKING_HOURS_PER_DAY, WorkTime.MAX_OVERTIME_HOURS_PER_DAY + 1),
                Date = DateTime.Now.AddDays(random.Next(-7, 0)),
                EmployeeId = employeeId
            };

            var url = $"api/v1/projects/{projectId}/workTime";

            // act
            var response = await Client.PostAsJsonAsync(url, workTime);

            // assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_InvalidHours_ShouldReturnBadRequest()
        {
            // arrange
            var fixture = new Fixture();
            var random = new Random();

            var project = DbContext.Projects
                .Add(new Entities.Project { Title = fixture.Create<string>() });

            var user = DbContext.Users
               .Add(new Entities.User
               {
                   Id = fixture.Create<int>(),
                   Email = fixture.Create<string>() + "@gmail.com",
                   PasswordHash = fixture.Create<string>(),
                   Role = fixture.Create<Role>()
               });

            var employee = DbContext.Employees
                .Add(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var projectId = project.Entity.Id;
            var employeeId = employee.Entity.Id;

            var workTime = new CreateWorkTimeRequest
            {
                Hours = random.Next(WorkTime.MAX_OVERTIME_HOURS_PER_DAY + 1, 1000),
                Date = DateTime.Now.AddDays(random.Next(-7, 0)),
                EmployeeId = employeeId
            };

            var url = $"api/v1/projects/{projectId}/workTime";

            // act
            var response = await Client.PostAsJsonAsync(url, workTime);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidDate_ShouldReturnBadRequest()
        {
            // arrange
            var fixture = new Fixture();
            var random = new Random();

            var project = DbContext.Projects
                .Add(new Entities.Project { Title = fixture.Create<string>() });

            var user = DbContext.Users
               .Add(new Entities.User
               {
                   Id = fixture.Create<int>(),
                   Email = fixture.Create<string>() + "@gmail.com",
                   PasswordHash = fixture.Create<string>(),
                   Role = fixture.Create<Role>()
               });

            var employee = DbContext.Employees
                .Add(new Entities.Employee
                {
                    Id = user.Entity.Id,
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                    Position = fixture.Create<Position>()
                });

            await DbContext.SaveChangesAsync();
            DbContext.ChangeTracker.Clear();

            var projectId = project.Entity.Id;
            var employeeId = employee.Entity.Id;

            var workTime = new CreateWorkTimeRequest
            {
                Hours = random.Next(WorkTime.MIN_WORKING_HOURS_PER_DAY, WorkTime.MAX_OVERTIME_HOURS_PER_DAY + 1),
                Date = DateTime.Now.AddDays(1),
                EmployeeId = employeeId
            };

            var url = $"api/v1/projects/{projectId}/workTime";

            // act
            var response = await Client.PostAsJsonAsync(url, workTime);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateProject()
        {
            // arrange
            var fixture = new Fixture();

            var project = new CreateProjectRequest
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
            var project = new CreateProjectRequest
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