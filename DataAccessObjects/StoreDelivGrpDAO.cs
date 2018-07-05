using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using IHF.BusinessLayer.BusinessClasses;
using Oracle.DataAccess.Client;
using System.Data;

using IHF.BusinessLayer.Util;
using IHF.EnterpriseLibrary;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class StoreDelivGrpDAO
    {
        #region "private constants"

        private const string GetAllStoreDelivGroups = "oms_cage_maintenance.f_sd_group_list";
        private const string InsertCageType = "oms_cage_maintenance.p_create_cagetype";
        private const string UpdateCarrier = "oms_cage_maintenance.p_update_cagetype_carrier";
        private const string DeleteCageType = "oms_cage_maintenance.p_remove_cagetype";
        private const string CreateStoreDeliveryGroupType = "oms_cage_maintenance.p_create_store_deliv_grp";
        private const string UpdateStoreDeliveryGroupType = "oms_cage_maintenance.p_update_store_deliv_grp";
        private const string CountryGroups = "oms_cage_maintenance.f_country_group_list";
        private const string DeleteStoreDeliveryGroupType = "oms_cage_maintenance.p_remove_store_deliv_grp";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet GetStoreDelivGroups()
        {

            return dataManager.ExecuteDataset(
                                                GetAllStoreDelivGroups.ToString(),
                                                null);
        }

        public DataSet GetCountryGroups()
        {

            return dataManager.ExecuteDataset(
                                                CountryGroups.ToString(),
                                                null);
        }


        public void CreateCageType(string I_cage_type_id, string I_cage_type_descr, string I_carrier_id, string I_country_group_id, string I_despatchable_ind, Int64 ? I_store_deliv_grp, string I_userlogin)
        {

            Object[] updParams = new Object[] { I_cage_type_id, I_cage_type_descr, I_carrier_id, I_country_group_id, I_despatchable_ind, I_store_deliv_grp, I_userlogin };

            dataManager.ExecuteNonQuery(InsertCageType.ToString(), updParams);

        }

        public void CreateStoreDeliveryGroupTypeDetails(ref Int64? I_store_deliv_group_id, string I_group_name, string I_description)
        {

            Object[] insParams = new Object[] { I_store_deliv_group_id, I_group_name, I_description };

            //dataManager.ExecuteNonQuery(CreateStoreDeliveryGroupType.ToString(), insParams);
            I_store_deliv_group_id = Convert.ToInt64(dataManager.ExecuteReturnMethodDecimal(CreateStoreDeliveryGroupType.ToString(), insParams));

        }


        public void UpdateStoreDeliveryGroupTypeDetails(Int64 I_store_deliv_group_id, string I_group_name, string I_description)
        {

            Object[] insParams = new Object[] { I_store_deliv_group_id, I_group_name, I_description };

            dataManager.ExecuteNonQuery(UpdateStoreDeliveryGroupType.ToString(), insParams);

        }


        public void RemoveCageType(string I_cage_type_id)
        {
            Object[] delParams = new Object[] { I_cage_type_id };

            dataManager.ExecuteNonQuery(DeleteCageType.ToString(),
                                                   delParams);

        }

        public void RemoveStoreDeliveryGroupType(Int64 I_store_deliv_group_id)
        {
            Object[] delParams = new Object[] { I_store_deliv_group_id };

            dataManager.ExecuteNonQuery(DeleteStoreDeliveryGroupType.ToString(),
                                                   delParams);

        }

        public void UpdateCageCarrier(string I_cage_type_id, string I_carrier_id, string I_userlogin)
        {
            Object[] updParams = new Object[] { I_cage_type_id, I_carrier_id, I_userlogin };

            dataManager.ExecuteNonQuery(UpdateCarrier.ToString(),
                                                   updParams);

        }


        #endregion

    }
}
