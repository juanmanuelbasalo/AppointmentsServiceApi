using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public class AppointmentsClientService : IAppointmentsClientService
    {
        readonly IGenericRepository<AppointmentsClient> repository;
        public AppointmentsClientService(IGenericRepository<AppointmentsClient> repository) => this.repository = repository; 
       
        public async Task<AppointmentsClientDto> CreateNewAppointmentsClient(AppointmentWithDetailsDto appointmentWithDetailsDto)
        {
            var appointmentClient = Mapper.Map<AppointmentsClient>(appointmentWithDetailsDto);
            repository.Insert(appointmentClient);
            var result = await repository.SaveAsync();
            if (result)
            {
                return Mapper.Map<AppointmentsClientDto>(appointmentClient);
            }
            return null;
        }

        public IEnumerable<AppointmentsClientDto> GetAllClientAppointments(Guid clientId)
        {
            var appointments = repository.FindAll(client => client.Appointment.UserId == clientId);
            return Mapper.Map<IEnumerable<AppointmentsClientDto>>(appointments);
        }
    }
}
