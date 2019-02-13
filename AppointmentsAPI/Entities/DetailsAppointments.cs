using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    [Table("detailsappointments")]
    public class DetailsAppointments : BaseEntity
    {
        [Column("detailsId")]
        [Key]
        public Guid DetailsId { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [NotMapped]
        public override Guid Id { get => DetailsId; set => DetailsId = value; }
    }
}
