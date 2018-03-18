using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_test
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager app) : base(app) { }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type("#username", account.UserName);
            Driver.FindElement(By.CssSelector("[type='submit']")).Click();

            this.WaitUntiTextIsPresentInElement("fieldset", account.UserName);
            Type("#password", account.Password);
            Driver.FindElement(By.CssSelector("[type='submit']")).Click();
            this.WaitUntiTextIsPresentInElement(".user-info", account.UserName);
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                var element =  Driver.FindElement(By.CssSelector("li:nth-of-type(3) [data-toggle='dropdown']"));
                element.Click();
                var wait = new WebDriverWait(Driver,TimeSpan.FromSeconds(3));
                wait.Until(x => x.FindElements(By.CssSelector("[aria-expanded='true']")).Count >= 1);
                Driver.FindElement(By.CssSelector("[href='/mantisbt-2.12.0/logout_page.php']")).Click();
                WaitUntiTextIsPresentInElement(".widget-main", "Вход");
            }
        }
        public bool IsLoggedIn()
        {
            return !IsElementPresent(By.CssSelector(".login-logo"));
        }
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                   && GetLoggedUserName() == account.UserName;
        }

        private string GetLoggedUserName()
        {
            return Driver.FindElement(By.CssSelector(".user-info")).Text;
        }
    }
}
