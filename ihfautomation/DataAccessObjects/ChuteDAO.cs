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
    public class ChuteDAO
    {
        #region "private constants"

        private const string SearchChute = "oms_chute_util.f_chutes";
        private const string GetChute = "oms_chute_util.f_get_chute";
        private const string ChuteTypeDD = "oms_chute_util.f_get_chute_type";
        private const string ChuteStatusDD = "oms_chute_util.f_get_chute_status";
        private const string EnableIndDD = "oms_chute_util.f_get_chute_enable_ind";
        private const string IntIndDD = "oms_chute_util.f_get_chute_int_ind";
        private const string AreaDD = "oms_chute_util.f_get_areas_dd";
        private const string CreateChute = "oms_chute_util.p_create_chute";
        private const string UpdateChute = "oms_chute_util.p_edit_chute";
        private const string ChutesInArea = "oms_chutes_in_area_setup.f_get_chutes_in_area";
        private const string UpdChuteSeq = "oms_chutes_in_area_setup.p_update_chute_seq";
        private const string chkarea = "oms_chutes_in_area_setup.f_check_area_in_use";
        private const string managestack = "oms_chutes_in_area_setup.p_update_chute_stack";
        private const string check_stack = "oms_chutes_in_area_setup.f_stack_exists";

        
        
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


        public DataSet Search_chute()
        {

            return dataManager.ExecuteDataset(
                                                SearchChute.ToString(),
                                                null);
        }


        public decimal Create_Chute(string    I_label,
                                      Int32   I_ch_status,
                                      Int32   I_ch_type,
                                      string  I_enb_ind,
                                      decimal I_area_id,
                                      string  I_userid,
                                      Int32   I_trolley_type)
        {
            decimal chute_id = 0;


            Object[] insParams = new Object[] { chute_id, 
                                                I_label, 
                                                I_ch_status, 
                                                I_ch_type,
                                                I_enb_ind,
                                                "F",
                                                I_area_id,
                                                I_userid,
                                                I_trolley_type
                                                 };

            return dataManager.ExecuteReturnMethod(CreateChute.ToString(),
                                                   insParams);

        }

        public decimal Update_Chute(decimal I_chute_id,
                                    string  I_label,
                                    Int32   I_ch_status,
                                    Int32   I_ch_type,
                                    string  I_enb_ind,
                                    decimal I_area_id,
                                    string  I_userid,
                                    Int32   I_trolley_type)
        {


            Object[] updParams = new Object[] { I_chute_id, 
                                                I_label, 
                                                I_ch_status, 
                                                I_ch_type,
                                                I_enb_ind,
                                                "F",
                                                I_area_id,
                                                I_userid,
                                                I_trolley_type};

            return dataManager.ExecuteReturnMethod(UpdateChute.ToString(),
                                                   updParams);
        }

        public DataSet Get_chuteclass(Int32 I_chute_id)
        {
            Int32 trolley_id = I_chute_id;
            Object[] delParams = new Object[] { I_chute_id };
            return dataManager.ExecuteDataset(GetChute.ToString(),
                                                   delParams);

        }

        public DataSet GetChuteType()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(ChuteTypeDD.ToString(), null);

            codes.Tables[0].TableName = "CT";

            return codes;
        }

        public DataSet GetChuteStatus()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(ChuteStatusDD.ToString(), null);

            codes.Tables[0].TableName = "CS";

            return codes;
        }

        public DataSet GetEnableInd()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(EnableIndDD.ToString(), null);

            codes.Tables[0].TableName = "EI";

            return codes;
        }
        public DataSet GetIntInd()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(IntIndDD.ToString(), null);

            codes.Tables[0].TableName = "II";

            return codes;
        }
        public DataSet GetArea()
        {
            DataSet codes = new DataSet();

            codes = dataManager.ExecuteDataset(AreaDD.ToString(), null);

            codes.Tables[0].TableName = "AR";

            return codes;
        }


        // methods for chutes in area

        public DataSet Chutes_In_Area(Int32 I_area_id)
        {

            Object[] Params = new Object[] { I_area_id };
            return dataManager.ExecuteDataset(
                                                ChutesInArea.ToString(),
                                                Params);
        }

        public decimal Update_Chute_seq(Int32 I_chute_id,
                                    Int32 I_real_seq,
                                    string I_userid
                                    )
        {


            Object[] updParams = new Object[] { I_chute_id, 
                                                I_real_seq,
                                                I_userid
                                                };

            return dataManager.ExecuteReturnMethod(UpdChuteSeq.ToString(),
                                                   updParams);
        }


        public string Check_area(Int32 area_id)
        {
            Object[] Params = new Object[] { area_id };


            return dataManager.GetValue(chkarea.ToString(),
                                                   Params);
        }

        public decimal Manage_stack(  Int32 I_area_id,
                                      Int32 I_nxt_stack,
                                      string I_userid
                                      )
        {
            decimal status_id = 0;


            Object[] insParams = new Object[] { status_id, 
                                                I_area_id, 
                                                I_nxt_stack, 
                                                I_userid
                                                 };

            return dataManager.ExecuteReturnMethod(managestack.ToString(),
                                                   insParams);

        }




        public bool CheckStack(string area_Id)
        {
            return this.dataManager.CheckBooleanValue(check_stack, new object[] { area_Id});
        }


        #endregion
    }
}
