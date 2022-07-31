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
            var project = Project.Create(title);

            // assert
            Assert.True(project.IsSuccess);
            Assert.False(project.IsFailure);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidTitle))]
        public void Create_InvalidTitle_ShouldReturnErrors(string title)
        {
            // act
            var project = Project.Create(title);

            // assert
            Assert.True(project.IsFailure);
            Assert.False(project.IsSuccess);
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