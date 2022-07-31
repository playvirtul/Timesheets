using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class EmployeesTests
    {
        [Theory]
        [InlineData(Position.Chief)]
        [InlineData(Position.StaffEmployee)]
        [InlineData(Position.Manager)]
        [InlineData(Position.Freelancer)]
        public void Create_ShouldCreateValidEmployee(Position position)
        {
            // arrange
            var fixture = new Fixture();

            var userId = fixture.Create<int>();
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();

            // act
            var employee = Employee.Create(userId, firstName, lastName, position);

            // assert
            Assert.False(employee.IsFailure);
            Assert.True(employee.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidParametres))]
        public void Create_InvalidParameters_ShouldReturnErrors(int userId, string firstName, string lastName, Position position)
        {
            // act
            var employee = Employee.Create(userId, firstName, lastName, position);

            // assert
            Assert.True(employee.IsFailure);
            Assert.False(employee.IsSuccess);
        }

        [Fact]
        public void AddProject_ShouldReturnEmptyString()
        {
            // arrange
            var fixture = new Fixture();

            var userId = fixture.Create<int>();
            var projectId = fixture.Create<int>();
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var position = fixture.Create<Position>();

            var employee = Employee.Create(userId, firstName, lastName, position);

            // act
            var result = employee.Value.AddProject(projectId);

            // assert
            Assert.Equal(string.Empty, result);
        }

        //[Fact]
        //public void AddWorkTime_ShouldReturnEmptyErrors()
        //{
        //    // arrange
        //    var fixture = new Fixture();
        //    var firstName = fixture.Create<string>();
        //    var lastName = fixture.Create<string>();
        //    var title = fixture.Create<string>();
        //    var hours = new Random().Next(1, 25);

        //    var chief = Chief.Create(firstName, lastName).Result;
        //    var project = Project.Create(title).Result;

        //    var newWorkTime = WorkTime.Create(chief.Id, project.Id, hours, DateTime.Now).Result;

        //    // act
        //    var errors = chief.AddWorkTime(project.Id, newWorkTime);

        //    // assert
        //    Assert.Empty(errors);
        //}

        public static IEnumerable<object[]> GenerateInvalidParametres()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                yield return new object[] { random.Next(-100, 1), " ", " ",  random.Next(10, 100) };
                yield return new object[] { random.Next(-100, 1), string.Empty, string.Empty, random.Next(10, 100) };
                yield return new object[] { random.Next(-100, 1), null, null, random.Next(10, 100) };
                var invalidString = Enumerable.Range(0, Employee.MAX_FIRSTNAME_LENGTH + 50);
                var incorrectString = string.Join(string.Empty, invalidString);
                yield return new object[] { random.Next(-100, 1), incorrectString, incorrectString, random.Next(10, 100) };
            }
        }
    }
}