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
    public class OrderDAO
    {
        #region "private constants"
        
        private const string ORDERDETAILS   = "IHF_TEST_UTIL.ORDERALLDETAILS";
        private const string SAVEORDER      = "IHF_TEST_UTIL.P_SaveOrder";
        
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
        
        public Order OrderDetail(int orderNumber)
        {
            return (Order)dataManager.Get(
                                            Order.ClassMethods.GetOrderDetail.ToString(), 
                                            this.order,
                                            new object[]{orderNumber})[0];
        }

        public List<Order> ListOfOrders()
        {
            return StrongOrderList(
                                    dataManager.Get(
                                                    Order.ClassMethods.GetAllOrderdetails.ToString(),
                                                    this.order));
        }

        public DataSet AllDetail()
        {
            return dataManager.ExecuteDataset(
                                                ORDERDETAILS.ToString(), 
                                                null);
            
        }

        public Order SaveOrder(Order order)
        {
            int returnResult = (int)this.dataManager.ExecuteReturnMethod(
                                                                        SAVEORDER, 
                                                                        new object[] { 
                                                                            order.OrderNumber, 
                                                                            order.Description });
            order.OrderNumber = returnResult;
            return order;
        }

        #endregion

    }
}
