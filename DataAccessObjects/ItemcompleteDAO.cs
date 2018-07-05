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
    public class ItemcompleteDAO
    {

        #region "private constants"

        private const string itemspacked = "oms_items_completed_reports.f_units_packed_by_hour";
        private const string itemspackeduser = "oms_items_completed_reports.f_units_packed_by_hour_user";
        private const string ordersbyhour = "oms_items_completed_reports.f_orders_by_hour";

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


        

        public DataSet Get_itemspacked(DateTime startdate, DateTime enddate)
        {

            Object[] delParams = new Object[] { startdate, enddate };
            return dataManager.ExecuteDataset(itemspacked.ToString(),
                                                   delParams);

        }

        public DataSet Get_itemspacked_user(DateTime startdate)
        {

            Object[] delParams = new Object[] { startdate};
            return dataManager.ExecuteDataset(itemspackeduser.ToString(),
                                                   delParams);

        }

        public DataSet Get_ordersbyhour(DateTime startdate, DateTime enddate)
        {

            Object[] delParams = new Object[] { startdate, enddate };
            return dataManager.ExecuteDataset(ordersbyhour.ToString(),
                                                   delParams);

        }

        #endregion
    }
}
