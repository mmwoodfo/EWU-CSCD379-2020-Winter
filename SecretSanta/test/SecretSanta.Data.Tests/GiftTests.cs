using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task AddGift_WithUser_ShouldCreateForeignRelationship()
        {
            // Arrange
            var gift = new Gift
            {
                Title = "A Gift",
                Description = "A Description",
                Url = "https://www.aURL.com",
            };
            var user = new User
            {
                FirstName = "Jane",
                LastName = "Doe",
                Santa = null,
                Gifts = new List<Gift>(),
                UserGroups = new List<UserGroup>()
            };

            //Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                gift.User = user;

                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(p => p.User).ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.AreNotEqual(0, gifts[0].Id);
            }
        }

    }
}