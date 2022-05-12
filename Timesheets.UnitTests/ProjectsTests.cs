using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class ProjectsTests
    {
        [Fact]
        public void Create_ShouldCreateValidProject()
        {
            // arrange
            var title = Guid.NewGuid().ToString();

            // act
            var (project, errors) = Project.Create(title);

            // assert
            Assert.NotNull(project);
            Assert.Empty(errors);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidTitle_ShouldReturnErrors(string title)
        {
            // act
            var (project, errors) = Project.Create(title);

            // assert
            Assert.Null(project);
            Assert.NotEmpty(errors);
        }

        [Fact]
        public void CreateWorkTime_ShouldReturnEmptyErrors()
        {
            // arrange
            var fixture = new Fixture();

            var employeeId = fixture.Create<int>();

            var projectId = fixture.Create<int>();

            var hours = new Random().Next(1, 25);

            var workTime = WorkTime.Create(employeeId, projectId, hours, DateTime.Now).Result;

            // act
            var errors = Project.CreateWorkTime(workTime, Array.Empty<WorkTime>());

            // assert
            Assert.Empty(errors);
        }

        [Fact]
        public void CreateWorkTime_ShouldReturnErrors()
        {
            // arrange
            var fixture = new Fixture();

            var employeeId = fixture.Create<int>();

            var projectId = fixture.Create<int>();

            var hours = new Random().Next(1, 25);

            var workTime = WorkTime.Create(employeeId, projectId, hours, DateTime.Now).Result;

            var workTimeInArray = WorkTime
                .Create(employeeId, projectId, WorkTime.MAX_OVERTIME_HOURS_PER_DAY, DateTime.Now).Result;

            // act
            var errors = Project.CreateWorkTime(workTime, new WorkTime[] { workTimeInArray });

            // assert
            Assert.NotEmpty(errors);
        }

        public static IEnumerable<object[]> GenerateInvalidTitle()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new string[] { " " };
                yield return new string[] { string.Empty };
                yield return new string[] { null };
                var invalidString = Enumerable.Range(0, Project.MAX_TITLE_LENGHT + 50);
                yield return new string[] { string.Join(string.Empty, invalidString) };
            }
        }
    }
}