using AppointmentsAPI.Dtos;
using AppointmentsAPI.Entities;
using AppointmentsAPI.Repositories;
using AppointmentsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        #region Get Methods
        [Test]
        public void GetAllUsers_AllUsers_ReturnAllUsers()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var expectedResult = new List<UserDto>
            {
                new UserDto{ Id = id1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", Email = "jb@2345", PhoneNumber = "11111" },
                new UserDto{ Id = id2, Name = "Pedro", LastName = "Grieta", Password = "4343rfgf676", Email = "pedro@343", PhoneNumber = "11111" }
            };

            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Strict);
            mock.Setup(p => p.GetAll()).Returns(new List<User>
            {
                new User{ Id = id1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", Email = "jb@2345", PhoneNumber = "11111" },
                new User{ Id = id2, Name = "Pedro", LastName = "Grieta", Password = "4343rfgf676", Email = "pedro@343", PhoneNumber = "11111" }
             }.AsQueryable());

            var service = new UserService(mock.Object);
            var result = service.GetAllUSers().ToList();

            var expectedIdResult = expectedResult.Select((item) => item.Id);
            var resultId = result.Select(item => item.Id);

            CollectionAssert.AreEqual(expectedIdResult, resultId);

            mock.VerifyAll();
        }
        [Test]
        public void GetUser_UserBasedOnId_ReturnCorrectUser()
        {
            var id1 = Guid.NewGuid();
            var expected = new User { Id = id1, Name = "Juan", LastName = "Basalo", Password = "12fdggf555", Email = "jb@2345", PhoneNumber = "11111"};
            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Strict);
            mock.Setup(p => p.Get(id1)).Returns(expected);

            var service = new UserService(mock.Object);
            var result = service.GetUser(id1);

            Assert.IsTrue((expected.Id == result.Id) && (expected.Name.Equals(result.Name) && (expected.Password.Equals(result.Password))));

            mock.VerifyAll();
        }
        #endregion
        #region Insert Methods
        [Test]
        public async Task InsertUser_InsertUserDto_ReturnSameUserAsInserted()
        {
            var id1 = Guid.NewGuid();
        
            var expectedUser = new User { Id = id1, Name = "Juan", LastName = "Basalo", Email = "juanBasalo", Password = "12345", PhoneNumber = "11111"};
            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Loose);
            mock.Setup(p => p.SaveAsync()).Returns(Task.FromResult(true));
            mock.Setup(p => p.Insert(expectedUser));

            var userDto = AutoMapper.Mapper.Map<UserDto>(expectedUser);
            var service = new UserService(mock.Object);
            var result = await service.InsertUser(userDto);

            Assert.IsTrue((expectedUser.Id == result.Id) && (expectedUser.Name.Equals(result.Name) && (expectedUser.Password.Equals(result.Password))));

            mock.Verify();
        }
        [Test]
        public async Task InsertUser_InsertUserNull_ReturnNull()
        {
            var id1 = Guid.NewGuid();
            
            var expectedUser = new User { Id = id1, Name = "Juan", LastName = "Basalo", Email = "juanBasalo", Password = "12345", PhoneNumber = "11111"};
            var mock = new Mock<IGenericRepository<User>>(MockBehavior.Loose);
            mock.Setup(p => p.SaveAsync()).Returns(Task.FromResult(false));
            mock.Setup(p => p.Insert(expectedUser));

            var userDto = AutoMapper.Mapper.Map<UserDto>(expectedUser);
            var service = new UserService(mock.Object);
            var result = await service.InsertUser(userDto);

            Assert.IsNull(result);
            mock.Verify();
        }
        #endregion
        #region Update Methods
        #endregion
        #region Delete Methods
        #endregion
    }
}