using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : EntityControllerTest<User>
    {
        protected override User CreateInstance()
        {
            return new User("Buzz","Lightyear");
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
}
