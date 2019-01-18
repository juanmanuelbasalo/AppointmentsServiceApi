using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
