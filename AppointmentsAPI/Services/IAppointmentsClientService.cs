﻿using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IAppointmentsClientService
    {
        Task<AppointmentsClientDto> CreateNewAppointmentsClientAsync(AppointmentWithDetailsDto appointmentWithDetailsDto);
        Task DeleteAppointmentClientAsync(AppointmentsClient appointmentWithDetailsDto);
        IEnumerable<AppointmentsClientDto> GetAllClientAppointments(Guid clientId, AppointmentQueryParameters appointmentParameters);
        int CountClientAppointments(Guid clientId);
        AppointmentsClient GetAppointment(Guid appointmentId);
        Task<AppointmentsClientDto> PatchAppointmentClientAsync(JsonPatchDocument<AppointmentWithDetailsDto> patchAppointmentDto, AppointmentsClient appointmentsClientDto, ControllerBase controller);
    }
}
