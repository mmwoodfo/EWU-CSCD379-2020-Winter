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
            ClickCreateButton();
            EnterGiftInformation("Title", "Description", "www.url.com", 1);
            ClickSubmitButton();
            Thread.Sleep(3000);

            var giftAttributes = GetListOfGifts();

            int index = giftAttributes.IndexOf("Title");

            //Assert that gift was added
            Assert.AreEqual("Title",giftAttributes[index]);
            Assert.AreEqual("Description", giftAttributes[index+1]);
            Assert.AreEqual("www.url.com", giftAttributes[index+2]);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            Driver.Quit();
        }
    }
}
