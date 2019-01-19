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
        void InsertUser(User entity);
        void DeleteUser(User entity);
        void UpdateUser(User entity);
        UserDto FindUser(Expression<Func<User, bool>> searchTerm);
        bool SaveUser();
    }
}
