using OpenQA.Selenium;

namespace UnitTestProject1
{
    class MainPage
    {
        private readonly IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ProductPage OpenProduct()
        {
            _driver.Navigate().GoToUrl("http://localhost/litecart");

            string selector;
            IWebElement element;
            selector = "#box-most-popular > div > ul > li:nth-child(1) > a.link";
            element = _driver.FindElement(By.CssSelector(selector));
            element.Click();

            return new ProductPage(_driver);
        }
    }
}
