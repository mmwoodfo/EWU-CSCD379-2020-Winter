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
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task CreateUser_ShouldSaveIntoDatabase()
        {
            // Arrange
            int userId = -1;

            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user1 = new User
                {
                    FirstName = "John",
                    LastName = "Doe"
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act & Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();
                Assert.IsNotNull(user);
                Assert.AreEqual("John", user.FirstName);
                Assert.AreEqual("Doe", user.LastName);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnInitialSave()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jdoe"));

            int userId = -1;

            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user1 = new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroups = new List<UserGroup>()
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroups = new List<UserGroup>()
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act & Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("jdoe", user.CreatedBy); //createdby not being saved
                Assert.AreEqual("jdoe", user.ModifiedBy);
            }
        }

        [TestMethod]
        public async Task CreateUser_ShouldSetFingerPrintDataOnUpdate()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "jdoe"));

            int userId = -1;

            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user1 = new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroups = new List<UserGroup>()
                };
                applicationDbContext.Users.Add(user1);

                var user2 = new User
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Santa = null,
                    Gifts = new List<Gift>(),
                    UserGroups = new List<UserGroup>()
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user1.Id;
            }

            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta => hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "mmwoodfo"));

            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {

                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "Meg";
                user.LastName = "Woodford";

                await applicationDbContext.SaveChangesAsync();
            }

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(a => a.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("jdoe", user.CreatedBy); //createdby not being saved
                Assert.AreEqual("mmwoodfo", user.ModifiedBy);
            }
        }
    }
}