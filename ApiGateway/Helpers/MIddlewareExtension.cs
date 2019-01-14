using ApiGateway.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Helpers
{
    public static class MIddlewareExtension
    {
        public static IApplicationBuilder RoutinMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoutingMiddleware>();
        }
    }
}
