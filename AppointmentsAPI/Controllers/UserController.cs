using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AppointmentsAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var usersDto = service.GetAllUSers();

            return usersDto.ToList();
        }

        // GET api/user/5
        [HttpGet("{id}", Name = nameof(GetSingleUser))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<UserDto> GetSingleUser(int id)
        {
            var userDto = service.GetUser(id);

            if (userDto == null)
            {
                return NotFound();
            }
            
            return userDto;
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto userDto)
        {
            var resultDto = await service.InsertUser(userDto);
            if (resultDto == null)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute(nameof(GetSingleUser), new { resultDto.Id }, resultDto);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UserUpdateDto userDto)
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

            return updatedUser;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserDto>> PartialUpdate(int id, [FromBody] JsonPatchDocument<UserUpdateDto> userUpdateDto)
        {
            if (userUpdateDto == null)
            {
                return BadRequest();
            }

            var userDto = service.GetUser(id);
            if(userDto == null)
            {
                return NotFound();
            }

            var userUpdated = await service.PatchUser(userUpdateDto, userDto);
            if(userUpdated == null)
            {
                return new StatusCodeResult(500);
            }

            return userUpdated;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var userDto = service.GetUser(id);
            if(userDto == null)
            {
                return NotFound();
            }

            var successfulDelete = await service.DeleteUser(userDto);
            if (!successfulDelete)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
;        }
    }
}
