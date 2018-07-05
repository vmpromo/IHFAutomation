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
    public partial class LoadOverview : System.Web.UI.Page
    {

        #region LocalMembers


        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();

        LoadManagementOverview _stats = new LoadManagementOverview();

        string theLoad = String.Empty;

        Int32 _totreadyreleasemultis;
        Int32 _totreadyreleasesingles;
        Int32 _totreleasedmultis;
        Int32 _totreleasedsingles;
        Int32 _totsortedmultis;
        Int32 _totsortedsingles;
        Int32 _totlocatedmultis;
        Int32 _totlocatedsingles;
        Int32 _totreadypackmultis;
        Int32 _totreadypacksingles;
        Int32 _totpackingmultis;
        Int32 _totpackingsingles;
        Int32 _totpackedmultis;
        Int32 _totpackedsingles;
        Int32 _totcagedmultis;
        Int32 _totcagedsingles;
        Int32 _totreadydespatchmultis;
        Int32 _totreadydespatchsingles;
        Int32 _totdespatchedmultis;
        Int32 _totdespatchedsingles;


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populateLoadList();
            }

            BindGridToDataSource();
        }

        protected void BindGridToDataSource()
        {
            string loadNumber = string.Empty;
            int? loadStatus = null;

            if (!(this.RadComboLoad.Text).ToUpper().Contains("ALL")) loadNumber = RadComboLoad.Text;
            if (this.rcbLoadStatus.SelectedValue != string.Empty) loadStatus = int.Parse(rcbLoadStatus.SelectedValue);


            List<LoadManagementOverview> itemcounts = _dashboardRp.GetLoadOverview(loadNumber, loadStatus);
            this.rgLoadOverview.DataSource = itemcounts;
            this.rgLoadOverview.DataBind();
        }

        protected void populateLoadList()
        {
            DataSet dsLoads = _dashboardRp.GetLoads();

            this.RadComboLoad.DataSource = dsLoads.Tables[0];
            this.RadComboLoad.DataTextField = "pick_Load_num";
            this.RadComboLoad.DataValueField = "pick_load_num";
            this.RadComboLoad.DataBind();
        }

        protected void rgLoadOverview_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void RadBtnGo_Click(object sender, EventArgs e)
        {

        }

        protected void RadComboLoad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void rgLoadOverview_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                try
                {
                    _totreadyreleasesingles = Int32.Parse(item["TotReadyReleaseSingles"].Text);
                }
                catch {}
                try
                {
                    _totreadyreleasemultis = Int32.Parse(item["TotReadyReleaseMultis"].Text);
                }
                catch { }
                try
                {
                    _totreleasedsingles = Int32.Parse(item["TotReleasedSingles"].Text);
                }
                catch { }
                try
                {
                    _totreleasedmultis = Int32.Parse(item["TotReleasedMultis"].Text);
                }
                catch { }
                try
                {
                    _totsortedsingles = Int32.Parse(item["TotSortedSingles"].Text);
                }
                catch { }
                try
                {
                    _totsortedmultis = Int32.Parse(item["TotSortedMultis"].Text);
                }
                catch { }
                try
                {
                    _totlocatedsingles = Int32.Parse(item["TotLocatedSingles"].Text);
                }
                catch { }
                try
                {
                    _totlocatedmultis = Int32.Parse(item["TotLocatedMultis"].Text);
                }
                catch { }
                try
                {
                    _totreadypacksingles = Int32.Parse(item["TotReadyPackSingles"].Text);
                }
                catch { }
                try
                {
                    _totreadypackmultis = Int32.Parse(item["TotReadyPackMultis"].Text);
                }
                catch { }
                try
                {
                    _totpackingsingles = Int32.Parse(item["TotPackingSingles"].Text);
                }
                catch { }
                try
                {
                    _totpackingmultis = Int32.Parse(item["TotPackingMultis"].Text);
                }
                catch { }
                try
                {
                    _totpackedsingles = Int32.Parse(item["TotPackedSingles"].Text);
                }
                catch { }
                try
                {
                    _totpackedmultis = Int32.Parse(item["TotPackedMultis"].Text);
                }
                catch { }
                try
                {
                    _totcagedsingles = Int32.Parse(item["TotCagedSingles"].Text);
                }
                catch { }
                try
                {
                    _totcagedmultis = Int32.Parse(item["TotCagedMultis"].Text);
                }
                catch { }
                try
                {
                    _totreadydespatchsingles = Int32.Parse(item["TotReadyDespatchSingles"].Text);
                }
                catch { }
                try
                {
                    _totreadydespatchmultis = Int32.Parse(item["TotReadyDespatchMultis"].Text);
                }
                catch { }
                try
                {
                    _totreadydespatchsingles = Int32.Parse(item["TotReadyDespatchSingles"].Text);
                }
                catch { }
                try
                {
                    _totdespatchedsingles = Int32.Parse(item["TotDespatchedSingles"].Text);
                }
                catch { }
                try
                {
                    _totdespatchedmultis = Int32.Parse(item["TotDespatchedMultis"].Text);
                }
                catch { }


                //Label lbl = (Label)item["totreadyreleasemulti"].FindControl("totreadyreleasemultiLabel");
                //string strtxt = lbl.Text.ToString();


            }

            if (e.Item is GridFooterItem)
            {
                GridFooterItem footerItem = (GridFooterItem)e.Item;
                Label lbl = (Label)footerItem["ReadyReleaseItems"].FindControl("totreadyreleasemultislabel");
                lbl.Text = _totreadyreleasemultis.ToString();

                lbl = (Label)footerItem["ReadyReleaseItems"].FindControl("totreadyreleasesingleslabel");
                lbl.Text = _totreadyreleasesingles.ToString();

                lbl = (Label)footerItem["ReleasedItems"].FindControl("totreleasedmultislabel");
                lbl.Text = _totreleasedmultis.ToString();

                lbl = (Label)footerItem["ReleasedItems"].FindControl("totreleasedsingleslabel");
                lbl.Text = _totreleasedsingles.ToString();

                lbl = (Label)footerItem["SortedItems"].FindControl("totsortedmultislabel");
                lbl.Text = _totsortedmultis.ToString();

                lbl = (Label)footerItem["SortedItems"].FindControl("totsortedsingleslabel");
                lbl.Text = _totsortedsingles.ToString();

                lbl = (Label)footerItem["LocatedItems"].FindControl("totlocatedmultislabel");
                lbl.Text = _totlocatedmultis.ToString();

                lbl = (Label)footerItem["LocatedItems"].FindControl("totlocatedsingleslabel");
                lbl.Text = _totlocatedsingles.ToString();

                lbl = (Label)footerItem["PackingItems"].FindControl("totpackingsingleslabel");
                lbl.Text = _totpackingsingles.ToString();

                lbl = (Label)footerItem["PackingItems"].FindControl("totpackingmultislabel");
                lbl.Text = _totpackingmultis.ToString();

                lbl = (Label)footerItem["PackedItems"].FindControl("totpackedmultislabel");
                lbl.Text = _totpackedmultis.ToString();

                lbl = (Label)footerItem["PackedItems"].FindControl("totpackedsingleslabel");
                lbl.Text = _totpackedsingles.ToString();

                lbl = (Label)footerItem["CagedItems"].FindControl("totcagedmultislabel");
                lbl.Text = _totcagedmultis.ToString();

                lbl = (Label)footerItem["CagedItems"].FindControl("totcagedsingleslabel");
                lbl.Text = _totreadydespatchsingles.ToString();

                lbl = (Label)footerItem["ReadyDespatchItems"].FindControl("totreadydespatchmultislabel");
                lbl.Text = _totreadydespatchmultis.ToString();

                lbl = (Label)footerItem["ReadyDespatchItems"].FindControl("totreadydespatchsingleslabel");
                lbl.Text = _totcagedsingles.ToString();

                lbl = (Label)footerItem["DespatchedItems"].FindControl("totdespatchedmultislabel");
                lbl.Text = _totdespatchedmultis.ToString();

                lbl = (Label)footerItem["DespatchedItems"].FindControl("totdespatchedsingleslabel");
                lbl.Text = _totdespatchedsingles.ToString();
            
            }
        }
    }
}