using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace mantis_test
{
    public class ApplicationManager
    {
        public IWebDriver Driver { get; set; }

        public string BaseUrl { get; set; }
        public LoginHelper Auth { get; set; }
        public ManagementMenuHelper Menu { get; set; }
        public ProjectManagementHelper Project { get; set; }
        public  APIHelper API { get; set; }

        public static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            BaseUrl = "http://localhost/mantisbt-2.12.0/";
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //initialize helper classes
            Auth = new LoginHelper(this);
            Menu = new ManagementMenuHelper(this, BaseUrl);
            Project = new ProjectManagementHelper(this);
            API = new APIHelper(this);
        }
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Menu.OpenHomePage();
                app.Value = newInstance;
            }
            return app.Value;
        }
        ~ApplicationManager()
        {
            Auth.Logout();
            try
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
            catch (Exception)
            {
            }
        }
    }
}
