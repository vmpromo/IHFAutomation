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
    public class PackPriorityDAO
    {
        #region "private constants"

        private const string GetPackPriority = "oms_pack_util.f_get_pack_priority";
        private const string UpdatePackPriority = "oms_pack_util.p_update_pack_priority";
        private const string GetPackPriorityVAl = "oms_pack_util.f_get_pack_priority_values";
        private const string UpdatePackPriorityval = "oms_pack_util.p_update_pack_priority_value";

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


        public DataSet Get_packPriority()
        {

            return dataManager.ExecuteDataset(
                                                GetPackPriority.ToString(),
                                                null);
        }

        public decimal Update_packpriority(string I_criterion, Int32 I_weight)
        {
            decimal O_status = 0;
            Object[] updParams = new Object[] { O_status, I_criterion, I_weight };
            dataManager.ExecuteReturnMethodDecimal(UpdatePackPriority,
                                                            updParams);

            return O_status;
        }

        public DataSet Get_packPriorityVal(string I_criterion)
        {

            Object[] searchParams = new Object[] { I_criterion };
            return dataManager.ExecuteDataset(
                                                GetPackPriorityVAl.ToString(),
                                                searchParams);
        }

        public decimal Update_packpriorityval(string I_criterion, string I_data_type, Int32 I_weight, string I_char_val, Int32 I_num_val, DateTime I_date_val)
        {
            decimal O_status = 0;
            Object[] updParams = new Object[] { O_status, I_criterion, I_data_type, I_weight, I_char_val, I_num_val, I_date_val };
            dataManager.ExecuteReturnMethodDecimal(UpdatePackPriorityval,
                                                            updParams);

            return O_status;
        }
        #endregion
    }
}
