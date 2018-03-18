using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_test
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager app) : base(app) { }

        public void CreateProject(ProjectModel newProject)
        {
            FillAllForms(newProject);
            SumbitProjectCreation();
        }

        private void SumbitProjectCreation()
        {
            Driver.FindElement(By.CssSelector("[type='submit']")).Click();
            WaitUntiTextIsPresentInElement(".alert-success.center", "Действие успешно выполнено.");
            Driver.FindElement(By.CssSelector($"[href='{ManagementMenuHelper.endPointForProjectsPage}']")).Click();
            WaitUntiTextIsPresentInElement("[type='submit']", "создать новый проект");
        }

        private void FillAllForms(ProjectModel newProject)
        {
            Type("#project-name",newProject.Name);
            SelectFromDropDown("#project-status", newProject.State);
            SelectCheckBox(newProject.InheritTheGlobalCategory);
            SelectFromDropDown("#project-view-state", newProject.Vision);
            Type("#project-description", newProject.Description);
        }

        private void SelectCheckBox(bool newProjectInheritTheGlobalCategory)
        {
            if (newProjectInheritTheGlobalCategory == false)
            {
                Driver.FindElement(By.CssSelector(".lbl")).Click();
            }    
        }

        private void SelectFromDropDown(string locatorOfDropDown, string valueToSelect)
        {
            var element = Driver.FindElement(By.CssSelector(locatorOfDropDown));
            if (element.Text != valueToSelect)
            {
                new SelectElement(Driver.FindElement(By.CssSelector(locatorOfDropDown))).SelectByText(valueToSelect);
            }
        }

        public List<string> GetProjectsList()
        {
            var projectCollection = new List<string>();
            var projects = Driver.FindElements(By.CssSelector("tbody")).First().FindElements(By.CssSelector("tr"));
            foreach (var project in projects)
            {
                projectCollection.Add(project.Text);
            }

            return projectCollection;
        }

        public List<string> VerifyProjectExists(ProjectModel newProject, int indexOfGroupToRemove)
        {
            var listOfAllProjects = GetProjectsList();
            var projectExists = listOfAllProjects.Count >= indexOfGroupToRemove + 1;
;
            while (!projectExists)
            {
                Manager.Menu.GoToCreateProjectPage();
                CreateProject(newProject);
                listOfAllProjects = GetProjectsList();
                projectExists = listOfAllProjects.Count >= indexOfGroupToRemove + 1;
            }

            return listOfAllProjects;
        }

        public void RemoveProject()
        {
           Driver.FindElement(By.CssSelector("#project-delete-form [type='submit']")).Click();
            WaitUntiTextIsPresentInElement(".alert-warning", "удалить этот проект");
            Driver.FindElement(By.CssSelector("[type='submit']")).Click();
            WaitUntiTextIsPresentInElement("[type='submit']", "создать новый проект");
        }
    }
}
