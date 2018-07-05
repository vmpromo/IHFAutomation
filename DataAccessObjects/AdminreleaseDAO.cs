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
    public class AdminreleaseDAO
    {

        #region "private constants"

        private const string SearchTrolley = "oms_admin_release.f_admin_release_trolleys";
        private const string UpdateTrolley = "oms_admin_release.f_edittrolley_label";
        private const string CODES = "mds_trolley_util.f_get_trolleydesc_bytype";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        private Order order = new Order();

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

        public DataSet Update_trolley(Int32 I_load_id, string I_label, string I_userid)
        {

            DataSet ds = new DataSet();


            Int32 load_id = I_load_id;
            string user_id = I_userid;
            string trolley_label = I_label;
            Object[] updParams = new Object[] { load_id, user_id, trolley_label };

            return dataManager.ExecuteDataset(
                                            UpdateTrolley.ToString(),
                                            updParams);

        }


        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);



        public DataSet GetCodesByType()
        {
            DataSet codes =
                        new DataSet();

            codes = dataManager.ExecuteDataset(
                                            CODES.ToString(),
                                            null);

            codes.Tables[0].TableName = "TT";

            return codes;
        }
        #endregion
    }
}
