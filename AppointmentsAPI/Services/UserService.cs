using AppointmentsAPI.Controllers;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> repository;
        public UserService(IGenericRepository<User> repository) => this.repository = repository;

        public async Task<bool> DeleteUser(UserDto entity)
        {
            var user = Mapper.Map<User>(entity);
            repository.Delete(user);

            return await repository.SaveAsync();
        }

        public IEnumerable<UserDto> FindUser(Expression<Func<UserDto, bool>> searchTerm)
        {
            var searchUser = Mapper.Map<Expression<Func<User, bool>>>(searchTerm);
            var user = repository.Find(searchUser);

            return Mapper.Map<IEnumerable<UserDto>>(user);
        }

        public IEnumerable<UserDto> GetAllUSers()
        {
            var user = repository.GetAll().ToList();
            var userDto = Mapper.Map<List<UserDto>>(user);
            return userDto;
        }

        public UserDto GetUser(Guid id)
        {
            var user = repository.Get(id);
            var userDto = Mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> InsertUser(UserDto entity)
        {
            var user = Mapper.Map<User>(entity);
            repository.Insert(user);

            var result = await repository.SaveAsync();

            if (result)
            {
                var userDto = Mapper.Map<UserDto>(user);
                return userDto;
            }

            return null;
        }

        public async Task<UserDto> PatchUser(JsonPatchDocument<UserUpdateDto> updateInfo, UserDto userToUpdate, UserController controller)
        {
            var userToPatch = Mapper.Map<UserUpdateDto>(userToUpdate);
            updateInfo.ApplyTo(userToPatch, controller.ModelState);

            controller.TryValidateModel(userToPatch);

            Mapper.Map(userToPatch, userToUpdate);

            var updatedUser = await UpdateUserAsync(userToUpdate);

            return updatedUser;
        }

        public async Task<UserDto> UpdateUserAsync(UserDto userDto)
        {
            var user = Mapper.Map<User>(userDto);

            repository.Update(user);
            var result = await repository.SaveAsync();

            return result ? userDto : null;
        }
    }
}
