using System;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class MoveTote : System.Web.UI.Page
    {
        private FailedtoteDAO _toteDao = new FailedtoteDAO();
        
        private string _barcode;
        private string toteBarcode     = string.Empty;
        private string toteLabel       = string.Empty;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            _barcode = this.Master.BarcodeValue;

            Initialise();

            if (!IsPostBack)
                this.Master.MessageBoard = "Scan Tote Barcode.";
            else
                ProcessBarcode();

        }

        private void ProcessBarcode()
        {
            if (ViewState["ToteBarcode"] == null && 
                ViewState["ToteLabel"  ] == null)
                
                GetToteDetails();
            
            else
            {
                toteBarcode = ViewState["ToteBarcode"].ToString();
                toteLabel   = ViewState["ToteLabel"  ].ToString();

                MoveToWorkstation();

                ViewState["ToteBarcode"] = null;
                ViewState["ToteLabel"  ] = null;

                this.Master.MessageBoard = "Scan Tote Barcode.";
            
            }
            CleanUp();
        }

        private void GetToteDetails()
        {
            try
            {
                toteLabel = _toteDao.ValidateTote(_barcode);
                
                ViewState["ToteBarcode"] = toteBarcode = _barcode;
                ViewState["ToteLabel"  ] = toteLabel;
                
                this.Master.MessageBoard = "Tote selected: <b>" + toteLabel + "</b>" +
                                           "<br/>" + "Scan destination workstation.";
            }
            catch (Exception exception)
            {
                HandleException(exception.Message);
            }
        }

        private void MoveToWorkstation()
        {
            try
            {
                string workstationLabel    = _toteDao.MoveTote(toteBarcode, 
                                                               _barcode,User.Identity.Name);
                
                this.Master.SuccessMessage = "Tote - <b>" + toteLabel + 
                                             "</b> moved to workstation <b>" + 
                                             workstationLabel + "</b>.";
                
                this.Master.DisplayMessage = true;

                this.Master.MessageBoard   = "Scan Tote Barcode.";
            }
            catch (Exception exception)
            {
                HandleException(exception.Message);
            }
        }

        private void Initialise()
        {
            this.Master.Reset();
            this.Master.RegisterStandardScript = true;
        }

        private void CleanUp()
        {
            this.Master.BarcodeValue = string.Empty;
        }

        private void HandleException(string exceptionMessage) {
            int indx = exceptionMessage.IndexOf(":");
            
            this.Master.ErrorMessage   = exceptionMessage.Substring(
                                            (indx + 1),
                                             exceptionMessage.IndexOf("ORA", (indx + 1)) - (indx + 1));
            this.Master.DisplayMessage = true;
        }

    }
}