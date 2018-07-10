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
    public class OverflowtoteDAO
    {
         #region "private constants"

        private const string SearchOFtote = "oms_overflow_tote_util.f_active_overflow_totes";
        private const string GetOFTote = "oms_overflow_tote_util.f_get_overflow_tote";
        
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


        public DataSet Search_OFTote()
        {

            return dataManager.ExecuteDataset(
                                                SearchOFtote.ToString(),
                                                null);
        }

        public DataSet Get_OFToteclass(Int32 I_overflowtote_id)
        {
            
            Object[] delParams = new Object[] { I_overflowtote_id };
            return dataManager.ExecuteDataset(GetOFTote.ToString(),
                                                   delParams);

        }

        #endregion
    }
}
