// Name: Login.aspx.cs
// Type: class file 
// Description: Code behind class for Login screen
//
//$Revision:   1.12  $
//
// Version   Date        Author     Reason
//  1.0      27/05/11    itmk       Initial Revision
//  1.1      01/06/11    itmk       navigation changes
//  1.2      01/06/11    itmk       navigation security complete!
//  1.3      17/06/11    itmk       NO CHANGES
//  1.4      24/06/11    itmk       multiple changes to handhelp screens, desktop master page, styles
//  1.5      28/06/11    itmk       no changes
//  1.6      30/06/11    itmk       handheld design complete
//  1.7      04/07/11    itmk       changes to UI design
//  1.8      13/07/11    itmk       refactored forced locate code and applied font changes to handheld and login screens
//  1.9      23/08/11    itmk       Despatch - Interim version
//  1.10     05/12/11    itmk       no change
//  1.11     05/12/11    itmk       no change
//  1.12     08/05/17    M Cackett  Cross border returns - read user and store details from URL.
//  1.13     10/05/17    M Cackett  Always accept user id if passed in query string and
//                                  validate user as "POS User".

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.OracleClient;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            if (!IsPostBack)
            {
                const string POSuser = "POS user";
                string userID="";
                string device="";
                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                //Check query parameters
                if (Request.QueryString["username"] != null)
                {
                    userID = Request.QueryString["username"];
                }

                if (Request.QueryString["device"] != null)
                {
                    device = Request.QueryString["device"];
                }

                    if (userID != "")
                    {
                        //Authenticate 
                        FormsAuthentication.SetAuthCookie(POSuser, true);

                        //Redirect to returns url
                        Response.Redirect(Request.Url.Scheme + "://" +
                                      Request.Url.Host + ":" +
                                      Request.Url.Port +
                                      "/Pages/Returns/Returns.aspx?username=" + userID +
                                      "&device=" + device);
                    }
            }

            if (new Browser().IsDevice())
            {
                CheckBox chk = (CheckBox) this.IHFLogin.FindControl("RememberMe");
                chk.Visible = false;
            }
            this.IHFLogin.Focus();
        }

        protected void IHFLogin_Init(object sender, EventArgs e)
        {

        }

        protected void IHFLogin_LoggedIn(object sender, EventArgs e)
        {

        }
        
    }
}