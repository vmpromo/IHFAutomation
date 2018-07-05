using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses
{
    class User:IDataService
    {
        #region "private variables and public properties"
        private decimal _userId;
        public decimal ID
        {
            get { return _userId; }
            set { _userId = value; } 
        }
        
        private string _userLogon;
        public string UserLogon
        {
            get { return _userLogon; }
            set { _userLogon = value; }
        }
        
        private string _foreName;
        public string ForeName
        {
            get { return _foreName; }
            set { _foreName = value; }
        }
        
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        
        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }
        
        private int _enabled;
        public int Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        
        private string _displayName;
        public string DiaplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        #endregion

    }
}
