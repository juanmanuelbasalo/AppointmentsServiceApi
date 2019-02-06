using AppointmentsAPI.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AppointmentsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace AppointmentsAPI.ExtensionMethods
{
    public static class HelperMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            return service.AddDbContext<AppointmentsDbContext>((options) => 
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });
        }
        public static IServiceCollection AddScopedCustomServices(this IServiceCollection service)
        {
            return service.AddScoped<IUserService, UserService>();
        }
        public static IMvcBuilder AddMvcContentNegotiation(this IServiceCollection mvcBuilder)
        {
            return mvcBuilder.AddMvc(options => 
            {
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder, ILoggerFactory loggerFactory)
        {
            return builder.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(errorFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                    }
                    await context.Response.WriteAsync("There was an error");
                });
            });
        }
        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder builder)
        {
            return builder.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/AppointmentV1/swagger.json", "Appointments Api"));
        }
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1,0);
            });
        }
    }
}
