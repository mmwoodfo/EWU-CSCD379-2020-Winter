using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{
    [TestClass()]
    public class GiftTests
    {
        [TestMethod()]
        public void Gift()
        {
            //Arrange
            int id = 0001;
            string title = "Test Gift";
            string description = "This gift is a test";
            string url = "http://gifttest.com";
            User janeDoe = new User(0001, "Jane", "Doe", null);

            //Act
            Gift gift = new Gift(id, title, description, url, janeDoe);

            //Assert
            Assert.AreEqual(gift.Id, id);
            Assert.AreEqual(gift.Title, title);
            Assert.AreEqual(gift.Description, description);
            Assert.AreEqual(gift.Url, url);
            Assert.IsNotNull(gift.User);
        }
    }
}
