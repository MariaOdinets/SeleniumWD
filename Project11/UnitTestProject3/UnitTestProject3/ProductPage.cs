using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    class ProductPage
    {
        private readonly IWebDriver _driver;

        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddProductToCart()
        {
            string selector;
            IWebElement element;
            selector = "#box-product > div.content > div.information > div.buy_now > form > table > tbody > tr > td > button";
            element = _driver.FindElement(By.CssSelector(selector));
            element.Click();

            AcceptAlert();
            int currentQuantity = GetQuantity();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
            //todo  wait.Until(webDriver => GetQuantity() == quantity + 1);
        }

        private void AcceptAlert()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = _driver.SwitchTo().Alert();
            alert.Accept();
        }

        private int GetQuantity()
        {
            string selector;
            IWebElement element;
            selector = "#cart > a.content > span.quantity";
            element = _driver.FindElement(By.CssSelector(selector));
            return Int32.Parse(element.Text);
        }
    }
}