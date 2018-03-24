using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_test.Mantis;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_test
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager app) : base(app) { }
        readonly AccountData account = new AccountData("administrator", "root");

        public List<ProjectData> GetProjectsList()
        {
            var client = new MantisConnectPortTypeClient();
            return client.mc_projects_get_user_accessible(account.UserName, account.Password).ToList();
        }

        public List<ProjectData> VerifyProjectExists(ProjectData newProject, int indexOfGroupToRemove)
        {
            var listOfProjects = GetProjectsList();
            while (listOfProjects.Count <= indexOfGroupToRemove )
            {
                CreateProject(newProject);
                listOfProjects = GetProjectsList();
            }

            return listOfProjects;
        }

        public void CreateProject(ProjectData newProject)
        {
            var client = new MantisConnectPortTypeClient();
            client.mc_project_add(account.UserName, account.Password, newProject);
        }
    }
}
