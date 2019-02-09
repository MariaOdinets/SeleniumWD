using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestFixture]
    public class Test
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void start()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [Test]
        public void FirstTest()
        {
            int i = 0;
            while (i < 3)
            {
                MainPage mainPage = new MainPage(_driver);

                ProductPage productPage = mainPage.OpenProduct();
                productPage.AddProductToCart();

                i++;
            }

            CartPage cartPage = new CartPage(_driver);
            cartPage.Open();
            cartPage.DeleteAllProductsInCart();
        }

        [TearDown]
        public void stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}