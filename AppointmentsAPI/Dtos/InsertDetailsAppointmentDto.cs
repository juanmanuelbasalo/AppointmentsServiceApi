using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class InsertDetailsAppointmentDto
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MinLength(10, ErrorMessage = "Minimo 10 caracteres.")]
        public string Address { get; set; }

    }
}
