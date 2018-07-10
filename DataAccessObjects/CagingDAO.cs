using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;
using IHF.BusinessLayer .Util ;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class CagingDAO
    {
        #region "private variables and constants"

        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string CAGE_SCAN = "oms_caging.p_scan_cage";
        private const string CAGE_ACTION = "oms_caging.p_cage_action";
        private const string ATTACH_CAGE = "oms_caging.p_attach_cage";
        private const string DETACH_CAGE = "oms_caging.p_detach_cage";
        private const string ENDBARCODE_SCAN = "oms_caging.p_end_barcode_scan";
        private const string PARCEL_SCAN = "oms_caging.p_record_parcel_scan";
        private const string PARCEL_DESTINATION = "oms_caging.p_parcel_destination";
        private const string MANUALSCANTOCAGE = "oms_caging.p_manual_scantocage";
        private const string MANUALREMOVECAGE = "oms_caging.p_removefrom_cage";

        #endregion

        public int getCageIdForBarcode(string cageBarcode, string user)
        {
            int cageID = 0;
            

            cageID = (int)_dataManager.ExecuteReturnMethod(CAGE_SCAN,
                                              new Object[]{
                                                 cageID,
                                                  cageBarcode,
                                                  user});
            return cageID;
        }

        public void determineCageAction(ref CageAction action, ref int lane_id, string actionBarcode, string user)
        {
            DataSet dsResult =  _dataManager.SelectDataSetProcedure(CAGE_ACTION,
                                                new Object[] {
                                                    actionBarcode,
                                                    user });
            action = (CageAction)(int.Parse(dsResult.Tables[0].Rows[0]["action"].ToString()));
            lane_id = int.Parse(dsResult.Tables[0].Rows[0]["lane_id"].ToString());
        }

        public void removeFromCage(ref int ordernumber, ref int parcelsRemaining, int cageID, int packageID,  string user)
        {
            DataSet dsResult = _dataManager.SelectDataSetProcedure(MANUALREMOVECAGE,
                                                new Object[] {
                                                    cageID,
                                                    packageID,
                                                    user });
            ordernumber = int.Parse(dsResult.Tables[0].Rows[0]["ordernumber"].ToString());
            parcelsRemaining = int.Parse(dsResult.Tables[0].Rows[0]["package_count"].ToString());
        }


        public void attachCage(int cageID, int laneID, string user)
        {
            _dataManager.ExecuteNonQuery(ATTACH_CAGE,
                                           new Object[]{
                                               cageID,
                                               laneID,
                                               user});
        }

        public void detachCage(int cageID, int laneID, string user)
        {
            _dataManager.ExecuteNonQuery(DETACH_CAGE,
                                           new Object[]{
                                               cageID,
                                               laneID,
                                               user});
        }


        public void endBarcodeScanned(int laneID, string user)
        {
            _dataManager.ExecuteNonQuery(ENDBARCODE_SCAN ,
                                           new Object[]{
                                               laneID,
                                               user});
        }

        public int getParcelIdForBarcode(string parcelBarcode, string user)
        {
            int parcelID = 0;
            parcelID =  (int)(_dataManager.ExecuteReturnMethod(PARCEL_SCAN,
                                              new Object[]{
                                                  parcelID,
                                                  parcelBarcode,
                                                  user}));
            return parcelID;
        }

        public void parcelDestination(int parcelID)
        {
            _dataManager.ExecuteNonQuery(PARCEL_DESTINATION,
                                           new Object[]{
                                               parcelID});
        }

        public void manualScanToCage(int parcelID, int cageID, string user)
        {
            _dataManager.ExecuteNonQuery(MANUALSCANTOCAGE,
                new Object[]{
                    parcelID,
                    cageID,
                    user});
        }

                                              
    
    }
}
