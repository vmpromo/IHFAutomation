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
    public partial class ManualCage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exceptionMessage = string.Empty;
            string message = string.Empty;
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                message = "Scan Parcel";
                step.Value = ManualCageStep.ParcelBarcodeScan.ToString();
            }
            else
            {
                CagingDAO cagingdao = new CagingDAO();
                string barcode = this.Master.BarcodeValue;

                switch (step.Value)
                {
                    case "ParcelBarcodeScan":
                        {
                            try
                            {
                                int parcelID = cagingdao.getParcelIdForBarcode(barcode, User.Identity.Name);
                                ViewState["parcelID"] = parcelID;
                                try
                                {
                                    cagingdao.parcelDestination(parcelID);
                                }
                                catch (Exception ex)
                                {
                                    exceptionMessage = ex.Message;
                                    if (isErrorMessage(ref exceptionMessage))
                                    {
                                        this.Master.ErrorMessage = exceptionMessage;
                                        this.Master.DisplayMessage = true;
                                        message = "Scan Parcel";
                                    }

                                    else
                                    {
                                        message = exceptionMessage + "</br>";
                                        // if caged already then just display message cage and expect parcel scan
                                        string sMsg = "PARCEL CAGED IN";
                                        int strlngth = sMsg.Length;
                                        if (message.ToUpper().Substring(0, strlngth) == sMsg)
                                        {
                                            message += "Scan Parcel";
                                            step.Value = ManualCageStep.ParcelBarcodeScan.ToString();
                                        }
                                        else
                                        {
                                            message += "Scan Cage";
                                            step.Value = ManualCageStep.CageBarcodeScan.ToString();
                                        }
                                    }
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
                                    this.Master.ErrorMessage = exceptionMessage;
                                    this.Master.DisplayMessage = true;
                                }
                                message = "Scan Parcel";
                            }
                        }
                        break;


                    case "CageBarcodeScan":
                        {
                            int parcelID = (int)ViewState["parcelID"];
                            try
                            {
                                int cageID = cagingdao.getCageIdForBarcode(barcode, User.Identity.Name);

                                try
                                {
                                    cagingdao.manualScanToCage(parcelID, cageID, User.Identity.Name);
                                    step.Value = ManualCageStep.ParcelBarcodeScan.ToString();
                                    message = "Parcel Caged.</br>";
                                    message += "Scan new parcel";
                                }
                                catch (Exception ex)
                                {
                                    exceptionMessage = ex.Message;
                                    if (isErrorMessage(ref exceptionMessage))
                                    {
                                        this.Master.ErrorMessage = exceptionMessage;
                                        this.Master.DisplayMessage = true;
                                        //message = "Scan Cage";
                                    }
                                    else
                                    {
                                        message = exceptionMessage + ". Scan new parcel";
                                        step.Value = ManualCageStep.ParcelBarcodeScan.ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                exceptionMessage = ex.Message;
                                if (isErrorMessage(ref exceptionMessage))
                                {
                                    this.Master.ErrorMessage = exceptionMessage;
                                    this.Master.DisplayMessage = true;
                                    message = "Scan Cage";
                                }
                                else
                                {
                                    message = exceptionMessage = ". Scan Cage";
                                }
                                break;
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