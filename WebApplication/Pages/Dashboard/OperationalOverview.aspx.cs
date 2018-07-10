// Name: OperationalView.cs
// Type: Code Behind Class for OperationalView.
// Description: Include local function and page 
//              events
//
//$Revision:   1.5  $
//
// Version   Date        Author    Reason
//  1.0      07/11/11    MSalman   Initial Released

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects.Dashboard;
using IHF.BusinessLayer.BusinessClasses.Dashboard;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class OperationalView : System.Web.UI.Page
    {


        #region LocalMembers


        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();

        //private const string ALERTS_WEB_PART = "AlertsWebPart.ascx";
        private const string SERVICE_GRP_PART = "ServicedGroupCounts.ascx";
        private const string SERVICE_TYP_PART = "ServiceNameCounts.ascx";



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


      

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {

            }


            LoadWebParts();
        }


        private void LoadWebParts()
        {
            //this.pnlAlertsContent.Controls.Add(LoadControl(ALERTS_WEB_PART));
            this.Panel1.Controls.Add(LoadControl(SERVICE_GRP_PART));
            this.Panel2.Controls.Add(LoadControl(SERVICE_TYP_PART));
        }

    }
}
