using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace IHF.BusinessLayer.BusinessClasses.FeatureToggle
{
    public class FeatureToggleConfigSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var serializer = new XmlSerializer(typeof(FeatureToggle));
            using (var reader = new StringReader(section.OuterXml))
            {
                return serializer.Deserialize(reader);
            }
        }

        public static FeatureToggle GetConfig()
        {
            return (FeatureToggle)ConfigurationManager.GetSection("FeatureToggle");
        }
    }
}
