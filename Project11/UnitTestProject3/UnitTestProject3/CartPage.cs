using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
            string selector;
            IWebElement element;
            selector = "#cart > a.link";
            element = _driver.FindElement(By.CssSelector(selector));
            element.Click();
        }

        public void DeleteAllProductsInCart()
        {
            int initialDucksCount = GetDucksCountInCartTable();
            while (initialDucksCount != 0)
            {
                Delete();
                WebDriverWait waitUntilTableRefresh = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
                waitUntilTableRefresh.Until(webDriver => GetDucksCountInCartTable() == initialDucksCount - 1);
                initialDucksCount--;
            }
        }

        private void Delete()
        {
            var element = _driver.FindElement(By.Name("remove_cart_item"));
            element.Click();
        }

        private int GetDucksCountInCartTable()
        {
            return _driver.FindElements(By.CssSelector("td.item")).Count;
        }
    }
}