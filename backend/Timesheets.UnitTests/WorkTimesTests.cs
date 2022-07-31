using AutoFixture;
using System;
using System.Collections.Generic;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class WorkTimesTests
    {
        [Fact]
        public void Create_ShouldCreateValidWorkTime()
        {
            // arrange
            var fixture = new Fixture();

            var employeeId = fixture.Create<int>();

            var projectId = fixture.Create<int>();

            var hours = new Random().Next(1, 25);

            // act
            var workTime = WorkTime.Create(employeeId, projectId, hours, DateTime.Now);

            // assert
            Assert.True(workTime.IsSuccess);
            Assert.False(workTime.IsFailure);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidParametres_ShouldReturnErrors(int employeeId, int projectId, int hours, int daysCount)
        {
            // arrange
            var date = DateTime.Now.AddDays(daysCount);

            // act
            var workTime = WorkTime.Create(employeeId, projectId, hours, date);

            // assert
            Assert.False(workTime.IsSuccess);
            Assert.True(workTime.IsFailure);
        }

        public static IEnumerable<object[]> GenerateInvalidTitle()
        {
            Random random = new Random();

            yield return new object[]
            {
                random.Next(-1000, 1),
                random.Next(-1000, 1),
                random.Next(-1000, 1),
                random.Next(1, 1000)
            };
        }
    }
}