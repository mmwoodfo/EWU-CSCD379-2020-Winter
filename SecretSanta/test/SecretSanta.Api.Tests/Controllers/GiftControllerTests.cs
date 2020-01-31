using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : EntityControllerTest<Gift>
    {
        public GiftControllerTests() : base(new TestableGiftSerivce())
        {

        }
        protected override Gift CreateInstance()
        {
            return SampleData.CreateArduinoGift();
        }
    }

    public class TestableGiftSerivce : EntityService<Gift>
    {
        protected override Gift CreateWithId(Gift entity, int id)
        {
            return new TestGift(entity, id);
        }
    }
    public class TestGift : Gift
    {
        public TestGift(Gift entity, int id)
            : base(entity?.Title ?? throw new ArgumentNullException(nameof(entity)), entity.Url, entity.Description, entity.User)
        {
            Id = id;
        }
    }
}
