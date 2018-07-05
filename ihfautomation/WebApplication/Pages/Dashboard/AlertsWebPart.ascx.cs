// Name: AlertsWebPart.cs
// Type: Code Behind Class for AlertsWebPart.
// Description: Include local function and page 
//              events
//
//$Revision:   1.4  $
//
// Version   Date        Author    Reason
//  1.0      07/11/11    MSalman   Initial Released

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects.Dashboard;
using IHF.BusinessLayer.BusinessClasses.Dashboard;


namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class AlertsWebPart : System.Web.UI.UserControl
    {


        #region LocalMembers

        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();

        private int PAGESIZE = 5;

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }

        PagedDataSource pgitems;

        #endregion



        protected void lnkNext_Click(object sender, EventArgs e)
        {

            this.PageNumber += 1;
            LoadErrorAlters();

            if (this.PageNumber < pgitems.PageCount)
                lnkPrev.Enabled = true;
            else
                lnkNext.Enabled = false;

        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            this.PageNumber -= 1;
            LoadErrorAlters();

            if (this.PageNumber > 0)
                lnkNext.Enabled = true;
            else
                lnkPrev.Enabled = false;


        }





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                LoadErrorAlters();
            }

        }

        private void LoadErrorAlters()
        {


            pgitems = new PagedDataSource();

            List<Alerts> lst = _dashboardRp.GetErrorAlerts();

            pgitems.DataSource = lst;

            pgitems.AllowPaging = true;

            pgitems.PageSize = PAGESIZE;

            pgitems.CurrentPageIndex = PageNumber;

            if (pgitems.CurrentPageIndex < pgitems.PageCount - 1)
                this.lnkNext.Enabled = true;
            else
                this.lnkNext.Enabled = false;

            this.lblCPage.Text = (this.PageNumber + 1).ToString() + " of " + pgitems.PageCount;



            rptAlertView.DataSource = pgitems;
            rptAlertView.DataBind();



        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {

            LoadErrorAlters();

        }


        protected void rptAlterView_OndataBinding(object sender, EventArgs e)
        {
            this.lblLastRefreshedAlerts.Text = DateTime.Now.ToString();
        }


        protected void rptAlertView_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void rptAlertView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Alerts alert = (Alerts)e.Item.DataItem;
                // get the associated object
                Object item = e.Item.DataItem;
                if (item == null)
                    return;

                PlaceHolder pnlNewErrors = (PlaceHolder)e.Item.FindControl("PhNewErrors");
                PlaceHolder pnlNoErrors = (PlaceHolder)e.Item.FindControl("PhNoErrors");

                if (int.Parse(alert.NewErrors) > 0)
                {
                    pnlNewErrors.Visible = true;
                    pnlNoErrors.Visible = false;
                }
                else
                {
                    pnlNewErrors.Visible = false;
                    pnlNoErrors.Visible = true;
                }
            }
        }

    }



}