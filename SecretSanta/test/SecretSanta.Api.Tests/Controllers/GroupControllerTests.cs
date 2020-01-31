using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : EntityControllerTest<Group>
    {
        public GroupControllerTests() : base(new TestableGroupSerivce())
        {

        }
        protected override Group CreateInstance()
        {
            return SampleData.CreateEnchantedForestGroup();
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
            : base(entity?.Title ?? throw new ArgumentNullException(nameof(entity)))
        {
            Id = id;
        }
    }
}

