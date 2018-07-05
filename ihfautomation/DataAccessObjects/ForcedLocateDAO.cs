using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class ForcedLocateDAO
    {
        #region "private varaibles and constants"

        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string VALIDATE_FAILED_TOTE = "oms_forced_locate.f_failed_tote_id";
        private const string VALIDATE_SKU = "oms_forced_locate.p_validate_upc_failed_tote";
        private const string VALIDATE_TROLLEY_LOCATION = "oms_locate.f_validate_location";
        private const string LOCATE_TO_FAILED_TOTE = "oms_forced_locate.p_locate_failed_tote";
        private const string LOCATE_TO_TROLLEY_LOCATION = "oms_forced_locate.p_locate_trolley_location";
        
        
        #endregion

        public decimal ValidateFailedTote(
            string barcode)
        {
            return (decimal)_dataManager.GetValuedecimal(
                VALIDATE_FAILED_TOTE, 
                new object[] { barcode });
        }

        public void ValidateSku(
            string barcode,
            decimal SrcFailedToteId,
            string  user)
        {
            _dataManager.ExecuteNonQuery(
                VALIDATE_SKU,
                new object[]{
                    barcode,
                    SrcFailedToteId,
                    user });
        }

        public int ValidateTrolleyLocation(
            string barcode)
        {
            return (int)_dataManager.GetValuedecimal(
                VALIDATE_TROLLEY_LOCATION,
                new object[] { barcode });
        }

        public void LocateToFailedTote(
            decimal SrcFailedToteId, 
            decimal DstFailedToteId, 
            string ItemBarcode, 
            string user)
        {
            _dataManager.ExecuteNonQuery(
                LOCATE_TO_FAILED_TOTE,
                new object[]{
                    SrcFailedToteId,
                    DstFailedToteId,
                    ItemBarcode,
                    user});
        }

        public string LocateToTrolleyLocation(
            decimal SrcFailedToteId,
            decimal TrolleyLocationID,
            string ItemBarcode,
            string user)
        {
            return _dataManager.GetStringforProcedure(
                                        LOCATE_TO_TROLLEY_LOCATION,
                                        new object[]{
                                        SrcFailedToteId,
                                        TrolleyLocationID,
                                        ItemBarcode,
                                        user});
        }

    }
}
