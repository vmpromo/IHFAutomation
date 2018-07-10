using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using IHF.BusinessLayer.Util;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI.HtmlControls;
using System.Text;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class ManualInductSku : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            string Iuser = Request.QueryString["userlogon"].ToString();
            //string Iuser = null;
            
            //receive the load id from the manualinductload page

            // give the message to enter the sku barcode

            // if the sku barcode is scanned then it autopostsback
            // but if they enter any other number then it needs to be handled


            

            ManualInductDAO load = new ManualInductDAO();
            string I_load_id = Request.QueryString["LoadID"].ToString();
            if (I_load_id == "All")
                I_load_id = null;

            decimal areaid = Convert.ToDecimal(Request.QueryString["areaid"].ToString());


            string I_message = null;

            if (Request.QueryString["message"] != null)
            {
                I_message = Request.QueryString["message"].ToString();
            }

            //string I_user;

            //HtmlGenericControl message;
            //message = (HtmlGenericControl)this.Master.FindControl("message");
            //if (message == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");


            //HtmlGenericControl PopupMessage;
            //PopupMessage = (HtmlGenericControl)this.Master.FindControl("PopupMessage");
            //if (PopupMessage == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");

            if (!IsPostBack)
            {
                if (I_message == "T1")
                {

                    this.Master.MessageBoard = "Put to Chute is completed successfully. Scan next SKU";
                }

                else if (I_message == "T2") // message exists
                {
                    this.Master.MessageBoard = "Put to Chute is Time OUT. Scan SKU";
                }
                else
                {
                    // on page load display this message
                    this.Master.MessageBoard = "Scan SKU";
                }
                
                
                
                //message.InnerText = "Please Enter the SKU barcode or Press: 0: Exit, 9: Home";
                                    
                                
            }


            if (IsPostBack)
            {
                // if the usr scans the sku barcode then validate the sku barcode and 
                // then redirect to put to chute
                try
                {

                    //TextBox skutxtbox = (TextBox)this.Master.FindControl("barcode");
                    //if (skutxtbox == null)
                    //    throw new Exception("Unknown error occurred. Please contact your system administrator.");


                    string I_sku_barcode = this.Master.BarcodeValue; //skutxtbox.Text.ToString();

                    string sku_status = load.validate_sku(I_sku_barcode, I_load_id, areaid);
                    if (sku_status == "T")
                    {
                        DataSet manual_induct_list = load.manual_induct(areaid, I_sku_barcode, Iuser, I_load_id );


                        DataTable manualinducttab = new DataTable();
                        manualinducttab = manual_induct_list.Tables[0];

                        decimal I_chute_id = 0;
                        decimal I_itemnumber = 0;
                        string I_chute_barcode = null;

                        foreach (DataRow row in manualinducttab.Rows)
                        {
                            I_chute_id = Int32.Parse(row["chute_id"].ToString());
                            I_itemnumber = Int32.Parse(row["itemnumber"].ToString());
                            I_chute_barcode = row["chute_barcode"].ToString();


                        }

                        if (I_load_id == null)
                            I_load_id = "All";

                        Response.Redirect("PutToChute.aspx?chuteID=" + I_chute_id + "&Itemnumber=" + I_itemnumber + "&user=" + Iuser + "&chutebarcode=" + I_chute_barcode + "&load=" + I_load_id + "&areaid=" + areaid.ToString());


                    }
                }
                catch (Exception ex)
                {
                    this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;
                    
                    /*
                    string errormsg = ex.Message.ToString();
                    char[] separator = new char[] { ':' };
                    string[] testarr = errormsg.Split(separator);

                    decimal arraylength = testarr.Length;

                    if (arraylength > 6)
                    {
                        //PopupMessage.InnerText = testarr[5].ToString();
                        this.Master.ErrorMessage = testarr[5].ToString();
                        this.Master.DisplayMessage = true;
                    }
                    else
                    {
                        //PopupMessage.InnerText = errormsg;
                        this.Master.ErrorMessage = errormsg;
                        this.Master.DisplayMessage = true;
                    }
                    */
                    //Panel panel;
                    //panel = (Panel)this.Master.FindControl("Panel1");
                    //if (panel == null)
                    //    throw new Exception("Unknown error occurred. Please contact your system administrator.");
                    //panel.Style.Add("display", "block");
                    //Response.Redirect("ManualInductLoad.aspx");
                }

            }

            
            // javascript to handle 0 and 9

            //HtmlInputHidden hdn = (HtmlInputHidden)this.Master.FindControl("ParentMenuId");
            //StringBuilder script = new StringBuilder();
            //TextBox txtbox = (TextBox)this.Master.FindControl("barcode");
            //if (txtbox == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");
            //HtmlForm masterForm = (HtmlForm)this.Master.FindControl("form1");
            //if (masterForm == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");

            //script.Append("function checkAndPost(){\n");
            //script.Append("\t var barcode = document.getElementById('" + txtbox.ClientID + "');\n");
            //script.Append("\t if (window.event.keyCode == 13) {\n");
            
            //script.Append("\t if (barcode.value == 9){\n");
            //script.Append("\t \t barcode.value = '';\n");
            //script.Append("\t \t document.getElementById('" + hdn.ClientID + "').value = '-1';\n");
            //script.Append("\t \t window.navigate('home.aspx');\n\t");
            //script.Append("\t \t return false;\n}\n");

            //script.Append("\t if (barcode.value == 0 && barcode.value.length > 0){\n");
            //script.Append("\t \t document.getElementById('" + hdn.ClientID + "').value = barcode.value;\n");
            //script.Append("\t \t barcode.value = '';\n");
            //script.Append("\t \t window.navigate('home.aspx');\n\t");
            //script.Append("\t \t return false;\n}\n");

            //script.Append("\t }\n");
            //script.Append("}\n");

            //ClientScript.RegisterClientScriptBlock(this.GetType(),
            //    "PostBack",
            //    script.ToString(),
            //    true);

            

        }
    }
}