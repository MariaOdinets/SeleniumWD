using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;

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
            GeneralPage();
            Information();
            Prices();
            Presence();
        }

        private void GeneralPage()
        {
            driver.FindElement(By.CssSelector("#box-apps-menu > li:nth-child(2) > a > span.name")).Click();
            driver.FindElement(By.CssSelector("#content > div:nth-child(6) > a:nth-child(2)")).Click();
            var td=  driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(1) > td"));
            td.FindElements(By.Name("status"))[0].Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("Pirate");
            driver.FindElement(By.Name("code")).SendKeys("12345");
            var gender = driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(7) > td > div"));
            gender.FindElements(By.Name("product_groups[]"))[2].Click();
            var quantity = driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(8) > td > table > tbody > tr > td:nth-child(1) > input[type=\"number\"]"));
            quantity.Clear();
            quantity.SendKeys("12");
            var uploadElement = driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(9) > td > table > tbody > tr:nth-child(1) > td > input[type=\"file\"]"));

            var path = "duck.jpg";
            path = Path.Combine(Directory.GetCurrentDirectory(), path);
            uploadElement.SendKeys(path);
            driver.FindElement(By
                .CssSelector("#tab-general > table > tbody > tr:nth-child(10) > td > input[type=\"date\"]"))
                .SendKeys("01012019");
            driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(11) > td > input[type=\"date\"]")).SendKeys("01012021");
        }

        private void Information()
        {
            driver.FindElement(By.CssSelector("#content > form > div > ul > li:nth-child(2) > a")).Click();
            Thread.Sleep(1000);
            var manufacturer = driver.FindElement(By.Name("manufacturer_id"));
            manufacturer.Click();
            manufacturer.FindElements(By.TagName("option"))[1].Click();
            driver.FindElement(By.CssSelector("#tab-information > table > tbody > tr:nth-child(3) > td > input[type=\"text\"]")).SendKeys(
                "duck, rubber duck, pirate");
            driver.FindElement(By.CssSelector("#tab-information > table > tbody > tr:nth-child(4) > td > span > input[type=\"text\"]")).SendKeys("rubber duck");
            driver.FindElement(By.CssSelector(
                    "#tab-information > table > tbody > tr:nth-child(5) > td > span > div > div.trumbowyg-editor"))
                .SendKeys("test description");
            driver.FindElement(By.CssSelector("#tab-information > table > tbody > tr:nth-child(6) > td > span > input[type=\"text\"]")).SendKeys(
                "Duck");
            driver.FindElement(By.CssSelector("#tab-information > table > tbody > tr:nth-child(7) > td > span > input[type=\"text\"]")).SendKeys(
                "Duck");

        }

        private void Prices()
        {
            driver.FindElement(By.CssSelector("#content > form > div > ul > li:nth-child(4) > a")).Click();
            driver.FindElement(By.Name("purchase_price")).SendKeys("1");
            var currency = driver.FindElement(By.Name("purchase_price_currency_code"));
            currency.Click();
            currency.FindElements(By.TagName("option"))[1].Click();
            driver.FindElement(By.Name("prices[USD]")).SendKeys("10");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("8");
            driver.FindElement(By.Name("save")).Click();

        }

        private void Presence()
        {
            var table =  driver.FindElement(By.CssSelector("#content > form > table"));
            var rows = table.FindElements(By.CssSelector("tr.row"));
            var cols = rows.Select(r => r.FindElements(By.TagName("td"))[2]).Select(c=>c.Text).ToList();

            Assert.True(cols.Contains("Pirate"));

        }
    
        private void Login()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
