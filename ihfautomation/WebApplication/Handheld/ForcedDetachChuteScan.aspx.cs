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
    public partial class ForcedDetachChuteScan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            string user_logon = User.Identity.Name;

            string I_terminal = this.Master.HostName;

            string I_message = null;

            if (Request.QueryString["message"] != null)
            {
                I_message = Request.QueryString["message"].ToString();
            }

            if (!IsPostBack)
            {

                if (I_message == "T") // message exists
                {
                    this.Master.MessageBoard = "Trolley successfully detached. Scan next Chute for Forced Detach";
                }
                else
                {
                    // on page load display this message
                    this.Master.MessageBoard = "Scan Chute for Forced Detach";
                }
                
                
            }
            else
            {
                
                string chute_barcode = this.Master.BarcodeValue;

                decimal chute_id = 0;
                string trolley_label = null;

                ForcedDetachDAO detdao = new ForcedDetachDAO();       
                try
                {
                    // validate chute
                    chute_id = detdao.Validate_Chute(chute_barcode, user_logon, I_terminal);

                    // check trolley attached
                    trolley_label = detdao.chute_attached(chute_id);

                    //debug.....
                    this.Master.MessageBoard = "trolley attached is " + trolley_label;

                    
                    if (trolley_label == string.Empty || trolley_label == null)
                    {
                        this.Master.ErrorMessage = "Error while fetching the attached trolley details";
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }

                    else
                    { 
                        // redirect to scan trolley barcode
                        Response.Redirect("ForcedDetachTrolleyScan.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + user_logon + "&trolleylabel=" + trolley_label);
                    }
                    
                }
                catch (Exception ex)
                {
                    this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;
                
                }
            }
        }
    }
}