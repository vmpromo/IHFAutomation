using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects.Cancellation
{
    public class OrderDAO
    {
        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string Order = "oms_cancellation.f_order_details";
        private const string OrderItems = "oms_cancellation.f_order_items";
        private const string Reasons = "oms_cancellation.f_reasons";
        private const string UpdateOrderItem = "oms_cancellation.p_processcancellation";


        public DataSet GetOrderItems(int type, int orderNumber)
        {
            return _dataManager.ExecuteDataset(OrderItems, new object[] { type, orderNumber });
        }

        public IDataReader GetOrderDetails(int orderNumber)
        {
            return _dataManager.ExecuteReader(Order, new object[] { orderNumber });
        }

        public DataView GetReasons(int eventType)
        {
            return new DataView(_dataManager.ExecuteDataset(Reasons, new object[] { eventType }).Tables[0]);
        }

        public string ProcessCancellation(
            int orderNumber,
            int itemNumber,
            int status,
            string statusName,
            string userLogin
            )
        {
            return _dataManager.GetStringforProcedure(
                             UpdateOrderItem, 
                             new object[] {  orderNumber,
                                             itemNumber,
                                             status,
                                             statusName,
                                             userLogin });
        }
    }
}
