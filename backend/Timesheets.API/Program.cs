using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;

namespace Timesheets.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                        .Enrich.WithSpan()
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services);
                })
                //.ConfigureLogging(configuration =>
                //{
                //logging.AddOpenTelemetry(options =>
                //{
                //    options.AddProcessor(new SimpleExportProcessor<LogRecord>(new ConsoleExporter<LogRecord>(new ConsoleExporterOptions()
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}