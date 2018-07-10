using System;
using System.Configuration;
using System.Web;
using IHF.BusinessLayer.Util;

namespace IHF.Security.UserManagement
{
    public class IHFNavigationHttpModule : IHttpModule
    {
        #region "private functions"

        private bool BrowserCheckEnabled()
        {
            return Convert.ToBoolean(ConfigurationManager
                    .AppSettings[Definitions.CONFIG_BROWSER_CHECK]
                    .ToString());
        }

        #endregion

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += new EventHandler(context_AuthoriseRequest);
        }

        public void context_AuthoriseRequest(object sender, EventArgs e)
        {
            if (BrowserCheckEnabled())
            {
                bool isDevice = new Browser().IsDevice();
                string requestedUrl = HttpContext.Current.Request.RawUrl;

                string handheldHomeUrl = ConfigurationManager.AppSettings["HHomeUrl"];
                string desktopHomeUrl = ConfigurationManager.AppSettings["HomeUrl"];
                if ((handheldHomeUrl == null || handheldHomeUrl == string.Empty) ||
                    (desktopHomeUrl == null || desktopHomeUrl == string.Empty))
                {
                    throw new Exception("Home page URLs not found in the Appsettings section of web.config");
                }

                string handheldErrorUrl = ConfigurationManager.AppSettings["HErrorUrl"];
                string desktopErrorUrl = ConfigurationManager.AppSettings["ErrorUrl"];
                if (handheldErrorUrl == null || handheldErrorUrl == string.Empty)
                {
                    throw new Exception("Handheld error page Url not found in the Appsettings section of web.config");
                }

                if (!isDevice)
                {
                    if (requestedUrl.ToLower().Contains(handheldHomeUrl.Substring(1)))
                    {
                        HttpContext.Current.Response.Redirect(desktopHomeUrl);
                    }
                    else if (requestedUrl.ToLower().Contains("/" + UrlRoot.Handheld.ToString().ToLower() + "/"))
                    {
                        throw new Exception("Cannot access handheld screens from desktop.");
                    }

                }

                if (isDevice)
                {
                    string loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
                    if (loginUrl == null || loginUrl == string.Empty)
                    {
                        throw new Exception("LoginUrl entry not found in AppSettings section of Web.config");
                    }

                    if (requestedUrl.ToLower().Contains(desktopHomeUrl.Substring(1)))
                    {
                        HttpContext.Current.Response.Redirect(handheldHomeUrl);
                    }
                    else if (requestedUrl.ToLower().Contains(desktopErrorUrl.Substring(1)))
                    {
                        int queryIndex = requestedUrl.IndexOf("?");
                        if (queryIndex >= 0)
                        {
                            handheldErrorUrl = handheldErrorUrl + requestedUrl.Substring(queryIndex);
                        }
                        HttpContext.Current.Response.Redirect(handheldErrorUrl);
                    }
                    else if (!requestedUrl.ToLower().Contains(loginUrl.Substring(1)) &&
                                requestedUrl.ToLower().Contains("/" + UrlRoot.Pages.ToString().ToLower() + "/"))
                    {
                        throw new Exception("Cannot access desktop screen from handheld.");

                    }
                }
            }
             
        }

        #endregion
    }
}
