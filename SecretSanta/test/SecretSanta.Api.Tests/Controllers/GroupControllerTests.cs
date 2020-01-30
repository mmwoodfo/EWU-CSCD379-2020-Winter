using SecretSanta.Api.Controllers;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Business;


namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        public void Create_GroupController_Success()
        {
            //Arrange
            var GroupService = new GroupService();

            //Act & Assert
            _ = new GroupController(GroupService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_ThrowException()
        {
            //Arrange, Act & Assert
            _ = new GroupController(null!);
        }

        [TestMethod]
        public async Task GetAll_WithExistingGroups_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_Success()
        {
            //Arrange
            var service = new GroupService();
            Group Group = SampleData.CreateEnchantedForestGroup();
            Group = await service.InsertAsync(Group);

            var controller = new GroupController(service);

            //Act
            ActionResult<Group> rv = await controller.Get(Group.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_404Error()
        {
            //Arrange
            var service = new GroupService();
            var controller = new GroupController(service);

            //Act
            ActionResult<Group> rv = await controller.Get(0);

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
        public void Update_Group_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Delete_GroupWithId_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }
    }

    public class GroupService : IGroupService
    {
        private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Group? Group))
            {
                Task<Group?> task1 = Task.FromResult<Group?>(Group);
                return task1;
            }
            Task<Group?> task2 = Task.FromResult<Group?>(null!);
            return task2;
        }

        public Task<Group> InsertAsync(Group entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGroup(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Group[]> InsertAsync(params Group[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> UpdateAsync(int id, Group entity)
        {
            throw new NotImplementedException();
        }
    }

    public class TestGroup : Group
    {
        public TestGroup(Group Group, int id)
            : base(Group.Title)
        {
            Id = id;
        }
    }
}
