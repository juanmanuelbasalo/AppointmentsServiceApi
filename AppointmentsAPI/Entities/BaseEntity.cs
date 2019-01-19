using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    public abstract class BaseEntity 
    {
        public abstract int Id { get; set; }
    }
}
