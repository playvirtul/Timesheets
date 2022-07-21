using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Timesheets.IntegrationalTests
{
    public class WorkTimesControllerTests : BaseControllerTests
    {
        public WorkTimesControllerTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task Get_ShouldReturnWorkTimes()
        {
            // arrange

            // act
            var response = await Client.GetAsync("api/v1/workTimes");

            // assert
            response.EnsureSuccessStatusCode();
        }
    }
}