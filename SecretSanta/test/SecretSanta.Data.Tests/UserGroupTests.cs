using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            List<Gift> gift = new List<Gift>
            {
                new Gift{
                    Title = "A gift",
                    Description = "A gift description",
                    Url = "http://www.GiftUrl.com"
                }
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
            user.Gifts = gift;
            user.UserGroups = new List<UserGroup>
            {
                new UserGroup { User = user, Group = group1 },
                new UserGroup { User = user, Group = group2 }
            };

            using(ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedUser = await dbContext.Users.Where(u => u.Id == user.Id)
                    .Include(u => u.UserGroups)
                    .ThenInclude(ug => ug.Group)
                    .SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedUser);
                Assert.AreEqual(2, retrievedUser.UserGroups.Count);
                Assert.IsNotNull(retrievedUser.UserGroups[0].Group);
                Assert.IsNotNull(retrievedUser.UserGroups[1].Group);
            }
        }
    }
}
