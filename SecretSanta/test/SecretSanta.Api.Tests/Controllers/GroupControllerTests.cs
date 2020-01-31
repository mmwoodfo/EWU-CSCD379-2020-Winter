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
    public class GroupControllerTests : EntityControllerTest<Group>
    {
        protected override Group CreateInstance()
        {
            return new Group("Cooking");
        }
    }
    public class TestableGroupSerivce : EntityService<Group>
    {
        protected override Group CreateWithId(Group entity, int id)
        {
            return new TestGroup(entity, id);

        }
    }
    public class TestGroup : Group
    {
        public TestGroup(Group entity, int id)
            : base(entity.Title)
        {
            Id = id;
        }
    }
}

