using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void Create_User_Success()
        {
            //Arrange
            int id = 0001;
            string firstName = "Jon";
            string lastName = "Doe";

            User janeDoe = new User(0001, "Jane", "Doe", null);
            List<Gift> gifts = new List<Gift>
            {
                new Gift(0001, "Gift 1", "A gift", "http://gifttest.com", janeDoe),
                new Gift(0002, "Gift 2", "A gift", "http://gifttest.com", janeDoe),
                new Gift(0003, "Gift 3", "A gift", "http://gifttest.com", janeDoe),
            };

            //Act
            User jonDoe = new User(id, firstName, lastName, gifts);

            //Assert
            Assert.AreEqual(jonDoe.Id, id);
            Assert.AreEqual(jonDoe.FirstName, firstName);
            Assert.AreEqual(jonDoe.LastName, lastName);
            CollectionAssert.AreEqual(jonDoe.Gifts, gifts);
        }
    }
}