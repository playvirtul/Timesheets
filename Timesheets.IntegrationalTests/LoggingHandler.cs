using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Timesheets.IntegrationalTests
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ITestOutputHelper _outputHelper;

        public LoggingHandler(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            PrintContent(request.Content);

            var response = base.Send(request, cancellationToken);

            PrintContent(response.Content);

            return response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                var requestJson = await request.Content.ReadAsStringAsync();
                PrintContent(requestJson);
            }

            var responseJson = await base.SendAsync(request, cancellationToken);
            var content = await responseJson.Content.ReadAsStringAsync();
            PrintContent(content);
            return responseJson;
        }

        private void PrintContent(HttpContent? content)
        {
            if (content == null)
            {
                return;
            }

            using var contentStream = content.ReadAsStream();

            if (contentStream.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                contentStream.CopyTo(memoryStream);

                var json = Encoding.UTF8.GetString(memoryStream.ToArray());
                PrintContent(json);
                contentStream.Seek(0, SeekOrigin.Begin);
            }
        }

        private void PrintContent(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    _outputHelper.WriteLine(JToken.Parse(json).ToString());
                }
                catch
                {
                    _outputHelper.WriteLine(json);
                }
            }
        }
    }
}