using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repositories.Core;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Repository;
using Services.Interfaces;
using Services.Model;
using Services.Service;
using Serilog;

namespace API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureScope(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            //services.AddScoped<TestConfiguration>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInformationRepository, InformationRepository>();
            services.AddScoped<IInformationService, InformationService>();
        }

        [Obsolete]
        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<UsersContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("DataBaseConnectionString")));

            //services.Configure<TestConfiguration>(Configuration.GetSection("Test"));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddLogging(build =>
            {
                build.AddConsole();
            });

            services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.ProfilePicture", Version = "v1" });
            });

            services.AddControllers();
        }
        public static void ConfigureExceptionHnadler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong {contextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal server error"
                        }.ToString());
                    }
                });
            });
        }
    }
}