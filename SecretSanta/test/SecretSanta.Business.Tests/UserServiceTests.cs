using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IEntityService<User> service = new UserService(dbContextInsert, Mapper);

            var river = SampleData.CreateRiverWillis();
            var brandon = SampleData.CreateBrandonFields();
            var jon = SampleData.CreateJonDoe();

            // Act
            await service.InsertAsync(river);
            await service.InsertAsync(brandon);
            await service.InsertAsync(jon);

            // Assert
            Assert.IsNotNull(river.Id);
            Assert.IsNotNull(brandon.Id);
            Assert.IsNotNull(jon.Id);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IEntityService<User> service = new UserService(dbContextInsert, Mapper);

            var river = SampleData.CreateRiverWillis();
            var brandon = SampleData.CreateBrandonFields();

            await service.InsertAsync(river);
            await service.InsertAsync(brandon);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            User riverFromDb = await dbContextFetch.Users.SingleAsync(item => item.Id == river.Id);

            const string Shenandoah = "Shenandoah";
            riverFromDb.LastName = Shenandoah;

            // Update Inigo Montoya using the princesses Id.
            await service.UpdateAsync(brandon.Id, riverFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            riverFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == river.Id);
            var brandonFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);

            Assert.AreEqual(
                (SampleData.River, Shenandoah), (brandonFromDb.FirstName, brandonFromDb.LastName));

            Assert.AreEqual(
                (SampleData.River, SampleData.Willis), (riverFromDb.FirstName, riverFromDb.LastName));
        }

    }
}