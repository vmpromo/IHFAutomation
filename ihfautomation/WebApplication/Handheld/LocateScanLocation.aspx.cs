using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;


namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class LocateScanLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            decimal I_chute_id = decimal.Parse(Request.QueryString["chuteID"].ToString());
            string I_chute_barcode = Request.QueryString["chutebarcode"].ToString();
            string I_user = Request.QueryString["userlogon"].ToString();
            decimal I_trolley_id = decimal.Parse(Request.QueryString["trolleyid"].ToString());
            decimal I_chute_type = decimal.Parse(Request.QueryString["chutetype"].ToString());
            decimal I_item = decimal.Parse(Request.QueryString["itemid"].ToString());
            string I_sku_barcode = Request.QueryString["skubarcode"].ToString();
            string I_label = Request.QueryString["locationname"].ToString();
            decimal I_chute_area = decimal.Parse(Request.QueryString["chutearea"] == null ? "0" : Request.QueryString["chutearea"].ToString());

            //string I_terminal = this.Master.HostName;
            string I_terminal = null;


            //this.Master.MessageBoard = "location is " + I_label;
                                
                // on page load display this message

                // split the label string

            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            string barcodetype_sku = "SKU";
            
            try
            {

                string[] labels = I_label.Split('.');

                List<string> labelname = new List<string>();

                foreach (string label in labels)
                {
                    labelname.Add(label);
                }
                
                
                
                StringBuilder sb = new StringBuilder();

                if (!String.IsNullOrEmpty(labelname[0]))
                {
                    if (!String.IsNullOrEmpty(labelname[1]))
                    {
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='font-size:20px;padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelname[0] + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelname[1] + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                    }

                    else
                    {
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='font-size:24px;padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelname[0] + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                    }
                }


                this.Master.MessageBoard = sb.ToString();
               
            }
            catch (Exception ex)
            {


                // activity logging
                setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                setclass.EventType = (Int32)EventType.ScanItemForLocate;
                setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.ItemvalidationFailed;
                setclass.ExpectedBarcodeType = barcodetype_sku;
                setclass.Barcode = I_sku_barcode;
                setclass.TerminalId = I_terminal;
                setclass.UserId = I_user;
                setclass.ChuteId = decimal.ToInt32(I_chute_id);
                setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                setclass.ItemNumber = decimal.ToInt32(I_item);
                    

                actlog.SaveUserActivity(setclass);


                
                this.Master.ErrorMessage = "Error: Retrieving location name failed";
                this.Master.DisplayMessage = true;

                // redirect to scan sku ???????????
                Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&chutearea=" + I_chute_area);
            }
                
            
            if (IsPostBack)
            {           
                string loc_barcode = this.Master.BarcodeValue;

                if (loc_barcode == string.Empty)
                {

                    this.Master.ErrorMessage = "Invalid Scan. Please scan again";
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;
                }
                else{

                    if (loc_barcode.Length > 50)
                    {
                        loc_barcode = loc_barcode.Substring(0, 50);

                    }

                LocateDAO locdao = new LocateDAO();
                
                // user scans trolley location or overflow tote
                // validate if it is a trolley location or overflow tote
                // prevalidate the location barcode
                string loc_chk = loc_barcode.Substring(0, 2).ToString();
                string barcode_loc = "Location";    
                decimal loc_id = 0;

                try
                {
                    loc_id = locdao.PreValidateLocation(loc_barcode);
                }
                catch (Exception ex3)
                {

                    
                    
                    // activity logging
                    setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                    setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                    setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                    setclass.EventType = (Int32)EventType.ScanLocation;
                    setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;
                    setclass.ExpectedBarcodeType = barcode_loc;
                    setclass.Barcode = loc_barcode;
                    setclass.TerminalId = I_terminal;
                    setclass.UserId = I_user;
                    setclass.ChuteId = decimal.ToInt32(I_chute_id);
                    setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                    setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                    setclass.ItemNumber = decimal.ToInt32(I_item);
                    
                    

                    actlog.SaveUserActivity(setclass);


                    this.Master.ErrorMessage = ex3.Message.Substring(ex3.Message.IndexOf(" ", 0), (ex3.Message.IndexOf("ORA", 1) - ex3.Message.IndexOf(" ", 0)));
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;
                }

               
                    
                if (loc_chk.ToUpper() == "LC")
                {

                    try
                    {
                        // if it is trolley location then validate using p_valid_location
                        decimal tr_loc = locdao.Validate_Location(loc_barcode, I_item, I_user, I_terminal);


                        if (tr_loc == 0)
                        {


                            // activity logging
                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.ScanLocation;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;
                            setclass.ExpectedBarcodeType = barcode_loc;
                            setclass.Barcode = loc_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = I_user;
                            setclass.ChuteId = decimal.ToInt32(I_chute_id);
                            setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                            setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                            setclass.ItemNumber = decimal.ToInt32(I_item);

                            actlog.SaveUserActivity(setclass);
                            
                            
                            // if location is invalid then error

                            this.Master.ErrorMessage = "Error: Location Validation Failed";
                            this.Master.DisplayMessage = true;


                        }
                        else
                        {

                            // else locate item using p_locate_item



                            string detached_ind = locdao.Locate_item(I_item, tr_loc, I_user, I_terminal);


                            if (!String.IsNullOrEmpty(detached_ind))
                            {
                                if (detached_ind == "F")
                                {
                                    // redirect to scan sku for scanning another sku or chute
                                    Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&chutearea=" + I_chute_area);
                                }
                                else if (detached_ind == "T")
                                {
                                    // log off user
                                    //locdao.Log_off_Chute(I_chute_id, I_user);


                                    // activity logging
                                    setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                    setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                                    setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                                    setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                                    setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;
                                    setclass.ExpectedBarcodeType = "Chute";
                                    setclass.Barcode = I_chute_barcode;
                                    setclass.TerminalId = I_terminal;
                                    setclass.UserId = I_user;
                                    setclass.ChuteId = decimal.ToInt32(I_chute_id);
                                    setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                                    setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                                    setclass.ItemNumber = decimal.ToInt32(I_item);
                                    setclass.SessionEndDateTime = DateTime.Now;
                                    


                                    actlog.SaveUserActivity(setclass);

                                    // locate main screen
                                    Response.Redirect("Locate.aspx?message=" + "T");
                                    //this.Master.BarcodeValue = string.Empty;
                                    //this.Master.SuccessMessage = "Trolley is successfully located and detached";
                                    //this.Master.DisplayMessage = true;
                                }
                            }

                        }
                    }
                    catch(Exception ex2)
                    {

                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.LocateItem;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.FailedToLocate;
                        setclass.ExpectedBarcodeType = barcode_loc;
                        setclass.Barcode = loc_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                        setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                        setclass.ItemNumber = decimal.ToInt32(I_item);
                        

                        actlog.SaveUserActivity(setclass);
                        
                        this.Master.ErrorMessage = ex2.Message.Substring(ex2.Message.IndexOf(" ", 0), (ex2.Message.IndexOf("ORA", 1) - ex2.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }
                }
                else if (loc_chk.ToUpper() == "OT")
                {
                    string barcode_ot = "Overflow Tote";       
                    try
                    {
                        // if it is OT location then validate using p_valid_location
                        decimal ot_loc = locdao.Validate_Tote(loc_barcode, I_item, I_user, I_terminal);

                        if (ot_loc == 0)
                        {

                            // activity logging
                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.ScanOFTForAttach;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.OFTValidationFailed;
                            setclass.ExpectedBarcodeType = barcode_ot;
                            setclass.Barcode = loc_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = I_user;
                            setclass.ChuteId = decimal.ToInt32(I_chute_id);
                            setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                            setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                            setclass.ItemNumber = decimal.ToInt32(I_item);
                            
                             

                            actlog.SaveUserActivity(setclass);
                            
                            // if location is invalid then error

                            this.Master.ErrorMessage = "Error: While validating Overflow Tote Location Location";
                            this.Master.DisplayMessage = true;


                        }
                        else
                        {

                            // else locate item using p_locate_item
                            // debug...........
                            //this.Master.MessageBoard = "item is:" + I_item + "ot loc is:" + ot_loc + "user is:" + I_user + "trolley id is" + I_trolley_id;

                            string detached_ind = locdao.Locate_tote_item(I_item, ot_loc, I_user, I_trolley_id, I_terminal);
                            if (!String.IsNullOrEmpty(detached_ind))
                            {
                                if (detached_ind == "F")
                                {
                                    // redirect to scan sku for scanning another sku or chute
                                    Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&chutearea=" + I_chute_area);
                                }
                                else if (detached_ind == "T")
                                {
                                    // log off user
                                    //locdao.Log_off_Chute(I_chute_id, I_user);
                                    // activity logging
                                    setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                    setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                                    setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                                    setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                                    setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;
                                    setclass.ExpectedBarcodeType = "Chute";
                                    setclass.Barcode = I_chute_barcode;
                                    setclass.TerminalId = I_terminal;
                                    setclass.UserId = I_user;
                                    setclass.ChuteId = decimal.ToInt32(I_chute_id);
                                    setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                                    setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                                    setclass.ItemNumber = decimal.ToInt32(I_item);
                                    setclass.SessionEndDateTime = DateTime.Now;
                                    


                                    actlog.SaveUserActivity(setclass);

                                    // end of log off



                                    // locate main screen
                                    Response.Redirect("Locate.aspx?message=" +"T");
                                    //this.Master.BarcodeValue = string.Empty;
                                    //this.Master.SuccessMessage = "Trolley is successfully located and detached";
                                    //this.Master.DisplayMessage = true;
                                }
                            }

                        }
                    }
                    catch (Exception ex1)
                    {

                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.LocateItem;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.FailedToLocateInOT;
                        setclass.ExpectedBarcodeType = barcode_ot;
                        setclass.Barcode = loc_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                        setclass.TrolleyLocationId = decimal.ToInt32(loc_id);
                        setclass.ItemNumber = decimal.ToInt32(I_item);
                        
                             
                        actlog.SaveUserActivity(setclass);
                        
                        
                        this.Master.ErrorMessage = ex1.Message.Substring(ex1.Message.IndexOf(" ", 0), (ex1.Message.IndexOf("ORA", 1) - ex1.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                        
                    }
                }
            }
                
            }//end of postback
        }
        
    }
}