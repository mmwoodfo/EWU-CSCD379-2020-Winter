using SecretSanta.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : EntityControllerTest<Gift>
    {
        protected override Gift CreateInstance()
        {
            return new Gift();
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
