﻿using AppointmentsAPI.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AppointmentsAPI.ExtensionMethods
{
    public static class HelperMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            return service.AddDbContext<AppointmentsDbContext>((options) => 
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}