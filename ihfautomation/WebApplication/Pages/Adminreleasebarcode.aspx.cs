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
using System.Data;
using IHF.BusinessLayer.Util;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace IHF.ApplicationLayer.Web
{
    public partial class Adminreleasebarcode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            BindData();

        }
        public void BindData()
        {
            AdminreleaseDAO amgr = new AdminreleaseDAO();
            DataSet dataSet = amgr.Search_trolley();
            RadGrid1.DataSource = dataSet.Tables[0];
        }
        /*
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string ID = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["load_id"].ToString();
            Int32 tr_type;
            string label = null;

            // assignment of users
            UserDAO userdao = new UserDAO();
            DataSet user = new DataSet();
            user = userdao.GetUsers();
            DataTable usertab = new DataTable();
            usertab = user.Tables[0];
            string displayname = null;
            foreach (DataRow row in usertab.Rows)
            {
                string userid = row["userid"].ToString();

                if (Int32.Parse(userid) == 24) // using the test userid
                    displayname = row["displayname"].ToString();
            }


            label = (editedItem["trolley_label"].Controls[0] as TextBox).Text;

            Int32 itrolleyid = Int32.Parse(ID);
            //RadComboBox trolleytype = (RadComboBox)editedItem.FindControl("trolleytype_RadComboBox");
            //tr_type = Int32.Parse(trolleytype.SelectedValue);


            AdminreleaseDAO amgrupd = new AdminreleaseDAO();
            DataSet dataSet = amgrupd.Update_trolley(itrolleyid, label, displayname);
            RadGrid1.DataSource = dataSet.Tables[0];
        }

        protected void RadGrid1_EditCommand(object sender, GridCommandEventArgs e)
        {


        }
        */
        //protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        //{

        //}

        //protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        //{

        //}

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("trolley_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "2";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype + itrolleyid);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);
            }
        }
    }
}