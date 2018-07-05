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
    public class ManualInductDAO
    {
        #region "private constants"

        private const string ManualInduct = "oms_manual_induction.p_manual_induct";
        private const string Puttochute = "oms_manual_induction.p_put_to_chute";
        private const string Getloadid = "oms_manual_induction.f_get_load_id";
        private const string Validateload = "oms_manual_induction.f_validate_load";
        private const string Validatesku = "oms_manual_induction.f_validate_sku";
        private const string cancellock = "oms_manual_induction.p_cancel_lock";
        
        
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


        public DataSet manual_induct( decimal areaid, string I_sku_barcode, string I_user, string I_load_id)
        {
            Object[] insParams = new Object[] {areaid, I_sku_barcode, I_user, I_load_id};
            return dataManager.SelectDataSetProcedure(
                                                ManualInduct.ToString(),
                                                insParams);
        } 

        public decimal put_to_chute(decimal I_returnval, decimal I_chute_id, string I_chute_barcode, string I_scanned_barcode, decimal I_itemnumber, string I_user )
        {

            Object[] insParams = new Object[] { I_returnval, I_chute_id, I_chute_barcode, I_scanned_barcode, I_itemnumber, I_user };

            
            return dataManager.ExecuteReturnMethod(Puttochute.ToString(),
                                                   insParams);
            
        }
        
        public DataSet Get_loadid(decimal areaid)
        {

            return dataManager.ExecuteDataset(
                                                Getloadid.ToString(),
                                                new Object[]{areaid});
        }
        
        public string validate_load(string I_load_id)
        {

            Object[] insParams = new Object[] { I_load_id };

            return dataManager.GetValue(Validateload.ToString(),
                                                   insParams);

        }

        public string validate_sku(string I_load_id, string I_sku_barcode, decimal areaid)
        {

            Object[] insParams = new Object[] { I_load_id, I_sku_barcode, areaid };

            return dataManager.GetValue(Validatesku.ToString(),
                                                   insParams);

        }

        public decimal cancel_lock(decimal I_req_id, decimal I_chute_id, decimal I_itemnumber,string I_user)
        {

            Object[] insParams = new Object[] { I_req_id, I_chute_id, I_itemnumber, I_user };

            return dataManager.ExecuteReturnMethod(cancellock.ToString(),
                                                   insParams);
            

        }

        #endregion
    }
}
