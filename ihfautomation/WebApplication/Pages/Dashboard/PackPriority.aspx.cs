using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Telerik.Web.UI;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.DataAccessObjects.Despatch;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class PackPriority : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindgrid();
                //Label1.Visible = false;
            }

        }

        protected void Bindgrid()
        {
            PackPriorityDAO packpriority_dao = new PackPriorityDAO();

            DataSet packpriority_ds = new DataSet();

            packpriority_ds = packpriority_dao.Get_packPriority();

            RadGrid2.DataSource = packpriority_ds.Tables[0];

            
        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.Bindgrid();

        }
        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;



                // for the image

                TableCell cell = dataItem["select"];

                Label SelectText = (Label)cell.FindControl("drilldown");
                HyperLink _myLink = (HyperLink)cell.FindControl("HyperLink1");

                string criterion = dataItem.GetDataKeyValue("criterion_name").ToString();

                string drill = ((DataRowView)e.Item.DataItem).Row["criterion_type"].ToString();
                Int32 type = 0;

                if (drill != null && drill != string.Empty)
                    type = Int32.Parse(drill);
                if (type == 1)
                {
                    //SelectText.Text = "Select";
                    _myLink.Text = "Select";
                    _myLink.NavigateUrl = "~/Pages/Dashboard/PackPriorityValue.aspx?criterion_name=" + criterion;
                }
                
                else
                {
                    //SelectText.Visible = false;
                    _myLink.Visible = false;
                }

            }
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string criterion_name = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["criterion_name"].ToString();
            Int32 wt_int;
            string wt_str = null;

            wt_str = (editedItem.FindControl("TB1") as TextBox).Text;

            //wt_str = (editedItem["criterion_weighting"].Controls[0] as TextBox).Text;
            //wt_str = (editedItem["TB1"].Controls[0] as TextBox).Text;
            
            wt_int = Int32.Parse(wt_str);

            PackPriorityDAO packpriority_dao = new PackPriorityDAO();

            Decimal status = packpriority_dao.Update_packpriority(criterion_name, wt_int);


            this.Bindgrid();
        }

        protected void RadGrid2_EditCommand(object sender, GridCommandEventArgs e)
        {


        }

    }
}