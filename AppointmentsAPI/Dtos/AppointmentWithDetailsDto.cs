using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class AppointmentWithDetailsDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public InsertDetailsAppointmentDto Details { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public InsertAppointmentDto Appointment { get; set; }

        [Range(minimum:1,maximum:4, ErrorMessage = "Numero entre 1 y 4")]
        public int StatusId { get; set; }
    }
}
