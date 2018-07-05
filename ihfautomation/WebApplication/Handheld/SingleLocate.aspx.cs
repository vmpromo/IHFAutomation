using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Resources;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.BusinessClasses.ManualSort;
using IHF.BusinessLayer.DataAccessObjects.ManualSort;

namespace IHF.ApplicationLayer.Web
{
    public partial class SingleLocate : System.Web.UI.Page
    {

        private enum MessageType
        {
            ErrorConfirm = 1,
            SussessConfirm = 2,
            NormalMessage = 3

        }


        private enum LocateScanMode
        {
            CS = 0,
            TS = 1

        }




        ManualSortDAO _manualSort = new ManualSortDAO();

        string _areaId = string.Empty;
        string _loadNo = string.Empty;



        protected void Page_Init(object sender, EventArgs e)
        {


            this.Master.Reset();


            if (!Page.IsPostBack)
            {

                this.Master.MessageBoard = "Scan the chute barcode";

            }


            this.Master.RegisterStandardScript = true;

        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Page.IsPostBack)
            {


                ScanItem();

            }


            ClearValue(true);
        }


        private void ScanItem()
        {


            ManualSingleLocate  singleLocate = new ManualSingleLocate();

            singleLocate = GetParamValues();

            ManualSingleLocate itemScan = _manualSort.GetSingleLocate(singleLocate);



            if (itemScan != null)
            {
                this.SetParamValues(itemScan);

                this.Master.Reset();

                if (true == itemScan.ActionResult && itemScan.ScanMode == LocateScanMode.CS.ToString())
                {
                    this.ShowMessage("Scan trolley to locate.", MessageType.NormalMessage);
                }
                else if (false == itemScan.ActionResult && itemScan.ScanMode == LocateScanMode.CS.ToString())
                {
                    this.ShowMessage(itemScan.ActionMessage, MessageType.ErrorConfirm);

                }
                else if (true == itemScan.ActionResult && itemScan.ScanMode == LocateScanMode.TS.ToString())
                {
                    ShowMessage("Chute successfully located, Scan Next Chute to locate.", MessageType.NormalMessage);

                }
                else if (false == itemScan.ActionResult && itemScan.ScanMode == LocateScanMode.TS.ToString())
                {
                    this.ShowMessage(itemScan.ActionMessage, MessageType.ErrorConfirm);

                }


                this.Master.RegisterStandardScript = true;

            }
        
        }


        private ManualSingleLocate GetParamValues()
        {

            ManualSingleLocate singleLocate = new ManualSingleLocate();

            if (this.hdnScanMode.Value != string.Empty)
            {
                singleLocate.ScanMode = this.hdnScanMode.Value;

            }

            if (this.hdnChuteId.Value != string.Empty)
            {
                singleLocate.ChuteID = this.hdnChuteId.Value;

            }

            if (this.hdnTrolleyId.Value != string.Empty)
            {
                singleLocate.TrolleyID = this.hdnTrolleyId.Value;

            }

            if (this.Master.BarcodeValue != string.Empty)
            {

                singleLocate.ScanBarcode = this.Master.BarcodeValue;
            }


            return singleLocate;

        }


        private void SetParamValues(ManualSingleLocate values)
        {


            if (null != values)
            {
                if (values.ActionResult == true)
                {
                    this.hdnScanMode.Value = (this.hdnScanMode.Value == LocateScanMode.CS.ToString()) ?
                                                                               LocateScanMode.TS.ToString() :
                                                                               LocateScanMode.CS.ToString();
                }
                else
                    this.hdnScanMode.Value = values.ScanMode;

                this.hdnChuteId.Value = values.ChuteID;

                this.hdnTrolleyId.Value = values.TrolleyID;

         
            }


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

        private void ClearValue(bool clear)
        {

            if (true == clear)
                this.Master.BarcodeValue = string.Empty;

        }

    }
}