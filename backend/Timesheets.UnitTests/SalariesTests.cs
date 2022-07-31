using AutoFixture;
using System;
using System.Collections.Generic;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class SalariesTests
    {
        [Theory]
        [InlineData(SalaryType.Monthly)]
        [InlineData(SalaryType.Hourly)]
        public void Create_ShouldCreateValidSalary(SalaryType salaryType)
        {
            // arrange
            var fixture = new Fixture();

            var employeeId = fixture.Create<int>();
            var amount = fixture.Create<decimal>();
            var bonus = fixture.Create<decimal>();

            // act
            var salary = Salary.Create(employeeId, amount, bonus, salaryType);

            // assert
            Assert.True(salary.IsSuccess);
            Assert.False(salary.IsFailure);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidParametres))]
        public void Create_ShouldCreateInValidSalary(int employeeId, decimal amount, decimal bonus, SalaryType salaryType)
        {
            // act
            var salary = Salary.Create(employeeId, amount, bonus, salaryType);

            // assert
            Assert.False(salary.IsSuccess);
            Assert.True(salary.IsFailure);
        }

        public static IEnumerable<object[]> GenerateInvalidParametres()
        {
            var random = new Random();

            var incorrectId = random.Next(-1000, 1);

            var incorrectAmount = random.Next(-1000, 1);

            var incorrectBonus = random.Next(-1000, 1);

            var incorrectRole = random.Next(10, 1000);

            yield return new object[] { incorrectId, incorrectAmount, incorrectBonus, incorrectRole };
        }
    }
}