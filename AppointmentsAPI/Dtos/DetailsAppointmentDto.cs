﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class DetailsAppointmentDto
    {
        public string Address { get; set; }
        public Guid Id { get; set; }
    }
}
