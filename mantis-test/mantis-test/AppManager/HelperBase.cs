using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_test
{
    public class HelperBase
    {
        public ApplicationManager Manager { get; set; }
        public IWebDriver Driver { get; set; }
        public HelperBase(ApplicationManager manager)
        {
            this.Manager = manager;
            this.Driver = manager.Driver;
        }

        public void Type(string locator, string text)
        {
            if (text != null)
            {
                Driver.FindElement(By.CssSelector(locator)).Clear();
                Driver.FindElement(By.CssSelector(locator)).SendKeys(text);
            }
        }
        public bool IsElementPresent(By locator)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            ICollection<IWebElement> collection = Driver.FindElements(locator);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            if (collection.Count == 0)
                return false;
            return collection.First().Displayed;
        }

        public void WaitUntiTextIsPresentInElement(string locator, string text)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElement(Driver.FindElement(By.CssSelector(locator)), text));
        }
    }
}
