using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using mantis_test.Mantis;

namespace mantis_test
{
    public class ProjectModel 
    {
        public ProjectModel() { }
        public ProjectModel(string name, string description, int indexOfState = 0, bool inheritTheGlobalCategory = false, int indexOfVisionOption = 0)
        {
            this.Name = name;
            this.State = listOfState[indexOfState];
            this.InheritTheGlobalCategory = inheritTheGlobalCategory;
            this.Vision = listOfVisionOptions[indexOfVisionOption];
            this.Description = description;

        }
        public string Name { get; set; }
        public string State { get; set; }

        readonly List<string> listOfState = new List<string>()
        {
            "в разработке",
            "выпущен",
            "стабильный",
            "устарел"
        };
        public bool InheritTheGlobalCategory { get; set; }
        public string Vision { get; set; }
        readonly List<string> listOfVisionOptions = new List<string>()
        {
            "публичная",
            "приватная"
        };
        public string Description { get; set; }
        private string allInfo;
        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    var a = (Vision == listOfVisionOptions[0]) ? "публичный" : "приватный";
                    return $"{Name} {State} {a} {Description}";
                }
            }
            set => allInfo = (value);
        }
        public ObjectRef GetViewState()
        {
            var listOfViewStates = new List<string>()
            {
                "public",
                "private"
            };
            return new ObjectRef{ name = listOfViewStates[new Random().Next(0, 1)]};
        }
        public ObjectRef GetStatus()
        {
            var listOfStatuses= new List<string>()
            {
                "development",
                "release",
                "stable",
                "obsolete"
            };
            return new ObjectRef { name = listOfStatuses[new Random().Next(0, 3)] };
        }
    }
}
