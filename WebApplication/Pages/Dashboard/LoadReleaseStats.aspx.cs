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
    public partial class LoadReleaseStats : System.Web.UI.Page
    {

        #region LocalMembers


        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();

        LoadReleaseStatistics _stats = new LoadReleaseStatistics();

        string theLoad = String.Empty;


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridToDataSource();

            if (!Page.IsPostBack)
            {
                populateLoadList();
            }
        }

        protected void RadGrid3_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void BindGridToDataSource()
        {
            string loadNumber = string.Empty;
            
            if (!(this.RadComboLoad.Text).ToUpper().Contains("ALL")) loadNumber = RadComboLoad.Text;

            List<LoadReleaseStatistics> stats = _dashboardRp.GetLoads(loadNumber);

            this.RadGrid3.DataSource = stats;
            this.RadGrid3.DataBind();
        }


        protected void populateLoadList()
        {
            DataSet dsLoads = _dashboardRp.GetLoads();

            this.RadComboLoad.DataSource = dsLoads.Tables[0];
            this.RadComboLoad.DataTextField = "pick_Load_num";
            this.RadComboLoad.DataValueField = "pick_load_num";
            this.RadComboLoad.DataBind();
        }

        protected void RadGrid3_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string loadnumber = dataItem.GetDataKeyValue("LoadNumber").ToString();

                ListBox ddlreleased = (ListBox)((GridDataItem)e.Item)["LinkedReleased"].FindControl("lbReleased");
                ListBox ddlunreleased = (ListBox)((GridDataItem)e.Item)["LinkedReleased"].FindControl("lbUnReleased");

                DashboardReportsDAO dao = new DashboardReportsDAO();

                DataSet dsLinkedReleased = dao.GetLinkedReleasedLoads(loadnumber);
                DataSet dsLinkedUnReleased = dao.GetLinkedUnReleasedLoads(loadnumber);

                if (dsLinkedReleased.Tables[0].Rows.Count == 0)
                {
                    ddlreleased.Visible = false;
                }

                if (dsLinkedUnReleased.Tables[0].Rows.Count == 0)
                {
                    ddlunreleased.Visible = false;
                }


                ddlreleased.DataSource = dsLinkedReleased.Tables[0];
                ddlreleased.DataTextField = "pick_load_num";

                ddlunreleased.DataSource = dsLinkedUnReleased.Tables[0];
                ddlunreleased.DataTextField = "pick_load_num";



            }
        }

        protected void RadBtnGo_Click(object sender, EventArgs e)
        {
            theLoad = RadComboLoad.Text;
        }

    }
}