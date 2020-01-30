using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Create_UserController_Success()
        {
            //Arrange
            var userService = new UserService();
            
            //Act & Assert
            _ = new UserController(userService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_ThrowException()
        {
            //Arrange, Act & Assert
            _ = new UserController(null!);
        }

        [TestMethod]
        public async Task GetAll_WithExistingUsers_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetById_WithExistingUser_Success()
        {
            //Arrange
            var service = new UserService();
            User user = SampleData.CreateJonDoe();
            user = await service.InsertAsync(user);

            var controller = new UserController(service);

            //Act
            ActionResult<User> rv = await controller.Get(user.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingUser_404Error()
        {
            //Arrange
            var service = new UserService();
            var controller = new UserController(service);

            //Act
            ActionResult<User> rv = await controller.Get(0);

            //Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public void Create_Post_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Update_User_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Delete_UserWithId_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }
    }

    public class UserService : IUserService
    {
        private Dictionary<int, User> Items { get; } = new Dictionary<int, User>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out User? user))
            {
                Task<User?> task1 = Task.FromResult<User?>(user);
                return task1;
            }
            Task<User?> task2 = Task.FromResult<User?>(null!);
            return task2;
        }

        public Task<User> InsertAsync(User entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestUser(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<User[]> InsertAsync(params User[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateAsync(int id, User entity)
        {
            throw new NotImplementedException();
        }
    }

    public class TestUser : User
    {
        public TestUser(User user, int id) 
            : base(user.FirstName, user.LastName)
        {
            Id = id;
        }
    }
}
