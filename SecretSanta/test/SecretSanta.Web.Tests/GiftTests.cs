using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using SecretSanta.Web.Api;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }
        [NotNull]
        private IWebDriver? Driver { get; set; }
        private static string ApiUrl { get;} = "https://localhost:44388";
        private static string WebUrl { get; } = "https://localhost:44394";
        private static Process? ApiHostProcess { get; set; }
        private static Process? NpmProcess { get; set; }
        private static Process? WebHostProcess { get; set; }
        string AppUrl { get; } = "https://localhost:44394/Gifts";

        [ClassInitialize]
        public static void ClassInitalize(TestContext testContext)
        {

            //var psiNpmRunDist = new ProcessStartInfo
            //{
            //    FileName = "cmd",
            //    RedirectStandardInput = true,
            //    WorkingDirectory = "C:\\Users\\mason\\Source\\Repos\\EWU-CSCD379-2020-Winter\\SecretSanta\\src\\SecretSanta.Web"
            //};
            //NpmProcess = Process.Start(psiNpmRunDist);
            //NpmProcess.StandardInput.WriteLine("npm run build:dev");
            //NpmProcess.WaitForExit(15000);
            //NpmProcess = Process.Start("cmd", "npm run build:dev run --prefix ..\\..\\..\\..\\..\\src\\SecretSanta.Web");
            //UserClient userClient = new UserClient();
            HttpClient client = new HttpClient();   
            //Uri uri = new Uri(ApiUrl + "api/User", UriKind.RelativeOrAbsolute);
            User user = new User();
            user.FirstName = "inigo";
            user.LastName = "montoya";
            //UserClient userClient = new UserClient(new HttpClient());
            //userClient.PostAsync(user);
            //Console.WriteLine(user.Id);
            using var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            client.PostAsync(ApiUrl+ "/api/User", content);
            client.PostAsync(ApiUrl, content);
            string ProjectPath = testContext.DeploymentDirectory;
            
            string ApiProjectPath = ProjectPath + "\\src\\SecretSanta.Api";
            string WebProjectPath = ProjectPath + "\\src\\SecretSanta.Web";
            Console.WriteLine(ProjectPath);
            ApiHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Api\\SecretSanta.Api.csproj");
            WebHostProcess = Process.Start("dotnet.exe", "run -p ..\\..\\..\\..\\..\\src\\SecretSanta.Web\\SecretSanta.Web.csproj");
            //UserClient userClient = new UserClient(new HttpClient());
            //userClient.PostAsync(user);
            

        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            //NpmProcess.Kill();
            //NpmProcess.Close();
            //NpmProcess.CloseMainWindow();
            ApiHostProcess.Kill();
            ApiHostProcess.CloseMainWindow();
            ApiHostProcess.Close();
            WebHostProcess.Kill();
            WebHostProcess.CloseMainWindow();
            WebHostProcess.Close();
        }
        [TestInitialize]
        public void TestInitialize()
        {
            //Driver = new ChromeDriver();
            Driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));//supposedly required for Azure
            Driver.Manage().Timeouts().ImplicitWait = new System.TimeSpan(0, 0, 10);
        }

        public void TakeScreenShot(string fileName)
        {
            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile($"{fileName}.png", ScreenshotImageFormat.Png);
        }

        //----------------- CREATE GIFTS -----------------//

        public void ClickCreateButton()
        {
            Driver.Navigate().GoToUrl(new Uri(AppUrl));
            IWebElement element = Driver.FindElement(By.Id("create-gift-btn"));
            element.Click();
        }

        public void EnterGiftInformation(string title, string description, string url, int index)
        {
            IWebElement giftTitleElement = Driver.FindElement(By.Id("gift-title"));
            IWebElement giftDescriptionElement = Driver.FindElement(By.Id("gift-description"));
            IWebElement giftUrlElement = Driver.FindElement(By.Id("gift-url"));
            IWebElement giftUserSelect = Driver.FindElement(By.Id("gift-user-dropdown"));

            //Add gift information
            giftTitleElement.SendKeys(title);
            giftDescriptionElement.SendKeys(description);
            giftUrlElement.SendKeys(url);
            SelectElement selectElement = new SelectElement(giftUserSelect);
            selectElement.SelectByIndex(index);

            //Assert
            Assert.AreEqual<string>(title, giftTitleElement.GetProperty("value"));
           Assert.AreEqual<string>(description, giftDescriptionElement.GetProperty("value"));
           Assert.AreEqual<string>(url, giftUrlElement.GetProperty("value"));
        }

        public void ClickSubmitButton()
        {
            IWebElement element = Driver.FindElement(By.Id("submit"));
            element.Click();
        }

        public List<string> GetListOfGifts()
        {
            return Driver.FindElement(By.Id("gift-table-list"))
          .FindElements(By.TagName("td"))
          .Select(e => e.Text)
          .ToList();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Create_Gift_Success()
        {
            //Arrange
            string title = "TestGift",
                   description = "This gift was made by the 'Create_Gift_Success()' test.",
                   url = "www.gifttest.com";
            Thread.Sleep(2000);
            ClickCreateButton();
            Thread.Sleep(5000);
            EnterGiftInformation(title, description, url, 0);
            Thread.Sleep(5000);
            ClickSubmitButton();
            Thread.Sleep(5000);

            //Act
            var giftAttributes = GetListOfGifts();
            int index = giftAttributes.IndexOf(title);

            //Assert
            Assert.AreEqual(title,giftAttributes[index]);
            Assert.AreEqual(description, giftAttributes[index+1]);
            Assert.AreEqual(url, giftAttributes[index+2]);

            //Take screen shot upon success
            TakeScreenShot("Create_Gift_Success_Test_Screenshot");
        }

        //----------------- VALIDATE LINKS -----------------//
        [TestMethod]
        [TestCategory("Chrome")]
        public void ValidateLinks_GiftsListPage()
        {
            Driver.Navigate().GoToUrl(new Uri(AppUrl));
            IReadOnlyCollection<IWebElement> links = Driver.FindElements(By.TagName("a"));

            foreach (var link in links)
            {
                if (link.Displayed) //there is a null/empty link to create the hamburger dropdown mobile menu
                {
                    string url = link.GetAttribute("href");
                    Assert.IsTrue(Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute));
                }
            }
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
