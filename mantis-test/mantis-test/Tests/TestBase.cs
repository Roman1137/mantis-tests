using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_test.Mantis;
using NUnit.Framework;

namespace mantis_test
{
    [TestFixture]
    public class TestBase
    {
        public ApplicationManager App { get; set; }
        [SetUp]
        public void SetupApplicationManager()
        {
            App = ApplicationManager.GetInstance();
            App.Auth.Login(new AccountData("administrator", "root"));
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public ProjectData GenerateProjectDataModel()
        {
            var newProjectModel = new ProjectData()
            {
                name = GenerateRandomString(10),
                description = GenerateRandomString(15),
                view_state = new ProjectModel().GetViewState(),
                status = new ProjectModel().GetStatus(),
            };

            return newProjectModel;
        }
    }
}
