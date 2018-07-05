using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using System.Configuration;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;


namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class RemoveForPack : System.Web.UI.Page
    {
       #region "private"
        LocateDAO _locatedao = new LocateDAO();
        RemoveForPackDAO _removepackdao = new RemoveForPackDAO();
        UserActivity _useract = new UserActivity();
        ActivityLogDAO _uadao = new ActivityLogDAO();

        decimal _trolleyid;
        decimal _locationid;
        decimal _overflowtoteid;
        string _barcode;
       #endregion

        private void validatechute(string barcode)
        {
            decimal chutetype;

            // validate chute using oms_attach_trolley.p_validate_chute
            this.chuteid.Value = _locatedao.Validate_Chute(barcode, User.Identity.Name, null).ToString();

            // find the chute type if it is singles = 1 or multi = 2
            chutetype = _locatedao.Get_chute_type(decimal.Parse(this.chuteid.Value));

            // If a multi chute
            if (chutetype == 2)
            {
                this.areaid.Value = _locatedao.Get_Chute_Area(decimal.Parse(this.chuteid.Value)).ToString();

                _trolleyid = _locatedao.Chute_Trolley(barcode, User.Identity.Name, null);
                this.trolleyid.Value = _trolleyid.ToString();

                if (_trolleyid > 0)
                {
                    if (this.ordernumber.Value == string.Empty)
                    {
                        //string service = _locatedao.ServiceForLocation(decimal.Parse(this.ordernumber.Value));

                    }
                    else
                        ShowMessage("Scan SKU barcode", MessageType.NormalMessage);
                    
                    this.step.Value = RemoveForPackStep.ScanSku.ToString();

                }
                else
                {
                    ShowMessage("No Trolley Attached to Chute", MessageType.ErrorConfirm);
                }
            }
            else
            {
                ShowMessage("Not Multi Item Order Chute", MessageType.ErrorConfirm);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                {
                    // on page load display this message
                    ShowMessage("Scan Chute", MessageType.NormalMessage);
                    this.step.Value = RemoveForPackStep.Initial.ToString();
                }

            }
            else
            {
                _barcode = (this.Master.BarcodeValue.Length > 50) ?  this.Master.BarcodeValue.Substring(0,50) : this.Master.BarcodeValue;

                if (_barcode != string.Empty)
                {

                    _barcode = _barcode.ToUpper();

                    switch (this.step.Value)
                    {
                        // Initial - scan chute to attach to chute
                        case "Initial":
                            {
                                /* Scan Chute and attach to chute  for session */
                                try
                                {
                                    validatechute(_barcode);

                                    string itemsInChute = "F";
                                    
                                    _removepackdao.itemsinchute(decimal.Parse(this.chuteid.Value), ref itemsInChute);

                                    if (itemsInChute == "F")
                                    {
                                        ShowMessage("No orders for this chute, please scan another chute", MessageType.NormalMessage);
                                        this.step.Value = RemoveForPackStep.Initial.ToString();
                                    }
                                    else
                                    {
                                        this.Master.MessageBoard = "Scan SKU barcode";
                                        this.step.Value = RemoveForPackStep.ScanSku.ToString();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    string exceptionMessage = ex.Message;
                                    if (isErrorMessage(ref exceptionMessage))
                                    {
                                        ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                    }
                                    else
                                    {
                                        // There aren't really messages passed back for non error conditions in this back end proc
                                        ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                    }
                                }
  
                                break;
                            }
                        case "ScanSku":
                            {

                                // If a chute bacode has been scanned
                                if (_barcode.Substring(0,2) == "CH")
                                {
                                    this.step.Value = RemoveForPackStep.Initial.ToString();
                                    validatechute(_barcode);
                                    string itemsInChute = "F";

                                    _removepackdao.itemsinchute(decimal.Parse(this.chuteid.Value), ref itemsInChute);

                                    if (itemsInChute == "F")
                                    {
                                        ShowMessage("No orders for this chute, please scan another chute", MessageType.NormalMessage);
                                        this.step.Value = RemoveForPackStep.Initial.ToString();
                                    }
                                    else
                                    {
                                        this.Master.MessageBoard = "Scan SKU barcode";
                                        this.step.Value = RemoveForPackStep.ScanSku.ToString();
                                    }

                                }
                                else
                                {
                                    try
                                    {
                                        itemid.Value = _locatedao.Find_item(decimal.Parse(this.chuteid.Value), _barcode, User.Identity.Name, string.Empty, decimal.Parse(this.areaid.Value)).ToString();

                                        if (decimal.Parse(itemid.Value) > 0)
                                        {
                                            
                                            this.singleschutelabel.Value = string.Empty;
                                            this.singleschuteid.Value = string.Empty;

                                            string IsManualSingle = "F";
                                            IsManualSingle = _locatedao.OrderForManualArea(decimal.Parse(this.areaid.Value), decimal.Parse(this.chuteid.Value), decimal.Parse(itemid.Value));

                                            if (IsManualSingle.ToUpper() != "F")
                                            {

                                                string[] singleschuteval;

                                                singleschuteval = IsManualSingle.Split('-');

                                                this.singleschuteid.Value = singleschuteval[0];
                                                this.singleschutelabel.Value = singleschuteval[1];
                                                this.step.Value = RemoveForPackStep.ScanSinglesChute.ToString();

                                                string msg = "Scan " + singleschuteval[1];
                                                ShowMessage(msg, MessageType.NormalMessage);

                                            }
                                            else
                                            {

                                                try
                                                {
                                                    string location_name = _locatedao.Find_location_name(decimal.Parse(itemid.Value));
                                                    if (location_name == null)
                                                    {
                                                        ShowMessage("Location not found for Item", MessageType.ErrorConfirm);
                                                        this.Master.MessageBoard = "Scan SKU Barcode";
                                                    }

                                                    try
                                                    {
                                                        // if last item then pre-warn user to remove the order
                                                        decimal itemCount = 0;
                                                        _removepackdao.itemsnotlocated(decimal.Parse(itemid.Value), ref itemCount);

                                                        string removeOrderStr = "";

                                                        if (itemCount < 2) removeOrderStr= ", then remove order";

                                                        string locstr = LocationMessage(location_name, removeOrderStr);
                                                        
                                                        // if (itemCount < 2) locstr += ", then remove order";

                                                        this.step.Value = RemoveForPackStep.ScanLocation.ToString();
                                                        ShowMessage(locstr, MessageType.NormalMessage);
                                                    }
                                                    catch
                                                    {
                                                        ShowMessage("Error getting location details", MessageType.ErrorConfirm);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    string exceptionMessage = ex.Message;
                                                    if (isErrorMessage(ref exceptionMessage))
                                                    {
                                                        ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                                    }
                                                    else
                                                    {
                                                        // There aren't really messages passed back for non error conditions in this back end proc
                                                        ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (itemid.Value == "0")
                                            {
                                                // if item not found
                                                ShowMessage("Item not for this chute", MessageType.ErrorConfirm);
                                                this.Master.BarcodeValue = string.Empty;
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exceptionMessage = ex.Message;
                                        if (isErrorMessage(ref exceptionMessage))
                                        {
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                        else
                                        {
                                            // There aren't really messages passed back for non error conditions in this back end proc
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                    }
                                }
                                break;
                            }
                        case "ScanLocation":
                            {
                                //This could be a overflow tote or a trolley location
                                if (_barcode.Substring(0, 2) == "LC")
                                {
                                    try
                                    {
                                        _locationid = _locatedao.Validate_Location(_barcode, decimal.Parse(itemid.Value), User.Identity.Name, null);

                                        if (_locationid == 0)
                                        {
                                            ShowMessage("Location Validation Failed", MessageType.ErrorConfirm);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exceptionMessage = ex.Message;
                                        if (isErrorMessage(ref exceptionMessage))
                                        {
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                        else
                                        {
                                            // There aren't really messages passed back for non error conditions in this back end proc
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                    }
                                }
                                else if (_barcode.Substring(0, 2) == "OT")
                                {
                                    try
                                    {
                                        _overflowtoteid = _locatedao.Validate_Tote(_barcode, decimal.Parse(itemid.Value), User.Identity.Name, null);

                                        if (_overflowtoteid == 0)
                                        {
                                            ShowMessage("Overflow Tote Validation Failed", MessageType.ErrorConfirm);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exceptionMessage = ex.Message;
                                        if (isErrorMessage(ref exceptionMessage))
                                        {
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                        else
                                        {
                                            // There aren't really messages passed back for non error conditions in this back end proc
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                    }                                       
                                }
                                else
                                {
                                    ShowMessage("Invalid location or overflow tote barcode", MessageType.ErrorConfirm);
                                    this.Master.BarcodeValue = string.Empty;
                                }

                                if (_locationid > 0 || _overflowtoteid > 0)
                                {


                                    string fullyLocatedInd = "F";
                                    decimal ordernumber = 0;
                                    decimal ordervolume = 0;
                                    string toteService = string.Empty;
                                    string toteBarcodePrefix = string.Empty;
                                    string toteTypeName = string.Empty;
                                    decimal currLocId = 0;
                                    string currLocBarcode = string.Empty;
                                    string currLocLabel = string.Empty;
                                    decimal numItems = 0;


                                    try
                                    {
                                        _removepackdao.locateitem(decimal.Parse(itemid.Value),
                                                                  _locationid, _overflowtoteid,
                                                                  decimal.Parse(this.trolleyid.Value),
                                                                  User.Identity.Name, null, ref ordernumber,
                                                                  ref fullyLocatedInd, ref ordervolume, ref toteService,
                                                                  ref toteBarcodePrefix, ref toteTypeName, ref currLocId,
                                                                  ref currLocBarcode, ref currLocLabel, ref numItems);

                                        if (fullyLocatedInd == "T")
                                        {
                                            this.Master.MessageBoard = "<embed id='snd' src='" + ConfigurationManager.AppSettings["SuccessSound"] + "' autostart='true' hidden='true'></embed>" +
                                            "Remove " + numItems.ToString() + " item(s) from " + currLocLabel + " to a " + toteTypeName +
                                            "<br />Scan location barcode";
                                            this.step.Value = RemoveForPackStep.ReScanLocation.ToString();

                                            this.locationbarcode.Value = currLocBarcode;
                                            this.totetype.Value = toteTypeName;
                                            this.toteservice.Value = toteService;
                                        }
                                        else
                                        {
                                            this.step.Value = RemoveForPackStep.ScanSku.ToString();
                                            ShowMessage("Scan SKU barcode", MessageType.NormalMessage);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string exceptionMessage = ex.Message;
                                        if (isErrorMessage(ref exceptionMessage))
                                        {
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                        else
                                        {
                                            // There aren't really messages passed back for non error conditions in this back end proc
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                    }                                        
                                }

                                break;
                            }
                        case "ReScanLocation":
                            {
                                if (_barcode == this.locationbarcode.Value)
                                {
                                    // log the activity - success
                                    _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                    _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                    _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPack;
                                    _useract.EventType = (int)EventType.RescanLocation;
                                    _useract.UserId = Shared.CurrentUser;
                                    _useract.Barcode = _barcode;
                                    _useract.Value1 = this.locationbarcode.Value;
                                    _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;
                                    
                                    _uadao.SaveUserActivity(_useract);

                                    // set step and show message
                                    this.step.Value = RemoveForPackStep.ScanTote.ToString();

                                    String msgStr = "Scan " + this.toteservice.Value + " to ";
                                    if (this.totelabel.Value == string.Empty)
                                    {
                                        msgStr += "a " + this.totetype.Value + ".";
                                    }
                                    else
                                    {
                                        msgStr += "tote " + this.totelabel.Value + ".";
                                    }

                                    ShowMessage(msgStr, MessageType.NormalMessage);

                                }
                                else
                                {

                                    // log the activity - failure
                                    _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                    _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                    _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPack;
                                    _useract.EventType = (int)EventType.RescanLocation;
                                    _useract.UserId = Shared.CurrentUser;
                                    _useract.Barcode = _barcode;
                                    _useract.Value1 = this.locationbarcode.Value;
                                    _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;

                                    _uadao.SaveUserActivity(_useract);


                                    // show the error message
                                    ShowMessage("Incorrect Location", MessageType.ErrorConfirm);

                                }
                                break;
                            }
                        case "ScanTote":
                            {
                                if (this.totebarcode.Value != string.Empty && this.totebarcode.Value != _barcode)
                                {
                                    ShowMessage("Scan to tote: " + this.totelabel.Value, MessageType.ErrorConfirm);
                                }
                                else
                                {
                                    decimal nextlocid = 0;
                                    string nextlocbarcode = string.Empty;
                                    string nextloclabel = string.Empty;
                                    decimal itemcount = 0;
                                    string totelabel = string.Empty;

                                    try
                                    {
                                        _removepackdao.movetotote(locationbarcode.Value, _barcode, User.Identity.Name, null, ref nextlocid, ref nextlocbarcode, ref nextloclabel, ref totelabel, ref itemcount);

                                        if (nextlocid == 0)
                                        {
                                            
                                            // no more items for this order
                                            // need to see if there are any more orders left in this chute

                                            string msg = this.toteservice.Value + " in tote";
                                            string itemsInChute = "F";

                                            _removepackdao.itemsinchute(decimal.Parse(this.chuteid.Value), ref itemsInChute);

                                            if (itemsInChute == "F")
                                            {
                                                msg += ". No more orders in this chute, please scan another chute";
                                                this.step.Value = RemoveForPackStep.Initial.ToString();
                                            }
                                            else
                                            {
                                                msg += ", scan next item for locate";
                                                this.step.Value = RemoveForPackStep.ScanSku.ToString();
                                            }

                                            ShowMessage(msg, MessageType.NormalMessage);

                                            // ShowMessage(this.toteservice.Value + " in tote, scan next item for locate", MessageType.NormalMessage);
                                            this.totebarcode.Value = string.Empty;
                                            this.totelabel.Value = string.Empty;
                                            // this.step.Value = RemoveForPackStep.ScanSku.ToString();

                                        }
                                        else
                                        {
                                            ShowMessage("Remove " + itemcount.ToString() + " item(s) from " + nextloclabel + " to " + totelabel + "<br />Scan location barcode", MessageType.NormalMessage);
                                            this.totebarcode.Value = _barcode;
                                            this.totelabel.Value = totelabel;
                                            this.locationbarcode.Value = nextlocbarcode;
                                            this.step.Value = RemoveForPackStep.ReScanLocation.ToString();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        string exceptionMessage = ex.Message;
                                        if (isErrorMessage(ref exceptionMessage))
                                        {
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                        else
                                        {
                                            // There aren't really messages passed back for non error conditions in this back end proc
                                            ShowMessage(exceptionMessage, MessageType.ErrorConfirm);
                                        }
                                    }
                                }
                                break;
                            }
                        case "ScanSinglesChute":
                            {
                                
                                string singleschutelabel = this.singleschutelabel.Value;
                                decimal singleschuteid = decimal.Parse(this.singleschuteid.Value);

                                if (_barcode == string.Empty)
                                {

                                    ShowMessage("Invalid scan. Please scan again.", MessageType.ErrorConfirm);
                                    this.Master.MessageBoard = "Scan " + singleschutelabel;

                                }
                                else
                                {

                                    LocateDAO locadao = new LocateDAO();

                                    // check if scanned chute is the same as the one passed in

                                    decimal scannedchuteid = 0;

                                    try
                                    {
                                        scannedchuteid = locadao.PreValidateChute(_barcode);
                                    }
                                    catch (Exception ex)
                                    {
                                        // activity logging
                                        _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                        _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                        _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPack;
                                        _useract.EventType = (int)EventType.ScanLocation;
                                        _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;
                                        _useract.ExpectedBarcodeType = "Chute barcode";
                                        _useract.Barcode = _barcode;
                                        _useract.UserId = Shared.CurrentUser;
                                        _useract.ChuteId = (int)singleschuteid;
                                        _useract.ItemNumber = Int32.Parse(this.itemid.Value);

                                        _uadao.SaveUserActivity(_useract);


                                        // display error
                                        string msg = ex.Message.Substring(ex.Message.IndexOf(" ", 0), ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0));
                                        ShowMessage(msg, MessageType.ErrorConfirm);
                                        this.Master.BarcodeValue = string.Empty;
                                    }

                                    if (scannedchuteid != 0)
                                    {
                                        if (scannedchuteid == singleschuteid)
                                        {
                                            // chute id is correct - move on to next SKU
                                            this.step.Value = RemoveForPackStep.ScanSku.ToString();
                                            ShowMessage("Scan SKU barcode", MessageType.NormalMessage);
                                            this.singleschutelabel.Value = string.Empty;
                                            this.singleschuteid.Value = string.Empty;
                                        }
                                        else
                                        {

                                            ShowMessage("Invalid scan. Please scan again.", MessageType.ErrorConfirm);

                                            // activity logging
                                            _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                            _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                            _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPack;
                                            _useract.EventType = (int)EventType.LogOffChuteForLocate;
                                            _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                                            _useract.ExpectedBarcodeType = "Chute";
                                            _useract.Barcode = _barcode;
                                            _useract.UserId = Shared.CurrentUser;
                                            _useract.ChuteId = (int)singleschuteid;
                                            _useract.ItemNumber = Int32.Parse(this.itemid.Value);
                                            _useract.SessionEndDateTime = DateTime.Now;

                                            _uadao.SaveUserActivity(_useract);

                                        }
                                    }
                                    else
                                    {

                                        ShowMessage("Invalid scan. Please scan again.", MessageType.ErrorConfirm);

                                        // activity logging
                                        _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                        _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                        _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPack;
                                        _useract.EventType = (int)EventType.LogOffChuteForLocate;
                                        _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.InvalidChuteType;
                                        _useract.ExpectedBarcodeType = "Chute";
                                        _useract.Barcode = _barcode;
                                        _useract.UserId = Shared.CurrentUser;
                                        _useract.ChuteId = (int)singleschuteid;
                                        _useract.ItemNumber = Int32.Parse(this.itemid.Value);
                                        _useract.SessionEndDateTime = DateTime.Now;

                                        _uadao.SaveUserActivity(_useract);

                                    }

                                }
                                break;
                            }

                    }
                }

            }
            this.Master.BarcodeValue = string.Empty;
        }

        protected bool isErrorMessage(ref string msg)
        {
            string theMessage = "";
            bool result = true;

            try
            {
                string[] lines = msg.Split("\n".ToCharArray());
                theMessage = lines[0];
            }
            catch
            {
                theMessage = msg;
                return false;
            }
            if (theMessage.ToUpper().IndexOf("ERROR: ") > 0)
            {
                msg = theMessage.Substring(18);
            }
            else
            {
                msg = theMessage.Substring(11);
                result = false;
            }
            return result;
        }
 

        private enum MessageType
        {
            ErrorConfirm = 1,
            SussessConfirm = 2,
            NormalMessage = 3

        }


        private void ShowMessage(string msg, MessageType type)
        {

            switch (type)
            {
                case MessageType.SussessConfirm:
                    this.Master.SuccessMessage = msg;
                    this.Master.DisplayMessage = true;
                    break;
                case MessageType.ErrorConfirm:
                    this.Master.ErrorMessage = msg;
                    this.Master.DisplayMessage = true;
                    break;
                case MessageType.NormalMessage:
                    this.Master.MessageBoard = msg;
                    break;

            }
        }

        private string LocationMessage(string locationlabel, string removeOrderStr)
        {

                string[] lines = locationlabel.Split('.');

                List<string> labelline = new List<string>();

                foreach (string line in lines)
                {
                    labelline.Add(line);
                }



                StringBuilder sb = new StringBuilder();

                if (!String.IsNullOrEmpty(labelline[0]))
                {
                    if (!String.IsNullOrEmpty(labelline[1]))
                    {
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='font-size:20px;padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelline[0] + removeOrderStr + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelline[1] + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                    }

                    else
                    {
                        sb.Append("<div>");
                        sb.Append("<table width='100%' cellspacing='0px' cellpadding='0px' style='border-width:1px;border-collapse:collapse; border-style:solid;border-color:White;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='font-size:24px;padding-left:2px;border-width:2px;border-style:solid;border-color:White;'>" + labelline[0] + removeOrderStr + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                    }
                }

                return sb.ToString();

        }

    }

}