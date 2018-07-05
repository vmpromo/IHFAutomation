// Name: IHFDashboard.master.cs
// Type: Master page for IHF DASHBOARD
// Description: Used for dashboard reporting screens
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      19/10/11    ITMK      Released version
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class IHFDashboard : System.Web.UI.MasterPage
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
    }
}