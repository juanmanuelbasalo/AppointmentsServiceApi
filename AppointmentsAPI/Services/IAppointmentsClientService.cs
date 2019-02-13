using AppointmentsAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IAppointmentsClientService
    {
        Task<bool> CreateNewAppointmentsClient(Guid AppointmentsId, int statusId, Guid detailsId);
        IEnumerable<AppointmentsClientDto> GetAllClientAppointments(Guid clientId);
    }
}
