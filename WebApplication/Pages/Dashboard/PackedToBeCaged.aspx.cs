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
    public partial class PackedToBeCaged : System.Web.UI.Page
    {
        
        string carrier = null;
        public string cages;
        string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            cages = "Click OK to Despatch";
            username = User.Identity.Name;
            
            Initialise();
            

            if (!IsPostBack)
            {

                this.Getdropdown();
                

                // this will load an empty radgrid in the page load
                carrier = "-1";

                
                
            }
            else
            {
                
                
                try
                {

                    string carrier_id = DD_carrier.SelectedItem.Value;

                    this.BindData_troverview(carrier_id);
                     

                                     
                }
                catch (Exception Ex)
                {
                    Label1.Text = "Error While Processing Request";
                    Label1.Visible = true;
                    Label1.ForeColor = Color.Red;
                }

            }



        }

        

        public void BindData_troverview(string I_carrier)
        {
            CageReportsDAO crdao = new CageReportsDAO();
            DataSet ds_crov = new DataSet();

            ds_crov = crdao.GetParcelstobecaged(I_carrier);
            RadGrid2.DataSource = ds_crov.Tables[0];


        }

        private void Initialise()
        {
            Label1.Visible = false;
            
            
        }

        private void Getdropdown()
        {

            CageReportsDAO crdao = new CageReportsDAO();
            DataSet ds_cd = new DataSet();
            DataTable dt_cd = new DataTable();


            ds_cd = crdao.GetCarriersWithSelect();
            dt_cd = ds_cd.Tables[0];

            foreach (DataRow row in dt_cd.Rows)
            {
                string item_code_str = row["Carrier_id"].ToString();
                string item_desc = row["Carrier"].ToString();
                DD_carrier.Items.Insert(0, new ListItem(item_desc, item_code_str));
            }
                    


            
        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData_troverview(carrier);

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "print")
            {

                GridDataItem dataItem = (GridDataItem)e.Item;
                String sorder = dataItem.GetDataKeyValue("order_no").ToString();
                decimal iorder = decimal.Parse(sorder);

                string user = User.Identity.Name;
                string machinename = Shared.UserHostName;
                
                PrintService ps = new PrintService();
                string test = ps.PrintPackDocuments(iorder, true, machinename, "L", user);

                Label1.Text = test;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            
            RadGrid2.Rebind();
            //Button1.Visible = true;
        }
        
        
    }
}