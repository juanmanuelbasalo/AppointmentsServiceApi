﻿using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        public async Task<bool> DeleteUser(UserDto entity)
        {
            var user = Mapper.Map<User>(entity);
            repository.Delete(user);

            return await repository.SaveAsync();
        }

        public UserDto FindUser(Expression<Func<UserDto, bool>> searchTerm)
        {
            throw new NotImplementedException();
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

        public async Task<UserDto> PatchUser(JsonPatchDocument<UserUpdateDto> updateInfo, UserDto userToUpdate)
        {
            var userToPatch = Mapper.Map<UserUpdateDto>(userToUpdate);
            updateInfo.ApplyTo(userToPatch);

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
