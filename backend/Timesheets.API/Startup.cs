using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.IO;
using Telegram.Bot;
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
            const string serviceName = "Timesheets.API";

            services.AddDbContext<TimesheetsDbContext>(
                options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString(nameof(TimesheetsDbContext)));
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                    };
                });

            services.AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<DataAccessMappingProfile>();
                    cfg.AddProfile<ApiMappingProfile>();
                });

            services.AddOpenTelemetryTracing((builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
                .AddJaegerExporter(options => options.AgentHost = "jaeger")
                .AddSource(serviceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName)));

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ApiVersionOperationFilter>();
                c.DocumentFilter<ApiVersionDocumentFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = serviceName, Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Timesheets.API.xml");
                c.IncludeXmlComments(filePath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });
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

            var botConfig = Configuration.GetSection(nameof(BotConfiguration)).Get<BotConfiguration>();

            services.AddHttpClient(nameof(TelegramBotClient))
                .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(botConfig.BotToken, httpClient));

            services.AddScoped<ITelegramApiClient, TelegramApiClient.TelegramApiClient>(x =>
            {
                var token = Configuration.GetSection(nameof(BotConfiguration)).Get<BotConfiguration>().BotToken;

                return new TelegramApiClient.TelegramApiClient(token);
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<ITelegramUsersService, TelegramUsersService>();
            services.AddScoped<IInvitationsService, InvitationsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<ISalariesService, SalariesService>();
            services.AddScoped<IWorkTimesService, WorkTimesService>();

            services.AddScoped<ITelegramUsersRepository, TelegramUsersRepository>();
            services.AddScoped<IInvitationsRepository, InvitationsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IWorkTimesRepository, WorkTimesRepository>();
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<ISalariesRepository, SalariesRepository>();
            services.AddScoped<IWorkTimesRepository, WorkTimesRepository>();
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

            app.UseCors(x =>
            {
                x.WithHeaders().AllowAnyHeader();
                x.WithOrigins("http://localhost:3000");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}