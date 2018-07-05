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
    public partial class AttachCage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exceptionMessage = string.Empty;
            string message = string.Empty;
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                message = "Scan action barcode";
                step.Value = AttachCageStep.ActionBarcodeScan.ToString();
            }
            else
            {
                CagingDAO cagingdao = new CagingDAO();
                string barcode = this.Master.BarcodeValue;
                switch (step.Value)
                {
                    case "ActionBarcodeScan":
                        {
                            try
                            {
                                CageAction action = CageAction.CageAttach;
                                int laneID = 0;
                                cagingdao.determineCageAction(ref action, ref laneID, barcode, User.Identity.Name);
                                ViewState["laneID"] = laneID;
                                ViewState["action"] = action;
                                if (action == CageAction.CageTypeEnd)
                                {
                                    try
                                    {
                                        cagingdao.endBarcodeScanned(laneID, User.Identity.Name);
                                        message = "Scan action barcode";
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
                                            message = exceptionMessage + ". Scan action barcode";
                                        }
                                    }
                                    step.Value = AttachCageStep.ActionBarcodeScan.ToString();
                                }
                                else
                                {
                                    step.Value = AttachCageStep.CageBarcodeScan.ToString();
                                    message = "Scan Cage to " + ((action == CageAction.CageAttach) ? "Attach" : "Detach");
                                }
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
                                    message = exceptionMessage;
                                }
                            }
                        }
                        break;


                    case "CageBarcodeScan":
                        {
                            int laneID = (int)ViewState["laneID"];
                            CageAction action = (CageAction)ViewState["action"];
                            int cageID = 0;
                            try
                            {
                                cageID = cagingdao.getCageIdForBarcode(barcode, User.Identity.Name);

                                if (action == CageAction.CageAttach)
                                {
                                    cagingdao.attachCage(cageID, laneID, User.Identity.Name);
                                }
                                else if (action == CageAction.CageDetach)
                                {
                                    cagingdao.detachCage(cageID, laneID, User.Identity.Name);
                                }
                                step.Value = AttachCageStep.ActionBarcodeScan.ToString();
                                message = ((action == CageAction.CageAttach) ? "Attached" : "Detached") + ": Scan action barcode";

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
                                    message = exceptionMessage;
                                }
                                message = "Scan action barcode";
                                step.Value = AttachCageStep.ActionBarcodeScan.ToString();
                                break;
                            }
                            break;
                            
                        }
                }

            }

            this.Master.MessageBoard = message; // +"    " + step.Value;
            this.Master.BarcodeValue = string.Empty;


        }

        protected bool isErrorMessage(ref string msg)
        {
            string theMessage = "";
            bool result = true;
             
            try
            {
                string []lines = msg.Split ("\n".ToCharArray());
                theMessage = lines[0];
            }
            catch
            {
                theMessage = msg;
                return false;
            }
            if (theMessage.IndexOf("ERROR: ") > 0)
            {
                msg = theMessage.Substring (18); 
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