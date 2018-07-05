using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class HandheldDAO:DeviceDAO
    {
        #region "overriden functions"
        public override decimal Add(Device device)
        {
            int returnResult =
                (int)this._dataManager.ExecuteReturnMethod(
                    ADD,
                    new object[] { 
                    device.ID,
                    device.Type,
                    null,
                    device.SerialNumber,
                    device.DeviceName,
                    device.CreatedBy,
                    device.CurrentUser});

            return returnResult;
        }

        public override int Modify(Device device)
        {
            int returnResult =
                (int)_dataManager.ExecuteReturnMethod(
                    MODIFY,
                    new object[]{
                        device.ID,
                        device.Type,
                        null,
                        device.SerialNumber,
                        device.DeviceName,
                        device.CurrentUser,
                        device.LastChangedBy
                    });

            return returnResult;
        }
        #endregion
    }
}
