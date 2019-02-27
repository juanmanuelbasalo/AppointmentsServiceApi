using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Helpers;
using AppointmentsAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<AppointmentsClientDto>> GetAllAppointmentsByClient(Guid id, 
            [FromQuery]AppointmentQueryParameters appointmentParamaters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var allClientAppointments = appointmentsClientService.GetAllClientAppointments(id, appointmentParamaters).ToList();

            if (allClientAppointments.Count <= 0) return NotFound();
            if (allClientAppointments == null) throw new Exception("Something went wrong retrieving the client appointments.");

            Response.Headers.Add("Appointment-Pagination",
                JsonConvert.SerializeObject(new { totalCount = appointmentsClientService.CountClientAppointments(id) }));

            return allClientAppointments;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentClient([FromBody]AppointmentWithDetailsDto appointmentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var appointmentClient = await appointmentsClientService.CreateNewAppointmentsClientAsync(appointmentDto);
            if (appointmentClient == null) throw new Exception("Something went wrong creating the appointment for the client");

            return CreatedAtRoute(nameof(GetAllAppointmentsByClient), new { appointmentClient.Appointment.Id}, appointmentClient.Appointment);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPatch("{appointmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<AppointmentsClientDto>> PartiallyUpdateAppointmentClient(Guid appointmentId, 
            [FromBody] JsonPatchDocument<AppointmentWithDetailsDto> patchAppointmentDto)
        {
            if (patchAppointmentDto == null) return BadRequest();

            var appointmentClientDto = appointmentsClientService.GetAppointment(appointmentId);
            if (appointmentClientDto == null) return NotFound();

            var updatedAppointmentClient = await appointmentsClientService.PatchAppointmentClientAsync(patchAppointmentDto, 
                appointmentClientDto, this);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (updatedAppointmentClient == null) return NoContent();

            return updatedAppointmentClient;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
