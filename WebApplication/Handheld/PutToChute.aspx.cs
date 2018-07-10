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
    public partial class PutToChute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                this.Master.Reset();

                this.Master.RegisterStandardScript = true;


                ManualInductDAO load = new ManualInductDAO();
                decimal I_chute_id = Int32.Parse(Request.QueryString["chuteID"]);
                decimal I_itemnumber = Int32.Parse(Request.QueryString["Itemnumber"]);
                string I_user = Request.QueryString["user"].ToString();
                string I_chute_barcode = Request.QueryString["chutebarcode"].ToString();
                string I_load = Request.QueryString["load"].ToString();
                decimal areaid = Convert.ToDecimal(Request.QueryString["areaid"].ToString());
               

                //HtmlGenericControl message;
                //message = (HtmlGenericControl)this.Master.FindControl("message");
                //if (message == null)
                //    throw new Exception("Unknown error occurred. Please contact your system administrator.");





                if (!IsPostBack)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Put to Chute: "+ I_chute_id + "<br/>");
                    sb.Append("<div>");
                    sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                    sb.Append("<tr>");
                    //sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>0:  Exit</td>");
                    sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  Cancel</td>");
                    //sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>9:  Home</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    //message.InnerHtml = sb.ToString();
                    this.Master.MessageBoard = sb.ToString();
                    //message.InnerText = "Please Enter the Chute barcode or Press: 0: Exit, 1: Cancel, 9: Home";
                }
                if (IsPostBack)
                {
                    //HtmlGenericControl PopupMessage;
                    //PopupMessage = (HtmlGenericControl)this.Master.FindControl("PopupMessage");
                    //if (PopupMessage == null)
                    //    throw new Exception("Unknown error occurred. Please contact your system administrator.");


                    //Panel panel;
                    //panel = (Panel)this.Master.FindControl("Panel1");
                    //if (panel == null)
                    //    throw new Exception("Unknown error occurred. Please contact your system administrator.");

                    try
                    {
                        //TextBox barcode = (TextBox)this.Master.FindControl("barcode");
                        //if (barcode == null)
                        //    throw new Exception("Unknown error occurred. Please contact your system administrator.");

                        if (this.Master.BarcodeValue == "1")
                        {
                            // you have entered cancel
                            decimal I_req_id = 0;
                            decimal manual_induct_req_id = load.cancel_lock(I_req_id, I_chute_id, I_itemnumber, I_user);
                            if (manual_induct_req_id != 0)
                            {
                                //message.InnerText = "Cancel Request for Put to Chute Completed";
                                this.Master.MessageBoard = "Cancel Request for Put to Chute Completed";

                                this.Master.BarcodeValue = string.Empty;
                                //Response.Redirect("ManualInductLoad.aspx");
                            }
                            else
                            {
                                //PopupMessage.InnerText = "Error occurred while processing Put to Chute";
                                this.Master.ErrorMessage = "Error occurred while processing Put to Chute";
                                this.Master.DisplayMessage = true;


                                //panel.Style.Add("display", "block");
                            }
                        }

                        else
                        {
                            // user scanned chute

                            string I_scanned_barcode = this.Master.BarcodeValue;//barcode.Text;

                            if (I_scanned_barcode == string.Empty || I_scanned_barcode == null)
                            {
                                this.Master.BarcodeValue = string.Empty;
                                this.Master.ErrorMessage = "Invalid Chute Barcode. Try again";
                                this.Master.DisplayMessage = true;

                                //panel.Style.Add("display", "block");
                                                                
                            
                            }
                            else
                            {


                                // set the return value as 0 initially and if there is an error in stored proc then that will be returned else it will be success
                                // changed this param to in out because there is no handling in datamanager.cs at the moment
                                decimal I_return_val = 0;

                                decimal put_to_chute_return_cd = load.put_to_chute(I_return_val, I_chute_id, I_chute_barcode, I_scanned_barcode, I_itemnumber, I_user);
                                if (put_to_chute_return_cd == 0)
                                {
                                    // success
                                    //message.InnerText = "Put to Chute is successfully completed";
                                    this.Master.MessageBoard = "Put to Chute is successfully completed";
                                    this.Master.BarcodeValue = string.Empty;
                                    //Response.Redirect("ManualInductLoad.aspx");
                                    Response.Redirect("ManualInductSku.aspx?LoadID=" + I_load + "&userlogon=" + I_user + "&message=" + "T1" + "&areaid=" + areaid.ToString());
                                }
                                else if (put_to_chute_return_cd == 2)
                                {
                                    // time out
                                    //PopupMessage.InnerText = "Put to Chute is Time OUT";
                                    this.Master.BarcodeValue = string.Empty;
                                    this.Master.ErrorMessage = "Put to Chute is Time OUT";
                                    this.Master.DisplayMessage = true;

                                    //panel.Style.Add("display", "block");
                                    // return to home page of manual induct
                                    //Response.Redirect("ManualInductLoad.aspx");
                                    Response.Redirect("ManualInductSku.aspx?LoadID=" + I_load + "&userlogon=" + I_user + "&message=" + "T2" + "&areaid=" + areaid.ToString());
                                }
                                else if (put_to_chute_return_cd == 1)
                                {
                                    // redirect to same page to rescan  chute barcode
                                    //PopupMessage.InnerText = "Invalid Chute Barcode. Try again";
                                    this.Master.BarcodeValue = string.Empty;
                                    this.Master.ErrorMessage = "Invalid Chute Barcode. Try again";
                                    this.Master.DisplayMessage = true;

                                    //panel.Style.Add("display", "block");

                                    this.Master.BarcodeValue = string.Empty;
                                    //Response.Redirect("ManualInductLoad.aspx");
                                }
                            }

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

                        //panel.Style.Add("display", "block");

                        //Response.Redirect("ManualInductLoad.aspx");
                    }
                }

                // javascript to handle 0 and 9

                //StringBuilder script = new StringBuilder();

                //script.Append("function checkAndPost(){\n");
                //script.Append("\t var barcode = document.getElementById('" + this.Master.BarcodeID + "');\n");
                //script.Append("\t if (document.activeElement.id == barcode.id) {\n");

                //script.Append("\t if (barcode.value == 1 ) {\n");
                //script.Append("\t \t document.getElementById('" + this.Master.FormID + "').submit();\n");
                //script.Append("\t \t return false;\n}\n\t");

                //script.Append("\t \t  if (barcode.value == " +
                //        (int)HandheldInput.Home + " ){\n alert('Inside ' + barcode.value);");
                //script.Append("\t \t \t  document.getElementById('" +
                //    this.Master.ParentID + "').value = '" +
                //    (int)HandheldInput.Root + "';\n");
                //script.Append("\t \t \t  window.navigate('home.aspx');\n");
                //script.Append("\t \t \t  return false;\n");
                //script.Append("\t \t } \n");
                //script.Append("\t \t  if (barcode.value ==  " +
                //    (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
                //script.Append("\t \t \t window.navigate('home.aspx?Signout=1');\n");
                //script.Append("\t \t \t return false;\n");
                //script.Append("\t \t } \n");
                //script.Append("\t \t \t document.getElementById('" +
                //    this.Master.ParentID + "').value = barcode.value;\n");
                //script.Append("\t \t \t document.getElementById('" +
                //    this.Master.FormID + "').submit();\n");
                //script.Append("\t } \n");//end if for activeElement
                //script.Append("} \n \n");//function end

                //ClientScript.RegisterClientScriptBlock(this.GetType(),
                //    "PostBack",
                //    script.ToString(),
                //    true);
            }

   
    }
}