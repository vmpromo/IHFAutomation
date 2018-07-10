using IHF.BusinessLayer.BusinessClasses;
using Oracle.DataAccess.Client;
using System.Data;

using IHF.BusinessLayer.Util;
using IHF.EnterpriseLibrary;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;
using System;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class StoreDelivCriteriaDAO
    {
        #region "private constants"

        private const string GetAllStoreDelivCriteria = "oms_cage_maintenance.f_sd_grp_crit_list";
        private const string GetAllCageAvailableServices = "oms_cage_maintenance.f_available_srvc_list";
        private const string GetAllCarrierServices = "oms_cage_maintenance.f_carrier_srvc_list";
        private const string GetAllStoreDelivCriteriaByType = "oms_cage_maintenance.f_grp_store_deliv_list";
        private const string GetAllAvailableStoreDelivCriteriaByType = "oms_cage_maintenance.f_avail_store_deliv_list";
        private const string AddStoreDelivGrpMap = "oms_cage_maintenance.p_create_store_deliv_grp_map";
        private const string DeleteStoreDelivGrpMap = "oms_cage_maintenance.p_remove_store_deliv_grp_map";
        private const string GetAllStoreDelivCriteriaTypes = "oms_cage_maintenance.f_store_deliv_crit_type";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"

        public DataSet GetStoreDelivCriteria (string I_store_deliv_grp_id)
        {
            Object[] cagetypeParam = new Object[] { I_store_deliv_grp_id };
            return dataManager.ExecuteDataset(
                                                GetAllStoreDelivCriteria.ToString(),
                                                cagetypeParam);
        }



        public DataSet GetStoreDelivCriteriaByType(string I_store_deliv_grp_id, string I_store_deliv_criteria_type)
        {
            Object[] cagetypeParam = new Object[] { I_store_deliv_grp_id, I_store_deliv_criteria_type };
            return dataManager.ExecuteDataset(
                                                GetAllStoreDelivCriteriaByType.ToString(),
                                                cagetypeParam);
        }



        public DataSet GetAvailableStoreDelivCriteriaByType(string I_store_deliv_grp_id, string I_store_deliv_criteria_type)
        {
            Object[] cagetypeParam = new Object[] { I_store_deliv_grp_id, I_store_deliv_criteria_type };
            return dataManager.ExecuteDataset(
                                                GetAllAvailableStoreDelivCriteriaByType.ToString(),
                                                cagetypeParam);
        }



        public DataSet GetAvailableServices(string I_cage_type_id)
        {
            Object[] cagetypeParam = new Object[] { I_cage_type_id };
            return dataManager.ExecuteDataset(
                                                GetAllCageAvailableServices.ToString(),
                                                cagetypeParam);
        }

        public DataSet GetCarrierServices(string I_carrier_id)
        {
            Object[] carrierIdParam = new Object[] { I_carrier_id };
            return dataManager.ExecuteDataset(
                                                GetAllCarrierServices.ToString(),
                                                carrierIdParam);
        }

        public void AddCriteriaToGroup(Int64? I_storeDelivGrpMapId, Int64 I_storeDelivGrpId, string I_criterionTypeCode, string I_criterionValue)
        {
            Object[] addParams = new Object[] { I_storeDelivGrpMapId, I_storeDelivGrpId, I_criterionTypeCode, I_criterionValue };

            dataManager.ExecuteNonQuery(AddStoreDelivGrpMap.ToString(), addParams);
        }


        public void RemoveCriteriaFromGroup(Int64 I_storeDelivGrpId, string I_criterionTypeCode, string I_criterionValue)
        {
            Object[] delParams = new Object[] { I_storeDelivGrpId, I_criterionTypeCode, I_criterionValue };

            dataManager.ExecuteNonQuery(DeleteStoreDelivGrpMap.ToString(), delParams);
        }

        public DataSet GetStoreDelivCriteriaTypes()
        {
            Object[] storeDelivCriteriaTypesParam = new Object[] { };
            return dataManager.ExecuteDataset(GetAllStoreDelivCriteriaTypes.ToString(), storeDelivCriteriaTypesParam);
        }



        #endregion

    }
}
