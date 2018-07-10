using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using IHF.BusinessLayer.Util;
using System.Data;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class TrolleyDetail : System.Web.UI.Page
    {
        Int32 trolley_id = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

                try
                {
                    string trolleyidstr = Request.QueryString["trolley_id"].ToString();
                    if (trolleyidstr != null)
                        trolley_id = Int32.Parse(trolleyidstr);

                    this.BindData_trdetail(trolley_id);
                    this.BindData_tritem(trolley_id);
                }
                catch (Exception ex)
                {

                }


            //}
            //else
            //{
                

            //    RadGrid1.Rebind();
            //    RadGrid2.Rebind();
            //}
        }

        public void BindData_trdetail(Int32 I_trolley_id)
        {
            TrolleyDAO trov = new TrolleyDAO();
            DataSet ds_trov = new DataSet();

            ds_trov = trov.Get_trolley_detail(I_trolley_id);
            RadGrid1.DataSource = ds_trov.Tables[0];


        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData_trdetail(trolley_id);

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        public void BindData_tritem(Int32 I_trolley_id)
        {
            TrolleyDAO trov = new TrolleyDAO();
            DataSet ds_trov = new DataSet();

            ds_trov = trov.Get_trolley_items(I_trolley_id);
            RadGrid2.DataSource = ds_trov.Tables[0];


        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData_tritem(trolley_id);

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                

                // for the image

                TableCell cell = dataItem["item_status"];
                Image completeImage = (Image)cell.FindControl("completeimage");
                //Label locatedText = (Label)cell.FindControl("itemslocated");

                string located = ((DataRowView)e.Item.DataItem).Row["item_status_cd"].ToString();
                Int32 change = 0;

                if (located != null && located != string.Empty)
                    change = Int32.Parse(located);
                if (change == 110)
                {
                    completeImage.ImageUrl = "~/Images/green.gif";
                    //changeText.Style["color"] = "green";
                }
                else if (change == 90)
                {
                    completeImage.ImageUrl = "~/Images/yellow.gif";
                    //changeText.Style["color"] = "red";
                }
                else if (change < 90 && change != 0)
                {
                    completeImage.ImageUrl = "~/Images/red.gif";
                }
                else
                {
                    completeImage.Visible = false;
                }

            }
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    //Response.Redirect("~/Pages/Dashboard/TrolleyOverview.aspx");
        //}

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //RadGrid2.PageSize = RadGrid2.MasterTableView.VirtualItemCount;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.MasterTableView.ExportToExcel();


        }
    }
}