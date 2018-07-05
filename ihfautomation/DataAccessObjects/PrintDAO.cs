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
    public class PrintDAO
    {
        #region "private constants"

        private const string SearchPrinter = "oms_print_util.f_printer_credentials";
        
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


        public DataSet Get_printer_details(string I_machine_name, decimal I_device_type)
        {
            
            Object[] insParams = new Object[] { I_machine_name, I_device_type };

          
            return dataManager.ExecuteDataset(SearchPrinter.ToString(),
                                                   insParams);

        }

        #endregion
    }
}
