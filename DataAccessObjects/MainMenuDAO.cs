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

        public IEnumerable<MainMenuDto> GetMenus(string userLogon, string application)
        {
            var dataset = dataManager.ExecuteDataset(SITEMAP, new object[] { userLogon, application }).Tables[0];

            return dataset
                .Rows
                .Cast<DataRow>()
                .Select(row => new MainMenuDto
                {
                    Id = decimal.Parse(row[0].ToString()),
                    Parent_Id = decimal.Parse(row[1].ToString()),
                    PageChildInd = row[2].ToString(),
                    Caption = row[3].ToString(),
                    Url = row[4].ToString()
                });
        }
    }
}
