using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsGroups()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group = SampleData.CreateEnchantedForestGroup();
            context.Groups.Add(group);
            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/Group");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Business.Dto.Group[] groups = JsonSerializer.Deserialize<Business.Dto.Group[]>(jsonData, options);

            Assert.AreEqual(group.Id, groups[0].Id);
            Assert.AreEqual(group.Title, groups[0].Title);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            //Arrange
            Business.Dto.GroupInput group = Mapper.Map<Group, Business.Dto.GroupInput>(SampleData.CreateEnchantedForestGroup());
            string jsonData = JsonSerializer.Serialize(group);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync("api/Group/42", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_WithValidId_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Group groupEntity1 = SampleData.CreateEnchantedForestGroup();
            Group groupEntity2 = SampleData.CreateEnchantedForestGroup();
            Group groupEntity3 = SampleData.CreateEnchantedForestGroup();

            context.Groups.Add(groupEntity1);
            context.Groups.Add(groupEntity2);
            context.Groups.Add(groupEntity3);

            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.DeleteAsync($"api/Group/{groupEntity1.Id}");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();

            using ApplicationDbContext contextAct = Factory.GetDbContext();

            List<Data.Group> groupsAfter = await context.Groups.ToListAsync();

            Assert.AreEqual(2, groupsAfter.Count);
        }

        [TestMethod]
        public async Task Delete_WithInvalidId_NotFound()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Group groupEntity = SampleData.CreateEnchantedForestGroup();

            context.Groups.Add(groupEntity);
            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.DeleteAsync($"api/Group/{42}");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
