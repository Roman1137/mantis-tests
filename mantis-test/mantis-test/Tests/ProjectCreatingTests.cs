using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class ProjectCreatingTests : TestBase
    {
        [Test]
        //[Repeat(25)]
        public void VerifyProjectCreation()
        {
            var newProject = GenerateProjectDataModel();

            App.Menu.GoToProjectsPage();
            var projectsBefore = App.API.GetProjectsList();

            App.API.CreateProject(newProject);

            var projectsAfter = App.API.GetProjectsList();

            projectsBefore.Add(newProject);
            projectsAfter.Sort();
            projectsBefore.Sort();
            Assert.AreEqual(projectsBefore, projectsAfter);
        }
    }
}
