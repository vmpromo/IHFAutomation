using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class DeviceDAO
    {
        #region "protected varaibles and constants"
        protected DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        protected Device _device = new Device();

        protected const string ADD = "oms_device_util.p_add_device";
        protected const string DEVICES = "oms_device_util.f_get_all_devices";
        protected const string DELETE = "oms_device_util.p_delete_device";
        protected const string MODIFY = "oms_device_util.p_modify_device";
        #endregion

        #region "public virtual functions"
        public virtual decimal Add(Device device)
        {
            int returnResult =
                (int)this._dataManager.ExecuteReturnMethod(
                    ADD,
                    new object[] { 
                    device.ID,
                    device.Type,
                    device.WorkstationId,
                    device.SerialNumber,
                    device.DeviceName,
                    device.CreatedBy,
                    device.CurrentUser});

            return returnResult;
        }

        public virtual int Modify(Device device)
        {
            int returnResult =
                (int)_dataManager.ExecuteReturnMethod(
                    MODIFY,
                    new object[]{
                        device.ID,
                        device.Type,
                        device.WorkstationId,
                        device.SerialNumber,
                        device.DeviceName,
                        device.CurrentUser,
                        device.LastChangedBy
                    });

            return returnResult;
        }

        #endregion

        #region "public function"
        public DataSet GetDevices()
        {
            DataSet devices =
                        new DataSet();

            devices = 
                _dataManager.ExecuteDataset(
                    DEVICES.ToString(),
                    null);

            devices.Tables[0].TableName = 
                "mds_device";

            return devices;
                
        }

        public Device GetDeviceByID(int deviceID)
        {

            return (Device)_dataManager.Get(
                                Device.ClassMethods.GetDevices.ToString(),
                                this._device,
                                new object[]
                                {
                                    deviceID
                                }
                                )[0];
        }

        public int Delete(Device device)
        {
            int returnResult =
                (int)_dataManager.ExecuteReturnMethod(
                    DELETE,
                    new object[]{
                        device.ID,
                        device.LastChangedBy
                    });

            return returnResult;
        }
        #endregion


    }
}
