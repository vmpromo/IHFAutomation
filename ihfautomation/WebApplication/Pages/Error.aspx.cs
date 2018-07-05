using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace IHF.ApplicationLayer.Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorPath = "Not known";
            string exceptionMessage = "Not known";

            if (Request.QueryString["aspxerrorpath"] != null)
            {
                errorPath = Request.QueryString["aspxerrorpath"].ToString();
            }

            if (Request.QueryString["exceptionmessage"] != null)
            {
                exceptionMessage =
                    Request.QueryString["exceptionmessage"].ToString();
            }

            errorIn.InnerText = errorPath;
            errorOn.InnerText = DateTime.Today.ToString("dd/MM/yyyy") + 
                                " , " + 
                                DateTime.Now.ToShortTimeString() + 
                                " hrs";
            
            errorMessage.InnerHtml = exceptionMessage;

        }
    }
}