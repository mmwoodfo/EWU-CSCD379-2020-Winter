using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IEntityService<Group> service = new GroupService(dbContextInsert, Mapper);

            var forest = SampleData.CreateEnchantedForestGroup();


            // Act
            await service.InsertAsync(forest);


            // Assert
            Assert.IsNotNull(forest.Id);

        }
    }
}
