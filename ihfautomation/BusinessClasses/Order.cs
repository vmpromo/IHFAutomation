using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses
{
    public class Order:IDataService
    {
        #region "private variables and constants"

        private const string ORDERDETAIL = "IHF_TEST_UTIL.GetOrderDetail";
        private const string ORDERDETAILS = "IHF_TEST_UTIL.OrderAllDetails";

        #endregion
        public enum ClassMethods
        {
            GetOrderDetail,
            GetAllOrderdetails
        }
        
        private int _orderNumber;
        private string _description;

        public int OrderNumber 
        {
            get { return _orderNumber; }
            set { _orderNumber = value; } 
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #region "private methods"
        
        private List<IDataService> GetStrongTypeList(IDataReader dataReader)
        {
            List<IDataService> listOfOrderDetails = new List<IDataService>();

            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderNumber = int.Parse(dataReader[0].ToString());
                order.Description = dataReader[1].ToString();
                listOfOrderDetails.Add(order);
            }

            return listOfOrderDetails;
        }
        
        #endregion

        [MethodMapper("GetOrderDetail", Order.ORDERDETAIL)]
        public List<IDataService> GetOrderDetails(IDataReader dataReader)
        {
            List<IDataService> listOfOrderDetails = new List<IDataService>();

            if (dataReader.Read())
            {
                this.OrderNumber = int.Parse(dataReader[0].ToString());
                this.Description = dataReader[1].ToString();
                listOfOrderDetails.Add(this);
            }
            return listOfOrderDetails;
            
        }

        [MethodMapper("GetAllOrderdetails", Order.ORDERDETAILS)]
        public List<IDataService> GetAllOrderDetails(IDataReader dataReader)
        {
            return GetStrongTypeList(dataReader);
            
        }




        
    }

    
}
