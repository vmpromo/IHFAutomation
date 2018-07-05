// Name: BookingCodeParams.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties, and methods used
//              for parameters uses to build booking codes.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      09/02/15    M Cackett Initial Release

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.Packing
{

    public class BookingCodeParams : IDataService
    {


        #region Private Memeber

        private const string GET_BOOKING_CODE_PARAMS = "OMS_BOOKING_CODE_UTIL.F_BOOKING_CODE";


        private IList<BookingCodeParams> _lstBookingCodeParams = new List<BookingCodeParams>();


        #endregion

        #region Function Mapping

        public enum ClassMethods
        {
            GetBookingCodeParams

        }

        #endregion

        #region  Properties

        public string ServiceGroupId { get; set; }

        public string CarrierServiceId { get; set; }

        public string MetapackOrder { get; set; }

        public string ServiceCodeSuffix { get; set; }

        public string DeliverByDateInd { get; set; }

        public int StoreCount { get; set; }

        public IList<BookingCodeParams> BookingCodeInfo
        {
            get
            {
                return _lstBookingCodeParams;
            }

            set
            {
                _lstBookingCodeParams = value;

            }

        }


        #endregion

        #region Local Functions


        [MethodMapper("GetBookingCodeParams", BookingCodeParams.GET_BOOKING_CODE_PARAMS)]
        public IList<IDataService> GetBookingCode(IDataReader reader) 
        {

            IList<IDataService> lst = new List<IDataService>();

            List<BookingCodeParams> items = new List<BookingCodeParams>();


            while (reader.Read())
            {
                BookingCodeParams obj = new BookingCodeParams();

                obj.ServiceGroupId = reader["SERVICE_GROUP_ID"].ToString() ?? string.Empty;
                obj.CarrierServiceId = reader["CARRIER_SERVICE_ID"].ToString() ?? string.Empty;
                obj.MetapackOrder = reader["METAPACK_ORDER"].ToString() ?? string.Empty;
                obj.ServiceCodeSuffix = reader["SERVICE_CODE_SUFFIX"].ToString() ?? string.Empty;
                obj.DeliverByDateInd = reader["DELIVER_BY_DATE_IND"].ToString() ?? string.Empty;
                obj.StoreCount = reader["STORE_COUNT"].ToString() != string.Empty ? Convert.ToInt32(reader["STORE_COUNT"].ToString()) : 0;
                items.Add(obj);
            }

            this._lstBookingCodeParams = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }

        #endregion



    }
}
