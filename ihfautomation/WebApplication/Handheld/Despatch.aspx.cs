using System;
using IHF.BusinessLayer.DataAccessObjects.Despatch;
using IHF.BusinessLayer.Util.Despatch;

namespace IHF.ApplicationLayer.Web.Handheld{
    public partial class Despatch : System.Web.UI.Page{
        
        private DespatchDAO _despatchDao   = new DespatchDAO();
        private string      _barcode;
        
        private string      carrierBarcode = string.Empty;
        private string      carrierName    = string.Empty;
        private string      output         = string.Empty;


        protected void Page_Load(object sender, EventArgs e){
            _barcode = this.Master.BarcodeValue;

            Initialise();

            if (!IsPostBack)
                this.Master.MessageBoard = "Scan carrier barcode";
            else
                ProcessBarcode();
        }

        private void Initialise(){
            this.Master.Reset();
            this.Master.RegisterStandardScript = true;
        }

        private void CleanUp(){
            this.Master.BarcodeValue = string.Empty;
        }

        private void DisplayMessageForAction(string output, decimal? count){
            if (output.CompareTo(((int)DespatchBarcode.DespatchCages).ToString()) == 0 && count > 0)
                this.Master.MessageBoard = carrierName + " - " + count + " Cage(s). Press 1 to Confirm, 2 to Cancel";
            else if (output.CompareTo(((int)DespatchBarcode.DespatchCages).ToString()) == 0 && count == 0){
                ViewState["Output"]        = null;
                this.Master.ErrorMessage   = "ERROR: No cages for despatch";
                this.Master.DisplayMessage = true;
            }
            else if (output.CompareTo(((int)DespatchBarcode.RemoveCages).ToString()) == 0)
                this.Master.MessageBoard   = "Selected action: " + DespatchBarcode.RemoveCages.ToString() + "<br/> Scan cage.";
            else{
                this.Master.SuccessMessage = "Cage '" + output.Trim() + "'" + "<br/>" + "Ready for Despatch";
                this.Master.DisplayMessage = true;
            }
        }

        private void ProcessBarcode()
        {
            if (ViewState["CarrierBarcode"] == null && ViewState["CarrierName"] == null)
                GetCarrier();
            else{
                carrierBarcode = ViewState["CarrierBarcode"].ToString();
                carrierName    = ViewState["CarrierName"].ToString();

                if (ViewState["Output"] == null)
                    FirstBarcodeCheck();
                else{
                    output = ViewState["Output"].ToString();

                    if (output.CompareTo(((int)DespatchBarcode.DespatchCages).ToString()) == 0)
                        ProcessDespatchOutput();
                    else if (output.CompareTo(((int)DespatchBarcode.RemoveCages).ToString()) == 0)
                        ProcessCageRemove();
                    else
                        FirstBarcodeCheck();
                }
                
            }
            CleanUp();
            
        }

        private void ProcessCageRemove()
        {
            try
            {
                string cageName = _despatchDao.RemoveCage(carrierBarcode, _barcode, User.Identity.Name);
                this.Master.SuccessMessage = "Cage " + cageName + " Removed.";
                this.Master.DisplayMessage = true;
            }
            catch (Exception exception)
            {
                int indx = exception.Message.IndexOf(":");
                this.Master.ErrorMessage = exception.Message.Substring((indx + 1),
                                                                         exception.Message.IndexOf("ORA", (indx + 1)) - (indx + 1));
                this.Master.DisplayMessage = true;

            }
            ViewState["Output"] = null;
            this.Master.MessageBoard = "Carrier selected: " + carrierName +
                                             "<br/>" + "Scan action or cage barcode.";
        }

        private void ProcessDespatchOutput()
        {
            if (_barcode == "1")
            {
                this.Master.MessageBoard = "Carrier selected: " + carrierName +
                                            "<br/>" + "Scan action or cage barcode.";

                string returnResult = _despatchDao.QueueForDespatch(carrierBarcode, User.Identity.Name).Trim();

                if (returnResult == "T")
                {
                    this.Master.SuccessMessage = "SUCCESS: " + carrierName + " cages Despatched.";
                    this.Master.DisplayMessage = true;
                }
                else
                {
                    this.Master.ErrorMessage = "ERROR: Despatched Failed,'" + returnResult + "'";
                    this.Master.DisplayMessage = true;
                }

                ViewState["Output"] = null;
            }
            else if (_barcode == "2")
            {
                this.Master.MessageBoard = "Carrier selected: " + carrierName +
                                            "<br/>" + "Scan action or cage barcode.";
                ViewState["Output"] = null;
            }
        }

        private void FirstBarcodeCheck()
        {
            try
            {
                decimal? cageID = null;
                output = _despatchDao.ProcessBarcode(carrierBarcode, _barcode, User.Identity.Name);
                ViewState["Output"] = output;

                if (output.CompareTo(((int)DespatchBarcode.DespatchCages).ToString()) == 0)
                {
                    cageID = _despatchDao.ValidateCagesForDespatch(carrierBarcode);

                }

                DisplayMessageForAction(output, cageID);
            }
            catch (Exception exception)
            {
                int indx = exception.Message.IndexOf(":");
                this.Master.ErrorMessage = exception.Message.Substring((indx + 1),
                                                                         exception.Message.IndexOf("ORA", (indx + 1)) - (indx + 1));
                this.Master.DisplayMessage = true;

            }
        }

        private void GetCarrier()
        {
            try
            {
                carrierName = _despatchDao.ValidateCarrier(_barcode, User.Identity.Name);
                ViewState["CarrierBarcode"] = carrierBarcode = _barcode;
                ViewState["CarrierName"] = carrierName;
                this.Master.MessageBoard = "Carrier selected: " + carrierName +
                                              "<br/>" + "Scan action or cage barcode.";
            }
            catch (Exception exception)
            {
                int indx = exception.Message.IndexOf(":");
                this.Master.ErrorMessage = exception.Message.Substring(
                                                                (indx + 1),
                                                                exception.Message.IndexOf("ORA", (indx + 1)) - (indx + 1));
                this.Master.DisplayMessage = true;
            }            
        }
    }
}