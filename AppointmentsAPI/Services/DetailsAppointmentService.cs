using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;

namespace AppointmentsAPI.Services
{
    public class DetailsAppointmentService : IDetailsAppointmentService
    {
        readonly IGenericRepository<DetailsAppointments> repository;
        public DetailsAppointmentService(IGenericRepository<DetailsAppointments> repository) => this.repository = repository;
    }
}
