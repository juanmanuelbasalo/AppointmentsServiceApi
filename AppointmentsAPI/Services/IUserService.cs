using AppointmentsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    interface IUserService
    {
        IQueryable GetAllUSers();
        User GetUser(int id);
        void InsertUser(User entity);
        void DeleteUser(User entity);
        void UpdateUser(User entity);
        User FindUser(Expression<Func<User, bool>> searchTerm);
        bool SaveUser();
    }
}
