// Name: IHFRoleProvider.cs
// Type: custom role provider class
// Description: This class is used implement IHF specific role provider
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      18/03/11    ITMK      Released version
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Data;

namespace IHF.Security.UserManagement
{
    public class IHFRoleProvider:RoleProvider
    {
        private string applicationName = string.Empty;

        private RoleDAO _roleDAO = null;
        private RoleDAO RoleDAO
        {
            get
            {
                if (this._roleDAO == null)
                    this._roleDAO = new RoleDAO(this.applicationName);

                return this._roleDAO;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return this.applicationName;
            }
            set
            {
                this.applicationName = value;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            this.applicationName = config[Definitions.CONFIG_APPLICATION_NAME];
        }

        #region Invalid Operations

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new InvalidOperationException();
        }

        public override void CreateRole(string roleName)
        {
            throw new InvalidOperationException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new InvalidOperationException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new InvalidOperationException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new InvalidOperationException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new InvalidOperationException();
        }

        #endregion

        public override bool IsUserInRole(string username, string roleName)
        {
            return this.RoleDAO.IsUserInRole(username, roleName);
        }

        public override bool RoleExists(string roleName)
        {
            return this.RoleDAO.RoleExists(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return this.RoleDAO.GetRolesForUser(username);
        }

        public override string[] GetAllRoles()
        {
            return this.RoleDAO.GetUserRoles();
        }

    }
}
