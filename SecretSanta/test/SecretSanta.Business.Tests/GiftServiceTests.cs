

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IEntityService<Gift> service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateJonDoe();

            var gift = new Gift("Ring Doorbell", "www.ring.com ", "The doorbell that saw too much", user);

            await service.InsertAsync(gift);

            // Act

            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(user.Id);
            Assert.AreSame(gift.User, user);
            Assert.AreEqual(user.Id, gift.User.Id);
        }

        [TestMethod]
        public async Task FetchByIdGift_ShouldIncludeUser()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IEntityService<Gift> service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateJonDoe();

            var gift = new Gift("Ring Doorbell", "www.ring.com ", "The doorbell that saw too much", user);

            await service.InsertAsync(gift);

            // Act

            // Assert
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            gift = await service.FetchByIdAsync(gift.Id);

            Assert.IsNotNull(gift.User);
        }
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task RemoveGiftById_ShouldThrowInvaildOperationException()
        {
            using var dbContext = new ApplicationDbContext(Options);

            IEntityService<Gift> service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateJonDoe();

            var gift = new Gift("Ring Doorbell", "www.ring.com ", "The doorbell that saw too much", user);

            await service.InsertAsync(gift);

            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            bool success = await service.DeleteAsync(gift.Id);

            Assert.IsTrue(success);

            using var dbContext3 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            gift = await service.FetchByIdAsync(gift.Id);//should throw 'Enumerator failed to MoveNextAsync' since it doesn't exist
        }
    }
}