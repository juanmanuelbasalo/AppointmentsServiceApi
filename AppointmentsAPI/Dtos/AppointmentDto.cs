﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Dtos
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}
