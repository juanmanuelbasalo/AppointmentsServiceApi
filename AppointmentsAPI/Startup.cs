using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Entities;
using AppointmentsAPI.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentsAPI.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppointmentsAPI.Dtos;
using Swashbuckle.AspNetCore.Swagger;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;

namespace AppointmentsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddCustomDbContext(Configuration);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScopedCustomServices();
            services.AddMvcContentNegotiation();
            services.AddSwaggerGen(config => config.SwaggerDoc("AppointmentV1", new Info { Title = "Appointments Api", Version = "v1" }));
            services.AddCustomApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppointmentsDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseCustomExceptionHandler(loggerFactory);
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseApiVersioning();

            app.UseSwagger();
            app.UseCustomSwaggerUI();

            AutoMapper.Mapper.Initialize((map) => 
            {
                map.CreateMap<User, UserDto>().ReverseMap();
                map.CreateMap<UserDto, UserUpdateDto>().ReverseMap();
                map.CreateMap<AppointmentDto, Appointment>().ReverseMap();
                map.CreateMap<Appointment, AppointmentInsertDto>().ReverseMap();
                map.CreateMap<AppointmentWithDetailsDto, Appointment>().ReverseMap();
                map.CreateMap<AppointmentWithDetailsDto, DetailsAppointments>().ReverseMap();
                map.CreateMap<AppointmentsClientDto, AppointmentsClient>().ReverseMap();
            });

            app.UseMvc();
        }
    }
}
