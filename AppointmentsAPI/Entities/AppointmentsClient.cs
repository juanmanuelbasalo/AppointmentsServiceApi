using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    [Table("appointments_client")]
    public class AppointmentsClient : BaseEntity
    {
        [Column("appointmentsId")]
        [Key]
        public override Guid Id { get; set; }
        [ForeignKey("Id")]
        public virtual Appointment Appointment { get; set; }

        [Column("statusId")]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual StatusAppointments Status { get; set; }

        [Column("detailsId")]
        public Guid DetailsId { get; set; }
        [ForeignKey("DetailsId")]
        public virtual DetailsAppointments Details { get; set; }
    }
}
