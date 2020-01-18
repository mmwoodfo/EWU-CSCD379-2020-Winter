using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserGroupTests : TestBase
    {
        [TestMethod]
        public async Task Create_UserWithManyGroups_Success()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jdoe"));

            Gift gift = new Gift
            { 
                Title = "A gift",
                Description = "A gift description",
                Url = "http://www.GiftUrl.com"
            };

            User user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            Group group1 = new Group
            {
                Name = "Group1"
            };

            Group group2 = new Group
            {
                Name = "Group2"
            };

            //Act
            user.UserGroups = new List<UserGroup>
            {
                new UserGroup { User = user, Group = group1 },
                new UserGroup { User = user, Group = group2 }
            };
            gift.User = user;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync();
            }

            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedGift = await dbContext.Gifts.Where(g => g.Id == gift.Id)
                    .Include(u => u.User)
                    .ThenInclude(ug => ug.UserGroups)
                    .ThenInclude(gr => gr.Group)
                    .SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedGift);
                Assert.AreEqual(2, retrievedGift.User.UserGroups.Count);
                Assert.IsNotNull(retrievedGift.User.UserGroups[0].Group);
                Assert.IsNotNull(retrievedGift.User.UserGroups[1].Group);
            }
        }
    }
}
