using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Entities
{
    [Table("statusappointments")]
    public class StatusAppointments 
    {
        [Column("statusId")]
        [Key]
        public int StatusId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public virtual ICollection<AppointmentsClient> AppointmentsClients { get; set; }
    }
}
