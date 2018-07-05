using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects.Returns;
using System.Data;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.Returns
{
    public partial class SearchOrder : System.Web.UI.Page
    {
        protected DataSet ds;

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();
            // ds = dao.searchCustomer(null, "watt", "", "", "", "", "", "", "");

            // rgCustomers.DataSource = ds;

            //var fds = from r in ds.Tables[0].AsEnumerable() where r.Field<string>("customerurn") == "0011605535"  select r.Field<string>("customerurn")

            //DataRow [] dt = ds.Tables[0].Select("customerurn='0011605535'");
            //RadGrid1.DataSource = ds.Tables[0];
            // RadGrid1.MasterTableView.DetailTables[0].DataSource = ds.Tables[1];

            //RadGrid1.Enabled = false;

            if (!Page.IsPostBack)
            {
                //rgCustomers.Visible = false;

            }
            else
                rgCustomers.Visible = true;
        }

        protected void rgCustomers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            if (!e.IsFromDetailTable)
            {
               

                bool dosearch = (rtbAddress.Text != string.Empty) || (rtbAddress.Text != string.Empty) || (rtbPostCode.Text != string.Empty) || (rtbFirstName .Text!= string.Empty) || (rtbLastName.Text != string.Empty) || (rtbEmail.Text != string.Empty);

                if (dosearch)
                {
                    ReturnsDAO dao = new ReturnsDAO();
                    ds = dao.searchCustomer(null, rtbLastName.Text, rtbFirstName.Text, rtbPostCode.Text, rtbAddress.Text, "", rtbEmail.Text, rtbTelephone.Text, "");
                    rgCustomers.DataSource = ds.Tables[0];
                }

            }

        }

        protected void rgCustomers_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {

                case "orders":
                    {
                        string customerurn = dataItem.GetDataKeyValue("customerurn").ToString();
                        DataSet dsorders = dao.getCustomerOrders(customerurn);
                        e.DetailTableView.DataSource = dsorders;
                        break;
                    }

                case "items":
                    {
                        int ordernumber = int.Parse(dataItem.GetDataKeyValue("ordernumber").ToString());
                        DataSet dsitems = dao.getOrderItems(ordernumber);
                        e.DetailTableView.DataSource = dsitems;
                        break;
                    }

            }

        }

        protected void rbSearch_Click(object sender, EventArgs e)
        {
            rgCustomers.Rebind();
        }
    }
}