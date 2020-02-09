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
    public class GiftControllTests
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
        public async Task Get_ReturnsGifts()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateArduinoGift();
            context.Gifts.Add(gift);
            context.SaveChanges();

            //Act
//Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/Gift");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);

            Assert.AreEqual(gift.Id, gifts[0].Id);
            Assert.AreEqual(gift.Title, gifts[0].Title);
            Assert.AreEqual(gift.Description, gifts[0].Description);
            Assert.AreEqual(gift.Url, gifts[0].Url);
            Assert.AreEqual(gift.UserId, gifts[0].UserId);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            //Arrange
            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(SampleData.CreateArduinoGift());
            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
//Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync("api/Gift/40", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesGift()
        {
            //Arrange
            Gift giftEntity = SampleData.CreateArduinoGift();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(giftEntity);
            context.SaveChanges();

            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(giftEntity);
            gift.Title += " title updated";
            gift.Description += " description updated";
            gift.Url += " url updated";

            string jsonData = JsonSerializer.Serialize(gift);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
//Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Author/{giftEntity.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(returnedJson, options);

            Assert.AreEqual(gift.Title, returnedGift.Title);
            Assert.AreEqual(gift.Description, returnedGift.Description);
            Assert.AreEqual(gift.Url, returnedGift.Url);

        }

        [TestMethod]
        public async Task Post_WithOutTitle_BadResult()
        {
            //Arrange
            Gift giftEntity = SampleData.CreateArduinoGift();
            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(giftEntity);

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
//Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PostAsync($"api/Gift/{giftEntity.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert

        }
    }
}
