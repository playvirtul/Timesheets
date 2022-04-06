using AutoFixture;
using Timesheets.BusinessLogic;
using Timesheets.Domain;
using Xunit;

namespace Timesheets.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Create_ShouldCreateValidProject()
        {
            //arrange
            var service = new ProjectsService();
            var fixture = new Fixture();

            var project = fixture.Create<Project>();

            //act
            var result = service.Create(project);

            //assert

        }
    }
}