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
    public class CagetypeServiceDAO
    {
        #region "private constants"

        private const string GetAllCageServices = "oms_cage_maintenance.f_cagetype_srvc_list";
        private const string GetAllCageAvailableServices = "oms_cage_maintenance.f_available_srvc_list";
        private const string GetAllCarrierServices = "oms_cage_maintenance.f_carrier_srvc_list";
        private const string AddServiceCage = "oms_cage_maintenance.p_add_srvc";
        private const string DeleteServiceCage = "oms_cage_maintenance.p_remove_srvc";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"

        public DataSet GetCageServices (string I_cage_type_id)
        {
            Object[] cagetypeParam = new Object[] { I_cage_type_id };
            return dataManager.ExecuteDataset(
                                                GetAllCageServices.ToString(),
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

        public void AddServiceToCageType(string I_cage_type_id, string I_carrier_service_id, string I_userlogin)
        {
            Object[] addParams = new Object[] { I_cage_type_id, I_carrier_service_id, I_userlogin };

            dataManager.ExecuteNonQuery(AddServiceCage.ToString(),
                                        addParams);
        }


        public void RemvoveServiceToCageType(string I_cage_type_id, string I_carrier_service_id, string I_userlogin)
        {
            Object[] delParams = new Object[] { I_cage_type_id, I_carrier_service_id, I_userlogin };

            dataManager.ExecuteNonQuery(DeleteServiceCage.ToString(),
                                        delParams);
        }

        #endregion

    }
}
