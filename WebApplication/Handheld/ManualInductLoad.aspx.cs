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
    public partial class ManualInductLoad : System.Web.UI.Page
    {
        // counter for the front end load id display
        static Int32 load_cnt;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            decimal areaid = decimal.Parse(Request.QueryString["areaid"]);

            string user_logon = User.Identity.Name;
            
            //when page load happens, call the oracle fn to get the load ids
            // load the following in the message pannel
            // 1. All, 2. lowest Loadid, 3. lowest - 1 load id 4. More
            // when more is pressed load 1. All, 2. lowest-2 Loadid, 3. lowest-3 load id 4. More

            ManualInductDAO load = new ManualInductDAO();
            DataSet loadlist = load.Get_loadid(areaid);
            List<string> loadidlist = new List<string>();

            DataTable loadtab = new DataTable();
            loadtab = loadlist.Tables[0];
            
            foreach (DataRow row in loadtab.Rows)
            {
                loadidlist.Add(row["load_id"].ToString());
                
            }
            
            // counter for actual number of load ids in the arraylist
            Int32 arraylistcnt = loadidlist.Count();
            
            //HtmlGenericControl message;
            //message = (HtmlGenericControl)this.Master.FindControl("message");
            //if (message == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");


            //HtmlGenericControl PopupMessage;
            //PopupMessage = (HtmlGenericControl)this.Master.FindControl("PopupMessage");
            //if (PopupMessage == null)
            //    throw new Exception("Unknown error occurred. Please contact your system administrator.");


            try
            {

                if (!IsPostBack)
                {

                    //this.Master.MenuParentId = 1;
                    

                    load_cnt = 0;

                    if (arraylistcnt != 0 && (load_cnt + 1) <= arraylistcnt - 1)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>2:  " + loadidlist[load_cnt] + "</td>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>3:  " + loadidlist[load_cnt + 1] + "</td>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>4:  More</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");

                        //message.InnerHtml = sb.ToString();
                        this.Master.MessageBoard = sb.ToString();

                        
                    }
                    else if (arraylistcnt != 0 && (load_cnt + 1) > arraylistcnt - 1)
                    {

                        StringBuilder sb = new StringBuilder();

                        sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>2:  " + loadidlist[load_cnt] + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");

                        //message.InnerHtml = sb.ToString();
                        this.Master.MessageBoard = sb.ToString();
                        //message.InnerText = "Please Enter Load ID. Press the appropriate Key:\n" + "1: All, 2: " + loadidlist[load_cnt] + ", 4: More";
                    }
                    else
                    {
                        //PopupMessage.InnerText = "No load Released for Manual Induction. Please try again Later";
                        this.Master.ErrorMessage = "No load Released for Manual Induction. Please try again Later";
                        this.Master.DisplayMessage = true;
                    }
                        

                    

                    //sb.Append()


                    //HttpContext.Current.Items["MyKey"] = "Key1";
                }

                if (IsPostBack)
                {
                    //TextBox barcode = (TextBox)this.Master.FindControl("barcode");
                    //if (barcode == null)
                    //    throw new Exception("Unknown error occurred. Please contact your system administrator.");
                    // user entered 1

                    if (this.Master.BarcodeValue == "1")
                    {
                        //message.InnerText = "The page was successfully posted for locating";
                        //this.Master.MessageBoard = "inside all";
                        Response.Redirect("ManualInductSku.aspx?LoadID=" + "All" + "&userlogon=" + user_logon + "&areaid=" + areaid.ToString());
                    }

                    // user entered a given load id

                    else if (this.Master.BarcodeValue == "2")
                    {
                        String load_status = load.validate_load(loadidlist[load_cnt]);
                        if (load_status == "T")
                        {
                            Response.Redirect("ManualInductSku.aspx?LoadID=" + loadidlist[load_cnt] + "&userlogon=" + user_logon + "&areaid=" + areaid.ToString());
                        }
                    }

                    // user entered a given load id
                    else if (this.Master.BarcodeValue == "3")
                    {
                        String load_status = load.validate_load(loadidlist[load_cnt + 1]);
                        if (load_status == "T")
                        {
                            Response.Redirect("ManualInductSku.aspx?LoadID=" + loadidlist[load_cnt + 1] + "&userlogon=" + user_logon + "&areaid=" + areaid.ToString());
                        }
                    }

                    // user entered a 4 for more load ids    
                    else if (this.Master.BarcodeValue == "4")
                    {

                        load_cnt = load_cnt + 2;
                        if (load_cnt > arraylistcnt - 1)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                            sb.Append("<div>");
                            sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                            sb.Append("<tr>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>9:  Home</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>0:  Exit</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</div>");

                            //message.InnerHtml = sb.ToString();
                            this.Master.MessageBoard = sb.ToString();

                            //message.InnerText = "No more load ids areleased for Manual Induct. Press the appropriate Key:\n" + "1: All, 9: Home, 0: Exit";
                        }
                        else if ((load_cnt + 1) > arraylistcnt - 1)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                            sb.Append("<div>");
                            sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                            sb.Append("<tr>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>2:  " + loadidlist[load_cnt] + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</div>");

                            //message.InnerHtml = sb.ToString();
                            this.Master.MessageBoard = sb.ToString();

                            //message.InnerText = "Please Enter Load ID. Press the appropriate Key:\n" + "1: All, 2: " + loadidlist[load_cnt];
                        }
                        else if ((load_cnt + 1) == (arraylistcnt - 1))
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                            sb.Append("<div>");
                            sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                            sb.Append("<tr>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>2:  " + loadidlist[load_cnt] + "</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>3:  " + loadidlist[load_cnt + 1] + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</div>");

                            //message.InnerHtml = sb.ToString();
                            this.Master.MessageBoard = sb.ToString();
                            //message.InnerText = "Please Enter Load ID. Press the appropriate Key:\n" + "1: All, 2: " + loadidlist[load_cnt] + ", 3: " + loadidlist[load_cnt + 1];
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("Please Enter Load ID. Press the appropriate Key: <br/>");
                            sb.Append("<div>");
                            sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                            sb.Append("<tr>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>1:  All</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>2:  " + loadidlist[load_cnt] + "</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>3:  " + loadidlist[load_cnt + 1] + "</td>");
                            sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>4:  More</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            sb.Append("</div>");

                            //message.InnerHtml = sb.ToString();
                            this.Master.MessageBoard = sb.ToString();
                            //message.InnerText = "Please Enter Load ID. Press the appropriate Key:\n" + "1: All, 2: " + loadidlist[load_cnt] + ", 3: " + loadidlist[load_cnt + 1] + ", 4: More";
                        }
                    }

                    else if (this.Master.BarcodeValue == "5" || this.Master.BarcodeValue == "6" || this.Master.BarcodeValue == "7" || this.Master.BarcodeValue == "8")
                    {
                        Response.Redirect("ManualInductLoad.aspx?&areaid=" + areaid.ToString());
                    }

                    this.Master.BarcodeValue = string.Empty;
                }

            }
            catch (Exception ex)
            {
                this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                this.Master.DisplayMessage = true;
                this.Master.BarcodeValue = string.Empty;
            }


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
            
            //script.Append("\t \t if (barcode.value == 1 || barcode.value == 2 || barcode.value == 3 || barcode.value == 4) {\n");
            //script.Append("\t \t \t document.getElementById('" + masterForm.ClientID + "').submit();\n");
            //script.Append("\t \t \t return false;\n}\n");

            //script.Append("\t \t if (barcode.value == " + (int)HandheldInput.Home + " ){\n");
            //script.Append("\t \t \t barcode.value = '';\n");
            //script.Append("\t \t \t document.getElementById('" + hdn.ClientID + "').value = '" + (int)HandheldInput.Root + "';\n");
            //script.Append("\t \t \t window.navigate('home.aspx');\n\t");
            //script.Append("\t \t \t return false;\n}\n");

            //script.Append("\t \t if (barcode.value ==  " + (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
            //script.Append("\t \t \t document.getElementById('" + hdn.ClientID + "').value = barcode.value;\n");
            //script.Append("\t \t \t barcode.value = '';\n");
            //script.Append("\t \t \t window.navigate('home.aspx');\n\t");
            //script.Append("\t \t \t return false;\n}\n");

            
            
            //script.Append("\t }\n");
            //script.Append("}\n");

            StringBuilder script = new StringBuilder();
            
            script.Append("function checkAndPost(){\n");
            script.Append("\t var barcode = document.getElementById('" + this.Master.BarcodeID + "');\n");
            script.Append("\t if (document.activeElement.id == barcode.id) {\n");

            script.Append("\t \t if (barcode.value == 1 || barcode.value == 2 || barcode.value == 3 || barcode.value == 4) {\n");
            script.Append("\t \t \t document.getElementById('" + this.Master.FormID + "').submit();\n");
            script.Append("\t \t \t return false;\n}\n");

            script.Append("\t \t if (barcode.value == " + (int)HandheldInput.Home + " ){\n");
            script.Append("\t \t \t barcode.value = '';\n");
            script.Append("\t \t \t document.getElementById('" + this.Master.ParentID + "').value = '" + (int)HandheldInput.Root + "';\n");
            script.Append("\t \t \t window.navigate('home.aspx');\n\t");
            script.Append("\t \t \t return false;\n}\n");

            script.Append("\t \t if (barcode.value ==  " + (int)HandheldInput.SignOut + "  && barcode.value.length > 0){\n");
            script.Append("\t \t \t document.getElementById('" + this.Master.ParentID + "').value = barcode.value;\n");
            script.Append("\t \t \t barcode.value = '';\n");
            script.Append("\t \t \t window.navigate('home.aspx?Signout=1');\n\t");
            script.Append("\t \t \t return false;\n}\n");



            script.Append("\t }\n");
            script.Append("}\n");

            ClientScript.RegisterClientScriptBlock(this.GetType(),
                "PostBack",
                script.ToString(),
                true);
            
        }
    }
}