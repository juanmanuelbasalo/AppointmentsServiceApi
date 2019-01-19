using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> repository;
        public UserService(IGenericRepository<User> repository) => this.repository = repository;

        public void DeleteUser(User entity)
        {
            throw new NotImplementedException();
        }

        public UserDto FindUser(Expression<Func<User, bool>> searchTerm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAllUSers()
        {
            var user = repository.GetAll().ToList();
            var userDto = Mapper.Map<List<UserDto>>(user);
            return userDto;
        }

        public UserDto GetUser(int id)
        {
            var user = repository.Get(id);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }

        public void InsertUser(User entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveUser()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
