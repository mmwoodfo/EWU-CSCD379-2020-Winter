using AutoMapper;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SecretSanta.Data.Tests;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        //Initialized in the test initialization
#nullable disable
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#nullable enable

        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        //============== UNIT TESTS ====================//
        [TestMethod]
        public async Task Get_ReturnsUsers()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateJonDoe();
            context.Users.Add(user);
            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/User");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Business.Dto.User[] users = JsonSerializer.Deserialize<Business.Dto.User[]>(jsonData, options);

            Assert.AreEqual(user.Id, users[0].Id);
            Assert.AreEqual(user.FirstName, users[0].FirstName);
            Assert.AreEqual(user.LastName, users[0].LastName);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            //Arrange
            Business.Dto.UserInput user = Mapper.Map<User, Business.Dto.UserInput>(SampleData.CreateBrandonFields());
            string jsonData = JsonSerializer.Serialize(user);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync("api/User/40", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesUser()
        {
            //Arrange
            User userEntity = SampleData.CreateRiverWillis();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Users.Add(userEntity);
            context.SaveChanges();

            Business.Dto.UserInput user = Mapper.Map<User, Business.Dto.UserInput>(userEntity);
            user.FirstName += " first name updated";
            user.LastName += " last name updated";

            string jsonData = JsonSerializer.Serialize(user);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Author/{userEntity.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Business.Dto.User returnedUser = JsonSerializer.Deserialize<Business.Dto.User>(returnedJson, options);

            Assert.AreEqual(user.FirstName, returnedUser.FirstName);
            Assert.AreEqual(user.LastName, returnedUser.LastName);
        }
    }
}
