using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static bool InheritGlobalTerritoty()
        {
            var a = rnd.Next(1, 2);
            return (a == 1);
        }
    }
}
