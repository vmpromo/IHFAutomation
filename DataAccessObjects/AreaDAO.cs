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
    public class AreaDAO
     
    {
        #region "private constants"

        private const string GetArea = "oms_area_util.f_get_areas";
        private const string GetAreaID = "oms_area_util.f_area_details_for_ID";
        private const string CreateArea = "oms_area_util.p_create_area";
        private const string EditArea = "oms_area_util.p_edit_area";
        private const string AreaTypeDD = "oms_area_util.f_get_area_type";
        private const string HandleSplitDD = "oms_area_util.f_get_area_handle_split";
        private const string ActiveIndDD = "oms_area_util.f_get_area_active_ind";
        private const string WAREHOUSES = "oms_area_util.f_warehouses";
        

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

        public DataSet Get_Warehouses()
        {

            return dataManager.ExecuteDataset(
                                                WAREHOUSES.ToString(),
                                                null);
        }


        public DataSet Get_Area()
        {

            return dataManager.ExecuteDataset(
                                                GetArea.ToString(),
                                                null);
        }

        public DataSet Get_Area_for_ID(Int32 I_area_id)
        {

            Object[] insParams = new Object[] { I_area_id };
            return dataManager.ExecuteDataset(
                                                GetAreaID.ToString(),
                                                insParams);
        }

        public decimal Create_Area(Int32 I_type_id,
                                      decimal I_warehouse_id,
                                      string I_area_desc, 
                                      string I_handle_split, 
                                      string I_act_ind,
                                      string I_userid,
                                      string I_allow_admin_release_ind)
        {
            decimal area_id = 0;
            
            
            Object[] insParams = new Object[] { area_id,
                                                I_warehouse_id,
                                                I_type_id, 
                                                I_area_desc, 
                                                I_handle_split,
                                                I_act_ind,
                                                I_userid,
                                                I_allow_admin_release_ind
                                                 };

            return dataManager.ExecuteReturnMethod(CreateArea.ToString(),
                                                   insParams);

        }

        public decimal Update_Area(decimal I_area_id,
                                   decimal I_warehouse_id,
                                      Int32   I_type_id,
                                      string I_area_desc,
                                      string I_handle_split,
                                      string I_act_ind,
                                      string I_userid,
                                      string I_allow_admin_release_ind)
        {


            Object[] updParams = new Object[] { I_area_id, 
                                                I_warehouse_id,
                                                I_type_id, 
                                                I_area_desc, 
                                                I_handle_split,
                                                I_act_ind,
                                                I_userid,
                                                I_allow_admin_release_ind };

            return dataManager.ExecuteReturnMethod(EditArea.ToString(),
                                                   updParams);
        }

        


        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);



        public DataSet GetAreaType()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(AreaTypeDD.ToString(), null);

            codes.Tables[0].TableName = "AT";

            return codes;
        }

        public DataSet GetAreaHandleSplit()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(HandleSplitDD.ToString(), null);

            codes.Tables[0].TableName = "HS";

            return codes;
        }

        public DataSet GetActiveInd()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(ActiveIndDD.ToString(), null);

            codes.Tables[0].TableName = "AI";

            return codes;
        }

        
        #endregion
    }
}
