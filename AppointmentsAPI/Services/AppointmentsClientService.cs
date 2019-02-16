using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
       
        public async Task<AppointmentsClientDto> CreateNewAppointmentsClientAsync(AppointmentWithDetailsDto appointmentWithDetailsDto)
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

        public AppointmentsClient GetAppointment(Guid appointmentId)
        {
            var appointmentClient = repository.Find(appointment => appointment.Appointment.Id == appointmentId);
            return appointmentClient;
        }

        public async Task<AppointmentsClientDto> PatchAppointmentClientAsync(JsonPatchDocument<AppointmentWithDetailsDto> patchAppointmentDto, 
            AppointmentsClient appointmentsClientDto, ControllerBase controller)
        {
            var appointmentToPatch = Mapper.Map<AppointmentWithDetailsDto>(appointmentsClientDto);
            patchAppointmentDto.ApplyTo(appointmentToPatch, controller.ModelState);
            controller.TryValidateModel(appointmentToPatch);

            Mapper.Map(appointmentToPatch, appointmentsClientDto);
            var updatedAppointment = await UpdateAppointmentClientAsync(appointmentsClientDto);

            return updatedAppointment;
        }

        private async Task<AppointmentsClientDto> UpdateAppointmentClientAsync(AppointmentsClient appointmentsClient)
        {
            repository.Update(appointmentsClient);
            var result = await repository.SaveAsync();

            var appointmentsClientDto = Mapper.Map<AppointmentsClientDto>(appointmentsClient);

            return result ? appointmentsClientDto : null;
        }
    }
}
