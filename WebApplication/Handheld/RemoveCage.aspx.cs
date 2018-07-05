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
    public partial class RemoveCage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string exceptionMessage = string.Empty;
            string message = string.Empty;
            this.Master.Reset();

            this.Master.RegisterStandardScript = true;

            if (!IsPostBack)
            {
                message = "Scan Cage";
                step.Value = RemoveCageStep.CageBarcodeScan.ToString();
            }
            else
            {
                CagingDAO cagingdao = new CagingDAO();
                string barcode = this.Master.BarcodeValue;

                switch (step.Value)
                {
                    case "CageBarcodeScan":
                        {
                            try
                            {
                                int cageID = cagingdao.getCageIdForBarcode(barcode, User.Identity.Name);
                                ViewState["cageID"] = cageID;
                                message = "Scan Parcel";
                                step.Value = RemoveCageStep.ParcelBarcodeScan.ToString();
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
                            }
                        }
                        break;


                    case "ParcelBarcodeScan":
                        {
                            int cageID = (int)ViewState["cageID"];
                            try
                            {
                                int parcelID = cagingdao.getParcelIdForBarcode(barcode, User.Identity.Name);

                                try
                                {
                                    int ordernumber = 0;
                                    int packCount = 0;
                                    cagingdao.removeFromCage(ref ordernumber, ref packCount, cageID, parcelID, User.Identity.Name);
                                    step.Value = RemoveCageStep.CageBarcodeScan.ToString();
                                    if (packCount > 0)
                                    {
                                        message = packCount.ToString() + " package(s), Order: " + ordernumber.ToString() + " still caged.";
                                        message += " Parcel Removed. Scan Cage";
                                    }
                                    else
                                        message = "Parcel Removed. Scan Cage.";
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
                                        message = exceptionMessage;
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
                                    message = "Scan Parcel";
                                }
                                else
                                {
                                    message = exceptionMessage  + ". Scan Parcel";
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