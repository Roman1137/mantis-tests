using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_test
{
    public class ManagementMenuHelper : HelperBase
    {
        public const string endPointForLogin = "login_page.php";
        public const string endPointForProjectsPage = "manage_proj_page.php";
        public const string endPointForCreateProjectPage = "manage_proj_create_page.php";

        public string BaseURL { get; set; }
        public ManagementMenuHelper(ApplicationManager app, string BaseUrl) : base(app)
        {
            this.BaseURL = BaseUrl;
        }

        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl(this.BaseURL + endPointForLogin);
            WaitUntiTextIsPresentInElement(".widget-main", "Вход");
        }

        public void GoToProjectsPage()
        {
            if (!IsThisPageOpened(BaseURL + endPointForProjectsPage, By.CssSelector(".active [href='/mantisbt-2.12.0/manage_proj_page.php']")))
                Driver.Navigate().GoToUrl(this.BaseURL + endPointForProjectsPage);
            WaitUntiTextIsPresentInElement("[type='submit']", "создать новый проект");
        }

        public void GoToCreateProjectPage()
        {
            if (!IsThisPageOpened(BaseURL + endPointForCreateProjectPage, By.CssSelector("#manage-project-create-form")))
                Driver.FindElement(By.CssSelector("[action='manage_proj_create_page.php']")).Click();
            WaitUntiTextIsPresentInElement("#manage-project-create-form", "Добавление проекта");
        }

        private bool IsThisPageOpened(string Url, By locator)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            if ((Driver.Url == Url) && (IsElementPresent(locator)))
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return true;
            }
            else
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return false;
            }
        }

        public void GoToEditProjectPage(int indexOfGroupToRemove)
        {
            Driver.Navigate().Refresh();
            WaitUntiTextIsPresentInElement("[type='submit']", "создать новый проект");
            Driver.FindElements(By.CssSelector("tbody"))
                .First().FindElements(By.CssSelector("tr"))[indexOfGroupToRemove]
                .FindElement(By.CssSelector("a[href]")).Click();
            WaitUntiTextIsPresentInElement("#manage-proj-update-div", "Изменить проект");
        }
    }
}
