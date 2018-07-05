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
    public class LoadReleaseDAO
    {
        #region "private constants"

        private const string GetLoad = "oms_load_release.f_get_load";
        private const string GetLoadStatus = "oms_load_release.f_load_status";
        private const string ManualSortStatus = "oms_load_release.f_manual_sort_status";
        private const string AreaDD = "oms_load_release.f_sort_area";
        private const string AreaDD_load = "oms_load_release.f_sort_area_load";
        private const string ActionDD = "oms_load_release.f_get_action";
        private const string updateload = "oms_load_release.p_update_load_release";
        
        

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

        public string Get_manual_sort_status()
        {
            

            return dataManager.GetValue(ManualSortStatus.ToString(),
                                                   null);
        }
        
        public DataSet Get_load_status()
        {

            return dataManager.ExecuteDataset(
                                                GetLoadStatus.ToString(),
                                                null);
        }
        
        public DataSet Get_load(Int32 I_load_status)
        {

            Object[] Params = new Object[] { I_load_status };
            
            return dataManager.ExecuteDataset(
                                                GetLoad.ToString(),
                                                Params);
        }

        public DataSet Get_area()
        {

            DataSet Areacodes = new DataSet();

            Areacodes = dataManager.ExecuteDataset(AreaDD.ToString(), null);

            Areacodes.Tables[0].TableName = "AC";

            return Areacodes;
        }

        public DataSet Get_area_load(string Load_id)
        {

            DataSet Areacodes = new DataSet();

            Object[] Params = new Object[] { Load_id };

            Areacodes = dataManager.ExecuteDataset(AreaDD_load.ToString(), Params);

            Areacodes.Tables[0].TableName = "ACL";

            return Areacodes;
        }

        public DataSet Get_action(Int32 I_status, string I_pick_load_id)
        {

            Object[] Params = new Object[] { I_status, I_pick_load_id };
            
            DataSet Actioncodes = new DataSet();

            Actioncodes = dataManager.ExecuteDataset(ActionDD.ToString(), Params);

            Actioncodes.Tables[0].TableName = "ACT";

            return Actioncodes;
        }


        public decimal Update_Load(string I_load_num,
                                    Int32 I_area_id,
                                    Int32 I_action_ind,
                                    int singleOrders,
                                    int multiOrders,
                                    string I_userid
                                    )
        {
            decimal resultCode = 0;

            Object[] updParams = new Object[] {resultCode,
                                               I_load_num, 
                                                I_area_id, 
                                                I_action_ind, 
                                                singleOrders,
                                                multiOrders,
                                                I_userid
                                                };

            resultCode =  dataManager.ExecuteReturnMethodDecimal(updateload.ToString(),
                                                          updParams);

            return resultCode;
        }


        #endregion
    }
}
