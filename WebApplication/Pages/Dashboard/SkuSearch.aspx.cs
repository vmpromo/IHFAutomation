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
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class SkuSearch : System.Web.UI.Page
    {
        string skuid = null;
        string loadid = null;
        Int32 areaid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Initialise();

            if (!IsPostBack)
            {

                SkuLabelDAO skudao = new SkuLabelDAO();
                DataSet areaCodesDs = new DataSet();
                DataTable areaCodesDt = new DataTable();

                areaCodesDs = skudao.Get_area_dropdown();

                areaCodesDt = areaCodesDs.Tables[0];

                foreach (DataRow row in areaCodesDt.Rows)
                {
                    string areaCodeStr = row["area_id"].ToString();

                    string areaDesc = row["area_descr"].ToString();

                    DD_area.Items.Insert(0, new ListItem(areaDesc, areaCodeStr));


                }

                // restore session values if they exist
                if (Session["skuid"] != null)
                {
                    skuid = (string) Session["skuid"];
                    TB_sku.Text = skuid;
                }

                if (Session["loadid"] != null)
                {
                    loadid = (string) Session["loadid"];
                    TB_load.Text = loadid;
                }

                if (Session["areaid"] != null)
                {
                    areaid = (Int32) Session["areaid"];
                    string area_id = areaid.ToString();
                    if (areaid == 0)
                        area_id = String.Empty;
                    DD_area.SelectedValue = area_id;
                }

            }
            else
            {

                try
                {
                    if (TB_sku.Text != "")
                    {
                        skuid = TB_sku.Text.ToString();
                        string area_id = DD_area.SelectedItem.Value;
                        //Int32 item_code = 0;
                        if (area_id != string.Empty)
                            areaid = Int32.Parse(area_id);
                        loadid = TB_load.Text.ToString();

                        this.BindData_skuupc(skuid);
                        this.BindData_skudtl(skuid, areaid, loadid);

                        // TB_sku.Text = string.Empty;
                        // TB_load.Text = string.Empty;
                        
                    }
                    else
                    {
                        Label4.Text = "Enter Sku or Scan SKU UPC";
                        Label4.ForeColor = Color.Red;


                    }
                }
                catch (Exception Ex)
                {
                    Label4.Text = "Error Fetching Records";
                    Label4.ForeColor = Color.Red;
                }

            }
            

            
        }

        private void Initialise()
        {
            Label4.Text = string.Empty;
            SetFocus(TB_sku);
            // TB_sku.Text = string.Empty;
            // TB_load.Text = string.Empty;
        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            
            this.BindData_skuupc(skuid);
            
        }
        
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
           
        }
        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
           
        }

        public void BindData_skuupc(string I_sku)
        {
            SkuLabelDAO skusearch = new SkuLabelDAO();

            DataSet dataSet_skuupc = skusearch.SkuSearch(I_sku);
            RadGrid1.DataSource = dataSet_skuupc.Tables[0];

            
        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            
            this.BindData_skudtl(skuid, areaid, loadid);
           
        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        public void BindData_skudtl(string I_sku, Int32 I_area, string I_load)
        {
            SkuLabelDAO skusearch = new SkuLabelDAO();

            
            DataSet dataSet_skudtl = skusearch.SkudetailsSearch(I_sku, I_area, I_load);
            RadGrid2.DataSource = dataSet_skudtl.Tables[0];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["skuid"] = skuid;
            Session["loadid"] = loadid;
            Session["areaid"] = areaid;

            RadGrid1.Rebind();
            RadGrid2.Rebind();
            SetFocus(TB_sku);
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {

            Session.Remove("skuid");
            Session.Remove("loadid");
            Session.Remove("areaid");
            Response.Redirect("SkuSearch.aspx");

        }
    }
}