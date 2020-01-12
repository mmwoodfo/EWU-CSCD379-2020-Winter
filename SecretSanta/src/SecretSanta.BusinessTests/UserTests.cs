using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Reflection;

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

            List<Gift> gifts = new List<Gift>{
            new Gift(0001, "Gift 1", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0002, "Gift 2", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0003, "Gift 3", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)) };

            //Act
            User jonDoe = new User(id, firstName, lastName, gifts);

            //Assert
            Assert.AreEqual(jonDoe.Id, id);
            Assert.AreEqual(jonDoe.FirstName, firstName);
            Assert.AreEqual(jonDoe.LastName, lastName);
            CollectionAssert.AreEqual(jonDoe.Gifts, gifts);
        }

        [TestMethod()]
        public void Create_User_InitlizeNullList()
        {
            //Arrange
            int id = 0001;
            string firstName = "Jon";
            string lastName = "Doe";

            //Act
            User jonDoe = new User(id, firstName, lastName, null);

            //Assert
            Assert.AreEqual(jonDoe.Id, id);
            Assert.AreEqual(jonDoe.FirstName, firstName);
            Assert.AreEqual(jonDoe.LastName, lastName);
            Assert.IsNotNull(jonDoe.Gifts);
        }

        [TestMethod()]
        public void FirstNameNull_User_ThrowException()
        {
            //Arrange
            int id = 0001;
            string lastName = "Doe";

            List<Gift> gifts = new List<Gift>{
            new Gift(0001, "Gift 1", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0002, "Gift 2", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0003, "Gift 3", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)) };

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new User(id, null, lastName, gifts));
        }

        [TestMethod()]
        public void LastNameNull_User_ThrowException()
        {
            //Arrange
            int id = 0001;
            string firstName = "Jon";

            List<Gift> gifts = new List<Gift>{
            new Gift(0001, "Gift 1", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0002, "Gift 2", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)),
            new Gift(0003, "Gift 3", "A gift", "http://gifttest.com", new User(0001, "Jane", "Doe", null)) };

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new User(id, firstName, null, gifts));
        }

        [TestMethod()]
        public void ReflectionTestingOf_UserProperties_ConfirmReadWritePermissions()
        {
            //Arrange
            Type type = typeof(User);

            //Act
            PropertyInfo[] properties = type.GetProperties();

            //id
            Assert.IsFalse(properties[0].CanWrite);
            Assert.IsTrue(properties[0].CanRead);

            //firstName
            Assert.IsTrue(properties[1].CanWrite);
            Assert.IsTrue(properties[1].CanRead);

            //lastName
            Assert.IsTrue(properties[2].CanWrite);
            Assert.IsTrue(properties[2].CanRead);

            //gift list
            Assert.IsFalse(properties[3].CanWrite);
            Assert.IsTrue(properties[3].CanRead);
        }
    }
}