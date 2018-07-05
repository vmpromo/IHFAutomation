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
    public class StoreDeliveryGroupDAO
    {
        #region "private constants"

        private const string StoreDeliveryGroups = "oms_cage_maintenance.f_sd_group_list";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet GetStoreDeliveryGroups()
        {

            return dataManager.ExecuteDataset(StoreDeliveryGroups.ToString(), null);
            
        }


        #endregion

    }
}
