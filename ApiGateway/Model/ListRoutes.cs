using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Model
{
    public class ListRoutes
    {
        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }
    }
}
