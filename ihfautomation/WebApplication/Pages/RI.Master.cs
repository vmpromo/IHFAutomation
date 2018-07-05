using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web.Pages
{
    public partial class test1 : System.Web.UI.MasterPage
    {
        public string ErrorMessage 
        { 
            set
            {
                Error.InnerHtml = value;
            } 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["SiteMapRoot"] = null;
        }
        protected void LoginName_Init(object sender, EventArgs e)
        {
            string welcomeText = "Welcome, ";
            string userDisplayName = string.Empty;
            userDisplayName = Membership.GetUser().UserName;
            LoginName.FormatString = welcomeText + userDisplayName;
        }

        protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            
        }
    }
}