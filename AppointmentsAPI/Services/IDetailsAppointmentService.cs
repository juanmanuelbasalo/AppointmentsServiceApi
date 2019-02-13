using AppointmentsAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IDetailsAppointmentService
    {
        Task<DetailsAppointmentDto> InsertNewDetails(AppointmentWithDetailsDto detailsAppointmentDto);
    }
}
