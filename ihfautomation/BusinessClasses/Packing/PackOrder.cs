// Name: PackOrder.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties, and method user
//              to call database functions.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released
//  1.1      29/07/11    MSalman   Function added from order
//                                 print.                                        
//  1.2      04/08/11    MSalman   new fileds added.
//  1.3      18/08/11    MSalman   New field added.          
//  1.4      24/08/11    MSalman   New field added.
//  1.5      27/09/11    MSalman   Data types are added.                    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.Packing
{
    [Serializable]
    public class PackOrder : IDataService
    {


        #region Private Memeber

        private const string GET_PACK_ORDER = "OMS_PACK.F_OPEN_ORDERS";

        private const string GET_ORDER_TO_PRINT = "OMS_PACK.F_OPEN_ORDER_FOR_PRINT";


        private List<PackOrder> _lstPackOrder = new List<PackOrder>();


        #endregion

        #region Function Mapping

        public enum ClassMethods
        {
            GetPackOrder,
            GetOrderForPrint

        }

        #endregion

        #region  Properties

        public int OrderNo { get; set; }

        public string OrderStatus { get; set; }

        public DateTime LastAction { get; set; }

        public string User { get; set; }

        public string Location { get; set; }

        public DateTime OrderDate { get; set; }

        public string ServiceGroupId { get; set; }

        public string ServiceGroupName { get; set; }

        public string CollectionWindow { get; set; }

        public int OrderItem { get; set; }

        public string DestinationType { get; set; }

        public string ProcessMode { get; set; }

        public string  ToteId { get; set; }

        public string ContainerLabel { get; set; }


        public List<PackOrder> PackOrderInfo
        {
            get
            {
                return _lstPackOrder;
            }

            set
            {
                _lstPackOrder = value;

            }

        }


        #endregion

        #region Local Functions


        [MethodMapper("GetPackOrder", PackOrder.GET_PACK_ORDER)]
        public IList<IDataService> GetPackOrder(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<PackOrder> items = new List<PackOrder>();


            while (reader.Read())
            {
                PackOrder obj = new PackOrder();

                obj.OrderNo = Convert.ToInt32(reader["ORDERNUMBER"].ToString());
                obj.OrderStatus = reader["ORDERSTATUS"].ToString() ?? string.Empty;
                obj.LastAction = reader["LASTACTION"].ToString()!=string.Empty? Convert.ToDateTime(reader["LASTACTION"].ToString()):DateTime.MinValue;
                obj.User = reader["USERID"].ToString() ?? string.Empty;
                obj.Location = reader["LOCATION"].ToString() ?? string.Empty;
                obj.OrderDate = reader["ORDERDATE"].ToString()!=string.Empty? Convert.ToDateTime(reader["ORDERDATE"].ToString()):DateTime.MinValue;
                obj.ServiceGroupId = reader["SERVICEGROUP"].ToString() ?? string.Empty;
                obj.ServiceGroupName = reader["SERVICEGROUPNAME"].ToString() ?? string.Empty;
                obj.CollectionWindow = reader["COLLECTIONWINDOW"].ToString() ?? string.Empty;
                obj.OrderItem = reader["ORDERITEMS"].ToString() !=string.Empty? Convert.ToInt32(reader["ORDERITEMS"].ToString()) :0;
                obj.DestinationType = reader["DESTINATIONTYPE"].ToString() ?? string.Empty;
                obj.ProcessMode = reader["PROCESSMODE"].ToString() ?? string.Empty;
                obj.ToteId = reader["TOTEID"].ToString() ?? string.Empty;
                obj.ContainerLabel = reader["ContainerLabel"].ToString() ?? string.Empty;


                items.Add(obj);
            }

            this._lstPackOrder = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }


        [MethodMapper("GetOrderForPrint", PackOrder.GET_ORDER_TO_PRINT)]
        public IList<IDataService> GetOrderForPrint(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<PackOrder> items = new List<PackOrder>();


            while (reader.Read())
            {
                PackOrder obj = new PackOrder();
                obj.OrderNo = Convert.ToInt32(reader["ORDERNUMBER"].ToString());
                obj.OrderStatus = reader["ORDERSTATUS"].ToString() ?? string.Empty;
                obj.LastAction = reader["LASTACTION"].ToString() != string.Empty ? Convert.ToDateTime(reader["LASTACTION"].ToString()) : DateTime.MinValue;
                obj.User = reader["USERID"].ToString() ?? string.Empty;
                obj.Location = reader["LOCATION"].ToString() ?? string.Empty;
                obj.OrderDate = reader["ORDERDATE"].ToString() != string.Empty ? Convert.ToDateTime(reader["ORDERDATE"].ToString()) : DateTime.MinValue;
                obj.ServiceGroupId = reader["SERVICEGROUP"].ToString() ?? string.Empty;
                obj.ServiceGroupName = reader["SERVICEGROUPNAME"].ToString() ?? string.Empty;
                obj.CollectionWindow = reader["COLLECTIONWINDOW"].ToString() ?? string.Empty;
                obj.OrderItem = reader["ORDERITEMS"].ToString() != string.Empty ? Convert.ToInt32(reader["ORDERITEMS"].ToString()) : 0;

                items.Add(obj);
            }

            this._lstPackOrder = items;

            lst.Add(this);

            reader.Close();

            return lst;
        }


        #endregion



    }
}
