using System;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using System.Web;
using System.Configuration;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            if (Request.QueryString["Signout"] != null &&  
                Request.QueryString["Signout"].ToString() == "1")
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Clear();
                Response.Redirect(ConfigurationManager.AppSettings["LoginUrl"]);
            }

            //this.Master.ErrorMessage = "Error";
            //this.Master.DisplayMessage = true;

            int input = (int)HandheldInput.Root;
            try
            {
                input = int.Parse(this.Master.ParentValue);
            }
            catch (Exception)
            {
                input = (int)HandheldInput.Root;
                this.Master.ErrorMessage = "Invalid input. Try again.";
                this.Master.DisplayMessage = true;
            }

            string user = User.Identity.Name;
            string application = ConfigurationManager.AppSettings["applicationName"];


            MainMenu menu = new MainMenu(user, application);
            HtmlTableCell menuItem;
            
            StringBuilder script = new StringBuilder();


            script.Append("function checkAndPost(){\n");
            script.Append("\t var barcode = document.getElementById('" + 
                this.Master.BarcodeID + "');\n");
            script.Append("\t if (document.activeElement.id == barcode.id){\n");

            var subMenu = menu.GetPagesForParent(input);
            for (int j = 0; j < subMenu.Count; j++)
            {
                menuItem = (HtmlTableCell)this.Master.FindControl("menuItem" + (j + 1));
                menuItem.InnerText = (j + 1) + " - " + subMenu[j].Caption.ToString();
                menuItem.Visible = true;

                script.Append("\t \t if (barcode.value == " + (j + 1) + "){\n");
                if (subMenu[j].PageChildInd == "0")
                {
                    script.Append("\t \t \t  window.navigate('" + subMenu[j].Url + "');\n");
                    script.Append("\t \t \t  return false;\n");
                    script.Append("\t \t } \n");
                }
                else
                {
                    script.Append("\t \t \t  document.getElementById('" + 
                        this.Master.ParentID + "').value = '" + 
                        subMenu[j].Id + "';\n");
                    script.Append("\t \t \t  document.getElementById('" + 
                        this.Master.FormID + "').submit();\n");
                    script.Append("\t \t \t  return false;\n");
                    script.Append("\t \t } \n");
                }

            }

            for (int i = subMenu.Count + 1; i <= 7; i++)
            {

                menuItem = (HtmlTableCell)this.Master.FindControl("menuItem" + i);
                menuItem.InnerText = i + " - NA";

                menuItem.Visible = false;

            }

            script.Append("\t \t  if (barcode.value == " + (int)HandheldInput.Home + " ){\n");
            script.Append("\t \t \t  document.getElementById('" + 
                this.Master.ParentID + "').value = '" + 
                (int)HandheldInput.Root + "';\n");
            script.Append("\t \t \t  window.navigate('home.aspx');\n");
            script.Append("\t \t \t  return false;\n");
            script.Append("\t \t } \n");


            script.Append("\t \t  if (barcode.value ==  " + 
                (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
            script.Append("\t \t \t window.navigate('home.aspx?Signout=1');\n");
            script.Append("\t \t \t return false;\n");

            script.Append("\t \t } \n");

            
            script.Append("\t \t \t document.getElementById('" + 
                this.Master.ParentID + "').value = barcode.value;\n");
            script.Append("\t \t \t document.getElementById('" + 
                this.Master.FormID + "').submit();\n");

            
            script.Append("\t } \n");//end if for activeElement

            //script.Append("\t //else if (document.activeElement.id == document.getElementById('" + 
            //    this.Master.MessageWindowID + "').id){\n");
            //script.Append("\t \t //HidePanel(); \n \t \t //Focus();\n } \n");


            script.Append("} \n \n");//function end

            ClientScript.RegisterClientScriptBlock(this.GetType(),
                "PostBack",
                script.ToString(),
                true);

            this.Master.BarcodeValue = String.Empty;

        }


    }
        
        
}