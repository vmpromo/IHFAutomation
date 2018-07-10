// Name: UserDAO.cs
// Type: class file 
// Description: Class for User Data Access Object
//
//$Revision:   1.20  $
//
// Version   Date        Author     Reason
//  1.0      10/03/11    itmk       Initial Revision
//  1.1      10/03/11    itmk       No Change
//  1.2      11/03/11    itmk       No Change
//  1.3      14/03/11    itmk       No Change
//  1.4      15/03/11    itmk       No Change
//  1.5      15/03/11    itmk       No Change
//  1.6      16/03/11    itmk       Added Comments
//  1.7      17/03/11    itmk       No Change
//  1.8      17/03/11    itaj1      No Change
//  1.9      18/03/11    itaj1      No Change
//  1.10     18/03/11    itmk       No Change
//  1.11     25/03/11    itmk       changes package name from mds_users to oms_users
//  1.12     25/03/11    itmk       No Change
//  1.13     25/03/11    itmk       No Change
//  1.14     08/04/11    itaj1      No Change
//  1.15     03/06/11    itmk       browser check, navigation, membershipprovider change 
//                                  to allow urls with the same text, master page to display 
//                                  login name and log off link, logos on master and 
//                                  error page to link to home page
//  1.16     23/08/11    itmk       Despatch - Interim version
//  1.17     07/09/11    itmk       returns link on desktop home page
//  1.18     05/12/11    itmk       No change.
//  1.19     20/10/14    itsg       SDFT-45 - Add Management Print
//  1.20     04/05/17    M Cackett  Cross Border Returns - added GetUserName
//                                  Used to check if user exists for CBR.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class UserDAO
    {
        #region "private varaibles and constants"
        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        private const string USERS = "OMS_USER.F_GET_ALL_USERS";
        private const string GETUSERBYBARCODE = "OMS_USER.F_GET_USER_BY_BARCODE";
        private const string USERFULLNAME = "OMS_USER.F_USER_FULL_NAME";

        #endregion

        #region "public functions"
        public DataSet GetUsers()
        {
            DataSet users =
                        new DataSet();

            users =
                _dataManager.ExecuteDataset(
                    USERS.ToString(),
                    null);

            users.Tables[0].TableName =
                "tblusers";

            return users;
        }

        public IDataReader GetUserFunctions(string user, string application) {
            return _dataManager.ExecuteReader("oms_user.f_user_functions", new object[] {user, application });
        }

        public DataSet GeUserByBarcode(string userBarcode)
        {

            Object[] Params = new Object[] { userBarcode };

            return _dataManager.ExecuteDataset(
                                                GETUSERBYBARCODE.ToString(),
                                                Params);
        }


        public string GetUserName(string user)
        {
            return this._dataManager.GetValue(USERFULLNAME, new object[] { user });
        }
        #endregion
    }
}
