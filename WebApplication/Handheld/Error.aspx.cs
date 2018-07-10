using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exceptionMessage = "Not known";

            if (Request.QueryString["exceptionmessage"] != null)
            {
                exceptionMessage = Request.QueryString["exceptionmessage"].ToString();
            }

            errorMessage.InnerHtml = exceptionMessage;
        }
    }
}