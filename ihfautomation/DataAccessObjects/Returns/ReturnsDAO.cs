// Name: ReturnsDAO.cs
// Type: class file 
// Description: Class for Returns Data Access Object
//
//$Revision:   1.7  $
//
// Version   Date        Author     Reason
//  1.0      16/11/12    J Watt     Initial Revision
//  1.1      04/12/12    J Watt     iorder
//  1.2      11/12/12    J Watt     Intermediate Checkin
//  1.3      11/12/12    J Watt     Checkin before promotion to DEV
//  1.4      17/12/12    J Watt     Added terminal_id to db call.
//  1.5      09/05/17    M Cackett  Cross border returns changes.
//  1.6      18/05/17    M Cackett  CBR call proc to process refund in store.
//  1.7      10/10/17    M Cackett  Added getOrderSource - used to stop store
//                                  users from using CBR app for store orders.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using IHF.BusinessLayer.Util;
using IHF.EnterpriseLibrary;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.DataAccessObjects.Returns
{
    public class ReturnsDAO
    {
        #region "private constants"

        private const string CustomerSearch = "oms_returns.f_customer_search";
        private const string OrdersForCustomer = "oms_returns.f_orders_for_customer";
        private const string ItemsForOrder = "oms_returns.f_items_for_order";
        private const string ItemsToReturn = "oms_returns.f_items_to_return";
        private const string ReturnActions = "oms_returns.f_return_actions";
        private const string OrderDetails = "oms_returns.f_orderdetails";
        private const string SkuCode = "oms_returns.f_sku";
        private const string ReturnAnItem = "oms_returns.p_return_item";
        private const string RefundAnItem = "oms_returns.p_return_item_in_store";
        private const string OrderSource = "oms_returns.f_order_source";


        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        private string connstr;

        #endregion

        public ReturnsDAO ()
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings cs in settings)
            {
             if (cs.ProviderName == "Oracle.DataAccess.Client") { connstr = cs.ConnectionString; break; }
            }

        }

        public DataSet searchCustomer(int? ordernumber, string surname, string firstname, string postcode, string address1, string customerurn, string emailaddress, string phonenumber, string countrycode)
        {
            DataSet ds = dataManager.ExecuteDataset(
                                                CustomerSearch.ToString(),
                                                new object[] { ordernumber, surname, firstname, postcode, address1, customerurn, emailaddress, phonenumber, countrycode });
            return ds;
        }

        public DataSet getReturnActions(string storeUser)
        {
            DataSet ds = dataManager.ExecuteDataset(ReturnActions.ToString(), new object[] { storeUser });

            return ds;
        }

        public DataSet getCustomerOrders(string customerURN)
        {
            DataSet ds = dataManager.ExecuteDataset(
                OrdersForCustomer.ToString(),
                new object[] { customerURN });

            return ds;
        }

        public DataSet getOrderItems(int ordernumber)
        {
            DataSet ds = dataManager.ExecuteDataset(
                                 ItemsForOrder.ToString(),
                                 new object[] { ordernumber });

            return ds;
        }

        public DataSet getOrderDetails(string packageBarcode)
        {
            DataSet ds = dataManager.ExecuteDataset(OrderDetails.ToString(), new object[] { packageBarcode });

            return ds;
        }

        public string getSKU(string skuUPC)
        {
            return dataManager.GetValue(SkuCode.ToString(), new object[] { skuUPC });
        }

        public DataSet getItemsToReturn (int ordernumber)
        {
            DataSet ds = dataManager.ExecuteDataset(
                                 ItemsToReturn.ToString(),
                                 new object[] { ordernumber });

            return ds;
        }

        public void ReturnItem (string parcelScannedInd, int itemnumber, int ordernumber, string actioncode, string taskdescription, string customerurn, string sku, string userlogin, string terminalId)
        {
            dataManager.ExecuteNonQuery(ReturnAnItem.ToString(), new object[] {parcelScannedInd, itemnumber, ordernumber, actioncode, taskdescription, customerurn, sku, userlogin, terminalId });
        }

        public void RefundItem(string parcelScannedInd, int itemnumber, int ordernumber, string actioncode, 
                               string taskdescription, string customerurn, string sku, string userlogin, 
                               string storeID, string terminalId)
        {
            dataManager.ExecuteNonQuery(RefundAnItem.ToString(), 
                                        new object[] { parcelScannedInd, itemnumber, ordernumber, actioncode, 
                                                       taskdescription, customerurn, sku, userlogin, storeID, 
                                                       terminalId
                                                     });
        }

        public string getOrderSource(string packageBarcode)
        {
            return this.dataManager.GetValue(OrderSource, new object[] { packageBarcode });
        }
    }
}
