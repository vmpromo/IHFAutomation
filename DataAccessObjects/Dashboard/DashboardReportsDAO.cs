//
// Name: DashboardReportsDAO.cs
// Type: class object.
// Description: Exception classes to handle errors.
//$Revision:   1.8  $
//
// Version   Date        Author    Reason
//  1.0      07/11/11    MSalman   Initial Release
 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Net;
using System.Configuration;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using System.Data;
using Oracle.DataAccess.Client;

namespace IHF.BusinessLayer.DataAccessObjects.Dashboard
{
    public class DashboardReportsDAO
    {

        #region "private constants"

        private const string ddsergrp = "oms_dashboard_report.f_service_groups";
        private const string ddsertyp = "oms_dashboard_report.f_service_type_grp_list";
        private const string ackalert = "oms_dashboard_report.p_acknowledge_alert";
        private const string _getlinkedloads = "OMS_DASHBOARD_REPORT.F_LINKED_LOAD_LIST";
        private const string _getLoads = "OMS_DASHBOARD_REPORT.F_LOAD_LIST";
        private const string _getLoadOverview = "OMS_DASHBOARD_REPORT.F_LOAD_MANAGEMENT";


        #endregion


        #region Private Members

        protected DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        private OperationalOverview _operationalOverview = new OperationalOverview();

        private Alerts _alerts = new Alerts();

        private  LoadReleaseStatistics _loadreleasestatistics = new LoadReleaseStatistics();

        private LoadManagementOverview _loadManagementOverview = new LoadManagementOverview();

        private LoadDetail _loadDetail = new LoadDetail();

        #endregion


        #region PerationalOverview

        public DataSet Get_service_group()
        {

            return _dal.ExecuteDataset(ddsergrp.ToString(), null);


        }

        public DataSet Get_service_type()
        {

            return _dal.ExecuteDataset(ddsertyp.ToString(), null);
        }


        public DataSet GetLinkedReleasedLoads(String loadNumber)
        {
            return _dal.ExecuteDataset(_getlinkedloads, new object[] { loadNumber, "T" });

        }

        public DataSet GetLinkedUnReleasedLoads(String loadNumber)
        {
            return _dal.ExecuteDataset(_getlinkedloads, new object[] { loadNumber, "F" });

        }

        public DataSet GetLoads()
        {
            return _dal.ExecuteDataset(_getLoads, new object[]{});

        }


        public List<OperationalOverview> GetOpertionalViewData(String serviceGroup)
        {
            List<OperationalOverview> _list = ((OperationalOverview)_dal.Get(OperationalOverview.ClassMethods.GetOperationalViewData.ToString()
                                                                        , this._operationalOverview
                                                                        , new object[] { serviceGroup })[0]).OverviewInfo;


            return _list;
        }

        public List<OperationalOverview> GetOpertionalCancellations(String serviceGroup)
        {
            List<OperationalOverview> _list = ((OperationalOverview)_dal.Get(OperationalOverview.ClassMethods.GetOperationalCancellations.ToString()
                                                                        , this._operationalOverview
                                                                        , new object[] { serviceGroup })[0]).OverviewInfo;


            return _list;
        }

        public List<OperationalOverview> GetOpertionalViewData2(String serviceType)
        {
            List<OperationalOverview> _list = new List<OperationalOverview>();

            if (serviceType != string.Empty)
            {
                Int32 srvcTypeGrp = int.Parse(serviceType);
                _list = ((OperationalOverview)_dal.Get(OperationalOverview.ClassMethods.GetOperationalViewData2.ToString()
                                                                            , this._operationalOverview
                                                                            , new object[] { srvcTypeGrp })[0]).OverviewInfo;
            }
            else
            {
                _list = ((OperationalOverview)_dal.Get(OperationalOverview.ClassMethods.GetOperationalViewData2.ToString()
                                                                            , this._operationalOverview
                                                                            , new object[] {})[0]).OverviewInfo;
            }


            return _list;
        }

        public List<OperationalOverview> GetOpertionalReturns(String serviceType)
        {
            List<OperationalOverview> _list = ((OperationalOverview)_dal.Get(OperationalOverview.ClassMethods.GetOperationalReturns.ToString()
                                                                        , this._operationalOverview
                                                                        , new object[] { serviceType })[0]).OverviewInfo;


            return _list;
        }

        public List<LoadManagementOverview> GetLoadOverview(String loadNumber, int ? loadstatus)
        {
            List<LoadManagementOverview> _list = ((LoadManagementOverview)_dal.Get(LoadManagementOverview.ClassMethods.GetLoadManagementOverview.ToString(),
                                                                                   this._loadManagementOverview,
                                                                                   new object[] { loadNumber, loadstatus })[0]).OverviewInfo;

            return _list;

        }

        public List<LoadDetail> GetLoadDetail(String loadNumber, Int32? itemStatusCode, String multiInd)
        {
            List<LoadDetail> _list = ((LoadDetail)_dal.Get(LoadDetail.ClassMethods.GetLoadDetail.ToString(),
                                                                                   this._loadDetail,
                                                                                   new object[] { loadNumber, itemStatusCode, multiInd })[0]).OverviewInfo;

            return _list;

        }

        #endregion

        #region Alerts

        public List<Alerts> GetErrorAlerts()
        {

            List<Alerts> _list = ((Alerts)_dal.Get(Alerts.ClassMethods.GetLatestAlerts.ToString()
                                                                     , this._alerts
                                                                     , new object[] { })[0]).AlertsInfo;


            return _list;

        }

        #endregion


        #region AlertDetails


        public List<Alerts> GetAlertDetail(string alertType, string acknowledgedInd)
        {
            List<Alerts> _list = new List<Alerts>();

            if (acknowledgedInd == string.Empty)
            {

                _list = ((Alerts)_dal.Get(Alerts.ClassMethods.GetAlertDetails.ToString()
                                                                             , this._alerts
                                                                             , new object[] { alertType })[0]).AlertsInfo;
            }
            else
            {
                _list = ((Alerts)_dal.Get(Alerts.ClassMethods.GetAlertDetails.ToString()
                                                             , this._alerts
                                                             , new object[] { alertType, acknowledgedInd })[0]).AlertsInfo;
            }


            return _list;
        
        }

        public  void AcknowledgeAlert(Int32 I_alert_id, string I_userid)
        {

            Object[] updParams = new Object[] { I_alert_id, I_userid };

            _dal.ExecuteNonQuery (ackalert, updParams);
        }

        public List<LoadReleaseStatistics> GetLoads(string I_pick_load_num)
        {
            List<LoadReleaseStatistics> _list = new List<LoadReleaseStatistics>();

            if (I_pick_load_num == string.Empty)
            {
                _list = ((LoadReleaseStatistics)_dal.Get(LoadReleaseStatistics.ClassMethods.GetLoads.ToString(), this._loadreleasestatistics, new object[] { })[0]).LoadStatistics;
            }
            else
            {
                _list = ((LoadReleaseStatistics)_dal.Get(LoadReleaseStatistics.ClassMethods.GetLoads.ToString(), this._loadreleasestatistics, new object[] {I_pick_load_num })[0]).LoadStatistics;
            }

            return _list;
        }


        #endregion

 

    }
}
