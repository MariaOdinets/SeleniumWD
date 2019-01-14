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
            var menus = driver.FindElements(By.Id("app-"));

            int i = 1;   
            
            foreach(var menuitem in menus)
            {
                VerifyMenuAndChilds($"//*[@id=\"box-apps-menu\"]/li[{i}]");
                i++;
            }
        }

        private void VerifyMenuAndChilds(string menuxpath)
        {
            var element = driver.FindElement(By.XPath(menuxpath));

            element.Click();
            Verifyh1();
            element = driver.FindElement(By.XPath(menuxpath));
            List<string> childelementsIds = element.FindElements(By.TagName("li")).Select(e => e.GetAttribute("id")).ToList();

            foreach (string id in childelementsIds)
            {
                var childMenu = driver.FindElement(By.Id(id));
                childMenu.Click();
                Verifyh1();

            }
        }

        private void Verifyh1()
        {
            var element = driver.FindElement(By.TagName("h1"));
            Assert.NotNull(element);
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
