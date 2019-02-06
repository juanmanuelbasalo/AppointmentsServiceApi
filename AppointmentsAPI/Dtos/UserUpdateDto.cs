using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Ingrese su nombre")]
        [StringLength(15, ErrorMessage = "Nombre muy largo.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingrese una contraseña.")]
        [StringLength(maximumLength: 15, MinimumLength = 6, ErrorMessage = "Minimo 6 caracteres, maximo 15")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ingrese su email.")]
        [EmailAddress(ErrorMessage = "email invalido.")]
        [StringLength(35, ErrorMessage = "Maximo 35 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su apellido.")]
        [StringLength(15, ErrorMessage = "Apellido muy largo.")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Formato incorrecto.")]
        public string PhoneNumber { get; set; }
    }
}
