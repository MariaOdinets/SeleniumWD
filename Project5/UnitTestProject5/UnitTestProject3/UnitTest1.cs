using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Support.Extensions;

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

            var table = driver.FindElement(By.CssSelector("#content > form > table > tbody"));

            var rowsIndexesWithZones = CheckTable(table);

            foreach (var index in rowsIndexesWithZones)
            {
                CheckZonesOrder(table, index);
                driver.ExecuteJavaScript("window.history.go(-1)");
                table = driver.FindElement(By.CssSelector("#content > form > table > tbody"));
            }

            Point2();

        }


        public void Point2()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");

            var table = driver.FindElement(By.CssSelector("#content > form > table > tbody"));

            var rows = table.FindElements(By.ClassName("row"));
            int i = 0;
            foreach (var row in rows)
            {
                var itemToClick = driver.FindElement(By.CssSelector("#content > form > table > tbody"))
                                        .FindElements(By.ClassName("row"))
                                        .ToList()[i]
                                        .FindElements(By.TagName("td"))[4];
                itemToClick.Click();
                VerifyZone();
                i++;
            }
        }

        private void VerifyZone()
        {
            var table = driver.FindElement(By.CssSelector("#table-zones > tbody"));
            var rows = table.FindElements(By.TagName("tr")).Skip(1).ToList();

            rows = rows.Take(rows.Count - 1).ToList();
            var zones = new List<string>();
            foreach (var row in rows)
            {
                var col = row.FindElements(By.TagName("td"))[2];
                var options = col.FindElements(By.TagName("option"));
                var selectedZone = options.First(e => e.Selected).Text;
                zones.Add(selectedZone);
            }
            Assert.AreEqual(zones.OrderBy(z => z).ToList(), zones);
            driver.ExecuteJavaScript("window.history.go(-1)");
        }


        private void CheckZonesOrder(IWebElement table, int index)
        {
            var row = table.FindElements(By.ClassName("row"))[index];
            var link = row.FindElements(By.TagName("td"))[4].FindElement(By.TagName("a"));

            link.Click();

            var rows = driver.FindElement(By.Id("table-zones")).FindElements(By.TagName("tr")).Skip(1).ToList();

            rows = rows.Take(rows.Count - 1).ToList();

            List<string> zoneNames = new List<string>();
            foreach (var zonerow in rows)
            {

                var columns = zonerow.FindElements(By.TagName("td"));
                zoneNames.Add(columns[2].Text);
            }

            Assert.AreEqual(zoneNames.OrderBy(z=>z).ToList(), zoneNames);

        }


        public List<int> CheckTable(IWebElement table)
        {
            var rows = table.FindElements(By.ClassName("row"));
            List<string> countries = new List<string>();


            List<int> countryIndexes = new List<int>();

            int index = 0;
            foreach (var row in rows)
            {
                var column = row.FindElements(By.TagName("td"))[4];
                var zone = row.FindElements(By.TagName("td"))[5].Text;

                int zoneCount = Int32.Parse(zone);
                if (zoneCount > 0)
                {
                    countryIndexes.Add(index);
                }

                index++;

                var link = column.FindElement(By.TagName("a"));
                var country = link.Text;
                countries.Add(country);
            }
            List<string> CountriesSorted = countries.OrderBy(e => e).ToList();
            Assert.AreEqual(CountriesSorted, countries);



            return countryIndexes;
        }

        private void Login()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/login.php");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
