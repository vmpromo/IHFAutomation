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
    public class TrolleyDAO
    {
        #region "private constants"

        private const string SearchTrolley = "oms_trolley_util.f_active_trolley";
        private const string DeleteTrolley = "oms_trolley_util.p_delete_trolley";
        private const string UpdateTrolley = "oms_trolley_util.p_edit_trolley";
        private const string InsertTrolley = "oms_trolley_util.p_create_trolley";
        private const string GetTrolley = "oms_trolley_util.f_get_trolley";
        private const string GetTrolleyLoc = "oms_trolley_util.f_get_trolley_locations";
        private const string CODES = "oms_trolley_util.f_get_trolleydesc_bytype";
        private const string class_codes = "oms_trolley_util.f_get_trolleydesc_byclass";
        private const string troverview = "oms_trolley_reports.p_trolley_overview";
        private const string trdetail = "oms_trolley_reports.f_trolley_details";
        private const string ddclass = "oms_trolley_reports.f_get_trolley_class";
        private const string ddstatus = "oms_trolley_reports.f_get_trolley_status";
        private const string ddsergrp = "oms_trolley_reports.f_get_service_group";
        private const string tritems = "oms_trolley_reports.f_trolley_items";
        private const string trwrk = "oms_trolley_reports.f_get_workstations";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        private Trolley trolley = new Trolley();

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


        public DataSet Search_trolley()
        {

            return dataManager.ExecuteDataset(
                                                SearchTrolley.ToString(),
                                                null);
        }

        public decimal Create_trolley(Int32 I_class_type, Int32 I_trolley_type, string I_label, string I_userid)
        {
            decimal trolley_id = 0;
            string user_id = I_userid;
            string trolley_label = I_label;
            Object[] insParams = new Object[] { trolley_id, I_class_type, I_trolley_type, user_id, trolley_label };

            return dataManager.ExecuteReturnMethod(InsertTrolley.ToString(),
                                                   insParams);

        }

        public decimal Update_trolley(Int32 I_trolley_id, string I_label, Int32 I_trolley_type, string I_userid)
        {

            Int32 trolley_id = I_trolley_id;
            string user_id = I_userid;
            string trolley_label = I_label;
            Object[] updParams = new Object[] { trolley_id, user_id, I_trolley_type, trolley_label };

            return dataManager.ExecuteReturnMethod(UpdateTrolley.ToString(),
                                                   updParams);
        }

        public decimal Delete_trolley(Int32 I_trolley_id, string I_userid)
        {
            Int32 trolley_id = I_trolley_id;
            string user_id = I_userid;
            Object[] delParams = new Object[] { trolley_id, user_id };

            return dataManager.ExecuteReturnMethod(DeleteTrolley.ToString(),
                                                   delParams);

        }


        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        public DataSet Get_trolleyclass(Int32 I_trolley_id)
        {
            Int32 trolley_id = I_trolley_id;
            Object[] delParams = new Object[] { trolley_id };
            return dataManager.ExecuteDataset(GetTrolley.ToString(),
                                                   delParams);

        }

        public DataSet Get_trolleyloc(Int32 I_trolley_id)
        {
            Int32 trolley_id = I_trolley_id;
            Object[] delParams = new Object[] { trolley_id };
            return dataManager.ExecuteDataset(GetTrolleyLoc.ToString(),
                                                   delParams);

        }

        public DataSet GetCodesByType()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(CODES.ToString(), null);

            codes.Tables[0].TableName = "TT";

            return codes;
        }

        public DataSet GetClassCodes()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(class_codes.ToString(), null);

            codes.Tables[0].TableName = "CD";

            return codes;
        }

        public DataSet Get_trolley_overview(string I_label, Int32 I_status, Int32 I_class, string I_service_group)
        {
            //DataSet ds_tr = new DataSet();
            Object[] ovParams = new Object[] { I_label, I_status, I_class, I_service_group };

            return dataManager.SelectDataSetProcedure(troverview.ToString(),
                                                   ovParams);
             

        }

        public DataSet Get_trolley_detail(Int32 trolley_id)
        {

            Object[] dtlParams = new Object[] { trolley_id};
            return dataManager.ExecuteDataset(trdetail.ToString(), dtlParams);

            
        }

        public DataSet Get_trolley_items(Int32 trolley_id)
        {

            Object[] dtlParams = new Object[] { trolley_id };
            return dataManager.ExecuteDataset(tritems.ToString(), dtlParams);


        }

        public DataSet Get_tr_class()
        {

            return dataManager.ExecuteDataset(ddclass.ToString(), null);

            
        }

        public DataSet Get_tr_status()
        {

            return dataManager.ExecuteDataset(ddstatus.ToString(), null);


        }
        public DataSet Get_service_group()
        {

            return dataManager.ExecuteDataset(ddsergrp.ToString(), null);


        }

        public DataSet Get_trolley_wrk(Int32 trolley_id)
        {

            Object[] dtlParams = new Object[] { trolley_id };
            return dataManager.ExecuteDataset(trwrk.ToString(), dtlParams);


        }
        #endregion
    }
}
