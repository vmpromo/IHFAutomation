using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses
{
    public class Device:IDataService
    {
        #region "private variables and constants"
        
        private decimal     _id;
        private decimal     _type;
        private decimal     _workstationId;
        private string      _serialNumber;
        private DateTime    _createdOn;
        private string      _barcode;
        private string      _deviceName;
        private string      _createdBy;
        private DateTime    _lastChangedOn;
        private string      _currentUser;
        private string      _lastChangedBy;

        private const string DEVICE_BY_ID = "oms_device_util.f_get_device_by_id";

        #endregion

        #region "properties"

        public decimal ID 
        {
            get { return _id; }
            set { _id = value; } 
        }

        public decimal Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public decimal WorkstationId 
        {
            get { return _workstationId; }
            set { _workstationId = value; } 
        }

        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }

        public DateTime  CreatedOn 
        {
            get { return _createdOn; }
            set { _createdOn = value; } 
        }

        public string    Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        public string    DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public string    CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public DateTime  LastChangedOn
        {
            get { return _lastChangedOn; }
            set { _lastChangedOn = value; }
        }

        public string CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public string LastChangedBy
        {
            get { return _lastChangedBy; }
            set { _lastChangedBy = value; }
        }

        #endregion

        #region "enum"

        public enum ClassMethods
        {
            GetDevices
        }
        #endregion

        #region "public functions for the data access layer to use"
        [MethodMapper("GetDevices",Device.DEVICE_BY_ID)]
        public List<IDataService> GetDevices(IDataReader dataReader)
        {
            List<IDataService> listOfDevices = new List<IDataService>();

            if (dataReader.Read())
            {
                this.ID = int.Parse(dataReader[0].ToString());
                this.DeviceName = dataReader[1].ToString();
                this.Type = int.Parse(dataReader[2].ToString());
                this.WorkstationId = Convert.IsDBNull(dataReader[3]) == true ? 0 : decimal.Parse(dataReader[3].ToString());
                this.SerialNumber = dataReader[4].ToString();
                this.Barcode = dataReader[5].ToString();
                this.CurrentUser= dataReader[6].ToString();

                listOfDevices.Add(this);
            }
            return listOfDevices;

        }
        #endregion

    }
}
