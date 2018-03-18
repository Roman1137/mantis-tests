using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class ProjectCreatingTests : TestBase
    {
        [Test]
        [Repeat(25)]
        public void VerifyProjectCreation()
        {
            var newProject = new ProjectModel
                (GenerateRandomString(10), GenerateRandomString(10), rnd.Next(0, 3), InheritGlobalTerritoty(), rnd.Next(0, 1));

            App.Menu.GoToProjectsPage();
            var projectsBefore = App.Project.GetProjectsList();

            App.Menu.GoToCreateProjectPage();
            App.Project.CreateProject(newProject);

            var projectsAfter = App.Project.GetProjectsList();

            projectsBefore.Add(newProject.AllInfo);
            projectsAfter.Sort();
            projectsBefore.Sort();
            Assert.AreEqual(projectsBefore, projectsAfter);

        }
    }
}
