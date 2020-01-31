using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : EntityControllerTest<User>
    {
        public UserControllerTests() : base(new TestableUserSerivce())
        {

        }
        protected override User CreateInstance()
        {
            return SampleData.CreateJonDoe();
        }
    }
    public class TestableUserSerivce : EntityService<User>
    {
        protected override User CreateWithId(User entity, int id)
        {
            return new TestUser(entity, id);

        }
    }
    public class TestUser : User
    {
        public TestUser(User entity, int id)
            : base(entity.FirstName, entity.LastName)
        {
            Id = id;
        }
    }
}

