using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Helpers;
using AppointmentsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AppointmentClientController : ControllerBase
    {
        readonly IAppointmentsClientService appointmentsClientService;
        public AppointmentClientController(IAppointmentsClientService appointmentsClientService)
        {
            this.appointmentsClientService = appointmentsClientService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = nameof(GetAllAppointmentsByClient))]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<AppointmentsClientDto>> GetAllAppointmentsByClient(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var allClientAppointments = appointmentsClientService.GetAllClientAppointments(id).ToList();
            if (allClientAppointments == null) throw new Exception("Something went wrong retrieving the client appointments.");

            return allClientAppointments;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody]AppointmentWithDetailsDto appointmentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var appointmentClient = await appointmentsClientService.CreateNewAppointmentsClient(appointmentDto);
            if (appointmentClient == null) throw new Exception("Something went wrong creating the appointment for the client");

            return CreatedAtRoute(nameof(GetAllAppointmentsByClient), new { appointmentClient.Appointment.Id}, appointmentClient.Appointment);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
