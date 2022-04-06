using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Timesheets.API;
using Xunit;

namespace Timesheets.IntegrationalTests
{
    public class ProjectsControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnProjects()
        {
            //arrange
            var application = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>();

            var testServer = new TestServer(application);

            var client = testServer.CreateClient();

            //act
            var response = await client.GetAsync("api/v1/projects");

            //assert
            response.EnsureSuccessStatusCode();
        }
    }
}