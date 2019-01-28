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
            //Login();
            Registration();
        }

        public void Registration()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/create_account");
            driver.FindElement(By.Name("firstname")).SendKeys("FirstName");
            driver.FindElement(By.Name("lastname")).SendKeys("LastName");
            driver.FindElement(By.Name("address1")).SendKeys("Street");
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).SendKeys("City");
            driver.FindElement(By.Name("country_code")).FindElement(By.CssSelector("option:nth-child(225)")).Click();
            Random rand = new Random();
            var randomNumber = rand.Next();
            string email = randomNumber + "@email.com";
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+3214785265");
            driver.FindElement(By.Name("password")).SendKeys("147852");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("147852");
            driver.FindElement(By.Name("create_account")).Click();
            driver.FindElement(By.Name("password")).SendKeys("147852");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("147852");
            driver.FindElement(By.Name("create_account")).Click();
            driver.Navigate().GoToUrl("http://localhost/litecart/en/logout");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys("147852");
            driver.Navigate().GoToUrl("http://localhost/litecart/en/logout");
        }



        //private void Login()
        //{
        //   driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
        //   driver.FindElement(By.Name("username")).SendKeys("admin");
        //   driver.FindElement(By.Name("password")).SendKeys("admin");
        //   driver.FindElement(By.Name("login")).Click();
        //}

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
