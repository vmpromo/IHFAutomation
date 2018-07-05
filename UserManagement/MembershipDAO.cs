using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer;

namespace IHF.Security.UserManagement
{
    internal class MembershipDAO
    {
        private DataManager _dataManager = new DataManager(IHF.BusinessLayer.Util.DBInstanceEnum.Ora);

        //these will be changed to point to mds_user in MNPUSERMASTER schema
        private const string CMD_VALIDATE_USER              = "OMS_USER.F_VALIDATE_USER";
        private const string CMD_AUTHORISED_TO_PAGE         = "OMS_USER.F_AUTHORISED_TO_PAGE";
        private const string CMD_GET_USER_FULL_NAME         = "OMS_USER.F_USER_FULL_NAME";
        
        
        internal bool ValidateUser ( string userName, string password )
        {
            return this._dataManager.CheckBooleanValue ( CMD_VALIDATE_USER, new object [ ] { userName, password } );
        }

		internal bool AuthorisedToPage ( string userName, string Url, string applicationName )
		{
            return this._dataManager.CheckBooleanValue(CMD_AUTHORISED_TO_PAGE, new object[] { userName, Url, applicationName });
		}

        internal string LoggedInUserName(string userName)
        {
            return this._dataManager.GetValue(CMD_GET_USER_FULL_NAME, new object[] { userName });
        }


    }
}
