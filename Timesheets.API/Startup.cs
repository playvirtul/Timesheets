using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using Timesheets.BusinessLogic;
using Timesheets.DataAccess.Postgre;
using Timesheets.DataAccess.Postgre.Repositories;
using Timesheets.Domain.Interfaces;

namespace Timesheets.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimesheetsDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString(nameof(TimesheetsDbContext))));

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ApiVersionOperationFilter>();
                c.DocumentFilter<ApiVersionDocumentFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheets.API", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Timesheets.API.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();

            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IWorkTimesRepository, WorkTimesRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                 options =>
                 {
                     foreach (var description in provider.ApiVersionDescriptions)
                     {
                         options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                     }
                 });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
