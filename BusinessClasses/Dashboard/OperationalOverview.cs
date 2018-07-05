
//
// Name: OperationalOverview.cs
// Type: Entity Class 
// Description: Business Entity class of a type OperationalOverview.
//
//$Revision:   1.6  $
//
// Version   Date        Author    Reason
//  1.0      25/10/11    MSalman   Initial Released


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses.Dashboard
{
    public class OperationalOverview : IDataService
    {


        #region private member


        private const string GET_OPERTATIONAL_OVERVIEW = "OMS_DASHBOARD_REPORT.F_COUNTSBYSERVICEGROUP";
        private const string GET_OPERATIONAL_OVERVIEW2 = "OMS_DASHBOARD_REPORT.F_COUNTSBYSERVICETYPEGRP";
        private const string GET_CANCELLATIONS = "OMS_DASHBOARD_REPORT.F_CANCELLATIONCOUNTS";
        private const string GET_RETURNS = "OMS_DASHBOARD_REPORT.F_RETURNSCOUNTS";

        private List<OperationalOverview> lstOpView = new List<OperationalOverview>();



        #endregion

        #region Function Mapping


        public enum ClassMethods
        {

            GetOperationalViewData,
            GetOperationalViewData2,
            GetOperationalCancellations,
            GetOperationalReturns

        }


        #endregion

        #region Properties

        public List<OperationalOverview> OverviewInfo
        {

            get
            {

                return lstOpView;
            }

            set
            {

                lstOpView = value;
            }


        }

       

        public string StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string LoadNumber { get; set; }

        public string MultiOrders { get; set; }

        public string MultiOrderItems { get; set; }

        public string SingleOrders { get; set; }

        public string SingleOrderItems { get; set; }

        public string TotalOrders { get; set; }

        public string TotalOrderItems { get; set; }

        public DateTime EarliestOrderDateTime { get; set; }
        #endregion

        #region Local Functions


        [MethodMapper("GetOperationalViewData", OperationalOverview.GET_OPERTATIONAL_OVERVIEW)]
        public IList<IDataService> GetOpertionalOverview(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<OperationalOverview> items = new List<OperationalOverview>();

            while (reader.Read())
            {

                OperationalOverview obj = new OperationalOverview();

                obj.StatusCode = reader["STATUS_CODE"].ToString() ?? string.Empty;
                obj.StatusDescription = reader["STATUSDESC"].ToString() ?? string.Empty;
                obj.LoadNumber = reader["PICKLOADCOUNT"].ToString() ?? string.Empty;
                obj.MultiOrders = reader["MULTIORDERS"].ToString() ?? string.Empty;
                obj.MultiOrderItems = reader["MULTITEMS"].ToString() ?? string.Empty;
                obj.SingleOrders = reader["SINGLEORDERS"].ToString() ?? string.Empty;
                obj.TotalOrders = reader["TOTALORDERS"].ToString() ?? string.Empty;
                obj.EarliestOrderDateTime = Convert.ToDateTime(reader["MINORDERDATE"].ToString() ?? DateTime.MinValue.ToString());


                items.Add(obj);
            }


            this.OverviewInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        [MethodMapper("GetOperationalCancellations", OperationalOverview.GET_CANCELLATIONS)]
        public IList<IDataService> GetOpertionalCancellations(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<OperationalOverview> items = new List<OperationalOverview>();

            while (reader.Read())
            {

                OperationalOverview obj = new OperationalOverview();

                //obj.StatusCode = reader["STATUS_CODE"].ToString() ?? string.Empty;
                //obj.StatusDescription = reader["STATUS"].ToString() ?? string.Empty;
                //obj.LoadNumber = reader["LOAD"].ToString() ?? string.Empty;
                obj.MultiOrders = reader["TOTMULTIS"].ToString() ?? string.Empty;
                obj.MultiOrderItems = reader["TOTMULTIITEMS"].ToString() ?? string.Empty;
                obj.SingleOrders = reader["TOTSINGLES"].ToString() ?? string.Empty;
                obj.TotalOrders = reader["TOTCANCELLATIONS"].ToString() ?? string.Empty;
                //obj.EarliestOrderDateTime = Convert.ToDateTime(reader["EARLIESTDATE"].ToString() ?? DateTime.MinValue.ToString());


                items.Add(obj);
            }


            this.OverviewInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }

        [MethodMapper("GetOperationalReturns", OperationalOverview.GET_RETURNS)]
        public IList<IDataService> GetOperationalReturns(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<OperationalOverview> items = new List<OperationalOverview>();

            while (reader.Read())
            {

                OperationalOverview obj = new OperationalOverview();

                //obj.StatusCode = reader["STATUS_CODE"].ToString() ?? string.Empty;
                //obj.StatusDescription = reader["STATUS"].ToString() ?? string.Empty;
                //obj.LoadNumber = reader["LOAD"].ToString() ?? string.Empty;
                obj.MultiOrders = reader["TOTMULTIS"].ToString() ?? string.Empty;
                obj.MultiOrderItems = reader["TOTMULTIITEMS"].ToString() ?? string.Empty;
                obj.SingleOrders = reader["TOTSINGLES"].ToString() ?? string.Empty;
                obj.TotalOrders = reader["TOTALORDERS"].ToString() ?? string.Empty;
                //bj.EarliestOrderDateTime = Convert.ToDateTime(reader["EARLIESTDATE"].ToString() ?? DateTime.MinValue.ToString());


                items.Add(obj);
            }


            this.OverviewInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }

        [MethodMapper("GetOperationalViewData2", OperationalOverview.GET_OPERATIONAL_OVERVIEW2)]
        public IList<IDataService> GetOpertionalOverviewData2(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<OperationalOverview> items = new List<OperationalOverview>();

            while (reader.Read())
            {

                OperationalOverview obj = new OperationalOverview();

                obj.StatusCode = reader["STATUS_CODE"].ToString() ?? string.Empty;
                obj.StatusDescription = reader["STATUSDESC"].ToString() ?? string.Empty;
                obj.LoadNumber = reader["PICKLOADCOUNT"].ToString() ?? string.Empty;
                obj.MultiOrders = reader["MULTIORDERS"].ToString() ?? string.Empty;
                obj.MultiOrderItems = reader["MULTITEMS"].ToString() ?? string.Empty;
                obj.SingleOrders = reader["SINGLEORDERS"].ToString() ?? string.Empty;
                obj.TotalOrders = reader["TOTALORDERS"].ToString() ?? string.Empty;
                obj.EarliestOrderDateTime = Convert.ToDateTime(reader["MINORDERDATE"].ToString() ?? DateTime.MinValue.ToString());


                items.Add(obj);
            }


            this.OverviewInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        #endregion


    }
}
