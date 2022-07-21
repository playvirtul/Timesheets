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
            var (employee, errors) = Employee.Create(userId, firstName, lastName, position);

            // assert
            Assert.NotNull(employee);
            Assert.Empty(errors);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidParametres))]
        public void Create_InvalidTitle_ShouldReturnErrors(int userId, string firstName, string lastName, Position position)
        {
            // act
            var (employee, errors) = Employee.Create(userId, firstName, lastName, position);

            // assert
            Assert.Null(employee);
            Assert.NotEmpty(errors);
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

            var employee = Employee.Create(userId, firstName, lastName, position).Result;

            // act
            if (employee == null)
            {
                throw new Exception("emloyee is null");
            }

            var result = employee.AddProject(projectId);

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