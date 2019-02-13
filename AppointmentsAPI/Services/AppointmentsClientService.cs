using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
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
       
        public async Task<bool> CreateNewAppointmentsClient(Guid appointmentsId, int statusId, Guid detailsId)
        {
            var appointmentClient = new AppointmentsClient { AppointmentsId = appointmentsId, StatusId = statusId, DetailsId = detailsId };
            repository.Insert(appointmentClient);
            var result = await repository.SaveAsync();
            return result;
        }

        public IEnumerable<AppointmentsClientDto> GetAllClientAppointments(Guid clientId)
        {
            //var appointments = repository.FindAll(client => client.Appointments.UserId == clientId);
            throw new NotImplementedException();
        }
    }
}
