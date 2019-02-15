using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class AppointmentsClientDto
    {
        public StatusAppointmentDto Status { get; set; }
        public DetailsAppointmentDto Details { get; set; }
        public AppointmentDto Appointment { get; set; }
    }
}
