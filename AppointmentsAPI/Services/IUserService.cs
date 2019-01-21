using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
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
        UserDto GetUser(int id);
        Task<UserDto> InsertUser(UserDto entity);
        Task<UserDto> UpdateUserAsync(UserDto userDto);
        void DeleteUser(UserDto entity);
        UserDto FindUser(Expression<Func<UserDto, bool>> searchTerm);
    }
}
