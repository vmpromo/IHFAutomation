// Name: PackResponse.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties, and methods use to call
//              database function for packing.
//
//$Revision:   1.17  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released
//  1.1      01/08/11    MSalman   New Status field added.
//  1.2      10/08/11    MSalman   Null check is added.
//  1.3      11/08/11    MSalman   New field is added. 
//  1.4      18/08/11    MSalman   New field is added.
//  1.5      18/08/11    MSalman   New Field is added.
//  1.6      26/08/11    MSalman   New Field is added.                                   
//  1.7      01/09/11    MSalman   New Field is added.                                   
//  1.8      05/09/11    MSalman   New Field is added.                                   
//  1.9      09/09/11    MSalman   New Field is added.                                   
//  1.10     15/09/11    MSalman   New Field is added.
//  1.11     19/09/11    MSalman   field name corrected.
//  1.17     03/04/12    M Khan    Added transport mode property.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.Packing
{
    public class PackResponse : IDataService
    {

        #region Private Memeber

        private const string VALIDATE_CONTAINER = "OMS_PACK.F_VALIDATE_CONTAINER";

        private const string PACK_SCAN_ITEM = "OMS_PACK.F_SCAN_ITEM";

        private const string INITIAL_NEXT_ORDER = "OMS_PACK.F_NEXT_ORDER";

        private List<PackResponse> _pResponse = new List<PackResponse>();


        #endregion

        #region Functions Mapping

        public enum ClassMethods
        {
            ValidateContainer,
            PackScanItem,
            InitializeNextOrder

        }
        #endregion

        #region Properties


        public List<PackResponse> PackResponseInfo
        {
            get
            {
                return _pResponse;
            }
            set
            {
                _pResponse = value;
            }
        }

        public string OrderNo { get; set; }

        public string PreviousOrderNo { get; set; }

        public string OrderStatus { get; set; }

        public string OrderItemStatus { get; set; }

        public string ItemInd { get; set; }

        public string RestrictedItemInd { get; set; }

        public string ExcessItemInd { get; set; }

        public string ToteId { get; set; }

        public string  PreviousToteId { get; set; }

        public string TrolleyId { get; set; }

        public string ChuteNo { get; set; }

        public string ActionId { get; set; }

        public string PreviousActionId { get; set; }

        public string PackMode { get; set; }

        public string PreviousPackMode { get; set; }

        public string DestinationType { get; set; }

        public string CurrentLocation { get; set; }

        public string OverFlowLocation { get; set; }

        public string CurrentItem { get; set; }

        public string PreviousCurrentItem { get; set; }

        public string TotalItem { get; set; }

        public string PreviousTotalItem { get; set; }

        public string TotalParcelBag { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string ActionMessage { get; set; }

        public string SuccessInd { get; set; }

        public string FailToteScaned { get; set; }

        public string  OrderCount { get; set; }

        public PackRequest PackingState { get; set; }

        public string  ContainerLabel { get; set; }

        public string PreviousContainerLabel { get; set; }

        public string  PreviousLocation { get; set; }

        public string PreviousTotalParcelBag { get; set; }

        public string PreviousOrderCount { get; set; }

        public string MissingItemToteId { get; set; }

        public string TransportMode { get; set; }

        #endregion


        #region Local Functions

        [MethodMapper("ValidateContainer", PackResponse.VALIDATE_CONTAINER)]
        public IList<IDataService> ValidateContainer(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            if (reader.Read())
            {
                this.PackMode = reader["TROLLEYTYPE"].ToString() ?? string.Empty;
                this.TrolleyId = reader["TROLLEYID"].ToString() == "0" ? string.Empty : reader["TROLLEYID"].ToString();
                this.ToteId = reader["FAILEDTOTEID"].ToString() == "0" ? string.Empty : reader["FAILEDTOTEID"].ToString();
                this.DestinationType = reader["DESTTYPE"].ToString() ?? string.Empty;
                this.CurrentLocation = reader["CURLOCATION"].ToString() ?? string.Empty;
                this.OrderNo = reader["ORDERNUMBER"].ToString() ?? string.Empty;
                this.ChuteNo = reader["CHUTELABEL"].ToString() ?? string.Empty;
                this.ErrorCode = reader["ERRORCODE"].ToString() ?? string.Empty;
                this.ErrorMessage = reader["ERRORLABEL"].ToString() ?? string.Empty;
                this.SuccessInd = reader["SUCCESSIND"].ToString() ?? string.Empty;
                this.ContainerLabel = reader["CONTAINERLABEL"].ToString() ?? string.Empty;
                this.OrderCount = reader["ORDERCOUNT"].ToString() ?? string.Empty;
                this.TransportMode = reader["TransportMode"].ToString() ?? "ROAD";
                if (this.TransportMode == "0" || this.TransportMode == "1") this.TransportMode = "ROAD";//default it to ROAD which is value 1
                if (this.TransportMode == "2") this.TransportMode = "AIR";//default it to ROAD which is value 1
                if (this.TransportMode == "3") this.TransportMode = "BOTH";//default it to ROAD which is value 1
                if (this.TransportMode == "4") this.TransportMode = "RIVAN";//default it to ROAD which is value 1
            }
            lst.Add(this);
            reader.Close();

            return lst;
        }


        [MethodMapper("PackScanItem", PackResponse.PACK_SCAN_ITEM)]
        public IList<IDataService> PackScanItem(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            if (reader.Read())
            {

                this.CurrentLocation = reader["CURRENT_LOCATION"].ToString()??string.Empty;
                this.OverFlowLocation = reader["CURRENT_OVERFLOW_LOCATION"].ToString() ?? string.Empty;

                // Silly way of initializing the value. that should have done at backend.
                this.CurrentItem = reader["CURRENT_ITEM_NUM"].ToString() ?? string.Empty;
                if (this.CurrentItem == "0") this.CurrentItem = string.Empty;

                this.TotalItem = reader["TOTALORDERITEMS"].ToString() ?? string.Empty;
                if (this.TotalItem == "0") this.TotalItem = string.Empty;

                this.TotalParcelBag = reader["TOTAL_PARCEL_BAG_NUM"].ToString() ?? string.Empty;
                if (this.TotalParcelBag == "0") this.TotalParcelBag = string.Empty;

                this.OrderNo = reader["ORDERNUMBER"].ToString() ?? string.Empty;
                if (this.OrderNo == "0") this.OrderNo = string.Empty;

                this.OrderStatus = reader["ORDER_STATUS"].ToString() ?? string.Empty;
                this.OrderItemStatus = reader["ORDER_ITEM_STATUS"].ToString() ?? String.Empty;
                this.ItemInd = reader["ITEM_IND"].ToString() ?? string.Empty;
                this.RestrictedItemInd = reader["RESTRICTED_ITEM_IND"].ToString() ?? string.Empty;
                this.ErrorCode = reader["ERROR_CODE"].ToString() ?? string.Empty;
                this.ErrorMessage = reader["ERROR_MESSAGE"].ToString() ?? string.Empty;
                this.ActionMessage = reader["ACTION_MESSAGE"].ToString() ?? String.Empty;
                this.TrolleyId = reader["TROLLEY_ID"].ToString() == "0" ? string.Empty : reader["TROLLEY_ID"].ToString();
                this.ToteId= reader["FAILED_TOTE_ID"].ToString()=="0" ? string.Empty : reader["FAILED_TOTE_ID"].ToString();
                this.ActionId = reader["ACTION_ID"].ToString() ?? string.Empty;
                this.PackMode = reader["PROCESS_MODE"].ToString() ?? string.Empty;
                this.DestinationType = reader["DESTINATION_TYPE"].ToString() ?? string.Empty;
                this.ExcessItemInd = reader["EXCESS_ITEM_IND"].ToString() ?? string.Empty;
                this.SuccessInd = reader["SUCCESS_IND"].ToString() ?? string.Empty;
                this.FailToteScaned = reader["FT_SCANNED"].ToString() ?? string.Empty;
                this.ContainerLabel = reader["CONTAINER_LABEL"].ToString() ?? string.Empty;
                this.OrderCount = reader["ORDERCOUNT"].ToString() ?? string.Empty;
                if (this.OrderCount == "0") this.OrderCount = string.Empty;

                this.PreviousActionId = reader["PREVIOUS_ACTION_ID"].ToString() ?? string.Empty;
                if (this.PreviousActionId == "0") this.PreviousActionId = string.Empty;

                this.PreviousToteId = reader["PREVIOUS_TOTE_ID"].ToString() ?? string.Empty;
                if (this.PreviousToteId == "0") this.PreviousToteId = string.Empty;

                this.PreviousOrderNo = reader["PREVIOUS_ORDERNUMBER"].ToString() ?? string.Empty;
                if (this.PreviousOrderNo == "0") this.PreviousOrderNo = string.Empty;

                this.PreviousCurrentItem = reader["PREVIOUS_CURRENT_ITEM"].ToString() ?? string.Empty;
                if (this.PreviousCurrentItem == "0") this.PreviousCurrentItem = string.Empty;

                this.PreviousTotalItem = reader["PREVIOUS_TOTAL_ITEM"].ToString() ?? string.Empty;
                if (this.PreviousTotalItem == "0") this.PreviousTotalItem = string.Empty;

                this.PreviousPackMode = reader["PREVIOUS_PROCESS_MODE"].ToString() ?? string.Empty;
                if (this.PreviousPackMode == "0") this.PreviousPackMode = string.Empty;

                this.PreviousContainerLabel = reader["PREVIOUS_CONTAINER_LABEL"].ToString() ?? string.Empty;
                if (this.PreviousContainerLabel == "0") this.PreviousContainerLabel = string.Empty;

                this.PreviousLocation = reader["PREVIOUS_LOCATION"].ToString() ?? string.Empty;
                if (this.PreviousLocation == "0") this.PreviousLocation = string.Empty;


                this.PreviousTotalParcelBag = reader["PREVIOUS_NUM_PARCEL"].ToString() ?? string.Empty;
                if (this.PreviousTotalParcelBag == "0") this.PreviousTotalParcelBag = string.Empty;

                this.PreviousOrderCount = reader["PREVIOUS_ORDER_COUNT"].ToString() ?? string.Empty;
                if (this.PreviousOrderCount == "0") this.PreviousOrderCount = string.Empty;

                this.MissingItemToteId = reader["MISSING_ITEM_TOTE_ID"].ToString() ?? string.Empty;
                if (this.MissingItemToteId == "0") this.MissingItemToteId = string.Empty;

                this.TransportMode = reader["TransportMode"].ToString() ?? "ROAD";
                if (this.TransportMode == "0" || this.TransportMode == "1") this.TransportMode = "ROAD";//default it to ROAD which is value 1
                if (this.TransportMode == "2") this.TransportMode = "AIR";//default it to ROAD which is value 1
                if (this.TransportMode == "3") this.TransportMode = "BOTH";//default it to ROAD which is value 1
                if (this.TransportMode == "4") this.TransportMode = "RIVAN";//default it to ROAD which is value 1                
            }
            lst.Add(this);
            reader.Close();

            return lst;
        }


        [MethodMapper("InitializeNextOrder", PackResponse.INITIAL_NEXT_ORDER)]
        public IList<IDataService> InitializeNextOrder(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            if (reader.Read())
            {

                this.PackMode = reader["TROLLEYTYPE"].ToString() ?? string.Empty;
                this.TrolleyId = reader["TROLLEYID"].ToString() == "0" ? string.Empty : reader["TROLLEYID"].ToString();
                this.ToteId = reader["FAILEDTOTEID"].ToString() == "0" ? string.Empty : reader["FAILEDTOTEID"].ToString();
                this.DestinationType = reader["DESTTYPE"].ToString() ?? string.Empty;
                this.CurrentLocation = reader["CURLOCATION"].ToString() ?? string.Empty;
                this.OrderNo = reader["ORDERNUMBER"].ToString() ?? string.Empty;
                this.ChuteNo = reader["CHUTELABEL"].ToString() ?? string.Empty;
                this.ErrorCode = reader["ERRORCODE"].ToString() ?? string.Empty;
                this.ErrorMessage = reader["ERRORLABEL"].ToString() ?? string.Empty;
                this.SuccessInd = reader["SUCCESSIND"].ToString() ?? string.Empty;
                this.ContainerLabel = reader["CONTAINERLABEL"].ToString() ?? string.Empty;
                this.OrderCount = reader["ORDERCOUNT"].ToString() ?? string.Empty;
                this.TransportMode = reader["TransportMode"].ToString() ?? "1";
                if (this.TransportMode == "0" || this.TransportMode == "1") this.TransportMode = "ROAD";//default it to ROAD which is value 1
                if (this.TransportMode == "2") this.TransportMode = "AIR";//default it to ROAD which is value 1
                if (this.TransportMode == "3") this.TransportMode = "BOTH";//default it to ROAD which is value 1
                if (this.TransportMode == "4") this.TransportMode = "RIVAN";//default it to ROAD which is value 1
            }
            lst.Add(this);
            reader.Close();

            return lst;
        }






        #endregion



    }
}
