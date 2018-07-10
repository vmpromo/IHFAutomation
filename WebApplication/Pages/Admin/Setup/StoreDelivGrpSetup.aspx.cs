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
using System.Text.RegularExpressions;

namespace IHF.ApplicationLayer.Web.Pages.Admin.Setup
{

    public partial class StoreDelivGrpSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //CagetypeServiceDAO cts = new CagetypeServiceDAO();
            //this.CageTypesRadGrid.DataSource = cts.GetCageServices("A").Tables[0];
            //DataBind();

        }


        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                StoreDelivGrpDAO storeDelivGrp = new StoreDelivGrpDAO();
                StoreDelivGroupsRadGrid.DataSource = storeDelivGrp.GetStoreDelivGroups().Tables[0];
            }
        }


        protected void StoreDelivGroupsRadGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            StoreDelivCriteriaDAO storeDelivCriteria = new StoreDelivCriteriaDAO();
            string storeDelivGroupId = dataItem.GetDataKeyValue("group_id").ToString();
            e.DetailTableView.DataSource = storeDelivCriteria.GetStoreDelivCriteria(storeDelivGroupId).Tables[0];
        }

        protected void StoreDelivGroupsRadGrid_EditCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void StoreDelivGroupsRadGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)StoreDelivGroupsRadGrid.MasterTableView.GetColumn("EditCommandColumn");
                editColumn.Visible = false;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
                // RadTextBox radtxtCageTypeDescr = (RadTextBox)e.Item.FindControl("radtxtCageTypDescrEdit");
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)StoreDelivGroupsRadGrid.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
            }

        }

        protected void StoreDelivGroupsRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem && !e.Item.Expanded)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string sdgroupname = dataItem.GetDataKeyValue("group_name").ToString();


                Button button = dataItem["DeleteColumn"].Controls[0] as Button;
                button.Attributes["onclick"] = "return confirm('Are you sure you want to delete store delivery group " +
                sdgroupname + "?')";
            }

            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {

                if (e.Item.ItemIndex < 0)
                {

                    // Inserting Group

                    GridEditableItem editedItem = e.Item as GridEditableItem;

                    ((Label)(e.Item.FindControl("lblStoreDeliveryGroup"))).Visible = true;

                    ((RadTextBox)(e.Item.FindControl("txtStoreDeliveryGroup"))).Visible = false;

                    RadComboBox rcbCriteriaType = (RadComboBox)e.Item.FindControl("rcbCriteriaType");
                    StoreDelivCriteriaDAO storeDelivCriteriaType = new StoreDelivCriteriaDAO();
                    rcbCriteriaType.DataSource = storeDelivCriteriaType.GetStoreDelivCriteriaTypes();
                    rcbCriteriaType.DataTextField = "sd_crit_type_code";
                    rcbCriteriaType.DataValueField = "sd_crit_type_value";
                    rcbCriteriaType.DataBind();
                    RadComboBoxItem rcbItemStoreDelivCriteriaType = new RadComboBoxItem("None", "-1");
                    rcbCriteriaType.Items.Add(rcbItemStoreDelivCriteriaType);
                    rcbCriteriaType.SelectedValue = "-1";

                }
                else
                {

                    // Editing Group

                    GridEditableItem editedItem = e.Item as GridEditableItem;

                    string groupid = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["group_id"].ToString();
                    DataRowView row = (DataRowView)e.Item.DataItem;

                    ((Label)(e.Item.FindControl("lblStoreDeliveryGroup"))).Visible = true;
                    ((Label)(e.Item.FindControl("lblStoreDeliveryGroup"))).Text = groupid;

                    ((RadTextBox)(e.Item.FindControl("txtStoreDeliveryGroup"))).Visible = false;

                    ((RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupName")).Text = (string)row.Row["group_name"];

                    ((RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupDescr")).Text = (string)row.Row["description"];

                    RadComboBox rcbCriteriaType = (RadComboBox)e.Item.FindControl("rcbCriteriaType");
                    StoreDelivCriteriaDAO storeDelivCriteriaType = new StoreDelivCriteriaDAO();
                    rcbCriteriaType.DataSource = storeDelivCriteriaType.GetStoreDelivCriteriaTypes();
                    rcbCriteriaType.DataTextField = "sd_crit_type_code";
                    rcbCriteriaType.DataValueField = "sd_crit_type_value";
                    rcbCriteriaType.DataBind();
                    RadComboBoxItem rcbItemStoreDelivCriteriaType = new RadComboBoxItem("None", "-1");
                    rcbCriteriaType.Items.Add(rcbItemStoreDelivCriteriaType);
                    rcbCriteriaType.SelectedValue = "-1";

                }
            }

        }

        protected void StoreDelivGroupsRadGrid_ItemEvent(object sender, GridItemEventArgs e)
        {

        }

        protected void RadListBoxTo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void StoreDelivGroupsRadGrid_ItemUpdated(object sender, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                DisplayMessage(true, "Cage " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cage_type_id"] + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Cage " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cage_type_id"] + " updated");
            }
        }

        protected void StoreDelivGroupsRadGrid_ItemInserted(object sender, GridInsertedEventArgs e)
        {

        }

        protected void StoreDelivGroupsRadGrid_ItemDeleted(object sender, GridDeletedEventArgs e)
        {

        }

        private void DisplayMessage(bool isError, string text)
        {
            Label label = (isError) ? this.Label1 : this.Label2;
            label.Text = text;
        }

        protected void StoreDelivGroupsRadGrid_InsertCommand(object sender, GridCommandEventArgs e)
        {

            Label txtStoreDeliveryGroup = (Label)e.Item.FindControl("lblStoreDeliveryGroup");
            RadTextBox txtStoreDeliveryGroupName = (RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupName");
            RadTextBox txtStoreDeliveryGroupDescr = (RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupDescr");
            RadComboBox txtCriteriaType = (RadComboBox)e.Item.FindControl("rcbCriteriaType");

            if (txtStoreDeliveryGroupName.Text.Length > 0 && txtStoreDeliveryGroupDescr.Text.Length > 0)
            {

                try
                {

                    Int64? retStoreDeliveryGroupId = 0;
                    string storeDeliveryGroupName = txtStoreDeliveryGroupName.Text;
                    string storeDeliveryGroupDescr = txtStoreDeliveryGroupDescr.Text;
                    string criterionTypeCode = txtCriteriaType.Text;
                    Int64? storeDelivGrpMapId = 0;

                    StoreDelivGrpDAO storeDelivGrp = new StoreDelivGrpDAO();
                    storeDelivGrp.CreateStoreDeliveryGroupTypeDetails(ref retStoreDeliveryGroupId, storeDeliveryGroupName.ToUpper(), storeDeliveryGroupDescr);
                    Int64 storeDeliveryGroupId = retStoreDeliveryGroupId.Value;

                    RadListBox rlbTo = (RadListBox)e.Item.FindControl("RadListBoxTo");

                    StoreDelivCriteriaDAO storeDelivCriteria = new StoreDelivCriteriaDAO();

                    foreach (RadListBoxItem rlbItem in rlbTo.Items)
                    {
                        storeDelivCriteria.AddCriteriaToGroup(storeDelivGrpMapId, storeDeliveryGroupId, criterionTypeCode, rlbItem.Value);
                    }

                    StoreDelivGroupsRadGrid.MasterTableView.IsItemInserted = true;
                    DisplayMessage(false, "Store delivery group " + storeDeliveryGroupName + " updated");
                    StoreDelivGroupsRadGrid.Rebind();
                }
                catch (Exception ex)
                {
                    string messageline = ex.Message.Split('\n')[0].Substring(10);
                    DisplayMessage(true, messageline);
                }

            }
        }

        protected void StoreDelivGroupsRadGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {

            Label txtStoreDeliveryGroup = (Label)e.Item.FindControl("lblStoreDeliveryGroup");
            RadTextBox txtStoreDeliveryGroupName = (RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupName");
            RadTextBox txtStoreDeliveryGroupDescr = (RadTextBox)e.Item.FindControl("txtStoreDeliveryGroupDescr");
            RadComboBox txtCriteriaType = (RadComboBox)e.Item.FindControl("rcbCriteriaType");

            if (txtStoreDeliveryGroupName.Text.Length > 0 && txtStoreDeliveryGroupDescr.Text.Length > 0 )
            {

                try
                {
                    Int64 storeDeliveryGroupId = Int64.Parse(txtStoreDeliveryGroup.Text);
                    string storeDeliveryGroupName = txtStoreDeliveryGroupName.Text;
                    string storeDeliveryGroupDescr = txtStoreDeliveryGroupDescr.Text;
                    string criterionTypeCode = txtCriteriaType.Text;
                    Int64? storeDelivGrpMapId = 0;

                    StoreDelivGrpDAO storeDelivGrp = new StoreDelivGrpDAO();
                    storeDelivGrp.UpdateStoreDeliveryGroupTypeDetails(storeDeliveryGroupId, storeDeliveryGroupName.ToUpper(), storeDeliveryGroupDescr);

                    RadListBox rlbTo = (RadListBox)e.Item.FindControl("RadListBoxTo");
                    RadListBox rlbFrom = (RadListBox)e.Item.FindControl("RadListBoxFrom");

                    StoreDelivCriteriaDAO storeDelivCriteria = new StoreDelivCriteriaDAO();

                    foreach (RadListBoxItem rlbItem in rlbFrom.Items)
                    {
                        storeDelivCriteria.RemoveCriteriaFromGroup(storeDeliveryGroupId, criterionTypeCode, rlbItem.Value);
                    }

                    foreach (RadListBoxItem rlbItem in rlbTo.Items)
                    {
                        storeDelivCriteria.AddCriteriaToGroup(storeDelivGrpMapId, storeDeliveryGroupId, criterionTypeCode, rlbItem.Value);
                    }

                    DisplayMessage(false, "Store delivery group " + storeDeliveryGroupName + " updated");
                    StoreDelivGroupsRadGrid.Rebind();
                }
                catch (Exception ex)
                {
                    string messageline = ex.Message.Split('\n')[0].Substring(10);
                    DisplayMessage(true, messageline);
                }

            }

        }




        protected void StoreDelivGroupsRadGrid_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CageTypesRadGrid.EditIndexes.Add(0);
                //CageTypesRadGrid.Rebind();
            }
        }

        protected void rcbCriteriaType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;

            RadListBox rlbFrom = (RadListBox)edititem.FindControl("RadListBoxFrom");
            RadListBox rlbTo = (RadListBox)edititem.FindControl("RadListBoxTo");

            Label lblStoreDeliveryGroup = (Label)edititem.FindControl("lblStoreDeliveryGroup");
            
            rlbFrom.Items.Clear();
            rlbTo.Items.Clear();
            StoreDelivCriteriaDAO storeDelivCriteria = new StoreDelivCriteriaDAO();
            DataTable dtAvailableCriteria = storeDelivCriteria.GetAvailableStoreDelivCriteriaByType(lblStoreDeliveryGroup.Text, combobox.Text).Tables[0];
            DataTable dtCriteria = storeDelivCriteria.GetStoreDelivCriteriaByType(lblStoreDeliveryGroup.Text, combobox.Text).Tables[0];
            
            foreach (DataRow dr in dtAvailableCriteria.Rows)
            {
                rlbFrom.Items.Add(new RadListBoxItem(dr["crit_value"] as string, dr["crit_code"] as string));

            }

            foreach (DataRow dr in dtCriteria.Rows)
            {
                rlbTo.Items.Add(new RadListBoxItem(dr["crit_value"] as string, dr["crit_code"] as string));

            }
        
        }


        protected void rcbCarriers_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            RadComboBox combobox = (RadComboBox)sender;
            GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;

            RadTextBox rtb = (RadTextBox)edititem.FindControl("txtCageType");

            RadListBox rlbFrom = (RadListBox)edititem.FindControl("RadListBoxFrom");
            RadListBox rlbTo = (RadListBox)edititem.FindControl("RadListBoxTo");

            Label lblCageType = (Label)edititem.FindControl("lblCageType");
            RadTextBox txtCageType = (RadTextBox)edititem.FindControl("txtCageType");

            rlbFrom.Items.Clear();
            if (combobox.SelectedValue == "-1")
            {
                rlbFrom.Items.Clear();
            }
            else
            {
                StoreDelivCriteriaDAO storeDelivCriteria = new StoreDelivCriteriaDAO();
                DataTable dtServices = storeDelivCriteria.GetCarrierServices(combobox.SelectedValue).Tables[0];

                foreach (DataRow dr in dtServices.Rows)
                {
                    rlbFrom.Items.Add(new RadListBoxItem(dr["carrier_service_descr"] as string, dr["carrier_service_id"] as string));

                }
            }
        }

        protected void StoreDelivGroupsRadGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            Int64 storeDeliveryGroupId = Int64.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["group_id"].ToString());
            string storeDeliveryGroupName = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["group_name"].ToString();
            StoreDelivGrpDAO storeDelivGrp = new StoreDelivGrpDAO();
            try
            {
                storeDelivGrp.RemoveStoreDeliveryGroupType(storeDeliveryGroupId);
                DisplayMessage(false, "Group " + storeDeliveryGroupName + " removed");
            }
            catch (Exception ex)
            {
                string messageline = ex.Message.Split('\n')[0].Substring(10);
                DisplayMessage(true, messageline);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
