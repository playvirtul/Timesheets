using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData("chief")]
        [InlineData("manager")]
        [InlineData("stuffemployee")]
        [InlineData("freelancer")]
        public void Create_ShouldCreateValidEmployee(string position)
        {
            // arrange
            var firstName = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();

            // act
            var (employee, errors) = Employee.Create(firstName, lastName, position);

            // assert
            Assert.NotNull(employee);
            Assert.Empty(errors);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidTitle_ShouldReturnErrors(string firstName, string lastName, string position)
        {
            // act
            var (employee, errors) = Employee.Create(firstName, lastName, position);

            // assert
            Assert.Null(employee);
            Assert.NotEmpty(errors);
        }

        public static IEnumerable<object[]> GenerateInvalidTitle()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return new string[] { " ", " ", " " };
                yield return new string[] { string.Empty, string.Empty, string.Empty };
                yield return new string[] { null, null, null };
                var invalidString = Enumerable.Range(0, 100 + 50);
                var incorrectString = string.Join(string.Empty, invalidString);
                yield return new string[] { incorrectString, incorrectString, incorrectString };
            }
        }
    }
}