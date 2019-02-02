using System;
using System.Collections.Generic;
using System.Linq;
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
            Login();
            Countries();
        }

        public void Countries()
        {
            driver.Navigate().GoToUrl(" http://localhost/litecart/admin/?app=countries&doc=countries");
            string selector;
            IWebElement element;

            selector = "#content > div > a";
            element = driver.FindElement(By.CssSelector(selector));
            element.Click();
            selector = "#content > form > table:nth-child(2) > tbody > tr:nth-child(2) > td > a > i";

            IReadOnlyCollection<IWebElement> list = driver.FindElements(By.CssSelector("i.fa.fa-external-link"));
            foreach (var webElement in list)
            {
                webElement.Click();

                // verify that new window was opened
                Assert.AreEqual(2, driver.WindowHandles.Count);


                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles.Last());
            }
           
            

        }

        
            
            //selector = "#cart > a.link";
            //element = driver.FindElement(By.CssSelector(selector));
            //element.Click();

       


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
