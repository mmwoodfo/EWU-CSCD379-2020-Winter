﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };

            // Act
            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
            Assert.IsNotNull(user.Gifts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetFirstNameToNull_ThrowsArgumentNullException()
        {
            new User
            {
                Id = 1,
                FirstName = null!,
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_SetLastNameToNull_ThrowsArgumentNullException()
        {
            new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = null!,
                Gifts = new List<Gift>()
            };
        }
    }
}
