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
        [ForeignKey("appointmentsId")]
        public Guid AppointmentsId { get; set; }

        [Column("statusId")]
        [ForeignKey("statusId")]
        public int StatusId { get; set; }

        [Column("detailsId")]
        [ForeignKey("detailsId")]
        public Guid DetailsId { get; set; }

        [NotMapped]
        public override Guid Id { get => AppointmentsId; set => AppointmentsId = value; }
    }
}
