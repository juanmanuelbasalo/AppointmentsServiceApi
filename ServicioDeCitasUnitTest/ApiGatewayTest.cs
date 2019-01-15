using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApiGateway.Model;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Net.Http;

namespace Tests
{
    [TestFixture]
    public class ApiGatewayTest
    {
        private IOptions<ListRoutes> routes;
        private Router routerService;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\JB_28\source\repos\ServicioDeCitasAPI\ApiGateway\")
            .AddJsonFile("routes.json",false)
            .Build();

            routes = Options.Create(configuration.Get<ListRoutes>());
        }
        [SetUp]
        public void Setup()
        {
            routerService = new Router(routes);
        }

        [TestCase("/appointment")]
        public async Task RouteRequestAsync_OKRequest_ReturnsOKResponse(string endPoint)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Path = new PathString(endPoint);
            request.Method = "GET";

            var message = await routerService.RouteRequestAsync(request);

            Assert.IsTrue(message.IsSuccessStatusCode, "It should give success.");
        }
        [TestCase("/notexist")]
        public async Task RouteRequestAsync_BadRequest_ReturnsError(string endPoint)
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Method = "GET";
            request.Path = new PathString(endPoint);

            var message = await routerService.RouteRequestAsync(request);

            Assert.IsTrue(message.StatusCode.Equals(System.Net.HttpStatusCode.NotFound));
        }
    }
}