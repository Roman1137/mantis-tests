﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_test.Mantis;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace mantis_test
{
    public class ProjectRemovaTests
    {
        [TestFixture]
        public class ProjectCreatingTests : TestBase
        {
            [Test]
            [Repeat(25)]
            public void VerifyProjectRemoval()
            {
                var newProject = GenerateProjectDataModel();

                const int indexOfProjectToRemove = 10;
                App.Menu.GoToProjectsPage();
                var projectsBefore = App.API.VerifyProjectExists(newProject, indexOfProjectToRemove);

                App.Menu.GoToEditProjectPage(indexOfProjectToRemove);
                App.Project.RemoveProject();

                var projectsAfter = App.API.GetProjectsList();

                projectsBefore.RemoveAt(indexOfProjectToRemove);
                projectsAfter.Sort();
                projectsBefore.Sort();
                Assert.AreEqual(projectsBefore, projectsAfter);
            }
        }
    }
}
