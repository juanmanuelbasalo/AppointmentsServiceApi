using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AppointmentsAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }

        // GET api/user
        [HttpGet]
        public ActionResult Get()
        {
            var usersDto = service.GetAllUSers();

            return Ok(usersDto);
        }

        // GET api/user/5
        [HttpGet("{id}", Name = "GetSingleUser")]
        public ActionResult Get(int id)
        {
            var userDto = service.GetUser(id);

            if (userDto == null)
            {
                return NotFound();
            }
            
            return Ok(userDto);
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            var resultDto = await service.InsertUser(userDto);

            if (resultDto == null)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingleUser", new { resultDto.Id }, resultDto);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserUpdateDto userDto)
        {
            var existingUser = service.GetUser(id);

            if(existingUser == null)
            {
                return NotFound();
            }

            Mapper.Map(userDto, existingUser);

            var updatedUser = await service.UpdateUserAsync(existingUser);

            if(updatedUser == null)
            {
                return new StatusCodeResult(500);
            }

            return Ok(updatedUser);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
