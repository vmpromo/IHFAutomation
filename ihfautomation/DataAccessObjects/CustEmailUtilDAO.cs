using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses;
using System.Data;


namespace IHF.BusinessLayer.DataAccessObjects
{
    public class CustEmailUtilDAO
    {
        #region "protected varaibles and constants"
        protected DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        protected Device _device = new Device();

        protected const string NOTIFYVANLATE = "oms_cust_email_util.p_late_van_run_notify";
        #endregion


        #region "public function"
        public void LateVanRunNotify (Int16 storeId, Int16 numDaysLate)
        {
            _dataManager.ExecuteNonQuery(NOTIFYVANLATE, new object[]{storeId, numDaysLate});                
        }

        #endregion


    }
}
