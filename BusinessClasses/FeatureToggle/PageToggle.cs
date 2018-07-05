using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IHF.BusinessLayer.BusinessClasses.FeatureToggle
{
    public class PageToggle
    {
        [XmlAttribute]
        public string Url { get; set; }

        [XmlAttribute]
        public bool RuleEnabled { get; set; }

        public string EnableForUsers
        {
            get { return string.Join(",", EnableForUsersList); }
            set
            {
                EnableForUsersList = value
                    .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(username => username.ToLower())
                    .ToArray();
            }
        }

        private string[] EnableForUsersList { get; set; }

        public bool ShouldShowPage(string user)
        {
            return EnableForUsersList.Contains(user.ToLower());
        }
    }
}
