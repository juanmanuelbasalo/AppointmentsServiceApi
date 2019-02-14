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
    public class AppointmentController : ControllerBase
    {
        readonly IAppointmentService service;
        readonly IAppointmentsClientService appointmentsClientService;
        readonly IDetailsAppointmentService detailsAppointmentService;
        public AppointmentController(IAppointmentService service, IAppointmentsClientService appointmentsClientService, 
            IDetailsAppointmentService detailsAppointmentService)
        {
            this.service = service;
            this.appointmentsClientService = appointmentsClientService;
            this.detailsAppointmentService = detailsAppointmentService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<AppointmentDto>> GetAllAppointmentsByClient(Guid id)
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = nameof(GetSingleAppointment))]
        public ActionResult<IEnumerable<AppointmentsClientDto>> GetSingleAppointment(Guid id)
        {
            return appointmentsClientService.GetAllClientAppointments(id).ToList();
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment([FromBody]AppointmentWithDetailsDto appointmentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var appointment = await service.CreateNewAppointment(appointmentDto);
            if (appointment == null) throw new Exception("Something went wrong creating the Appointment.");

            var detailsAppointment = await detailsAppointmentService.InsertNewDetails(appointmentDto);
            if (detailsAppointment == null) throw new Exception("Something went wrong creating the details");

            var appointmentClient = await appointmentsClientService.CreateNewAppointmentsClient(appointment.Id, (int)StatusEnum.Waiting, detailsAppointment.Id);
            if (!appointmentClient) throw new Exception("Something went wrong creating the appointment for the client");

            return CreatedAtRoute(nameof(GetSingleAppointment), new { appointment.Id }, appointment);
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
