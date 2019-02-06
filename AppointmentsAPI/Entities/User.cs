using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("userId")]
        [Key]
        public override Guid Id{ get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("lastName")]
        public string LastName { get; set; }
        [Column("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
