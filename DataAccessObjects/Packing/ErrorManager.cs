//
// Name: ErrorManager.cs
// Type: class object.
// Description: Exception Manager.
//$Revision:   1.3$
//
// Version   Date        Author    Reason
//  1.0      28/07/11    MSalman   Initial Release
//  1.1      12/08/11    MSalman   New Error Type added       
//  1.2      07/10/11    MSalman   New Error Type added    
//  1.3      13/02/12    M Khan    Added code for inserting errors in to activity log table
//  1.4      24/09/12    J Watt    Logging order number and consignment codes on failure.
//  1.8      08/10/12    J Watt    Change for error logging.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.Util;
using System.Data;

namespace IHF.BusinessLayer.DataAccessObjects.Packing
{
    public sealed class ErrorManager
    {
        private static DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        public static void ThrowMetaPackFailure(string message)
        {
            throw new MetaPackFailure(message);
        }

        public static void ThrowFailToCreateAndAllocateConsignment(string message)
        {
            throw new ConsignmentAllocationFailure(message);
        }

        public static void ThrowFailToGetTheLabel(string message)
        {

            throw new LabelReceivedFailure(message);
        }

        public static void ThrowFailToGetTheDocument(string message)
        {

            throw new DocumentReceivedFailure(message);
        }


        public static void ThrowCancelAllocationFailure(string message)
        {

            throw new CancelAllocationFailure(message);
        }


        public static void ThrowSaveDocumentFailure(string message)
        {
            throw new SaveDocumentFailure(message);

        }


        public static void ThrowSaveLabelFailure(string message)
        {
            throw new SaveLabelFailure(message);

        }

        public static void ThrowFailToPrint(string message)
        {
            throw new PrintDocumentFailure(message);
        }


        public static string GetExceptionMessage(Exception ex)
        {
            string message = ex.Message;

            if (ex != null)
            {
                Exception inner = ex;
                if (inner is ConsignmentAllocationFailure)
                    message = "MetaPack Allocation Failure. ";
                else if (inner is MetaPackFailure)
                    message = "MetaPack Failure. ";
                //message = "Meta Pack fail to create the consignment. " + inner.Message;
                else if (inner is LabelReceivedFailure)
                    message = "Meta Pack fail to create Label(s).";
                else if (inner is DocumentReceivedFailure)
                    message = "Meta Pack fail to create document(s).";
                else if (inner is CancelAllocationFailure)
                    message = "Meta Pack fail to cancel the Consignment Allocation.";
                else if (inner is PrintDocumentFailure)
                    message = "Fail to print document(s)";
                else if (inner is SaveLabelFailure)
                    message = "Fail to  save the label(s)";
                else
                    message = inner.Message;

            }

            return message;
        }

        public static string GetMessageAndLogException(Exception ex, UserActivity userActivity)
        {
            try
            {
                // Code for logging the Exception. In the Database.
                ErrorDetails errorDetails = ErrorAndMessage(ex.Message);

                ActivityLogDAO activityLog = new ActivityLogDAO();
                userActivity = SetUserActivityObject(errorDetails, userActivity);
                userActivity.TerminalId = Shared.UserHostName;
                userActivity.UserId = Shared.CurrentUser;
                activityLog.SaveUserActivity(userActivity);
            }
            catch (Exception e)
            {
            }

            return GetExceptionMessage(ex);
        }

        private static UserActivity SetUserActivityObject(ErrorDetails errorDetails, UserActivity activity)
        {
            activity.AppSystem = (int)ActivityLogEnum.AppSystem.IHF;
            activity.ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack;
            
            //check if any error code returned by Metapack
            GetEventTypeForMetapack(ref activity, errorDetails);
            
            return activity;
        }

        private static void GetEventTypeForMetapack(ref UserActivity activity, ErrorDetails errorDetails)
        {
            if (errorDetails.ErrorCode.Contains(EventType.E10.ToString()))
                activity.EventType = (int)EventType.E10;
            else if (errorDetails.ErrorCode.Contains(EventType.E20.ToString()))
                activity.EventType = (int)EventType.E20;
            else if (errorDetails.ErrorCode.Contains(EventType.E30.ToString()))
                activity.EventType = (int)EventType.E30;
            else if (errorDetails.ErrorCode.Contains(EventType.E40.ToString()))
                activity.EventType = (int)EventType.E40;
            else
                //if no metapack errors then check for network errors
                CheckForNetworkErrors(ref activity, errorDetails.ErrorMessage);

            if (activity.Value2 == "" || activity.Value2 == string.Empty || activity.Value2 == null)
            {
                int messageLength = errorDetails.ErrorMessage.Length > 200 ? 200 : errorDetails.ErrorMessage.Length;
                activity.Value2 = errorDetails.ErrorMessage.Substring(0,messageLength);
            }
            activity.Value1 = errorDetails.ErrorCode;
        }

        private static void CheckForNetworkErrors(ref UserActivity activity, string errorMessage)
        {
            activity.EventType = (int)EventType.NetworkOrServiceDown;
            activity.ReasonCode = null;

            IDataReader dr = _dal.ExecuteReader("oms_common.f_reasons", new object[] { (int)EventType.NetworkOrServiceDown });

            string errorDescription = string.Empty;
            while (dr.Read())
            {
                errorDescription = dr[3].ToString().ToLower();

                if (errorMessage.ToLower().Contains(errorDescription))
                {
                    activity.ReasonCode = Convert.ToInt32(dr[1].ToString());
                    activity.Value2     = errorMessage.Substring(errorMessage.ToLower().IndexOf(errorDescription), errorDescription.Length);
                    break;
                }
            }

            if (activity.ReasonCode == null)
            {
                activity.ReasonCode = (int)Enumerations.EventReason.OtherError;
                //data limit of value2 column is 200 characters. 
                //Substring error message to avoid runtime exception.
                int messageLength = errorMessage.Length > 200 ? 200 : errorMessage.Length;
                activity.Value2 = errorMessage.Substring(0, messageLength);
            }
        }

        private static ErrorDetails ErrorAndMessage(string exceptionMessage)
        {
            string[] exceptionDetails;

            exceptionDetails = exceptionMessage.Split(" ".ToCharArray(), 2);

            if (exceptionDetails[0].Length <= 6 && exceptionDetails[0].Substring(0,1) == "E")
            {
                return new ErrorDetails(exceptionDetails[0], exceptionDetails[1]);
            }

            return new ErrorDetails("", exceptionMessage);
        }

        private class ErrorDetails
        {
            public ErrorDetails(string errorCode, string errorMessage)
            {
                this.ErrorCode    = errorCode;
                this.ErrorMessage = errorMessage;
            }

            internal string ErrorCode    { get; set; }

            internal string ErrorMessage { get; set; }
        }
    }

    
}
