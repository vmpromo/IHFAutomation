using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IHF.BusinessLayer.DataAccessObjects.Dashboard;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class LoadDetail : System.Web.UI.Page
    {
        #region LocalMembers


        private DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();
        private string _loadNumber ="";
        private string _loadStatus = "";
        private int? _itemStatusCode;
        private string _itemStatus;
        private string _multiInd = "";

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            _loadNumber = Request.QueryString["LoadNumber"].ToString();
            _loadStatus = Request.QueryString["LoadStatus"].ToString();
            if (Request.QueryString["ItemStatusCode"] != null)
            {
                if (Request.QueryString["ItemStatusCode"].ToString() != string.Empty)
                {
                    _itemStatusCode = Convert.ToInt32(Request.QueryString["ItemStatusCode"].ToString());
                }
            }
            if (Request.QueryString["ItemStatus"] != null)
                _itemStatus = Request.QueryString["ItemStatus"].ToString();
            if (Request.QueryString["MultiInd"] != null)
                _multiInd = Request.QueryString["MultiInd"].ToString();

            SetupHeader();

            populateLoadDetail(_loadNumber, _itemStatusCode, _multiInd);
        }

        public void populateLoadDetail(string loadNumber, int? itemStatusCode, string multiInd)
        {
            List<IHF.BusinessLayer.BusinessClasses.Dashboard.LoadDetail> loaddetail = _dashboardRp.GetLoadDetail(loadNumber, itemStatusCode, multiInd);
            this.RadGrid3.DataSource = loaddetail;
            this.RadGrid3.DataBind();
        }

        private void SetupHeader()
        {
            this.lbLoadNumber.Text = _loadNumber.ToString();
            this.lbLoadStatus.Text = _loadStatus.ToUpper();
            this.lbItemStatus.Text = _itemStatus != null ? _itemStatus.ToUpper() : "ALL";
            this.lbOrderType.Text = _multiInd != string.Empty ? (_multiInd == "T" ? "MULTI" : "SINGLES") : "ALL";
            
        }

        protected void RadGrid3_ItemCreated(object sender, GridItemEventArgs e)
        {

        }

        protected void RadGrid3_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void RadGrid3_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {

        }

        protected void RadGrid3_PreRender(object sender, EventArgs e)
        {
            foreach (GridColumn column in RadGrid3.Columns)
            {
                if (column.UniqueName == "CarrierServiceGroup" || column.UniqueName == "ServiceGroupDescr")
                {
                    if (_itemStatusCode == null ||
                        _itemStatusCode == 150 ||
                        _itemStatusCode == 170 ||
                        _itemStatusCode == 180 ||
                        _itemStatusCode == 190)
                    {
                        (column as GridBoundColumn).Visible = false;
                    }                    
                }
                else if (column.UniqueName == "ServiceTypeDescr")
                {
                    if (_itemStatusCode == null ||
                        _itemStatusCode == 40 ||
                        _itemStatusCode == 50 ||
                        _itemStatusCode == 90 ||
                        _itemStatusCode == 110 ||
                        _itemStatusCode == 120 ||
                        _itemStatusCode == 130)
                    {
                        (column as GridBoundColumn).Visible = false;
                    }
                }

            }
            RadGrid3.Rebind();

        }

    }
}