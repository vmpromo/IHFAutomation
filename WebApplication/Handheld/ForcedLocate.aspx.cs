using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects;
using System.Data;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class ForcedLocate : System.Web.UI.Page
    {
        private ForcedLocateDAO _forcedLocateDao = new ForcedLocateDAO();

        private string _message = string.Empty;
        private string _barcode = string.Empty;
            
        private bool ProcessSource()
        {
            try
            {
                decimal SrcFailedToteId = _forcedLocateDao.ValidateFailedTote(_barcode);
                ViewState["SrcFailedToteId"] = SrcFailedToteId;
                _message = "Scan Item";
                step.Value = ForcedLocateStep.ItemScan.ToString();
                return true;
            }
            catch (Exception exception)
            {
                _message = "Scan current location";
                string exceptionmessage = exception.Message;
                if (isErrorMessage(ref exceptionmessage))
                {
                    this.Master.ErrorMessage = exceptionmessage;
                }
                else
                {
                    this.Master.ErrorMessage = exceptionmessage;
                }
                this.Master.DisplayMessage = true;
                return false;
            }
        }

        private bool ProcessSku()
        {

            decimal SrcFailtedToteId = 0;

            if (ViewState["SrcFailedToteId"] != null)
            {
                SrcFailtedToteId = (decimal)ViewState["SrcFailedToteId"];
            }

            try
            {
               _forcedLocateDao.ValidateSku(_barcode, SrcFailtedToteId, User.Identity.Name);
               _message = "Scan destination location";
               ViewState["ItemBarcode"] = _barcode;
               step.Value = ForcedLocateStep.DestinationScan.ToString();
               return true;

            }
            catch (Exception exception)
            {
                _message = "Scan Item";

                string exceptionmessage = exception.Message;
                if (isErrorMessage(ref exceptionmessage))
                {
                    this.Master.ErrorMessage = exceptionmessage;
                }
                else
                {
                    this.Master.ErrorMessage = exceptionmessage;
                }

                this.Master.DisplayMessage = true;
                return false;
            }
        }

        private void ProcessDestination()
        {
            decimal SrcFailtedToteId = 0;
            string SkuId = string.Empty;
            decimal DestinationLocationId = 0;

            if (ViewState["SrcFailedToteId"] != null && ViewState["ItemBarcode"] != null)
            {
                SrcFailtedToteId = (decimal)ViewState["SrcFailedToteId"];
                SkuId = ViewState["ItemBarcode"].ToString();

                if (_barcode.Contains("FT"))
                {
                    try
                    {
                        DestinationLocationId = _forcedLocateDao.ValidateFailedTote(_barcode);

                        _forcedLocateDao.LocateToFailedTote(
                            SrcFailtedToteId,
                            DestinationLocationId,
                            SkuId,
                            User.Identity.Name);

                        _message = "Item located </br>";
                        _message += "Scan current location";
                        step.Value = ForcedLocateStep.OldLocation.ToString();
                    }
                    catch (Exception exception)
                    {
                        _message = "Scan Item";
                        string exceptionmessage = exception.Message;
                        if (isErrorMessage(ref exceptionmessage))
                        {
                            this.Master.ErrorMessage = exceptionmessage;
                        }
                        else
                        {
                            this.Master.ErrorMessage = exceptionmessage;
                        }
                        this.Master.DisplayMessage = true;
                    }
                }
                else if (_barcode.Contains("LC") || _barcode.Contains ("OT"))
                {

                    try
                    {
                        DestinationLocationId = _forcedLocateDao.ValidateTrolleyLocation(_barcode);

                        string returnResult = _forcedLocateDao.LocateToTrolleyLocation(
                                                SrcFailtedToteId,
                                                DestinationLocationId,
                                                SkuId,
                                                User.Identity.Name);

                        _message = returnResult == "T" ? "Trolley complete </br>" : "Item located </br>";
                        _message += "Scan next location";
                        step.Value = ForcedLocateStep.ItemScan.ToString();
                    }
                    catch (Exception exception)
                    {
                        _message = "Scan Item";
                        string exceptionmessage = exception.Message;
                        if (isErrorMessage(ref exceptionmessage))
                        {
                            this.Master.ErrorMessage = exceptionmessage;
                        }
                        else
                        {
                            this.Master.ErrorMessage = exceptionmessage;
                        }
                        this.Master.DisplayMessage = true;
                    }
                }
                else
                {
                    _message = "Scan Item";
                    this.Master.ErrorMessage = "Invalid barcode";
                    this.Master.DisplayMessage = true;
                }
            }
            else
            {
                _message = "Scan current location.";
                this.Master.ErrorMessage = "There was an error. Please start again.";
                step.Value = ForcedLocateStep.OldLocation.ToString();
                this.Master.DisplayMessage = true;
            }
        }

        private void Initialise()
        {
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                _message = "Scan current location";
                step.Value = ForcedLocateStep.OldLocation.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _barcode = this.Master.BarcodeValue;

            Initialise();

            if (IsPostBack)
            {
                
                switch (step.Value)
	            {
                    case "OldLocation":
                        {
                            ProcessSource();       
                            break;
                        }
                    case "ItemScan":
                        {
                            ProcessSku();
                            break;
                        }
                    case "DestinationScan":
                        {
                            ProcessDestination();
                            break;
                        }
                    default:
                        {
                            _message = "There was an error. Please try again";
                            step.Value = ForcedLocateStep.OldLocation.ToString();
                            break;
                        }
		            
	            }
                
            }

            this.Master.MessageBoard = _message;
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
            if (theMessage.IndexOf("ERROR: ") > 0)
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

    }
}