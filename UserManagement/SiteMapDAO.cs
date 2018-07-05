// Name: SitemapDAO.cs
// Type: Data Access for Sitemap
// Description: This class is used to get site map data from database.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      17/03/11    ITMK      Released version
//  

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.Common;
using IHF.EnterpriseLibrary.Data;

namespace IHF.Security.UserManagement
{
    internal class SitemapDAO
    {
        private DataManager _dataManager = new DataManager(IHF.BusinessLayer.Util.DBInstanceEnum.Ora);

        private const string CMD_SITE_MAP                    = "";
        private const string PARAM_SITE_MAP_ROLE             = "";
        private const string PARAM_SITE_MAP_APPLICATION_NAME = "";
        private const string CMD_GET_USER_SITE_MAP           = "OMS_USER.F_GET_USER_SITE_MAP";

        public DataSet GetSiteMap(string applicationName, string userName)
        {
            DataSet dst = new DataSet();
            dst.Tables.Add(GetSiteMapForUser(userName, applicationName));
            return dst;
        }

        private DataTable GetSiteMapForUser(string username, string applicationName)
        {
            username = username.ToUpper();
            DataTable dt = this.CreateNewSiteMapTable(username);
            IDataReader iReader = this._dataManager.ExecuteReader(CMD_GET_USER_SITE_MAP, 
                                                                  new object[] { username, applicationName });
            while (iReader.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = iReader[0];
                dr[1] = iReader[1];
                if (iReader[2].ToString() == "1")
                    dr[2] = true;
                else
                    dr[2] = false;

                dr[3] = iReader[3];
                dr[4] = iReader[4];
                dt.Rows.Add(dr);

            }
            iReader.Close();
            return dt;
        }

        public DataTable CreateNewSiteMapTable(string username)
        {
            DataTable dt = new DataTable(username);

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("HasChilds", typeof(bool));
            dt.Columns.Add("Caption", typeof(string));
            dt.Columns.Add("Url", typeof(string));

            return dt;
        }
    }
}
