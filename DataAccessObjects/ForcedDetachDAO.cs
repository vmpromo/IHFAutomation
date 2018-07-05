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
    public class ForcedDetachDAO
    {

        #region "private constants"

        private const string Validatechute = "oms_attach_trolley.p_validate_chute";
        private const string ChuteAttached = "oms_detach_trolley.f_chute_attached";
        private const string ValidateTrolley = "oms_detach_trolley.f_validate_trolley_barcode";
        private const string TrolleyAttached = "oms_detach_trolley.f_trolley_attached";
        private const string ManualDetach = "oms_detach_trolley.p_manual_detach_trolley";
        

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        //private Trolley trolley = new Trolley();

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


        public decimal Validate_Chute(string I_chute_barcode, string user, string terminal_id)
        {
            decimal chute_id = 0;
            Object[] detParams = new Object[] { chute_id, I_chute_barcode, user, terminal_id };

            chute_id = dataManager.ExecuteReturnMethodDecimal(
                                                Validatechute.ToString(),
                                                detParams);
            return chute_id;
        }

        public string chute_attached(decimal I_chute_id)
        {
            Object[] detParams = new Object[] { I_chute_id };

            return dataManager.GetValue(ChuteAttached.ToString(),
                                        detParams);
        }

        public decimal Validate_Trolley(string trolley_barcode)
        {


            Object[] detParams = new Object[] { trolley_barcode };

            return dataManager.GetValuedecimal(ValidateTrolley.ToString(),
                                            detParams);
        }

        public decimal Trolley_Attached(decimal chute_id, decimal trolley_id)
        {


            Object[] detParams = new Object[] { chute_id, trolley_id };

            return dataManager.GetValuedecimal(TrolleyAttached.ToString(),
                                            detParams);
        }

        public void Manual_detach(decimal I_trolley_id, string I_user_logon)
        {

            Object[] detParams = new Object[] { I_trolley_id, I_user_logon };

            dataManager.ExecuteNonQuery(ManualDetach.ToString(),
                                        detParams);



        }
        
        #endregion
    }
}
