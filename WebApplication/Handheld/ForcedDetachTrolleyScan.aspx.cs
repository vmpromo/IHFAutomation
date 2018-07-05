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
    public partial class ForcedDetachTrolleyScan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            decimal I_chute_id = decimal.Parse(Request.QueryString["chuteID"].ToString());
            string I_chute_barcode = Request.QueryString["chutebarcode"].ToString();
            string I_user = Request.QueryString["userlogon"].ToString();
            string I_trolley_label = Request.QueryString["trolleylabel"].ToString();

            if (!IsPostBack)
            {
                // on page load display this message
                this.Master.MessageBoard = "The Chute is attached to Trolley: " 
                                           + I_trolley_label 
                                           + ". Scan Trolley Barcode for Detach";
            }
            else
            {

                string trolley_barcode = this.Master.BarcodeValue;

                ForcedDetachDAO detdao = new ForcedDetachDAO();       

                decimal trolley_id = 0;
                decimal chute_id = 0;

                // validate the trolley barcode using oms_detach_trolley.p_validate_trolley_barcode

                try
                {
                    trolley_id = detdao.Validate_Trolley(trolley_barcode);

                    chute_id = detdao.Trolley_Attached(I_chute_id, trolley_id);

                    detdao.Manual_detach(trolley_id, I_user);

                    this.Master.BarcodeValue = string.Empty;
                    this.Master.SuccessMessage = "Trolley successfully detached";
                    this.Master.DisplayMessage = true;
                    
                    // redirect to scan chute barcode
                    //Response.Redirect("Locate.aspx?message=" + "T");
                    Response.Redirect("ForcedDetachChuteScan.aspx?message=" + "T");

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