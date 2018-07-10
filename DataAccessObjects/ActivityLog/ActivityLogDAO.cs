//
// Name: ActivityLogDAO.cs
// Type: ADO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the Activity Log entities
//
//$Revision:   1.1 $
//
// Version   Date        Author    Reason
//  1.0      20/07/11    MSalman   Initial Release
//  1.1      01/08/11    MSalman   Namespaces updated.
//  1.2      15/09/11    MSalman   New Column is added.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using Com.MetaPack.DeliveryManager;
using System.Data;
using Oracle.DataAccess.Client;

namespace IHF.BusinessLayer.DataAccessObjects.ActivityLog
{
    public class ActivityLogDAO
    {
        #region Private Memebers

        protected DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        private const string SAVE_USER_ACTIVITY = "OMS_ACTIVITY_UTIL.P_SAVE_ACTIVITY";


        #endregion


        #region Local Functions

        public void SaveUserActivity(UserActivity act)
        {

            this._dal.ExecuteNonQuery(
                            SAVE_USER_ACTIVITY,
                                new object[] { 
                                              act.AppSystem,
                                              act.ApplicationId,
                                              act.Barcode,
                                              act.CageId == 0?null:act.CageId,
                                              act.ChuteId,
                                              act.ConsignmentId,
                                              act.EventType,
                                              act.ExpectedBarcodeType,
                                              act.FailToteId,
                                              act.ItemNumber,
                                              act.LaneId,
                                              act.ModuleId,
                                              act.OrderNumber,
                                              act.ParcelId,
                                              act.Sku,
                                              act.ReasonCode,
                                              act.ResultCode,
                                              act.SessionId,
                                              act.SortLoadId,
                                              act.TerminalId,
                                              act.TrolleyId,
                                              act.TrolleyLocationId,
                                              act.UserId,
                                              act.WorkStationId,
                                              act.OverflowToteId,
                                              act.PigeonHoleId,
                                              act.InternationalInd,
                                              act.OrderTypeId,
                                              act.TrolleyTypeId,
                                              act.ParcelCode,
                                              act.CarrierId,
                                              act.CarrierServiceId,
                                              act.CageType,
                                              act.DeviceID,
                                              act.ManifestId,
                                              act.Value1,
                                              act.Value2,
                                              act.Value3,
                                              act.Ref1,
                                              act.Ref2,
                                              act.Ref3,
                                              act.SessionStartDateTime,
                                              act.SessionEndDateTime
                                             
                                              });


        }

        #endregion




    }
}
