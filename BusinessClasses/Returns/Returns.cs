using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace IHF.BusinessLayer.BusinessClasses.Returns
{
    public class Returns:IReturns
    {
        public string Server { get; set; }

        public string ServerNumber { get; set; }

        public string Port { get; set; }

        public string DefaultPage { get; set; }

        public Returns(string databaseName)
        {
            Server       = databaseName.Replace("oms", "ihf");
            ServerNumber = ConfigurationManager.AppSettings["servernumber"].ToString();
            Port         = ConfigurationManager.AppSettings["returns_port"].ToString();
            DefaultPage  = ConfigurationManager.AppSettings["returns_default_page"].ToString();
        }

        
        public string FormUrl()
        {
            return "http://" + Server + ServerNumber + Port + DefaultPage;
        }

    }
}