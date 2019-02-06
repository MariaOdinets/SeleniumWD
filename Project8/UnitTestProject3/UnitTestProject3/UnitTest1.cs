using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            string selector;
            IWebElement element;
            int i = 0;
            while (i < 3)
            {
                driver.Navigate().GoToUrl("http://localhost/litecart");
                OpenProduct();

                int quantity = GetQuantity();

                AddProductToCart();
                AcceptAlert();
                int currentQuantity = GetQuantity();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                wait.Until(webDriver => GetQuantity() == quantity + 1);
                i++;
            }

            selector = "#cart > a.link";
            element = driver.FindElement(By.CssSelector(selector));
            element.Click();

            int initialDucksCount = GetDucksCountInCartTable();
            while (initialDucksCount != 0)
            {
                Delete();
                WebDriverWait waitUntilTableRefresh = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                waitUntilTableRefresh.Until(webDriver => GetDucksCountInCartTable() == initialDucksCount - 1);
                initialDucksCount--;
            }
          
        }

        private int GetDucksCountInCartTable()
        {
            return driver.FindElements(By.CssSelector("td.item")).Count;
        }

        private void Delete()
        {
            string selector;
            IWebElement element;
         
            element = driver.FindElement(By.Name("remove_cart_item"));
            element.Click();
        }

        private int GetQuantity()
        {
            string selector;
            IWebElement element;
            selector = "#cart > a.content > span.quantity";
            element = driver.FindElement(By.CssSelector(selector));
            return Int32.Parse(element.Text);
        }

        private void AcceptAlert()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        private void AddProductToCart()
        {
            string selector;
            IWebElement element;
            selector = "#box-product > div.content > div.information > div.buy_now > form > table > tbody > tr > td > button";
            element = driver.FindElement(By.CssSelector(selector));
            element.Click();
        }

        private void OpenProduct()
        {
            string selector;
            IWebElement element;
            selector = "#box-most-popular > div > ul > li:nth-child(1) > a.link";
            element = driver.FindElement(By.CssSelector(selector));
            element.Click();
        }


        //private void Login()
        //{
        //    driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
        //    driver.FindElement(By.Name("username")).SendKeys("admin");
        //    driver.FindElement(By.Name("password")).SendKeys("admin");
        //    driver.FindElement(By.Name("login")).Click();
        //}

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
