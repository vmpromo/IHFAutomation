using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IHF.BusinessLayer.BusinessClasses.FeatureToggle
{
    public class FeatureToggle
    {
        [XmlArray(ElementName = "PageToggles")]
        [XmlArrayItem(ElementName = "Page")]
        public PageToggle[] PageToggles
        {
            get
            {
                return _togglesByUrl.Values.ToArray();
            }
            set
            {
                _togglesByUrl = value.ToDictionary(toggle => toggle.Url.ToLower(), toggle => toggle);
            }
        }

        private Dictionary<string, PageToggle> _togglesByUrl = null;

        public bool IsPageEnabledForUser(string user, string url)
        {
            url = url.ToLower();
            if (_togglesByUrl == null || !_togglesByUrl.ContainsKey(url))
            {
                return true;
            }

            var pageToggle = _togglesByUrl[url];
            return !pageToggle.RuleEnabled || pageToggle.ShouldShowPage(user);
        }
    }
}
