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
    public partial class ScanToVan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exceptionMessage = string.Empty;
            string message = string.Empty;
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                message = "Scan Store Cage";
                step.Value = ScanToVanStep.CageBarcodeScan.ToString();
            }
            else
            {
                string barcode = this.Master.BarcodeValue;
                ScanToVanDAO dao = new ScanToVanDAO();

                switch (this.step.Value)
                {
                    case "CageBarcodeScan" :
                        {
                            try
                            {
                                decimal cageID = dao.getCageIdForBarcode(barcode, Shared.CurrentUser, string.Empty) ; // was failing - Shared.UserHostName);
                                ViewState["cageID"] = cageID;
                                step.Value = ScanToVanStep.VanRunBarcodeScan.ToString();
                                message = "Scan Van Barcode";
                            }
                            catch (Exception ex)
                            {
                                exceptionMessage = ex.Message;
                                if (isErrorMessage(ref exceptionMessage))
                                {
                                    this.Master.ErrorMessage = exceptionMessage;
                                    this.Master.DisplayMessage = true;
                                }
                                else
                                {
                                    message = exceptionMessage + ". Scan Van Barcode";
                                    step.Value = ScanToVanStep.VanRunBarcodeScan.ToString();
                                }
                            }
                            break;
                        }

                    case "VanRunBarcodeScan" :
                        {
                            try
                            {
                                decimal cageid = (decimal)ViewState["cageID"];
                                dao.scanToVan(cageid, barcode, Shared.CurrentUser, string.Empty); // was failing - Shared.UserHostName);
                                step.Value = ScanToVanStep.CageBarcodeScan.ToString();

                                message = "Cage scanned to van.</br>";
                                message += "Scan Store Cage";
                            }
                            catch (Exception ex)
                            {
                                exceptionMessage = ex.Message;
                                if (isErrorMessage(ref exceptionMessage))
                                {
                                    this.Master.ErrorMessage = exceptionMessage;
                                    this.Master.DisplayMessage = true;
                                }
                                else
                                {
                                    message = exceptionMessage + ". Scan Store Cage";
                                    step.Value = ScanToVanStep.CageBarcodeScan.ToString();
                                }
                            }

                            break;
                        }
                }

            }
            this.Master.MessageBoard = message;
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