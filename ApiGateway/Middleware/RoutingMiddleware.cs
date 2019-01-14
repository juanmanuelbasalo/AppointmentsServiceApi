using ApiGateway.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Middleware
{
    public class RoutingMiddleware
    {
        private readonly RequestDelegate next;
        readonly Router router;
        public RoutingMiddleware(RequestDelegate next, IOptions<ListRoutes> env)
        {
            this.next = next;
            router = new Router(env);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var content = await router.RouteRequestAsync(context.Request);
            await context.Response.WriteAsync(await content.Content.ReadAsStringAsync());
        }
    }
}
