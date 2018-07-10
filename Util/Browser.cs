using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace IHF.BusinessLayer.Util
{
    public class Browser
    {
        public bool IsDevice()
        {
            string agent = HttpContext.Current.Request.UserAgent;

            string handheldBrowser = ConfigurationManager.AppSettings["HandheldBrowser"];

            bool isHandheld;

            try
            {
                isHandheld = agent.ToLower().Contains(handheldBrowser);

            }
            catch (Exception)
            {
                isHandheld = false;
            }
            
            return isHandheld;
        }
    }
}
