using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class RemoveForPackClearup : System.Web.UI.Page
    {

        #region "private"
        LocateDAO _locatedao = new LocateDAO();
        RemoveForPackDAO _removepackdao = new RemoveForPackDAO();
        UserActivity _useract = new UserActivity();
        ActivityLogDAO _uadao = new ActivityLogDAO();

        decimal _trolleyid;
        //decimal _locationid;
        //decimal _overflowtoteid;
        decimal _nextordernumber;
        string _toteservice;
        decimal _nextlocid;
        string _nextlocbarcode;
        string _nextloclabel;
        decimal _nextlocitemcount;
        string _totebarcodeprefix;
        string _totetypename;
        decimal _failedtoteid;
        string _failedtotelabel;
        string _failedtotebarcode;
        string _barcode;

        string _msgstring;
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
                // *** SG *** this.areaid.Value = _locatedao.Get_Chute_Area(decimal.Parse(this.chuteid.Value)).ToString();

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

                    this.step.Value = RemoveForPackClearupStep.ScanLocation.ToString();
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
                    this.step.Value = RemoveForPackClearupStep.Initial.ToString();
                }

            }
            else
            {

                _barcode = (this.Master.BarcodeValue.Length > 50) ?  this.Master.BarcodeValue.Substring(0,50) : this.Master.BarcodeValue;

                _barcode = _barcode.ToUpper();

                if (_barcode != string.Empty)
                {

                    switch (this.step.Value)
                    {
                        // Initial - scan chute to attach to chute
                        case "Initial":
                            {
                                /* Scan Chute and attach to chute for session */
                                try
                                {
                                    validatechute(_barcode);
                                    
                                    //
                                    // display tote service if moving on to a new order, 
                                    //     e.g. "STD in tote."
                                    // get the next order info and display, 
                                    //     e.g. "Remove 5 items(s) from 107A to a small tote", 
                                    //     or "Remove 1 item(s) from 110C to PS1819"
                                    // display "Scan Location Barcode" message
                                    //
                                    // if there is no order to move for this chute then return to Scan Chute message
                                    //

                                    decimal ? ordNumber = null;

                                    if (this.ordernumber.Value != string.Empty)
                                    {
                                        ordNumber = decimal.Parse(this.ordernumber.Value);
                                    }

                                    // get next order info
                                    _removepackdao.nextorderandloc(decimal.Parse(this.chuteid.Value), ordNumber, ref _nextordernumber, ref _toteservice, ref _nextlocid, ref _nextlocbarcode, ref _nextloclabel, ref _nextlocitemcount, ref _totebarcodeprefix, ref _totetypename, ref _failedtoteid, ref _failedtotelabel, ref _failedtotebarcode);
                                    
                                    if (_nextordernumber == 0)
                                    {
                                        // no order for this location - invite user to scan another chute
                                        ShowMessage("No orders in this chute. Scan another chute", MessageType.NormalMessage);
                                    }
                                    else
                                    {

                                        _msgstring = string.Empty;

                                        // if order has changed display the "STD in tote." message
                                        if (this.ordernumber.Value != String.Empty && decimal.Parse(this.ordernumber.Value) != _nextordernumber)
                                        {
                                            _msgstring = this.toteservice.Value + " in tote. ";
                                        }

                                        // display the remove X items from loc to tote/tote type message
                                        _msgstring += "Remove " + _nextlocitemcount.ToString() + " item(s) from " + _nextloclabel + " to ";
                                        if (_failedtoteid != 0)
                                        {
                                            _msgstring += _failedtotelabel;
                                        }
                                        else
                                        {
                                            _msgstring += " a " + _totetypename;
                                        }

                                        // new line and Scan location barcode message
                                        _msgstring += "<br> Scan location barcode";

                                        // assembled the string, now display it
                                        this.Master.MessageBoard = _msgstring;

                                        // save values we will need later -
                                        this.ordernumber.Value = _nextordernumber.ToString();
                                        this.toteservice.Value = _toteservice;
                                        this.nextlocbarcode.Value = _nextlocbarcode;
                                        this.nextloclabel.Value = _nextloclabel;
                                        this.failedtotelabel.Value = _failedtotelabel;
                                        this.failedtotebarcode.Value = _failedtotebarcode;
                                        this.totetypename.Value = _totetypename;

                                        // save next step
                                        this.step.Value = RemoveForPackClearupStep.ScanLocation.ToString();

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

                        //
                        // Validate the location - if valid invite user to scan the tote
                        //
                        case "ScanLocation":
                            {

                                try
                                {
                                    if (_barcode != this.nextlocbarcode.Value)
                                    {

                                        // log the failure
                                        _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                        _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                        _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPackClrup;
                                        _useract.EventType = (int)EventType.ScanLocation;
                                        _useract.UserId = Shared.CurrentUser;
                                        _useract.OrderNumber = int.Parse(this.ordernumber.Value);
                                        _useract.Barcode = _barcode;
                                        _useract.Value1 = this.nextlocbarcode.Value;
                                        _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.LocationValidationFailed;

                                        _uadao.SaveUserActivity(_useract);


                                        ShowMessage("Please scan correct barcode for location " + this.nextloclabel.Value, MessageType.ErrorConfirm);

                                    }
                                    else
                                    {

                                        // log the successful scan
                                        _useract.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
                                        _useract.ApplicationId = (int)ActivityLogEnum.ApplicationID.RemoveForPack;
                                        _useract.ModuleId = (int)ActivityLogEnum.ModuleID.RemoveForPackClrup;
                                        _useract.EventType = (int)EventType.ScanLocation;
                                        _useract.UserId = Shared.CurrentUser;
                                        _useract.OrderNumber = int.Parse(this.ordernumber.Value);
                                        _useract.Barcode = _barcode;
                                        _useract.Value1 = this.nextlocbarcode.Value;
                                        _useract.ResultCode = (Int32)ActivityLogEnum.ResultCd.Success;

                                        _uadao.SaveUserActivity(_useract);


                                        String msgStr = "Scan " + this.toteservice.Value + " to ";
                                        if (this.failedtotelabel.Value == string.Empty)
                                        {
                                            msgStr += "a " + this.totetypename.Value + ".";
                                        }
                                        else
                                        {
                                            msgStr += "tote " + this.failedtotelabel.Value + ".";
                                        }

                                        ShowMessage(msgStr, MessageType.NormalMessage);
                                        // ShowMessage("Scan tote", MessageType.NormalMessage);

                                        // save next step
                                        this.step.Value = RemoveForPackClearupStep.ScanTote.ToString();

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


                        // Scan tote - three possibilities - 
                        //
                        //     1) more locations for the same order
                        //
                        //     2) a new order
                        //
                        //     3) no more orders for this chute
                        //
                        case "ScanTote":
                            {
                                try
                                {

                                    // if order has already been partially moved ensure the tote entered
                                    // is the same as the one it has been partially moved to
                                    if (this.failedtotebarcode.Value != string.Empty && _barcode != this.failedtotebarcode.Value)
                                    {
                                        ShowMessage("Please scan correct barcode for tote " + this.failedtotelabel.Value, MessageType.ErrorConfirm);
                                    }
                                    else
                                    {
                                        _removepackdao.moveincompletetotote(decimal.Parse(this.chuteid.Value), decimal.Parse(this.ordernumber.Value), this.nextlocbarcode.Value, _barcode, User.Identity.Name, string.Empty, ref _nextordernumber);

                                        // no more orders - scan a new chute
                                        if (_nextordernumber == 0)
                                        {

                                            ShowMessage(this.toteservice.Value + " in tote. Scan chute", MessageType.NormalMessage);

                                            // save next step
                                            this.step.Value = RemoveForPackClearupStep.Initial.ToString();

                                            // clear out saved values
                                            this.chuteid.Value = string.Empty;
                                            this.ordernumber.Value = string.Empty;
                                            this.toteservice.Value = string.Empty;
                                            this.nextlocbarcode.Value = string.Empty;
                                            this.failedtotelabel.Value = string.Empty;
                                            this.failedtotebarcode.Value = string.Empty;
                                            this.totetypename.Value = string.Empty;


                                        }
                                        else
                                        {

                                            _msgstring = string.Empty;

                                            // if order has changed tell user which toteservice
                                            if (decimal.Parse(this.ordernumber.Value) == _nextordernumber)
                                            {
                                                _msgstring = this.toteservice.Value + " in tote. ";
                                            }

                                            // get next location, etc. and display details
                                            // get next order info
                                            _removepackdao.nextorderandloc(decimal.Parse(this.chuteid.Value), _nextordernumber, ref _nextordernumber, ref _toteservice, ref _nextlocid, ref _nextlocbarcode, ref _nextloclabel, ref _nextlocitemcount, ref _totebarcodeprefix, ref _totetypename, ref _failedtoteid, ref _failedtotelabel, ref _failedtotebarcode);

                                            // display the remove X items from loc to tote/tote type message
                                            _msgstring += "Remove " + _nextlocitemcount.ToString() + " item(s) from " + _nextloclabel + " to ";
                                            if (_failedtoteid != 0)
                                            {
                                                _msgstring += _failedtotelabel;
                                            }
                                            else
                                            {
                                                _msgstring += " a " + _totetypename;
                                            }

                                            // new line and Scan location barcode message
                                            _msgstring += "<br> Scan location barcode";

                                            // assembled the string, now display it
                                            this.Master.MessageBoard = _msgstring;

                                            // save values we will need later -
                                            this.ordernumber.Value = _nextordernumber.ToString();
                                            this.toteservice.Value = _toteservice;
                                            this.nextlocbarcode.Value = _nextlocbarcode;
                                            this.nextloclabel.Value = _nextloclabel;
                                            this.failedtotelabel.Value = _failedtotelabel;
                                            this.failedtotebarcode.Value = _failedtotebarcode;
                                            this.totetypename.Value = _totetypename;

                                            // save next step
                                            this.step.Value = RemoveForPackClearupStep.ScanLocation.ToString();
                                    

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
                    //this.Master. = msg;
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

    }
}