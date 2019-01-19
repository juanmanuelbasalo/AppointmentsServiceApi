using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IGenericRepository<User> repository;
        public UserController(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }

        // GET api/user
        [HttpGet]
        public ActionResult Get()
        {
            var users = repository.GetAll().ToList();
            var usersDto = Mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var user = repository.Get(id);
            var userDto = Mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
