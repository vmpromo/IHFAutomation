using System;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using System.Web;
using System.Configuration;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class Locate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;
            string I_message = null;

            if (Request.QueryString["message"] != null)
            {
                I_message = Request.QueryString["message"].ToString();
            }

            

            string user_logon = User.Identity.Name;

            //string I_terminal = this.Master.HostName; //Shared.UserHostName;

            string I_terminal = null;

            string barcodetype = "Chute";
                    
            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            if (!IsPostBack)
            {
                if (I_message == "T") // message exists
                {
                    this.Master.MessageBoard = "Trolley is successfully located and detached. Scan next Chute for Locate";
                }
                else
                {
                    // on page load display this message
                    this.Master.MessageBoard = "Scan Chute for Locate";
                }
                
                
            }
            else
            {

                string chute_barcode = this.Master.BarcodeValue;

                if (chute_barcode != string.Empty)
                {

                    if (chute_barcode.Length > 50)
                    {
                        chute_barcode = chute_barcode.Substring(0, 50);

                    }
                }

                decimal chute_id = 0;
                decimal chute_type = 0;
                decimal chute_area = 0;

                LocateDAO locdao = new LocateDAO();                


                try
                {

                    // validate chute using oms_attach_trolley.p_validate_chute

                    chute_id = locdao.Validate_Chute(chute_barcode, user_logon, I_terminal);

                    
                    // find the chute type if it is singles = 1 or multi = 2

                    chute_type = locdao.Get_chute_type(chute_id);


                    if (chute_type == 2)
                    {
                        chute_area = locdao.Get_Chute_Area(chute_id);
                    }


                }
                catch(Exception ex)
                {
                    // activity logging
                    
                    
                    setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                    setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                    setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                    setclass.EventType = (Int32)EventType.ScanChuteForAttach;                    
                    setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.ChuteScanFailed;
                    setclass.ExpectedBarcodeType = barcodetype;
                    setclass.Barcode = chute_barcode;                    
                    setclass.TerminalId = I_terminal;
                    setclass.UserId = user_logon;
                    setclass.ChuteId = decimal.ToInt32(chute_id);

                    

                    actlog.SaveUserActivity(setclass);
                     
                    
                    

                    //this.Master.MessageBoard = chute_id.ToString();
                    this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;

                }
                
                
                try
                {
                    

                    // find the trolley attached p_chute_trolley

                    

                    decimal trolley_id = locdao.Chute_Trolley(chute_barcode, user_logon, I_terminal);
                    if (trolley_id == 0)
                    {
                        // if the trolley id is null then ask user to scan trolley
                        // redirect to attach trolley

                        // no trolley attached to chute
                        if (chute_type == 1) // singles
                        {
                            // redirect to scan trolley page
                            Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + user_logon + "&chutetype=" + chute_type + "&chutearea=" + chute_area);
                        }
                        else if (chute_type == 2) // multi
                        {
                            // redirect to scan trolley page
                            Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + user_logon + "&chutetype=" + chute_type + "&chutearea=" + chute_area);
                        }

                        else
                        {
                            
                            // activity logging


                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.LogOnChuteForLocate;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                            setclass.ExpectedBarcodeType = barcodetype;
                            setclass.Barcode = chute_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = user_logon;
                            setclass.ChuteId = decimal.ToInt32(chute_id);
                    
                                     

                            actlog.SaveUserActivity(setclass);


                            // end of activity logging
                            
                            this.Master.ErrorMessage = "Please enter either Chute Type: Single or Chute Type: Multi";
                            this.Master.DisplayMessage = true;
                            this.Master.BarcodeValue = string.Empty;

                            
                        }
                        
                    }
                    else
                    { 
                        // trolley is already attached
                        if (chute_type == 1) // singles
                        {
                            // redirect to scan trolley page
                            Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + user_logon + "&chutetype=" + chute_type + "&chutearea=" + chute_area);
                        }
                        else if (chute_type == 2) // multi
                        {
                            // redirect to scan sku
                            // Log user to chute using p_log_on_to_chute



                            // ************ commented out for time being ************ /                            
                            //locdao.Log_on_to_Chute(chute_barcode, user_logon);
                            // ************ commented out for time being ************ /  


                            Response.Redirect("LocateScanSku.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + user_logon + "&trolleyid=" + trolley_id + "&chutetype=" + chute_type + "&chutearea=" + chute_area);


                        }
                        else
                        {
                            // activity logging


                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.LogOnChuteForLocate;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                            setclass.ExpectedBarcodeType = barcodetype;
                            setclass.Barcode = chute_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = user_logon;
                            setclass.ChuteId = decimal.ToInt32(chute_id);



                            actlog.SaveUserActivity(setclass);
                    
                   


                            // end of activity logging
                            
                            this.Master.ErrorMessage = "Please enter either Chute Type: Single or Chute Type: Multi";
                            this.Master.DisplayMessage = true;
                            this.Master.BarcodeValue = string.Empty;
                        }
                        
                        
                    }


                }
                catch (Exception ex1)
                {
                    // activity logging


                    setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                    setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                    setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                    setclass.EventType = (Int32)EventType.LogOnChuteForLocate;
                    setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.LogonFailed;
                    setclass.ExpectedBarcodeType = barcodetype;
                    setclass.Barcode = chute_barcode;
                    setclass.TerminalId = I_terminal;
                    setclass.UserId = user_logon;
                    setclass.ChuteId = decimal.ToInt32(chute_id);



                    actlog.SaveUserActivity(setclass);
                    
                    
                    

                    // end of activity logging

                    this.Master.ErrorMessage = ex1.Message.Substring(ex1.Message.IndexOf(" ", 0), (ex1.Message.IndexOf("ORA", 1) - ex1.Message.IndexOf(" ", 0)));
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;
                }
            }


            
        }
    }
}