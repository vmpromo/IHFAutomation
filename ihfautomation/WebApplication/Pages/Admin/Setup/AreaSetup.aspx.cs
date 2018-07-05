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

namespace IHF.ApplicationLayer.Web.Pages.Admin.Setup
{
    public partial class AreaSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Error.Visible = false;
            BindData();

        }

        protected void grdAreaSetup_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            //this.grdAreaSetup.DataSource = GetData().Tables[0];
            BindData();       


        }

        public void BindData()
        {
            AreaDAO amgr = new AreaDAO();
            DataSet dataSet = amgr.Get_Area();
            grdAreaSetup.DataSource = dataSet.Tables[0];
        }

        protected void grdAreaSetup_DeleteCommand(object sender, GridCommandEventArgs e)
        { 
        
        }

        protected void grdAreaSetup_ItemDataBound(object sender, GridItemEventArgs e)
        {

            

            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {

                
                GridEditableItem editedItem = e.Item as GridEditableItem;                
                GridEditFormInsertItem insert = e.Item as GridEditFormInsertItem;


                

                AreaDAO area_dao = new AreaDAO();

                // area type drop down

                DataSet ds_areatype = new DataSet();
                ds_areatype = area_dao.GetAreaType();

                RadComboBox areatype = (RadComboBox)editedItem
                                                .FindControl("Area_type_RadComboBox");

                areatype.DataSource = ds_areatype.Tables["AT"];
                areatype.DataTextField = "type_short_name";
                areatype.DataValueField = "area_type_id";
                areatype.DataBind();
                areatype.Items.Insert(0, new RadComboBoxItem("", "0"));

                DataSet dsWarehouse = new DataSet();
                dsWarehouse = area_dao.Get_Warehouses();

                RadComboBox warehousecb = (RadComboBox)editedItem.FindControl("rcbWarehouse");
                warehousecb.DataSource = dsWarehouse.Tables[0];
                warehousecb.DataTextField = "warehouse_name";
                warehousecb.DataValueField = "warehouse_id";
                warehousecb.DataBind();



                // area handle split drop down

                DataSet ds_handlesplit = new DataSet();
                ds_handlesplit = area_dao.GetAreaHandleSplit();

                RadComboBox areahandlesplit = (RadComboBox)editedItem
                                                .FindControl("handle_split_RadComboBox");

                areahandlesplit.DataSource = ds_handlesplit.Tables["HS"];
                areahandlesplit.DataTextField = "handle_split_short_name";
                areahandlesplit.DataValueField = "handle_split_code";
                areahandlesplit.DataBind();
                //areahandlesplit.Items.Insert(0, new RadComboBoxItem("", "0"));


                // allow admin release drop down - use the same dataset for the handle split

                RadComboBox allowadminrelease = 
                    (RadComboBox)editedItem.FindControl("allow_admin_release_ind_RadComboBox");

                allowadminrelease.DataSource = ds_handlesplit.Tables["HS"];
                allowadminrelease.DataTextField = "handle_split_short_name";
                allowadminrelease.DataValueField = "handle_split_code";
                allowadminrelease.DataBind();


                // area active indicator drop down

                DataSet ds_actind = new DataSet();
                ds_actind = area_dao.GetActiveInd();

                RadComboBox areaactiveind = (RadComboBox)editedItem
                                                .FindControl("Act_ind_RadComboBox");

                areaactiveind.DataSource = ds_actind.Tables["AI"];
                areaactiveind.DataTextField = "act_ind_short_name";
                areaactiveind.DataValueField = "act_ind_code";
                areaactiveind.DataBind();
                areaactiveind.Items.Insert(0, new RadComboBoxItem("", "0"));



                if (e.Item.ItemIndex != -1)
                {
                    Int32 AreaID = Int32.Parse(
                                        editedItem
                                        .OwnerTableView
                                        .DataKeyValues[e.Item.ItemIndex]["Area_id"]
                                        .ToString());

                    

                    DataSet ds_areadtls = new DataSet();
                    ds_areadtls = area_dao.Get_Area_for_ID(AreaID);

                    DataTable dt_areadtls = ds_areadtls.Tables[0];

                    Int32 area_type = 0;
                    string area_desc = null;
                    string area_sl = null;
                    string area_status = null;
                    string warehouse_id = string.Empty;
                    string area_allow_admin_release = null;
                    
                    foreach (DataRow row in dt_areadtls.Rows)
                    {
                         area_type = Int32.Parse(row["Area_type"].ToString());
                         area_desc = row["Area_desc"].ToString();
                         area_sl = row["Handle_Split_load_ind"].ToString();
                         area_status = row["Active_ind_cd"].ToString();
                         warehouse_id = row["warehouse_id"].ToString();
                         area_allow_admin_release = row["allow_admin_release_ind_cd"].ToString();
                    }

                    RadTextBox areadesc = (RadTextBox)editedItem.FindControl("Area_desc_TextBox");
                    areadesc.Text = area_desc;

                    areatype.SelectedValue = area_type.ToString();
                    areahandlesplit.SelectedValue = area_sl;
                    areaactiveind.SelectedValue = area_status;

                    RadComboBox rcbWarehouse = (RadComboBox)editedItem.FindControl("rcbWarehouse");
                    rcbWarehouse.SelectedValue = warehouse_id;

                    allowadminrelease.SelectedValue = area_allow_admin_release;

                }

            }

        }

        protected void grdAreaSetup_InsertCommand(object sender, GridCommandEventArgs e)
        {
            // insert area record in the database
            
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            string area_desc = null;
            Int32 area_typ_int;
            string handle_split_str;
            string act_ind_str;
            string displayname = User.Identity.Name;
            decimal warehouseid;
            string allow_admin_release_ind_str;

            

            RadTextBox areadesc = (RadTextBox)editedItem.FindControl("Area_desc_TextBox");
            area_desc = areadesc.Text.ToString();

            RadComboBox areatype = (RadComboBox)editedItem.FindControl("Area_type_RadComboBox");
            RadComboBox areasplit = (RadComboBox)editedItem.FindControl("handle_split_RadComboBox");
            RadComboBox areaactind = (RadComboBox)editedItem.FindControl("Act_ind_RadComboBox");
            RadComboBox warehousecb = (RadComboBox)editedItem.FindControl("rcbWarehouse");
            RadComboBox allowadminreleaseind = (RadComboBox)editedItem.FindControl("allow_admin_release_ind_RadComboBox");


            area_typ_int = Int32.Parse(areatype.SelectedValue);
            handle_split_str = areasplit.SelectedValue.ToString();
            act_ind_str = areaactind.SelectedValue.ToString();
            warehouseid = decimal.Parse(warehousecb.SelectedValue);
            allow_admin_release_ind_str = allowadminreleaseind.SelectedValue.ToString();

            

            AreaDAO areainsert = new AreaDAO();
            try
            {
                decimal areaid = areainsert.Create_Area(area_typ_int,
                                                        warehouseid,
                                                        area_desc,
                                                        handle_split_str,
                                                        act_ind_str,
                                                        displayname,
                                                        allow_admin_release_ind_str);
                if (areaid == 0)
                {
                    // display error
                    string err_msg = "Area that handles Split already exist";
                    HandleError(err_msg, 1);
                }
                else
                {
                    HandleError("Area Added", 0);
                }

            }
            catch(Exception ex_ins)
            {
                string err = ex_ins.Message;
                HandleError(err, 1);
            }
            BindData();
        
        }

        protected void grdAreaSetup_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;


            string area_desc = null;
            Int32 area_typ_int;
            string handle_split_str;
            string act_ind_str;
            string displayname = User.Identity.Name;
            decimal areaid;
            decimal warehouseid;
            string allow_admin_release_ind_str;
                
                
            areaid = decimal.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Area_id"].ToString());

            RadTextBox areadesc = (RadTextBox)editedItem.FindControl("Area_desc_TextBox");
            area_desc = areadesc.Text.ToString();

            RadComboBox areatype = (RadComboBox)editedItem.FindControl("Area_type_RadComboBox");
            RadComboBox areasplit = (RadComboBox)editedItem.FindControl("handle_split_RadComboBox");
            RadComboBox areaactind = (RadComboBox)editedItem.FindControl("Act_ind_RadComboBox");
            RadComboBox warehousecb = (RadComboBox)editedItem.FindControl("rcbWarehouse");
            RadComboBox allowadminreleaseind = (RadComboBox)editedItem.FindControl("allow_admin_release_ind_RadComboBox");

            area_typ_int = Int32.Parse(areatype.SelectedValue);
            handle_split_str = areasplit.SelectedValue.ToString();
            act_ind_str = areaactind.SelectedValue.ToString();
            warehouseid = decimal.Parse(warehousecb.SelectedValue);
            allow_admin_release_ind_str = allowadminreleaseind.SelectedValue.ToString();


            AreaDAO areaedit = new AreaDAO();
            try
            {
                decimal areaidupd = areaedit.Update_Area(areaid,
                                                         warehouseid,
                                                         area_typ_int,
                                                         area_desc,
                                                         handle_split_str,
                                                         act_ind_str,
                                                         displayname,
                                                         allow_admin_release_ind_str);
                string err_msg = null;
                if (areaidupd == 0)
                {
                    // display error
                    err_msg = "Area that handles Split already exist";
                    HandleError(err_msg, 1);
                }
                else if (areaidupd == -1)
                {
                    err_msg = "Area Type cannot be changed - Chutes Attached";
                    HandleError(err_msg, 1);
                }
                else
                {
                    HandleError("Area Updated", 0);
                }
            }
            catch (Exception ex_upd)
            {
                string err = ex_upd.Message;
                HandleError(err, 1);
            }
            BindData();
        }

        protected void grdAreaSetup_ItemCreated(object sender, GridItemEventArgs e)
        { }

        protected void grdAreaSetup_ItemCommand(object sender, GridCommandEventArgs e)
        { }

        protected void HandleError(string I_message, Int32 I_status)
        {
            Error.Text = I_message;
            Error.Visible = true;

            if (I_status == 0)// success
                Error.ForeColor = Color.Blue;
            else //failure
                Error.ForeColor = Color.Red;
        }

        

    }
}