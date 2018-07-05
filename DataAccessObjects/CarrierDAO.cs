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
    public class CarrierDAO
    {
        #region "private constants"

        private const string GetAllCarriers = "oms_cage_maintenance.f_carrier_list";

        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        #endregion

        #region "Methods available to the presentation layer (web)"


        public DataSet GetCarriers()
        {

            return dataManager.ExecuteDataset(
                                                GetAllCarriers.ToString(),
                                                null);
        }


        #endregion

    }
}
