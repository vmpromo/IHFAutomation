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
    public partial class ServicedGroupCounts : System.Web.UI.UserControl
    {
       #region LocalMembers


        DashboardReportsDAO _dashboardRp = new DashboardReportsDAO();

      //  private const string ALERTS_WEB_PART = "AlertsWebPart.ascx";

        int loadNo = 0;
        int multiOrders = 0;
        int multiOrderItems = 0;
        int singleOrders = 0;
        int totalOrders = 0;
        int totalOrderItems = 0;



        #endregion
     

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                LoadOperationalOverview();
                PopulateServiceGroupsDDL();
            }

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            LoadOperationalOverview();

        }

        private void LoadOperationalOverview()
        {

            List<OperationalOverview> lst = _dashboardRp.GetOpertionalViewData(ddlServiceGroup.SelectedValue.ToString());

            rptOperationalView.DataSource = lst;
            rptOperationalView.DataBind();

            
        }

        protected void rptOperationalView_ItemCommand(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                multiOrders = 0;
                multiOrderItems = 0;
                singleOrders = 0;
                totalOrders = 0;
                totalOrderItems = 0;
            }


            if ((e.Item.DataItem != null) && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {

                int multiItems = 0;
                int singleItems = 0;
               
                loadNo += Convert.ToInt32(((OperationalOverview)e.Item.DataItem).LoadNumber ?? "0");

                multiOrders += Convert.ToInt32(((OperationalOverview)e.Item.DataItem).MultiOrders ?? "0");

                multiItems = Convert.ToInt32(((OperationalOverview)e.Item.DataItem).MultiOrderItems ?? "0");

                multiOrderItems += multiItems;

                singleItems = Convert.ToInt32(((OperationalOverview)e.Item.DataItem).SingleOrders ?? "0");

                singleOrders += singleItems;


                totalOrders += Convert.ToInt32(((OperationalOverview)e.Item.DataItem).TotalOrders ?? "0");

                totalOrderItems += (multiItems + singleItems);


                ((Label)e.Item.FindControl("lblTotalMultiAndSignleItems")).Text = (multiItems + singleItems).ToString("#,###");

            }


            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)e.Item.FindControl("lblLoadNumber")).Text = loadNo.ToString("#,##0");
                ((Label)e.Item.FindControl("lblMultiOrderTotal")).Text = multiOrders.ToString("#,##0");
                ((Label)e.Item.FindControl("lblMultiOrderItemTotal")).Text = multiOrderItems.ToString("#,##0");
                ((Label)e.Item.FindControl("lblSingleOrderTotal")).Text = singleOrders.ToString("#,##0");
                ((Label)e.Item.FindControl("lblOrderTotal")).Text = totalOrders.ToString("#,##0");
                ((Label)e.Item.FindControl("lblOrderItemTotal")).Text = totalOrderItems.ToString("#,##0");

                List<OperationalOverview> lstCancellations = _dashboardRp.GetOpertionalCancellations(ddlServiceGroup.SelectedValue.ToString());

                string multiorders = Convert.ToInt32(lstCancellations[0].MultiOrders).ToString("#,##0");
                ((Label)e.Item.FindControl("lblMultiOrderCancel")).Text = Convert.ToInt32(lstCancellations[0].MultiOrders).ToString("#,##0");
                ((Label)e.Item.FindControl("lblMultiOrderCancelItem")).Text = Convert.ToInt32(lstCancellations[0].MultiOrderItems).ToString("#,##0");
                ((Label)e.Item.FindControl("lblSingleOrderCancel")).Text = Convert.ToInt32(lstCancellations[0].SingleOrders).ToString("#,##0");
                ((Label)e.Item.FindControl("lblCancelOrderTotal")).Text = (Convert.ToInt32(lstCancellations[0].MultiOrders) +
                                                                           Convert.ToInt32(lstCancellations[0].SingleOrders)).ToString("#,##0");
                ((Label)e.Item.FindControl("lblCancelItemTotal")).Text = (Convert.ToInt32(lstCancellations[0].MultiOrderItems) +
                                                                          Convert.ToInt32(lstCancellations[0].SingleOrders)).ToString("#,##0");
                

            }


            this.lblOpViewLastRefreshed.Text = DateTime.Now.ToString();
        }





        protected void PopulateServiceGroupsDDL()
        {
            DashboardReportsDAO dao = new DashboardReportsDAO();
            DataSet dsDDL = dao.Get_service_group();

            foreach (DataRow row in dsDDL.Tables[0].Rows)
            {
                string item_code_str = row["code"].ToString();
                string item_desc = row["codedesc"].ToString();
                this.ddlServiceGroup.Items.Add(new ListItem(item_desc, item_code_str));
            }
     
        }


    }
}