using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData(Position.Chief)]
        [InlineData(Position.StaffEmployee)]
        [InlineData(Position.Manager)]
        [InlineData(Position.Freelancer)]
        public void Create_ShouldCreateValidEmployee(Position position)
        {
            // arrange
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();

            // act
            var chief = Chief.Create(firstName, lastName).Result;

            var (employee, errors) = chief.Create(firstName, lastName, position);

            // assert
            Assert.NotNull(employee);
            Assert.Empty(errors);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidTitle_ShouldReturnErrors(string firstName, string lastName, Position position)
        {
            // act
            var chiefName = "test";

            var chief = Chief.Create(chiefName, chiefName).Result;

            var (employee, errors) = chief.Create(firstName, lastName, position);

            // assert
            Assert.Null(employee);
            Assert.NotEmpty(errors);
        }

        public static IEnumerable<object[]> GenerateInvalidTitle()
        {
            var randomRole = new Random();

            for (int i = 0; i < 10; i++)
            {
                yield return new object[] { " ", " ",  randomRole.Next(10, 100) };
                yield return new object[] { string.Empty, string.Empty, randomRole.Next(10, 100) };
                yield return new object[] { null, null, randomRole.Next(10, 100) };
                var invalidString = Enumerable.Range(0, 100 + 50);
                var incorrectString = string.Join(string.Empty, invalidString);
                yield return new object[] { incorrectString, incorrectString, randomRole.Next(10, 100) };
            }
        }
    }
}