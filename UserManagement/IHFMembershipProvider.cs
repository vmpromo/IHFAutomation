// Name: IHFMembershipProvider.cs
// Type: custom Membership provider class
// Description: This class is used implement IHF specific membership provider
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      17/03/11    ITMK      Released version
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Data;

namespace IHF.Security.UserManagement
{
    public class IHFMembershipProvider:MembershipProvider
    {
        private string applicationName = string.Empty;
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

        #region Invalid Operations

        public override bool ChangePassword ( string username, string oldPassword, string newPassword )
        {
            throw new InvalidOperationException ( ) ;
        }

        public override bool ChangePasswordQuestionAndAnswer ( string username, string password, string newPasswordQuestion, string newPasswordAnswer )
        {
            throw new InvalidOperationException ( );
        }

        public override MembershipUser CreateUser ( string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status )
        {
            throw new InvalidOperationException ( );
        }

        public override bool DeleteUser ( string username, bool deleteAllRelatedData )
        {
            throw new InvalidOperationException ( );
        }

        public override MembershipUserCollection FindUsersByEmail ( string emailToMatch, int pageIndex, int pageSize, out int totalRecords )
        {
            throw new InvalidOperationException ( );
        }

        public override MembershipUserCollection FindUsersByName ( string usernameToMatch, int pageIndex, int pageSize, out int totalRecords )
        {
            throw new InvalidOperationException ( ); ;
        }

        public override MembershipUserCollection GetAllUsers ( int pageIndex, int pageSize, out int totalRecords )
        {
            throw new InvalidOperationException ( );
        }

        public override int GetNumberOfUsersOnline ( )
        {
            throw new InvalidOperationException ( );
        }

        public override string GetPassword ( string username, string answer )
        {
            throw new InvalidOperationException ( );
        }

       

        public override MembershipUser GetUser ( object providerUserKey, bool userIsOnline )
        {
            throw new InvalidOperationException ( );
        }

        public override string GetUserNameByEmail ( string email )
        {
            throw new InvalidOperationException ( );
        }
      
        public override string ResetPassword ( string username, string answer )
        {
            throw new InvalidOperationException ( );
        }

        public override bool UnlockUser ( string userName )
        {
            throw new InvalidOperationException ( );
        }

        public override void UpdateUser ( MembershipUser user )
        {
            throw new InvalidOperationException ( );
        }

        #endregion

        #region Properties

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 3; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 1; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 1; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Clear; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return string.Empty; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        #endregion

        public IHFMembershipProvider()
        {
        }

        public override void Initialize ( string name, System.Collections.Specialized.NameValueCollection config )
        {
            base.Initialize ( name, config );

            this.applicationName = config [ Definitions.CONFIG_APPLICATION_NAME ];
        }

        public override bool ValidateUser ( string username, string password )
        {
            MembershipDAO membershipDAO = new MembershipDAO();
            return membershipDAO.ValidateUser ( username, password );
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipDAO membershipDAO = new MembershipDAO();
            
            string userName = membershipDAO.LoggedInUserName(username);
            
            MembershipUser membershipUser = new MembershipUser(
                                                    this.Name,
                                                    userName,
                                                    null,
                                                    "",
                                                    "",
                                                    "",
                                                    false,
                                                    false,
                                                    DateTime.Now,
                                                    DateTime.Now,
                                                    DateTime.Now,
                                                    DateTime.Now,
                                                    DateTime.Now);

            return membershipUser;
        }

    }
}
