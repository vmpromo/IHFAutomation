
//
// Name: Alerts.cs
// Type: Entity Class 
// Description: Business Entity class of a type Alerts.
//
//$Revision:   1.4  $
//
// Version   Date        Author    Reason
//  1.0      01/11/11    MSalman   Initial Released
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses.Dashboard
{
    [Serializable]
    public class Alerts : IDataService
    {

        #region private member


        private const string GET_ERROR_ALERTS = "OMS_DASHBOARD_REPORT.F_ALERT";

        private const string GET_ALERT_DETAIL = "OMS_DASHBOARD_REPORT.F_ALERT_DETAIL";


        private List<Alerts> lstAlerts = new List<Alerts>();



        #endregion

        #region Function Mapping


        public enum ClassMethods
        {

            GetLatestAlerts,
            GetAlertDetails

        }


        #endregion

        #region Properties



        public string Priority { get; set; }

        public string ErrorType { get; set; }

        public string NewErrors { get; set; }

        public string ErrorInLastHour { get; set; }

        public string Total { get; set; }

        public string Acknowledged { get; set; }

        public DateTime ErrorTime { get; set; }

        public string ErrorDetail { get; set; }

        public string AcknowledgedBy { get; set; }

        public string AcknowledgedTime { get; set; }


        public List<Alerts> AlertsInfo
        {
            get
            {

                return lstAlerts;
            }

            set
            {
                lstAlerts = value;
            }

        }

        #endregion

        #region LocalFunctions

        [MethodMapper("GetLatestAlerts", Alerts.GET_ERROR_ALERTS)]
        public IList<IDataService> GetLatestAlerts(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<Alerts> items = new List<Alerts>();

            while (reader.Read())
            {

                Alerts obj = new Alerts();

                obj.Priority = reader["PRIORITY"].ToString() ?? string.Empty;
                obj.ErrorType = reader["ERRORTYPE"].ToString() ?? string.Empty;
                obj.NewErrors = reader["NEW"].ToString() ?? string.Empty;
                obj.ErrorInLastHour = reader["LASTHOUR"].ToString() ?? string.Empty;
                obj.Total = reader["TOTAL"].ToString() ?? string.Empty;


                items.Add(obj);
            }


            this.AlertsInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        [MethodMapper("GetAlertDetails", Alerts.GET_ALERT_DETAIL)]
        public IList<IDataService> GetAlertDetails(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<Alerts> items = new List<Alerts>();

            while (reader.Read())
            {

                Alerts obj = new Alerts();

                obj.Acknowledged = reader["ACKNOWLEDGED"].ToString() ?? string.Empty;
                obj.ErrorTime = Convert.ToDateTime(reader["ERRORTIME"].ToString() ?? DateTime.MinValue.ToString());
                obj.Priority = reader["PRIORITY"].ToString() ?? string.Empty;
                obj.ErrorType = reader["ERROR TYPE"].ToString() ?? string.Empty;
                obj.ErrorDetail = reader["ERROR DETAIL"].ToString() ?? string.Empty;
                obj.AcknowledgedBy = reader["ACKNOWLEDGED BY"].ToString() ?? string.Empty;
                obj.AcknowledgedTime = reader["ACKNOWLEDGED TIME"].ToString();

                items.Add(obj);
            }


            this.AlertsInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }





        #endregion







    }
}
