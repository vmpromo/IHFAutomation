// Name: IHFUserAccessHttpModule.cs
// Type: HttpModule for user authorization
// Description: HttpModule for user authorization
//
//$Revision:   1.17  $
//
// Version   Date        Author    Reason
//  1.15     19/10/11    ITMK      Added aplication name filter to suport IHFDSH application
//  1.16     20/10/11    ITMK      Commented dashboard filter code.
//  1.17     23/11/11    ITMK      Un-Commented dashboard filter code.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using System.Web.UI;



namespace IHF.Security.UserManagement
{
    public sealed class IHFUserAccessHttpModule : IHttpModule
    {
        #region local members

        ActivityLogDAO _log = new ActivityLogDAO();

        #endregion

        #region "private functions"

        private bool DisableNavigationSecurity()
        {
            return ConfigurationManager
                    .AppSettings[Definitions.CONFIG_NAVIGATION_SECURITY]
                    .ToString()
                    .ToUpper() == "FALSE";
        }

        #endregion

        #region IHttpModule Members

        public void Dispose()
        {
            //do nothing
        }

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += new EventHandler(context_AuthorizeRequest);
        }

        public void context_AuthorizeRequest(object sender, EventArgs e)
        {
            if (DisableNavigationSecurity())
            {
                HttpApplication application = (HttpApplication)sender;
                HttpRequest request = application.Request;
                HttpResponse response = application.Response;

                string loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
                if (loginUrl == null || loginUrl.Trim() == String.Empty)
                {
                    throw new Exception("LoginUrl entry not found in appSettings section of Web.config");
                }

                string errorUrl = ConfigurationManager.AppSettings["ErrorUrl"];
                if (errorUrl == null || errorUrl.Trim() == String.Empty)
                {
                    throw new Exception("ErrorUrl entry not found in appSettings section of Web.config");
                }

                int i = request.Path.LastIndexOf("/");
                string page = request.Path.Substring(i + 1, (request.Path.Length - (i + 1)));

                if (page != "WebResource.axd" && page != null)
                {
                    int j = loginUrl.LastIndexOf("/");
                    string loginPage = loginUrl.Substring(j + 1, (loginUrl.Length - (j + 1)));

                    int k = errorUrl.LastIndexOf("/");
                    string errorPage = errorUrl.Substring(k + 1, (errorUrl.Length - (k + 1)));

                    int l = page.LastIndexOf(".");
                    string extension = "";
                    if (page.Length - (l + 1) < 4)
                    {
                        // URL string does not contain a Querystring
                        extension = page.Substring(l + 1, (page.Length - (l + 1)));
                    }
                    else
                    {
                        // URL string may contain a querystring therefore only extract the four characters
                        // that follow the last "."
                        extension = page.Substring(l + 1, 4);
                    }

                    // Only check authority of the page requested is not the login page 
                    // Or the error page 
                    // And has an .aspx extension
                    if (!(page.Trim().ToUpper().Equals(loginPage.ToUpper()))
                        && !(page.Trim().ToUpper().Equals(errorPage.ToUpper()))
                        && (extension.Trim().ToUpper().Equals("ASPX")))
                    {
                        MembershipDAO membershipDAO = new MembershipDAO();
                        if (!new Browser().IsDevice()
                            || new Browser().IsDevice()
                            && page.ToLower().Equals("home.aspx"))
                            page = "%/" + page;
                        //page = "%/" + page;

                        /**/
                        string applicationName = ConfigurationManager.AppSettings[Definitions.CONFIG_APPLICATION_NAME];

                        if (request.Path.ToLower().Contains("/dashboard/"))
                        {
                            applicationName = ConfigurationManager.AppSettings[Definitions.CONFIG_IHFDASH_APPLICATION_NAME];
                        }
                        /**/

                        if (!membershipDAO.AuthorisedToPage(application.User.Identity.Name, page, applicationName))
                        {
                            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                                response.Redirect(request.ApplicationPath + loginUrl + "?ReturnUrl=" + request.Path, true);
                            else
                                throw new Exception("Not authorised to page!");
                        }
                        else
                        {

                            //_log.SaveUserActivity(new UserActivity
                            //{
                            //    AppSystem = (int)ActivityLogEnum.AppSystem.OMS,


                            //});

                        }
                    }
                }
            }
        }


        #endregion
    }
}
