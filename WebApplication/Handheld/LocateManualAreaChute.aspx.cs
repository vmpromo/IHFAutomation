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
    public partial class LocateManualAreaChute : System.Web.UI.Page
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
            string I_chute_area = Request.QueryString["chutearea"].ToString();
            string I_T_chute_label = Request.QueryString["tchutelabel"].ToString();
            decimal I_T_chute_id = decimal.Parse(Request.QueryString["tchuteid"].ToString());


            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            string barcodetype_sku = "Chute";

            try
            {

                StringBuilder sb = new StringBuilder();

                if (!String.IsNullOrEmpty(I_T_chute_label))
                {

                    sb.Append("<div>");
                    sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='font-size:24px;padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + I_T_chute_label + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

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
                setclass.UserId = I_user;
                setclass.ChuteId = decimal.ToInt32(I_chute_id);
                setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                setclass.ItemNumber = decimal.ToInt32(I_item);


                actlog.SaveUserActivity(setclass);



                this.Master.ErrorMessage = "Error: Retrieving Chute label";
                this.Master.DisplayMessage = true;

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
                else
                {

                    if (loc_barcode.Length > 50)
                    {
                        loc_barcode = loc_barcode.Substring(0, 50);

                    }

                    LocateDAO locdao = new LocateDAO();


                    //  Check if the scan chute id similar to target chute id.
                    decimal chute_id = 0;

                    try
                    {
                        chute_id = locdao.PreValidateChute(loc_barcode);
                    }
                    catch (Exception ex3)
                    {


                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.ScanLocation;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;
                        setclass.ExpectedBarcodeType = "Chute barcode";
                        setclass.Barcode = loc_barcode;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                        setclass.ItemNumber = decimal.ToInt32(I_item);

                        actlog.SaveUserActivity(setclass);


                        this.Master.ErrorMessage = ex3.Message.Substring(ex3.Message.IndexOf(" ", 0), (ex3.Message.IndexOf("ORA", 1) - ex3.Message.IndexOf(" ", 0)));
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;
                    }



                    if (chute_id != 0)
                    {


                        if (chute_id == I_T_chute_id)
                        {
                            // redirect to scan sku for scanning another sku or chute
                            Response.Redirect("LocateScanSku.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&chutearea=" + I_chute_area);
                        }
                        else
                        {

                            this.Master.ErrorMessage = "Invalid Chute. Please scan again";
                            this.Master.DisplayMessage = true;
                            this.Master.BarcodeValue = string.Empty;

                            // activity logging
                            setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                            setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                            setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                            setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                            setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                            setclass.ExpectedBarcodeType = "Chute";
                            setclass.Barcode = I_chute_barcode;
                            setclass.UserId = I_user;
                            setclass.ChuteId = decimal.ToInt32(I_chute_id);
                            setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                            setclass.ItemNumber = decimal.ToInt32(I_item);
                            setclass.SessionEndDateTime = DateTime.Now;



                            actlog.SaveUserActivity(setclass);

                        }


                    }

                    else
                    {

                        this.Master.ErrorMessage = "Invalid Chute. Please scan again";
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;




                        // activity logging
                        setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                        setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                        setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                        setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                        setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                        setclass.ExpectedBarcodeType = "Chute";
                        setclass.Barcode = I_chute_barcode;
                        setclass.UserId = I_user;
                        setclass.ChuteId = decimal.ToInt32(I_chute_id);
                        setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                        setclass.ItemNumber = decimal.ToInt32(I_item);
                        setclass.SessionEndDateTime = DateTime.Now;



                        actlog.SaveUserActivity(setclass);

                    }

                }

            }
        }
    }
}