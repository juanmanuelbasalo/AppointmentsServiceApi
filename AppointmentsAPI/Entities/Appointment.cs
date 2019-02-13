using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    [Table("appointments")]
    public class Appointment : BaseEntity
    {
        [Column("appointmentsId")]
        [Key]
        public override Guid Id { get; set; }
        [Column("time")]
        public TimeSpan Time { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("userId")]
        [ForeignKey("userId")]
        public Guid UserId { get; set; }
    }
}
