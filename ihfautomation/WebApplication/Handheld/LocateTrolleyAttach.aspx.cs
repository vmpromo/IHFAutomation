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
    public partial class LocateTrolleyAttach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            decimal I_chute_id = decimal.Parse(Request.QueryString["chuteID"].ToString());
            string I_chute_barcode = Request.QueryString["chutebarcode"].ToString();
            string I_user = Request.QueryString["userlogon"].ToString();
            decimal I_chute_type = decimal.Parse(Request.QueryString["chutetype"].ToString());
            //Default area to sorter when not passed.
            decimal I_chute_area = decimal.Parse(Request.QueryString["chutearea"] == null? "1" : Request.QueryString["chutearea"].ToString());

            //string I_terminal = this.Master.HostName;
            string I_terminal = null;

            string barcodetype = "Trolley";

            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            if (!IsPostBack)
            {
                // on page load display this message
                this.Master.MessageBoard = "Scan Trolley Barcode for Attach";
            }
            else
            {

                string trolley_barcode = this.Master.BarcodeValue;

                if (trolley_barcode != string.Empty)
                {

                    if (trolley_barcode.Length > 50)
                    {
                        trolley_barcode = trolley_barcode.Substring(0, 50);

                    }
                }

                LocateDAO locdao = new LocateDAO();

                //decimal trolley_id = 0;

                // check if the chute is singles or multi
                if (I_chute_type == 1) // singles
                {
                    decimal trolley_id_singles = 0;
                    // validate the trolley barcode using oms_attach_trolley.p_validate_trolley

                    try
                    {
                        trolley_id_singles = locdao.Validate_Trolley(I_chute_id, trolley_barcode, I_user, I_terminal);

                    }
                    catch (Exception ex1)
                    {

                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.ScanTrolleyForAttach;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.TrolleyScanFailed;
                        setclass.ExpectedBarcodeType = barcodetype;
                        setclass.Barcode = trolley_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(trolley_id_singles);


                        actlog.SaveUserActivity(setclass);

                        this.Master.ErrorMessage = ex1.Message.Substring(ex1.Message.IndexOf(" ", 0), (ex1.Message.IndexOf("ORA", 1) - ex1.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }
                    // attach the trolley
                    try
                    {
                        // if no errors then attach trolley to chute using oms_attach_trolley.p_attach_trolley_to_chute
                        locdao.Attach_Trolley(I_chute_id, trolley_id_singles, I_user, I_terminal);

                        // Log user to chute using p_log_on_to_chute

                        /************ commented out for time being ************/
                        //locdao.Log_on_to_Chute(I_chute_barcode, I_user);
                        /************ commented out for time being ************/



                        
                        // trolley attach success for singles

                        // log off user from locating session


                        /************ commented out for time being ************/
                        //locdao.Log_off_Chute(I_chute_id, I_user);


                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;
                        setclass.ExpectedBarcodeType = barcodetype;
                        setclass.Barcode = trolley_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(trolley_id_singles);


                        actlog.SaveUserActivity(setclass);

                        /************ commented out for time being ************/

                        // redirect to main page of locate to scan another chute

                        Response.Redirect("Locate.aspx");


                        
                    }
                    catch (Exception ex)
                    {
                        // activity logging

                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.TrolleyAttachLocate;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.FailedToAttach;
                        setclass.ExpectedBarcodeType = barcodetype;
                        setclass.Barcode = trolley_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(trolley_id_singles);



                        actlog.SaveUserActivity(setclass);

                        this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }

                }// chute type is 1

                else if (I_chute_type == 2) // multi
                { 
                    // check if chute is attached to a trolley
                    decimal chutes_trolley_id = 0;
                    try
                    {
                        chutes_trolley_id = locdao.Chute_Trolley(I_chute_barcode, I_user, I_terminal);
                    }
                    catch (Exception xx1)
                    {
                        // activity logging

                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.TrolleyAttachLocate;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.FailedToAttach;
                        setclass.ExpectedBarcodeType = barcodetype;
                        setclass.Barcode = trolley_barcode;
                        setclass.TerminalId = I_terminal;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(chutes_trolley_id);



                        actlog.SaveUserActivity(setclass);

                        this.Master.ErrorMessage = xx1.Message.Substring(xx1.Message.IndexOf(" ", 0), (xx1.Message.IndexOf("ORA", 1) - xx1.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }

                    // if no trolley is attached

                    if (chutes_trolley_id == 0)
                    {
                        // validate the trolley barcode using oms_attach_trolley.p_validate_trolley
                        decimal trolley_id_new = 0;

                        try
                        {
                            trolley_id_new = locdao.Validate_Trolley(I_chute_id, trolley_barcode, I_user, I_terminal);

                        }
                        catch (Exception xx2)
                        {

                            // activity logging
                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.ScanTrolleyForAttach;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.TrolleyScanFailed;
                            setclass.ExpectedBarcodeType = barcodetype;
                            setclass.Barcode = trolley_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = I_user;
                            setclass.ChuteId = decimal.ToInt32(I_chute_id);
                            setclass.TrolleyId = decimal.ToInt32(trolley_id_new);


                            actlog.SaveUserActivity(setclass);

                            this.Master.ErrorMessage = xx2.Message.Substring(xx2.Message.IndexOf(" ", 0), (xx2.Message.IndexOf("ORA", 1) - xx2.Message.IndexOf(" ", 0)));
                            this.Master.DisplayMessage = true;
                            this.Master.BarcodeValue = string.Empty;
                        }

                        // attach trolley

                        try
                        {
                            // if no errors then attach trolley to chute using oms_attach_trolley.p_attach_trolley_to_chute
                            locdao.Attach_Trolley(I_chute_id, trolley_id_new, I_user, I_terminal);

                            // Log user to chute using p_log_on_to_chute

                            /************ commented out for time being ************/
                            //locdao.Log_on_to_Chute(I_chute_barcode, I_user);
                            /************ commented out for time being ************/

                            // redirect to scan sku

                            Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + trolley_id_new + "&chutetype=" + I_chute_type + "&chutearea =" + I_chute_area);

                        }

                        catch (Exception xx3)
                        {
                            // activity logging

                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.TrolleyAttachLocate;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.FailedToAttach;
                            setclass.ExpectedBarcodeType = barcodetype;
                            setclass.Barcode = trolley_barcode;
                            setclass.TerminalId = I_terminal;
                            setclass.UserId = I_user;
                            setclass.ChuteId = decimal.ToInt32(I_chute_id);
                            setclass.TrolleyId = decimal.ToInt32(trolley_id_new);



                            actlog.SaveUserActivity(setclass);

                            this.Master.ErrorMessage = xx3.Message.Substring(xx3.Message.IndexOf(" ", 0), (xx3.Message.IndexOf("ORA", 1) - xx3.Message.IndexOf(" ", 0)));
                            this.Master.DisplayMessage = true;
                            this.Master.BarcodeValue = string.Empty;
                        }


                    }

                    else // another trolley is attached
                    {
                        // redirect to scan sku

                        Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + chutes_trolley_id + "&chutetype=" + I_chute_type + "&chutearea =" + I_chute_area);

                    
                    }
                
                }// chute type is 2


            }// end of post back

   
            
        }
    }
}