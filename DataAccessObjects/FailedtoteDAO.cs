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
    public class FailedtoteDAO
    {
        #region "private constants"

        private const string SearchFtote     = "oms_failed_tote_util.f_failed_totes";
        private const string Sku             = "oms_failed_tote_util.f_excess_totes_skus";
        private const string ExcessTotes     = "oms_failed_tote_util.f_excess_totes_skus";
        private const string RemoveTote      = "oms_failed_tote_util.p_remove_sku";
        private const string ValidFailedTote = "oms_failed_tote_util.f_valid_failed_tote";
        private const string MoveFailedTote  = "oms_failed_tote_util.p_move_failed_tote";
        private const string CreateOrderTote = "oms_failed_tote_util.p_create_order_tote";

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


        public DataSet Search_FTote()
        {

            return dataManager.ExecuteDataset(
                                                SearchFtote.ToString(),
                                                null);
        }

        public DataSet GetSku(object[] param)
        {
            return dataManager.ExecuteDataset(ExcessTotes, 
                                              param);
        }

        public void RemoveSku(int skuFailedToteId, int reasonId, string user) {
            dataManager.ExecuteNonQuery(RemoveTote, new object[] { skuFailedToteId, reasonId, user });
        }

        public string ValidateTote(string barcode) {
            return dataManager.GetValue(ValidFailedTote, new object[] { barcode });
        }

        public string MoveTote(string toteBarcode, string workstationBarcode, string user) {
            return dataManager.GetStringforProcedure(MoveFailedTote, new object[] { toteBarcode, workstationBarcode, user });
        }

        public decimal MoveOrderIntoTote(decimal ordernumber, int failreasoncode, string errormessage, string metapackerrorcode, string metapackresponse, string terminalid, string userlogon)
        {

            decimal failedtoteid = 0;

            Object[] insParams = new Object[] { failedtoteid, 
                                                ordernumber,
                                                failreasoncode, 
                                                errormessage,
                                                metapackerrorcode,
                                                metapackresponse,
                                                terminalid,
                                                userlogon
                                                 };

            failedtoteid = dataManager.ExecuteReturnMethodDecimal(CreateOrderTote.ToString(),
                                                   insParams);

            return failedtoteid;
        }



        #endregion
    }
}
