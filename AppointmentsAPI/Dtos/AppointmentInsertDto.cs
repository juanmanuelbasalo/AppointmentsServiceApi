using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class AppointmentInsertDto
    {
        [Required(ErrorMessage = "Ingrese una hora.")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Ingrese una fecha.")]
        public DateTime Date { get; set; }
    }
}
