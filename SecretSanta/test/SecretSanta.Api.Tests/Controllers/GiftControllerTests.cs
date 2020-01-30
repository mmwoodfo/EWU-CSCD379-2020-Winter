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
    public class GiftControllerTests
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            //Arrange
            var GiftService = new GiftService();

            //Act & Assert
            _ = new GiftController(GiftService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_ThrowException()
        {
            //Arrange, Act & Assert
            _ = new GiftController(null!);
        }

        [TestMethod]
        public async Task GetAll_WithExistingGifts_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_Success()
        {
            //Arrange
            var service = new GiftService();
            Gift Gift = SampleData.CreateArduinoGift();
            Gift = await service.InsertAsync(Gift);

            var controller = new GiftController(service);

            //Act
            ActionResult<Gift> rv = await controller.Get(Gift.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_404Error()
        {
            //Arrange
            var service = new GiftService();
            var controller = new GiftController(service);

            //Act
            ActionResult<Gift> rv = await controller.Get(0);

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
        public void Update_Gift_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Delete_GiftWithId_Success()
        {
            //Arrange


            //Act


            //Assert
            Assert.Fail();
        }
    }

    public class GiftService : IGiftService
    {
        private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gift>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Gift?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Gift? Gift))
            {
                Task<Gift?> task1 = Task.FromResult<Gift?>(Gift);
                return task1;
            }
            Task<Gift?> task2 = Task.FromResult<Gift?>(null!);
            return task2;
        }

        public Task<Gift> InsertAsync(Gift entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGift(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Gift[]> InsertAsync(params Gift[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Gift?> UpdateAsync(int id, Gift entity)
        {
            throw new NotImplementedException();
        }
    }

    public class TestGift : Gift
    {
        public TestGift(Gift Gift, int id)
            : base(Gift.Title, Gift.Description, Gift.Url, Gift.User)
        {
            Id = id;
        }
    }
}
