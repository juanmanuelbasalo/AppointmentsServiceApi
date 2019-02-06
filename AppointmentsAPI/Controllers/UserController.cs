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
using Microsoft.Extensions.Logging;

namespace AppointmentsAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService service;
        readonly ILogger<UserController> logger;
        public UserController(IUserService service, ILogger<UserController> logger)
        {
            this.service = service;
            this.logger = logger;
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
        public ActionResult<UserDto> GetSingleUser(Guid id)
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultDto = await service.InsertUser(userDto);
            if (resultDto == null)
            {
                throw new Exception("Something went wrong registering the user.");
            }

            return CreatedAtRoute(nameof(GetSingleUser), new { resultDto.Id }, resultDto);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserDto>> Put(Guid id, [FromBody] UserUpdateDto userDto)
        {
            var existingUser = service.GetUser(id);
            if(existingUser == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Mapper.Map(userDto, existingUser);

            var updatedUser = await service.UpdateUserAsync(existingUser);
            if(updatedUser == null)
            {
                throw new Exception("Something went wrong updating the user.");
            }

            return updatedUser;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UserDto>> PartialUpdate(Guid id, [FromBody] JsonPatchDocument<UserUpdateDto> userUpdateDto)
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

            var userUpdated = await service.PatchUser(userUpdateDto, userDto, this);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(userUpdated == null)
            {
                throw new Exception("Something went wrong updating the user.");
            }

            return userUpdated;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var userDto = service.GetUser(id);
            if(userDto == null)
            {
                return NotFound();
            }

            var successfulDelete = await service.DeleteUser(userDto);
            if (!successfulDelete)
            {
                throw new Exception("Something went wrong trying to delete this user.");
            }

            return NoContent();
;        }
    }
}
