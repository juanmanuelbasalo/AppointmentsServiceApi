using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AppointmentsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ServicioDeCitasUnitTest
{
    [TestFixture]
    public class AppointmentsApiTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            AutoMapper.Mapper.Initialize((map) => map.CreateMap<User, UserDto>().ReverseMap());
        }  
        
        [Test]
        public void GetAllUsers_AllUsers_ReturnAllUsers()
        {
            var expectedResult = new List<UserDto>
            {
                new UserDto{ Id = 1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", UserName = "jb@2345" },
                new UserDto{ Id = 2, Name = "Pedro", LastName = "Grieta", Password = "4343rfgf676", UserName = "pedro@343" }
            };

            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Strict);
            mock.Setup(p => p.GetAll()).Returns(new List<User>
            {
                new User{ Id = 1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", UserName = "jb@2345" },
                new User{ Id = 2, Name = "Pedro", LastName = "Grieta", Password = "4343rfgf676", UserName = "pedro@343" }
            }.AsQueryable());

            var service = new UserService(mock.Object);
            var result = service.GetAllUSers().ToList();

            var expectedIdResult = expectedResult.Select((item) => item.Id);
            var resultId = result.Select(item => item.Id);

            CollectionAssert.AreEqual(expectedIdResult, resultId);

            mock.VerifyAll();
        }

        [TestCase(1)]
        public void GetUser_UserBasedOnId_ReturnCorrectUser(int id)
        {
            var expected = new User { Id = 1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", UserName = "jb@2345" };
            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Strict);
            mock.Setup(p => p.Get(id)).Returns(expected);

            var service = new UserService(mock.Object);
            var result = service.GetUser(id);

            Assert.IsTrue((expected.Id == result.Id) && (expected.Name.Equals(result.Name) && (expected.Password.Equals(result.Password))));

            mock.VerifyAll();
        }
    }
}
