using SecretSanta.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data.Tests;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : EntityControllerTest<Gift>
    {
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
            : base(entity.Title, entity.Url, entity.Description, entity.User)
        {
            Id = id;
        }
    }
}
