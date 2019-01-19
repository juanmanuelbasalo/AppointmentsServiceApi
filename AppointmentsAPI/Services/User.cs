using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Services
{
    public class User : IUserService
    {
        public void DeleteUser(User entity)
        {
            throw new NotImplementedException();
        }

        public User FindUser(Expression<Func<User, bool>> searchTerm)
        {
            throw new NotImplementedException();
        }

        public IQueryable GetAllUSers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
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
