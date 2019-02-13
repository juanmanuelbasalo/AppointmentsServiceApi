using AppointmentsAPI.Controllers;
using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUSers();
        UserDto GetUser(Guid id);
        Task<UserDto> InsertUser(UserDto entity);
        Task<UserDto> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUser(UserDto entity);
        UserDto FindUser(Expression<Func<User, bool>> searchTerm);
        Task<UserDto> PatchUser(JsonPatchDocument<UserUpdateDto> updateInfo, UserDto userToUpdate, UserController controller);
        UserDto Authenticate(string email, string password);
    }
}
