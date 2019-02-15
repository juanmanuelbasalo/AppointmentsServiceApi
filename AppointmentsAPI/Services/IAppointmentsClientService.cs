using AppointmentsAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IAppointmentsClientService
    {
        Task<AppointmentsClientDto> CreateNewAppointmentsClient(AppointmentWithDetailsDto appointmentWithDetailsDto);
        IEnumerable<AppointmentsClientDto> GetAllClientAppointments(Guid clientId);
    }
}
