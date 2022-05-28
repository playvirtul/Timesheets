using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Timesheets.API
{
    internal class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Parameters.Any())
            {
                return;
            }

            var version = operation.Parameters.FirstOrDefault(x => x.Name.ToLower() == "version");

            if (version != null)
            {
                operation.Parameters.Remove(version);
            }
        }
    }
}
