using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace AppointmentsAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        readonly IGenericRepository<Appointment> repository;
        public AppointmentService(IGenericRepository<Appointment> repository) => this.repository = repository;
   
        public Task<AppointmentDto> ChangeAppointmentDate(JsonPatchDocument<AppointmentDto> jsonPatch, AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentDto> ChangeAppointmentTime(JsonPatchDocument<AppointmentDto> jsonPatch, AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AppointmentDto> CreateNewAppointment(AppointmentWithDetailsDto appointmentDto)
        {
            var appointment = Mapper.Map<Appointment>(appointmentDto);
            repository.Insert(appointment);
            var result = await repository.SaveAsync();
            if (result)
            {
                var entity = Mapper.Map<AppointmentDto>(appointment);
                return entity;
            }

            return null;
        }

        public Task<bool> DeleteAppointment(AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }
    }
}
