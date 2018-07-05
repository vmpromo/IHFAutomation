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
    public class SystemParamsDAO
    {
        #region "private constants"

        private const string GetParams = "oms_system_params_util.f_get_system_param_dd";
        private const string GetParamtype = "oms_system_params_util.f_get_param_type";
        private const string GetParamvalue = "oms_system_params_util.f_get_param_value";
        private const string GetParamtypcode = "oms_system_params_util.f_get_param_type_code";
        private const string EditParamvalue = "oms_system_params_util.p_edit_system_param";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "private methods"

        private List<Order> StrongOrderList(List<IDataService> listOfResult)
        {
            List<Order> listOfOrders = new List<Order>();

            foreach (Order order in listOfResult)
            {
                listOfOrders.Add((Order)order);
            }
            return listOfOrders;
        }

        #endregion

        #region "Methods available to the presentation layer (web)"




        public DataSet Get_pararms()
        {

            return dataManager.ExecuteDataset(
                                                GetParams.ToString(),
                                                null);
        }

        public string Get_param_type(Int32 param_id)
        {
            Object[] insParams = new Object[] { param_id
                                                 };

            return dataManager.GetValue(GetParamtype.ToString(),
                                                   insParams);
        }


        public string Get_param_value(Int32 param_id)
        {
            Object[] insParams = new Object[] { param_id
                                                 };

            return dataManager.GetValue(GetParamvalue.ToString(),
                                                   insParams);
        }

        public string Get_param_type_code(Int32 param_id)
        {
            Object[] insParams = new Object[] { param_id
                                                 };

            return dataManager.GetValue(GetParamtypcode.ToString(),
                                                   insParams);
        }

        public decimal Update_params(Int32 I_param_id,
                                    string I_param_type,
                                    string I_param_val,
                                    string I_userid
                                    )
        {


            Object[] updParams = new Object[] { I_param_id,
                                                I_param_type,
                                                I_param_val,
                                                I_userid};

            return dataManager.ExecuteReturnMethod(EditParamvalue.ToString(),
                                                   updParams);
        }

        #endregion
    }
}
