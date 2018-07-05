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
    public partial class CagesToBeDespatched : System.Web.UI.Page
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

                GetCageIds();
                HiddenField1.Value = cages;
                
            }
            else
            {
                GetCageIds();
                HiddenField1.Value = cages;
                
                try
                {

                    string carrier_id = DD_carrier.SelectedItem.Value;

                    this.BindData_troverview(carrier_id);
                     

                    //GetCageIds();

                    Button1.Visible = true;


                                     
                }
                catch (Exception Ex)
                {
                    Label1.Text = "Error While Processing Request";
                    Label1.Visible = true;
                    Label1.ForeColor = Color.Red;
                }

            }



        }

        public void GetCageIds()
        {
            CageReportsDAO cg_firstload = new CageReportsDAO();
            string carrierid = DD_carrier.SelectedItem.Value;
            string cagesids = cg_firstload.Getcageidstring(carrierid);
            if (cagesids == string.Empty || cagesids == "null" || cagesids == "0")
            {

                cages = "No cages for Despatch";
            }
            else
            {
                cages = "Click OK to Despatch " + cagesids + " Cages for " + carrierid;
            }

        }

        public void BindData_troverview(string I_carrier)
        {
            CageReportsDAO crdao = new CageReportsDAO();
            DataSet ds_crov = new DataSet();

            ds_crov = crdao.GetCarriersTobeDespatched(I_carrier);
            RadGrid2.DataSource = ds_crov.Tables[0];


        }

        private void Initialise()
        {
            Label1.Visible = false;
            Button1.Visible = false;
            
        }

        private void Getdropdown()
        {

            CageReportsDAO crdao = new CageReportsDAO();
            DataSet ds_cd = new DataSet();
            DataTable dt_cd = new DataTable();


            ds_cd = crdao.GetCarriers();
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
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                String scageid = dataItem.GetDataKeyValue("cage_id").ToString();
                Int32 icageid = Int32.Parse(scageid);

                
                CageReportsDAO cagedao = new CageReportsDAO();

                decimal cg_status = cagedao.GetCageStatus(icageid);

                // for the image

                TableCell cell = dataItem["ReadyForDespatch"];
                Button ReadyBtn = (Button)cell.FindControl("btn_ready");

                if (cg_status == 40)//detached
                {
                    ReadyBtn.Visible = true;
                }
                else
                {
                    ReadyBtn.Visible = false;
                }
                

            }
            
        }
        protected void RadGrid2_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            
            RadGrid2.Rebind();
            //Button1.Visible = true;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string carrid = DD_carrier.SelectedItem.Value;
            DespatchDAO desdao = new DespatchDAO();
            CageReportsDAO crdao = new CageReportsDAO();
            try
            {

                string carrbarcode = crdao.GetCarrierBarcode(carrid);

                decimal cagecount = desdao.ValidateCagesForDespatch(carrbarcode);
                if (cagecount > 0)
                {
                    string queueoutput = desdao.QueueForDespatch(carrbarcode, username);
                }

                this.BindData_troverview(carrid);

                RadGrid2.Rebind();

                CageReportsDAO cg_firstload = new CageReportsDAO();
                string carrierid = DD_carrier.SelectedItem.Value;
                string cagesids = cg_firstload.Getcageidstring(carrierid);
                if (cagesids == string.Empty || cagesids == "null" || cagesids == "0")
                {

                    Label1.Text = string.Empty;
                }
                else
                { 
                    Label1.Text = "Cages Despatched for " + carrid;
                    Label1.ForeColor = Color.Blue;
                }

            }
            catch (Exception Ex2)
            {
                Label1.Text = "Error:" + Ex2.Message.Substring(Ex2.Message.IndexOf(" ", 0), (Ex2.Message.IndexOf("ORA", 1) - Ex2.Message.IndexOf(" ", 0)));
                Label1.Visible = true;
                Label1.ForeColor = Color.Red;
            }
            
            Label1.Visible = true;


            
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string carr_id = DD_carrier.SelectedItem.Value;
            try
            {
                // get carrier barcode
                

                CageReportsDAO crdao = new CageReportsDAO();
                string carrbarcode = crdao.GetCarrierBarcode(carr_id);

                // get cage barcode
                Button btn = (Button)sender;
                GridDataItem dataitem = (GridDataItem)btn.NamingContainer;

                String scageid = dataitem.GetDataKeyValue("cage_id").ToString();
                Int32 icageid = Int32.Parse(scageid);

                string cagebarcode = crdao.GetCageBarcode(icageid);

                // call ready to despatch
            

                DespatchDAO desdao = new DespatchDAO();

            
                string cagename = desdao.ProcessBarcode(carrbarcode, cagebarcode, username);

                Label1.Text = "Cage " + scageid + " Is Ready For despatch";
                Label1.ForeColor = Color.Blue;

                Label1.Visible = true;
            }
            catch (Exception ex)
            {
                Label1.Text = "Error:" + ex.Message.Substring(ex.Message.IndexOf(" ", 0), (ex.Message.IndexOf("ORA", 1) - ex.Message.IndexOf(" ", 0)));
                Label1.Visible = true;
                Label1.ForeColor = Color.Red;
            }
            

            //RadGrid2.Rebind();

            this.BindData_troverview(carr_id);

            RadGrid2.Rebind();

            GetCageIds();
            HiddenField1.Value = cages;
        }
        
    }
}