using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [Test]
        public void FirstTest()
        {
            Login();

            var elements = driver.FindElements(By.CssSelector(".product"));
            foreach (var element in elements)
            {
                var sticker = element.FindElements(By.CssSelector(".sticker"));
                Assert.NotNull(sticker);
            }

        }


        private void Login()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/rubber-ducks-c-1/");
            
        }
            

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
