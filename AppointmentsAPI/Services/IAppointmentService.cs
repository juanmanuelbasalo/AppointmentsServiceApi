using AppointmentsAPI.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateNewAppointment(AppointmentWithDetailsDto appointmentDto);
        Task<bool> DeleteAppointment(AppointmentDto appointmentDto);
        Task<AppointmentDto> ChangeAppointmentDate(JsonPatchDocument<AppointmentDto> jsonPatch, AppointmentDto appointmentDto);
        Task<AppointmentDto> ChangeAppointmentTime(JsonPatchDocument<AppointmentDto> jsonPatch, AppointmentDto appointmentDto);
    }
}
