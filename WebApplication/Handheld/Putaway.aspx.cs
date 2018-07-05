// Name: Putaway.aspx.cs
// Type: class file 
// Description: Code behind the putaway hand held form
//
//$Revision:   1.9  $
//
// Version   Date        Author     Reason
//  1.0      08/05/18    A Petrescu Initial Revision
//  1.1      10/05/18    A Petrescu Updated the error messages strings
//  1.2      14/05/18    A Petrescu Showed putaway location on new line
//  1.3      21/05/18    M Cackett  Location processing
//                       S Remedios
//  1.4      22/05/18    M Cackett  Change to error message when ORDS service call fails
//  1.5      07/06/18    M Cackett  Change to call putaway ORDS endpoint
//                       S Patel
//                       S Remedios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.BusinessClasses.Putaway;
using IHF.BusinessLayer.DataAccessObjects.Returns;
using System.Net;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class Putaway : System.Web.UI.Page
    {
        private const string StateScanLpn = "StateScanLpn";
        private const string StateScanLocation = "StateScanLocation";
        private readonly LocationServiceWrapper _locationService;
        private readonly PutawayServiceWrapper _putawayService;
        private readonly CmsServiceWrapper _cmsService;

        private readonly Dictionary<LocationStatusCode, string> _locationErrorToMessage = new Dictionary<LocationStatusCode, string>
        {
            {LocationStatusCode.NotFound, "Invalid Location<br />{0}"},
            {LocationStatusCode.Invalid, "Not Active Location<br />{0}"},
            {LocationStatusCode.HTTPerror, "WMS service unavailable. Item has not been putaway."}
        };

        private readonly PutawayDAO _putawayDao;

        public Putaway()
        {
            _putawayDao = new PutawayDAO();
            _locationService = new LocationServiceWrapper();
            _putawayService = new PutawayServiceWrapper();
            _cmsService = new CmsServiceWrapper();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Reset();


            if (!IsPostBack)
            {//get
                InitializePage();
            }
            else
            {//post
                if (pageState.Value == StateScanLpn)
                {
                    ScanLpn();
                }
                else if (pageState.Value == StateScanLocation)
                {
                    ScanLocation();
                }
                else
                {
                    InitializePage();    
                }
            }

            SetMessageBoard();
            this.Master.RegisterStandardScript = true;
        }

        private void InitializePage()
        {
            this.Master.BarcodeValue = "";
            pageState.Value = StateScanLpn;
            scannedLPN.Value = string.Empty;
            scannedItemNumber.Value = string.Empty;
        }

        private void SetMessageBoard()
        {
            if (pageState.Value == StateScanLpn)
            {
                this.Master.MessageBoard = "Scan LPN Barcode";
            }
            else
            {
                this.Master.MessageBoard = "Scan Location for " + scannedLPN.Value;
            }
        }

        private void ScanLocation()
        {
            if (this.Master.BarcodeValue == "")
            {
                return;
            }
            var scannedValue = this.Master.BarcodeValue.Trim();
            this.Master.BarcodeValue = "";

            //validate location
            if (scannedValue.Length != 7 )
            {
                ShowError("Invalid Location<br />" + scannedValue);
                return;
            }

            //check location in wms
            var locationStatus = _locationService.ChkValidLocation(scannedValue);
            if (_locationErrorToMessage.ContainsKey(locationStatus))
            {
                ShowError(string.Format(_locationErrorToMessage[locationStatus], scannedValue));
                return;
            }

            //write inpt_case_hdr and inpt_case_dtl
            var putawaySucceeded = _putawayService.PutawayIntoWMS(scannedLPN.Value, putawaySku.Value, scannedValue);
            if (!putawaySucceeded)
            {
                ShowError("Failed to putaway LPN into WMS");
                return;
            }

            //write location to putaway_dtm, to avoid re-putting the item away if CMS fails
            _putawayDao.UpdActualLocation(scannedLPN.Value, scannedValue);

            // write to cms inventory ords endpoint
            if (!_cmsService.PutawayItem(putawaySku.Value, scannedLPN.Value, orderNumber.Value))
            {
                ShowError(string.Format("Unable to update CMS inventory for sku {0}, lpn {1}.", putawaySku.Value, scannedLPN.Value));
                return;
            }
            
            //set state to scan lpn
            pageState.Value = StateScanLpn;

            //clear messages
            ClearMessages();
            InitializePage();
        }


        private void ScanLpn()
        {
            if (this.Master.BarcodeValue == "")
            {
                return;
            }

            var scannedValue = this.Master.BarcodeValue.Trim();
            this.Master.BarcodeValue = "";

            if (scannedValue.Length != 11 ||
                !scannedValue.Take(2).All(letter => letter >= 'A' && letter <= 'Z') ||
                !scannedValue.Skip(2).All(number => number >= '0' && number <= '9'))
            {
                ShowError("Invalid LPN");
                return;
            }

            var lpnInformation = _putawayDao.LoadLPNInformation(scannedValue);
            if (!ValidateLPN(lpnInformation))
            {
                return;
            }

            scannedLPN.Value = scannedValue;
            orderNumber.Value = lpnInformation.OrderNumber;
            scannedItemNumber.Value = lpnInformation.ItemNumber;
            pageState.Value = StateScanLocation;
            putawaySku.Value = lpnInformation.Sku;
        }

        private bool ValidateLPN(LpnInformation lpnInformation)
        {
            if (lpnInformation == null)
            {
                ShowError("LPN not found");
                return false;
            }

            if (lpnInformation.ActionCode != 30)
            {
                ShowError("LPN not suitable for putaway");
                return false;
            }

            if (!string.IsNullOrEmpty(lpnInformation.ActualLocation))
            {
                ShowError("LPN already putaway in<br />" + lpnInformation.ActualLocation);
                return false;
            }

            return true;
        }

        private void ShowError(string errorMessage)
        {
            this.Master.ErrorMessage = errorMessage;
            this.Master.DisplayMessage = true;
        }

        private void ClearMessages()
        {
            this.Master.ErrorMessage = "";
            this.Master.DisplayMessage = false;
        }
    }
}