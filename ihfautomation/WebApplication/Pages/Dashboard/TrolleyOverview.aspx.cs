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
    public partial class TrolleyOverview : System.Web.UI.Page
    {
        string trlabel = null;
        Int32 trclass = 0;
        Int32 trstatus = 0;
        string sergrp = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            Initialise();

            if (!IsPostBack)
            {

                this.Getdropdown("Class");
                this.Getdropdown("Status");
                this.Getdropdown("Service");

                // this will load an empty radgrid in the page load
                trlabel = "-1";


            }
            else
            {

                try
                {


                    trlabel = TB_label.Text.ToString();
                    string status_cd = DD_status.SelectedItem.Value;

                    if (status_cd != string.Empty)
                        trstatus = Int32.Parse(status_cd);

                    string class_cd = DD_class.SelectedItem.Value;

                    if (class_cd != string.Empty)
                        trclass = Int32.Parse(class_cd);

                    sergrp = DD_sergrp.SelectedItem.Value;

                    this.BindData_troverview(trlabel, trstatus, trclass, sergrp);
                        


                    
                }
                catch (Exception Ex)
                {
                    //this.Master.ErrorMessage = "Error Fetching Records";
                }

            }



        }

        public void BindData_troverview(string I_trlabel, Int32 I_trstatus, Int32 I_trclass, string I_sergrp)
        {
            TrolleyDAO trov = new TrolleyDAO();
            DataSet ds_trov = new DataSet();

            ds_trov = trov.Get_trolley_overview(I_trlabel, I_trstatus, I_trclass, I_sergrp);
            RadGrid2.DataSource = ds_trov.Tables[0];


        }

        private void Initialise()
        {
            //Label4.Text = string.Empty;
            SetFocus(TB_label);
            
        }

        private void Getdropdown(string Input)
        {

            TrolleyDAO trdao = new TrolleyDAO();
            DataSet ds_cd = new DataSet();
            DataTable dt_cd = new DataTable();

            

            
            
            switch (Input)
            { 
                case "Class":
                    ds_cd = trdao.Get_tr_class();
                    dt_cd = ds_cd.Tables[0];

                    foreach (DataRow row in dt_cd.Rows)
                    {
                        string item_code_str = row["code"].ToString();
                        string item_desc = row["codedesc"].ToString();
                        DD_class.Items.Insert(0, new ListItem(item_desc, item_code_str));
                    }
                    break;
                case "Status":
                    ds_cd = trdao.Get_tr_status();
                    dt_cd = ds_cd.Tables[0];

                    foreach (DataRow row in dt_cd.Rows)
                    {
                        string item_code_str = row["code"].ToString();
                        string item_desc = row["codedesc"].ToString();
                        DD_status.Items.Insert(0, new ListItem(item_desc, item_code_str));
                    }
                    break;
                case "Service":
                    ds_cd = trdao.Get_service_group();
                    dt_cd = ds_cd.Tables[0];

                    foreach (DataRow row in dt_cd.Rows)
                    {
                        string item_code_str = row["code"].ToString();
                        string item_desc = row["codedesc"].ToString();
                        DD_sergrp.Items.Insert(0, new ListItem(item_desc, item_code_str));
                    }
                    break;
            }


            
        }

        protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            this.BindData_troverview(trlabel, trstatus, trclass, sergrp);

        }

        protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                
                String strolleyid = dataItem.GetDataKeyValue("trolley_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);

                DataSet ds_wrkst = new DataSet();
                TrolleyDAO trolleydao = new TrolleyDAO();
                
                ds_wrkst = trolleydao.Get_trolley_wrk(itrolleyid);

                

                if (ds_wrkst.Tables[0].Rows.Count > 0)
                {
                    RadComboBox wrkst = (RadComboBox)e.Item.FindControl("wrkst_RadComboBox");
                    wrkst.Visible = true;
                    wrkst.DataSource = ds_wrkst.Tables[0];

                    wrkst.DataTextField = "label";
                    wrkst.DataValueField = "wrkid";
                    wrkst.DataBind();

                    

                    //trolleyclassType.Items.Insert(0, new RadComboBoxItem("", "0"));
                }


                else
                {
                    RadComboBox wrkst = (RadComboBox)e.Item.FindControl("wrkst_RadComboBox");
                    wrkst.Visible = false;
                }


                // for the image

                TableCell cell = dataItem["percent_located"];
                Image completeImage = (Image)cell.FindControl("completeimage");
                Label locatedText = (Label)cell.FindControl("itemslocated");

                string located = ((DataRowView)e.Item.DataItem).Row["percent_located"].ToString();
                Int32 change = -1;

                if ( located != null && located != string.Empty)
                    change = Int32.Parse(located);
                if (change == 100)
                {
                    completeImage.ImageUrl = "~/Images/green.gif";
                    //changeText.Style["color"] = "green";
                }
                else if (change >= 60)
                {
                    completeImage.ImageUrl = "~/Images/yellow.gif";
                    //changeText.Style["color"] = "red";
                }
                else if (change < 60 && change >= 0)
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

        protected void Button1_Click(object sender, EventArgs e)
        {

            
            RadGrid2.Rebind();
        }

        
    }
}