using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.EnterpriseLibrary.Data;

namespace IHF.Security.UserManagement
{
    internal class RoleDAO
    {
        private DataManager _dataManager = new DataManager(IHF.BusinessLayer.Util.DBInstanceEnum.Ora);

        private const string CMD_GET_ROLES      = "OMS_USER.F_ALL_ROLES";
        private const string CMD_GET_USER_ROLES = "OMS_USER.F_USER_ROLE";
        private const char VALUES_SEPARATOR     = '@';
        private static string[] userRoles       = null;

        private string applicationName = string.Empty;

        internal RoleDAO(string applicationName)
        {
            this.applicationName = applicationName;
        }

        internal string[] GetUserRoles()
        {
            if (userRoles == null)
            {
                IDataReader iReader = this._dataManager.ExecuteReader(CMD_GET_ROLES, new object[] { applicationName });

                StringBuilder sb = new StringBuilder();
                while (iReader.Read())
                    sb.AppendFormat("{0}{1}", iReader[0].ToString(), VALUES_SEPARATOR);

                iReader.Close();

                sb.Remove(sb.Length - 1, 1);//Remove the last '@'
                userRoles = sb.ToString().Split(VALUES_SEPARATOR);
            }
            return userRoles;
        }

        internal string[] GetRolesForUser(string userName)
        {
            IDataReader iReader = this._dataManager.ExecuteReader(CMD_GET_USER_ROLES, new object[] { userName, applicationName });

            StringBuilder sb = new StringBuilder();
            while (iReader.Read())
                sb.AppendFormat("{0}{1}", iReader[0].ToString(), VALUES_SEPARATOR);

            iReader.Close();

            sb.Remove(sb.Length - 1, 1);//Remove the last '@'
            return sb.ToString().Split(VALUES_SEPARATOR);
        }

        internal bool RoleExists(string roleName)
        {
            foreach (string role in this.GetUserRoles())
                if (roleName == role)
                    return true;

            return false;
        }

        internal bool IsUserInRole(string username, string roleName)
        {
            foreach (string role in this.GetRolesForUser(username))
                if (role == roleName)
                    return true;

            return false;
        }

    }
}
