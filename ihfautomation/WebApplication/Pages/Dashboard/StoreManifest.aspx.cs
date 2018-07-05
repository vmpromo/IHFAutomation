using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class StoreManifest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateStoreDDL();
            populateVanRunList();
            populateManifestGrid();
        }

        protected void CreateManifestStore_Click(object sender, EventArgs e)
        {
            string sstoreid = rcbStore.SelectedValue;

            if (sstoreid == string.Empty)
            {
                lbMsg.Text = "No store selected";
                lbMsg.ForeColor = System.Drawing.Color.Red;
                lbMsg2.Text = "";

            }
            else
            {
                decimal storeid = decimal.Parse(sstoreid);
                try
                {
                    StoreManifestDAO dao = new StoreManifestDAO();

                    decimal manifestid = dao.CreateStoreManifest(storeid, null, Shared.CurrentUser, Shared.UserHostName);

                    lbMsg.Text = "Manifest " + manifestid.ToString() + " created";
                    lbMsg.ForeColor = System.Drawing.Color.Blue;
                    lbMsg2.Text = "";
                    lbMsg2.ForeColor = System.Drawing.Color.Blue;

                    rcbVanRun.ClearSelection();
                    rcbVanRun.Text = "";
                    rcbVanRun.DataBind();

                    rcbStore.ClearSelection();
                    rcbStore.Text = "";
                    rcbStore.DataBind();


                    populateManifestGrid();
                    rgManifestList.DataBind();
                }
                catch (Exception ex)
                {
                    lbMsg.Text = ex.Message;
                    lbMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void CreateManifestVanRun_Click(object sender, EventArgs e)
        {
            string vanrunid = rcbVanRun.SelectedValue;

            if (vanrunid == string.Empty)
            {
                lbMsg2.Text = "No van run selected";
                lbMsg.Text = "";
                lbMsg2.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                try
                {
                    StoreManifestDAO dao = new StoreManifestDAO();

                    decimal manifestid = dao.CreateStoreManifest(null, vanrunid, Shared.CurrentUser, Shared.UserHostName);

                    lbMsg2.Text = "Manifest " + manifestid.ToString() + " created";
                    lbMsg2.ForeColor = System.Drawing.Color.Blue;
                    lbMsg.Text = "";
                    lbMsg.ForeColor = System.Drawing.Color.Blue;

                    rcbVanRun.ClearSelection();
                    rcbVanRun.Text = "";
                    rcbVanRun.DataBind();

                    rcbStore.ClearSelection();
                    rcbStore.Text = "";
                    rcbStore.DataBind();

                    populateManifestGrid();
                    rgManifestList.DataBind();
                }
                catch (Exception ex)
                {
                    lbMsg2.Text = ex.Message;
                    lbMsg2.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        private void populateStoreDDL()
        {
            StoreManifestDAO dao = new StoreManifestDAO();

            DataSet  ds = dao.GetStoreList();


            rcbStore.DataSource = ds.Tables[0];
            rcbStore.DataTextField = "storename";
            rcbStore.DataValueField = "storeid";
        }


        private void populateVanRunList()
        {
            StoreManifestDAO dao = new StoreManifestDAO();

            DataSet ds = dao.GetVanRunList();
            rcbVanRun.DataSource = ds.Tables[0];
            rcbVanRun.DataTextField = "rte_name";
            rcbVanRun.DataValueField = "rte_id";


        }

        private void populateManifestGrid()
        {
            StoreManifestDAO dao = new StoreManifestDAO();

            DataSet ds = dao.GetManifestList();

            rgManifestList.DataSource = ds.Tables[0];
        }

        protected void rgManifestList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                decimal manifestid = decimal.Parse(dataItem.GetDataKeyValue("manifest_id").ToString());


                PrintService ps = new PrintService();
                string retval = ps.PrintStoreManifest(manifestid, Shared.UserHostName, Shared.CurrentUser);

                if (retval.Substring(0,5).ToUpper() == "ERROR")
                    lbMsg.ForeColor = System.Drawing.Color.Red;
                else
                    lbMsg.ForeColor = System.Drawing.Color.Blue;

                lbMsg.Text = retval;
            } 
   
        }

    }
}