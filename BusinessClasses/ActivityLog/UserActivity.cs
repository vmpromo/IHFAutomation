// Name: UserActivity.cs
// Type: Business Entity Class for Activity Log.
// Description: Class contains properties, and method user
//              to call data acccess functions.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      20/07/11    MSalman   Initial Released
//  1.1      16/08/11    MSalman   New Types are added.     
//  1.2      14/09/11    MSalman   New Types are added.     

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses.ActivityLog
{
    public class UserActivity : IDataService
    {


        #region Private Memebers
        
        private const string GET_USER_ACTIVITY_LOG = "";

        private List<UserActivity> _alog = new List<UserActivity>();

        #endregion

        #region Function Mapping

        public enum ClassMethods
        {
            GetActivitLog
        }


        #endregion

        #region Local Properties

        public int ActivityLogId { get; set; }
        //MDS / OMS
        public int AppSystem { get; set; }

        public int? EventType { get; set; }
        //INDUCT ,LOCATE , PACK , CAGE , DESPATCH, RETURNS.
        public int? ApplicationId { get; set; }

        //MANUAL INDUCTION , LOAD RELEASE , RE-PACK, PACK , FORCE PACK , MANUAL CAGING , AUTO CAGING. 
        public int? ModuleId { get; set; }

        public string SortLoadId { get; set; }

        public string Barcode { get; set; }

        public string ExpectedBarcodeType { get; set; }

        public int? OrderNumber { get; set; }

        public int? ItemNumber { get; set; }

        //PRD_LVL_CHILD
        public int? Sku { get; set; }

        public string UserId { get; set; }

        public string TerminalId { get; set; }

        public int? ChuteId { get; set; }

        public int? TrolleyId { get; set; }

        public int? TrolleyLocationId { get; set; }

        public int? ConsignmentId { get; set; }

        public int? ParcelId { get; set; }

        public int? LaneId { get; set; }

        public int? CageId { get; set; }

        public int? FailToteId { get; set; }

        public int? ResultCode { get; set; }

        public int? ReasonCode { get; set; }

        public int? WorkStationId { get; set; }

        public DateTime? EventDateTime { get; set; }

        public int? SessionId { get; set; }

        public int? OverflowToteId { get; set; }

        public string PigeonHoleId { get; set; }

        public string InternationalInd { get; set; }

        public int? OrderTypeId { get; set; }

        public int? TrolleyTypeId { get; set; }

        public string ParcelCode { get; set; }

        public string CarrierId { get; set; }

        public string CarrierServiceId { get; set; }

        public string CageType { get; set; }

        public int? DeviceID { get; set; }

        public string ManifestId { get; set; }

        public string Value1 { get; set; }

        public string Value2 { get; set; }

        public string Value3 { get; set; }

        public string Ref1 { get; set; }

        public string Ref2 { get; set; }

        public string Ref3 { get; set; }

        public DateTime? SessionStartDateTime { get; set; }

        public DateTime? SessionEndDateTime { get; set; }


        
        public List<UserActivity> UserActivityLogInfo
        {
            get {
                return _alog;
            }

            set {
                _alog = value;
            }
        
        }

        #endregion

        #region Local Functions 


        [MethodMapper("GetActivitLog", UserActivity.GET_USER_ACTIVITY_LOG)]
        public IList<IDataService> GetPackConsignment(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<UserActivity> items = new List<UserActivity>();


            while (reader.Read())
            {
                UserActivity obj = new UserActivity();


                items.Add(obj);

            }
            this.UserActivityLogInfo= items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        #endregion


    }
}
