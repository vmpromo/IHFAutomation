// Name: AlertDetails.cs
// Type: Code Behind Class for AlertDetails.
// Description: Include local function and page 
//              events
//
//$Revision:   1.4  $
//
// Version   Date        Author    Reason
//  1.0      07/11/11    MSalman   Initial Released

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects.Dashboard;
using IHF.BusinessLayer.BusinessClasses.Dashboard;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class AlertDetails : System.Web.UI.Page
    {


        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();
        private bool hidecolumns = false;


        protected void Page_Load(object sender, EventArgs e)
        {

            string errorType = string.Empty;
            string onlyNew = string.Empty;


            if (!Page.IsPostBack)
            {


                if (!string.IsNullOrEmpty(Request.QueryString["et"].ToString()))
                {


                    errorType = Request.QueryString["et"].ToString();
                    onlyNew = Request.QueryString["onlynew"].ToString();
                    hidecolumns = (onlyNew == "true") ? true : false;

                    if (!string.IsNullOrEmpty(errorType))
                        LoadAlertDetail(errorType, (onlyNew=="true")? "F" : string.Empty );

                }

            }

        }

        protected void grdOrder_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["GridData"] != null)
            {
                this.grdAlertDetails.DataSource = (List<Alerts>)ViewState["GridData"];

            }
        }


        private void LoadAlertDetail(string alertType, string onlynew)
        {
            
            List<Alerts> lstAlerts = _dashboardRp.GetAlertDetail(alertType, onlynew);

            if (lstAlerts.Count > 0) ViewState["GridData"] = lstAlerts;

            this.grdAlertDetails.DataSource = lstAlerts;

            
            this.grdAlertDetails.ClientSettings.Selecting.AllowRowSelect=true;

            this.grdAlertDetails.AllowMultiRowSelection = true;

            if (hidecolumns)
            {
                (this.grdAlertDetails.Columns.FindByUniqueName("AcknowledgedBy")).Visible = false;
                (this.grdAlertDetails.Columns.FindByUniqueName("AcknowledgedTime")).Visible = false;
            }


            this.grdAlertDetails.DataBind();
        }


        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in grdAlertDetails.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }



        protected void btnAcknowledge_Click(object sender, EventArgs e)
        {
            GridItemCollection gridRows = grdAlertDetails.Items;
            DashboardReportsDAO dashdao = new DashboardReportsDAO();
            string errorType = string.Empty;
            string onlyNew = string.Empty;

            foreach (GridDataItem dataItem in gridRows)
            {
                Int32 logID = int.Parse(dataItem.GetDataKeyValue("logid").ToString());

                if (dataItem.Selected)
                {
                    string userid = User.Identity.Name;
                    dashdao.AcknowledgeAlert(logID, userid);
                    
                }


            }
            if (!string.IsNullOrEmpty(Request.QueryString["et"].ToString()))
            {


                errorType = Request.QueryString["et"].ToString();

                onlyNew = Request.QueryString["onlynew"].ToString();
                hidecolumns = (onlyNew == "true") ? true : false;


                if (!string.IsNullOrEmpty(errorType))
                    LoadAlertDetail(errorType, (onlyNew == "true") ? "F" : string.Empty);

            }

        }

        protected void grdAlertDetails_ItemEvent(object sender, GridItemEventArgs e)
        {
            
        }

        protected void grdAlertDetails_UpdateCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdAlertDetails_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.PreRender += new EventHandler(grdAlertDetails_ItemPreRender);
            }

        }

        private void grdAlertDetails_ItemPreRender(object sender, EventArgs e)
        {
            ((sender as GridDataItem)["CheckBoxTemplateColumn"].FindControl("CheckBox1") as CheckBox).Checked = (sender as GridDataItem).Selected;
        }

        protected void grdAlertDetails_PreRender(object sender, EventArgs e)
        {
            GridHeaderItem headerItem = grdAlertDetails.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = grdAlertDetails.SelectedItems.Count == grdAlertDetails.Items.Count;

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grdAlertDetails_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (hidecolumns)
            {
                if (e.Column.UniqueName.ToUpper() == "ACKNOWLEDGEDBY" || e.Column.UniqueName == "ACKNOWLEDGEDTIME")
                {
                    e.Column.Visible = false;
                }
            }

        }





    }
}