using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.ExtensionMethods
{
    public static class ServicesExtensionMethods
    {
        public static IIdentityServerBuilder AddCustomIdentityServer(this IServiceCollection service, IConfiguration configuration)
        {
            return service.AddIdentityServer()
                .AddOperationalStore(options => 
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                    };
                } )
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
                    };
                });
        }
    }
}
