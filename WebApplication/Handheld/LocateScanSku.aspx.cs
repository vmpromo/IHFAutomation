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
    public partial class LocateScanSku : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string IsManualSingle = "F";
            string chute_label = string.Empty;
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            decimal I_chute_id = decimal.Parse(Request.QueryString["chuteID"].ToString());
            string I_chute_barcode = Request.QueryString["chutebarcode"].ToString();
            string I_user = Request.QueryString["userlogon"].ToString();
            decimal I_trolley_id = decimal.Parse(Request.QueryString["trolleyid"].ToString());
            decimal I_chute_type = decimal.Parse(Request.QueryString["chutetype"].ToString());
            decimal I_chute_area = decimal.Parse(Request.QueryString["chutearea"]==null?"0":Request.QueryString["chutearea"].ToString());
            

            //string I_terminal = this.Master.HostName;
            string I_terminal = null;


            UserActivity setclass = new UserActivity();
            ActivityLogDAO actlog = new ActivityLogDAO();

            if (!IsPostBack)
            {
                // on page load display this message
                this.Master.MessageBoard = "Scan SKU Barcode";
            }
            else
            {

                string sku_barcode = this.Master.BarcodeValue;
                if (sku_barcode == string.Empty)
                {
                    this.Master.ErrorMessage = "Invalid Scan. Please scan again";
                    this.Master.DisplayMessage = true;
                    this.Master.BarcodeValue = string.Empty;

                }
                else
                {


                    if (sku_barcode.Length > 50)
                    {
                        sku_barcode = sku_barcode.Substring(0, 50);

                    }

                    LocateDAO locdao = new LocateDAO();

                    decimal item_id = 0;
                    string location_name = null;

                    // the user can either scan a sku or another chute


                    // check if it is a chute or sku barcode

                    if (sku_barcode.Length < 2)
                    {
                        this.Master.ErrorMessage = "Invalid Barcode";
                        this.Master.DisplayMessage = true;
                        this.Master.BarcodeValue = string.Empty;

                    }
                    else
                    {
                        string chk_barcode = sku_barcode.Substring(0, 2).ToUpper();


                        if (chk_barcode.ToUpper() == "CH")
                        {
                            // user has scanned the chute

                            decimal chute_id = 0;
                            decimal chute_type = 0;
                            LocateDAO locdaoch = new LocateDAO();
                            string chute_barcode = sku_barcode;

                            string barcodetype_ch = "Chute";
                            try
                            {




                                // logoff user from previous session


                                /************ commented out for time being ************/
                                //locdaoch.Log_off_Chute(I_chute_id, I_user);


                                // activity logging

                                setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                                setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                                setclass.EventType = (Int32)EventType.LogOffChuteForLocate;
                                setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;
                                setclass.ExpectedBarcodeType = barcodetype_ch;
                                setclass.Barcode = chute_barcode;
                                setclass.TerminalId = I_terminal;
                                setclass.UserId = I_user;
                                setclass.ChuteId = decimal.ToInt32(chute_id);



                                actlog.SaveUserActivity(setclass);
                                /************ commented out for time being ************/



                                // validate chute using oms_attach_trolley.p_validate_chute

                                chute_id = locdaoch.Validate_Chute(chute_barcode, I_user, I_terminal);

                                // Log user to chute using p_log_on_to_chute


                                /************ commented out for time being ************/
                                //locdaoch.Log_on_to_Chute(chute_barcode, I_user);
                                /************ commented out for time being ************/


                                // find the chute type if it is singles = 1 or multi = 2

                                chute_type = locdaoch.Get_chute_type(chute_id);

                                if (chute_type == 2)
                                {
                                    I_chute_area = locdao.Get_Chute_Area(chute_id);

                                }


                                // find the trolley attached p_chute_trolley

                                decimal trolley_id_ch = locdaoch.Chute_Trolley(chute_barcode, I_user, I_terminal);

                                if (trolley_id_ch == 0)
                                {
                                    // if the trolley id is null then ask user to scan trolley
                                    // redirect to attach trolley

                                    // no trolley attached to chute
                                    if (chute_type == 1) // singles
                                    {
                                        // redirect to scan trolley page
                                        Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + I_user + "&chutetype=" + chute_type + "&chutearea =" + I_chute_area);
                                    }
                                    else if (chute_type == 2) // multi
                                    {
                                        // redirect to scan trolley page
                                        Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + I_user + "&chutetype=" + chute_type + "&chutearea =" + I_chute_area);
                                    }
                                }
                                else
                                {

                                    // trolley is already attached
                                    if (chute_type == 1) // singles
                                    {
                                        // redirect to scan trolley page
                                        Response.Redirect("LocateTrolleyAttach.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + I_user + "&chutetype=" + chute_type + "&chutearea =" + I_chute_area);
                                    }
                                    else if (chute_type == 2) // multi
                                    {
                                        // redirect to scan sku

                                        Response.Redirect("LocateScanSku.aspx?chuteID=" + chute_id + "&chutebarcode=" + chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + trolley_id_ch + "&chutetype=" + chute_type + "&chutearea =" + I_chute_area);

                                    }
                                    else
                                    {
                                        this.Master.ErrorMessage = "Wrong Chute Type";
                                        this.Master.DisplayMessage = true;
                                        this.Master.BarcodeValue = string.Empty;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                // activity logging


                                setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                                setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                                setclass.EventType = (Int32)EventType.ScanChuteForAttach;
                                setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.ChuteScanFailed;
                                setclass.ExpectedBarcodeType = barcodetype_ch;
                                setclass.Barcode = chute_barcode;
                                setclass.TerminalId = I_terminal;
                                setclass.UserId = I_user;
                                setclass.ChuteId = decimal.ToInt32(chute_id);


                                actlog.SaveUserActivity(setclass);


                                this.Master.ErrorMessage = ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                                this.Master.DisplayMessage = true;
                                this.Master.BarcodeValue = string.Empty;
                            }
                        }
                        else
                        {
                            // user has scanned a SKU
                            // Validate SKU
                            // find item using p_find_chute_item
                            string barcodetype = "SKU";
                            try
                            {



                                item_id = locdao.Find_item(I_chute_id, sku_barcode, I_user, I_terminal, I_chute_area);


                                if (item_id > 0)
                                {
                                    IsManualSingle = locdao.OrderForManualArea(I_chute_area, I_chute_id, item_id);

                                    if (IsManualSingle.ToUpper() != "F")
                                    {

                                        string[] T_chute_Val;


                                        T_chute_Val = IsManualSingle.Split('-');


                                        Response.Redirect("LocateManualAreaChute.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&itemid=" + item_id + "&skubarcode=" + sku_barcode + "&chutearea=" + I_chute_area + "&tchutelabel=" + T_chute_Val[1] + "&tchuteid=" + T_chute_Val[0]);

                                    }

                                }


                                if (item_id == 0)
                                {
                                    // if item not found
                                    this.Master.ErrorMessage = "Item not for this chute";
                                    this.Master.DisplayMessage = true;
                                    this.Master.BarcodeValue = string.Empty;
                                    this.Master.MessageBoard = "Please Scan the SKU Barcode";
                                }
                                else
                                {
                                    // else display trolley location using p_location_name_for_item



                                    location_name = locdao.Find_location_name(item_id);
                                    if (location_name == null)
                                    {
                                        this.Master.ErrorMessage = "Location not found for Item";
                                        this.Master.DisplayMessage = true;

                                        this.Master.MessageBoard = "Scan SKU Barcode";
                                    }
                                    else
                                    {
                                        // display location label

                                        // redirect to scan trolley location
                                        //this.Master.MessageBoard = "location is " + location_name;
                                        Response.Redirect("LocateScanLocation.aspx?chuteID=" + I_chute_id + "&chutebarcode=" + I_chute_barcode + "&userlogon=" + I_user + "&trolleyid=" + I_trolley_id + "&chutetype=" + I_chute_type + "&itemid=" + item_id + "&skubarcode=" + sku_barcode + "&locationname=" + location_name + "&chutearea=" + I_chute_area);
                                    }

                                }
                            }
                            catch (Exception ex1)
                            {
                                // activity logging
                                setclass.AppSystem = (Int32)ActivityLogEnum.AppSystem.IHF;
                                setclass.ApplicationId = (Int32)ActivityLogEnum.ApplicationID.AttachAndLocate;
                                setclass.ModuleId = (Int32)ActivityLogEnum.ModuleID.AttachAndlocate;
                                setclass.EventType = (Int32)EventType.ScanItemForLocate;
                                setclass.ResultCode = (Int32)ActivityLogEnum.ResultCd.ItemvalidationFailed;
                                setclass.ExpectedBarcodeType = barcodetype;
                                setclass.Barcode = sku_barcode;
                                setclass.TerminalId = I_terminal;
                                setclass.UserId = I_user;
                                setclass.ChuteId = decimal.ToInt32(I_chute_id);
                                setclass.TrolleyId = decimal.ToInt32(I_trolley_id);
                                setclass.ItemNumber = decimal.ToInt32(item_id);




                                actlog.SaveUserActivity(setclass);

                                this.Master.ErrorMessage = ex1.Message.Substring(ex1.Message.IndexOf(" ", 0), (ex1.Message.IndexOf("ORA", 1) - ex1.Message.IndexOf(" ", 0)));
                                this.Master.DisplayMessage = true;
                                this.Master.BarcodeValue = string.Empty;

                            }
                        }// sku

                    } // barcode length greater than 2

                } // barcode not empty

            }//end of else



        }




    }
}