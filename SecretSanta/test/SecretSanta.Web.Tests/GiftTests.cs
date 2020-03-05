using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SecretSanta.Web.Tests
{
    [TestClass]
    public class GiftTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }
        [NotNull]
        private IWebDriver? Driver { get; set; }
        string AppUrl { get; } = "https://localhost:44394/Gifts";

        [TestInitialize]
        public void TestInitialize()
        {
            Driver = new ChromeDriver();
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

            ClickCreateButton();
            EnterGiftInformation(title, description, url, 1);
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
