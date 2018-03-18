using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_test
{
    public class ProjectRemovaTests
    {
        [TestFixture]
        public class ProjectCreatingTests : TestBase
        {
            ProjectModel newProject = new ProjectModel
                (GenerateRandomString(10), GenerateRandomString(10), rnd.Next(0, 3), InheritGlobalTerritoty(), rnd.Next(0, 1));

            [Test]
            [Repeat(25)]
            public void VerifyProjectRemoval()
            {
                const int indexOfGroupToRemove = 5;
                App.Menu.GoToProjectsPage();
                var projectsBefore = App.Project.VerifyProjectExists(newProject, indexOfGroupToRemove);

                App.Menu.GoToEditProjectPage(indexOfGroupToRemove);
                App.Project.RemoveProject();

                var projectsAfter = App.Project.GetProjectsList();

                projectsBefore.RemoveAt(indexOfGroupToRemove);
                projectsAfter.Sort();
                projectsBefore.Sort();
                Assert.AreEqual(projectsBefore, projectsAfter);
            }
        }
    }
}
