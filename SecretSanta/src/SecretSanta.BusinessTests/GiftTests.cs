using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace SecretSanta.Business.Tests
{
    [TestClass()]
    public class GiftTests
    {
        [TestMethod()]
        public void Create_Gift_Success()
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

        [TestMethod()]
        public void TitleNull_Gift_ThrowException()
        {
            //Arrange
            int id = 0001;
            string description = "This gift is a test";
            string url = "http://gifttest.com";
            User janeDoe = new User(0001, "Jane", "Doe", null);

            //Act
            Assert.ThrowsException<ArgumentNullException>(() => new Gift(id, null, description, url, janeDoe));
        }

        [TestMethod()]
        public void DescriptionNull_Gift_ThrowException()
        {
            //Arrange
            int id = 0001;
            string title = "Test Gift";
            string url = "http://gifttest.com";
            User janeDoe = new User(0001, "Jane", "Doe", null);

            //Act
            Assert.ThrowsException<ArgumentNullException>(() => new Gift(id, title, null, url, janeDoe));
        }

        [TestMethod()]
        public void UrlNull_Gift_ThrowException()
        {
            //Arrange
            int id = 0001;
            string title = "Test Gift";
            string description = "This gift is a test";
            User janeDoe = new User(0001, "Jane", "Doe", null);

            //Act
            Assert.ThrowsException<ArgumentNullException>(() => new Gift(id, title, description, null, janeDoe));
        }

        [TestMethod()]
        public void UserNull_Gift_ThrowException()
        {
            //Arrange
            int id = 0001;
            string title = "Test Gift";
            string description = "This gift is a test";
            string url = "http://gifttest.com";

            //Act
            Assert.ThrowsException<ArgumentNullException>(() => new Gift(id, title, description, url, null));
        }

        [TestMethod()]
        public void ReflectionTestingOf_UserProperties_ConfirmReadWritePermissions()
        {
            //Arrange
            Type type = typeof(Gift);

            //Act
            PropertyInfo[] properties = type.GetProperties();

            //id
            Assert.IsFalse(properties[0].CanWrite);
            Assert.IsTrue(properties[0].CanRead);

            //title
            Assert.IsTrue(properties[1].CanWrite);
            Assert.IsTrue(properties[1].CanRead);

            //description
            Assert.IsTrue(properties[2].CanWrite);
            Assert.IsTrue(properties[2].CanRead);

            //url
            Assert.IsTrue(properties[2].CanWrite);
            Assert.IsTrue(properties[2].CanRead);

            //user
            Assert.IsTrue(properties[2].CanWrite);
            Assert.IsTrue(properties[2].CanRead);
        }
    }
}
