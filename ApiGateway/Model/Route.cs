using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Model
{
    public class Route
    {
        public string Endpoint { get; set; }
        public Destination Destination { get; set; }
    }
}
