using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.Util;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    internal class MainMenuDAO
    {
        #region "private constants"

        private const string SITEMAP = "OMS_USER.F_GET_USER_SITE_MAP";

        #endregion

        #region "private variables"

        private DataManager dataManager =
            new DataManager(DBInstanceEnum.Ora);

        #endregion

        public DataTable GetMenus(string userLogon, string application)
        {
            return dataManager.ExecuteDataset(SITEMAP, new object[] { userLogon, application }).Tables[0];

            
        }
    }
}
