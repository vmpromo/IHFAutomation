using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.Configuration;
using System.Web.UI;


namespace IHF.ApplicationLayer.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Page p = (Page)HttpContext.Current.Handler;
            string errorPath = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] == null ? 
                                "Unknown" : HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].ToString();
            
            try
            {
                Exception exception = Server.GetLastError().GetBaseException();

                string exceptionMessage = string.Empty;

                if (exception != null)
                {
                    exceptionMessage = exception.Message;
                }
                StringBuilder errorPage = new StringBuilder();
                errorPage.Append(ConfigurationManager.AppSettings["ErrorUrl"]);
                errorPage.Append("?");
                errorPage.Append("exceptionmessage=");
                errorPage.Append(HttpUtility.UrlEncode(exceptionMessage));
                errorPage.Append("&");
                errorPage.Append("aspxerrorpath=");
                errorPage.Append(errorPath);
                Response.Redirect(errorPage.ToString());
            }
            catch (Exception exception)
            {
                Server.ClearError();
                Response.Redirect(ConfigurationManager.AppSettings["ErrorUrl"] + 
                    "?exceptionmessage=" + exception.Message);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}