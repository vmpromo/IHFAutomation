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

public partial class CageTypeSetup : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // CagetypeServiceDAO cts = new CagetypeServiceDAO();
       // this.RadListBox1.DataSource = cts.GetCageServices("A").Tables[0];
       // DataBind();
 
    }


    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            CagetypeDAO cagetype = new CagetypeDAO();
            CageTypesRadGrid.DataSource = cagetype.GetCageTypes().Tables[0];
        }
    }


    protected void CageTypesRadGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        CagetypeServiceDAO cts = new CagetypeServiceDAO ();
        string cageTypeId = dataItem.GetDataKeyValue("cage_type_id").ToString();
        e.DetailTableView.DataSource = cts.GetCageServices(cageTypeId).Tables[0];
    }

    protected void CageTypesRadGrid_EditCommand(object sender, GridCommandEventArgs e)
    {
    }

    protected void CageTypesRadGrid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
        {
            GridEditCommandColumn editColumn = (GridEditCommandColumn)CageTypesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
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
            GridEditCommandColumn editColumn = (GridEditCommandColumn)CageTypesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
            if (!editColumn.Visible)
                editColumn.Visible = true;
        }

    }

    protected void CageTypesRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {

        //if (e.Item is GridCommandItem)
        //{
        //    Button addButton = e.Item.FindControl("AddNewRecordButton") as Button;
        //    addButton.Visible = false;
        //    LinkButton lnkButton = (LinkButton)e.Item.FindControl("InitInsertButton");
        //    lnkButton.Visible = false;
        //} 
 
        //Control cont = e.Item.NamingContainer;
        if (e.Item is GridDataItem  && !e.Item.Expanded)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            string contactName = dataItem.GetDataKeyValue("cage_type_id").ToString();

            Button button = dataItem["DeleteColumn"].Controls[0] as Button;
            button.Attributes["onclick"] = "return confirm('Are you sure you want to delete cage type " +
            contactName + "?')";
        }

        if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
        {

            if (e.Item.ItemIndex < 0)
            {
                // Inserting Cage Type

                GridEditableItem editedItem = e.Item as GridEditableItem;


                ((RadTextBox)e.Item.FindControl("txtCageType")).Enabled = true;

                ((Label)(e.Item.FindControl("lblCageType"))).Visible = false;

                RadComboBox rcbCarriers = (RadComboBox)e.Item.FindControl("rcbCarriers");
                RadComboBox rcbCarriers2 = (RadComboBox)e.Item.FindControl("rcbCarriers2");
                CarrierDAO carrier = new CarrierDAO();
                rcbCarriers.DataSource = carrier.GetCarriers();
                rcbCarriers.DataTextField = "carrier_descr";
                rcbCarriers.DataValueField = "carrier_id";
                rcbCarriers.Items.Add(new RadComboBoxItem("Select Carrier"));
                rcbCarriers.DataBind();
                RadComboBoxItem rcbItem = new RadComboBoxItem("Select Carrier", "-1");
                rcbCarriers.Items.Add(rcbItem);
                rcbCarriers.SelectedValue = "-1";

                rcbCarriers2.DataSource = carrier.GetCarriers();
                rcbCarriers2.DataTextField = "carrier_descr";
                rcbCarriers2.DataValueField = "carrier_id";
                rcbCarriers2.Items.Add(new RadComboBoxItem("Select Carrier"));
                rcbCarriers2.DataBind();
                rcbCarriers2.Items.Add(rcbItem);
                rcbCarriers2.SelectedValue = "-1";

                RadComboBox rcbStoreDeliveryGroup = (RadComboBox)e.Item.FindControl("rcbStoreDeliveryGroup");
                StoreDeliveryGroupDAO storeDeliveryGroup = new StoreDeliveryGroupDAO();
                rcbStoreDeliveryGroup.DataSource = storeDeliveryGroup.GetStoreDeliveryGroups();
                rcbStoreDeliveryGroup.DataTextField = "group_name";
                rcbStoreDeliveryGroup.DataValueField = "group_id";
                rcbStoreDeliveryGroup.DataBind();
                RadComboBoxItem rcbItemStoreDeliveryGroup = new RadComboBoxItem("None", "-1");
                rcbStoreDeliveryGroup.Items.Add(rcbItemStoreDeliveryGroup);
                rcbStoreDeliveryGroup.SelectedValue = "-1";
                
                CagetypeDAO cagetypedao = new CagetypeDAO();
                RadComboBox rcbCntryGrps = (RadComboBox)e.Item.FindControl("rcbCntryGrps");
                rcbCntryGrps.DataSource = cagetypedao.GetCountryGroups();
                rcbCntryGrps.DataTextField = "country_group_descr";
                rcbCntryGrps.DataValueField = "country_group_id";
                rcbCntryGrps.DataBind();
                RadComboBoxItem rcbitemCntryGrp = new RadComboBoxItem("All", "-1");
                rcbCntryGrps.Items.Add(rcbitemCntryGrp);
                rcbCntryGrps.SelectedValue = "-1";

            }
            else
            {
                // Editing Cage Type


                GridEditableItem editedItem = e.Item as GridEditableItem;
                
                string cagetype = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cage_type_id"].ToString();
                DataRowView row = (DataRowView)e.Item.DataItem;

                ((Label)(e.Item.FindControl("lblCageType"))).Visible = true;
                ((Label)(e.Item.FindControl("lblCageType"))).Text = cagetype;

                ((RadTextBox)(e.Item.FindControl("txtCageType"))).Visible = false;


                ((RadTextBox)e.Item.FindControl("txtCageTypeDescr")).Text = (string)row.Row["cage_type_descr"];

                string despatchable_ind = (string)row.Row["despatchable_ind"];
                if (despatchable_ind == "T")
                {
                    ((CheckBox)e.Item.FindControl("cbDespatchableInd")).Checked = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("cbDespatchableInd")).Checked = false;
                }

                RadComboBox rcbCarriers = (RadComboBox)e.Item.FindControl("rcbCarriers");
                CarrierDAO carrier = new CarrierDAO();
                rcbCarriers.DataSource = carrier.GetCarriers();
                rcbCarriers.DataTextField = "carrier_descr";
                rcbCarriers.DataValueField = "carrier_id";
                rcbCarriers.SelectedValue = (string)row.Row["carrier_id"];
                rcbCarriers.DataBind();

                RadComboBox rcbCarriers2 = (RadComboBox)e.Item.FindControl("rcbCarriers2");
                rcbCarriers2.DataSource = carrier.GetCarriers();
                rcbCarriers2.DataTextField = "carrier_descr";
                rcbCarriers2.DataValueField = "carrier_id";
                rcbCarriers2.SelectedValue = (string)row.Row["carrier_id"];
                rcbCarriers2.DataBind();

                RadComboBox rcbStoreDeliveryGroup = (RadComboBox)e.Item.FindControl("rcbStoreDeliveryGroup");
                StoreDeliveryGroupDAO storeDeliveryGroup = new StoreDeliveryGroupDAO();
                rcbStoreDeliveryGroup.DataSource = storeDeliveryGroup.GetStoreDeliveryGroups();
                rcbStoreDeliveryGroup.DataTextField = "group_name";
                rcbStoreDeliveryGroup.DataValueField = "group_id";
                rcbStoreDeliveryGroup.DataBind();
                RadComboBoxItem rcbItemStoreDeliveryGrp = new RadComboBoxItem("None", "-1");
                rcbStoreDeliveryGroup.Items.Add(rcbItemStoreDeliveryGrp);

                if (row.Row["sd_group_id"] is System.DBNull)
                {
                    rcbStoreDeliveryGroup.SelectedValue = "-1";
                }
                else
                {
                    Int64 stgroupid = (Int64)row.Row["sd_group_id"];
                    rcbStoreDeliveryGroup.SelectedValue = stgroupid.ToString();
                }


                CagetypeDAO cagetypedao = new CagetypeDAO();
                RadComboBox rcbCntryGrps = (RadComboBox)e.Item.FindControl("rcbCntryGrps");
                rcbCntryGrps.DataSource = cagetypedao.GetCountryGroups();
                rcbCntryGrps.DataTextField = "country_group_descr";
                rcbCntryGrps.DataValueField = "country_group_id";
                rcbCntryGrps.DataBind();
                RadComboBoxItem rcbitemCntryGrp = new RadComboBoxItem("All", "-1");
                rcbCntryGrps.Items.Add(rcbitemCntryGrp);

                if (row.Row["country_group_id"] is System.DBNull)
                {
                    rcbCntryGrps.SelectedValue = "-1";
                }
                else
                {
                    rcbCntryGrps.SelectedValue = (string)row.Row["country_group_id"];
                }
                RadListBox rlbFrom = (RadListBox)e.Item.FindControl("RadListBoxFrom");
                RadListBox rlbTo = (RadListBox)e.Item.FindControl("RadListBoxTo");
                CagetypeServiceDAO cts = new CagetypeServiceDAO();
                rlbFrom.DataSource = cts.GetAvailableServices(cagetype).Tables[0];
                rlbTo.DataSource = cts.GetCageServices(cagetype).Tables[0];
                rlbFrom.DataBind();
                rlbTo.DataBind();
            }
        }

    }

    protected void CageTypesRadGrid_ItemEvent(object sender, GridItemEventArgs e)
    {

    }

    protected void RadListBoxTo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CageTypesRadGrid_ItemUpdated(object sender, GridUpdatedEventArgs e)
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

    protected void CageTypesRadGrid_ItemInserted(object sender, GridInsertedEventArgs e)
    {

    }

    protected void CageTypesRadGrid_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }

    private void DisplayMessage(bool isError, string text)
    {
        Label label = (isError) ? this.Label1 : this.Label2;
        label.Text = text;
    }

    protected void CageTypesRadGrid_InsertCommand(object sender, GridCommandEventArgs e)
    {
        RadTextBox txtCageType = (RadTextBox)e.Item.FindControl("txtCageType");
        RadTextBox txtCageTypeDescr = (RadTextBox)e.Item.FindControl("txtCageTypeDescr");
        RadComboBox rcbCarrier = (RadComboBox)e.Item.FindControl("rcbCarriers");
        RadComboBox rcbCntryGrps = (RadComboBox)e.Item.FindControl("rcbCntryGrps");
        CheckBox cbDespatchableInd = (CheckBox)e.Item.FindControl("cbDespatchableInd");
        RadComboBox rcbStoreDeliveryGroup = (RadComboBox)e.Item.FindControl("rcbStoreDeliveryGroup");

        if (txtCageType.Text.Length > 0 && txtCageTypeDescr.Text.Length > 0 && rcbCarrier.SelectedValue != "-1")
        {

            try
            {

                string cageType = txtCageType.Text;
                string cageTypeDescr = txtCageTypeDescr.Text;
                string carrierID = rcbCarrier.SelectedValue;
                string userLogin = User.Identity.Name;
                string carrierGrpId = rcbCntryGrps.SelectedValue == "-1" ? "" : rcbCntryGrps.SelectedValue;
                string despatchableInd = cbDespatchableInd.Checked == true ? "T" : "F";
                Int64? storeDeliveryGroup = Int64.Parse(rcbStoreDeliveryGroup.SelectedValue);
                if (storeDeliveryGroup < 0) storeDeliveryGroup = null;

                CagetypeDAO cage = new CagetypeDAO();
                cage.CreateCageType(cageType, cageTypeDescr, carrierID, carrierGrpId, despatchableInd, storeDeliveryGroup, userLogin);

                RadListBox rlbTo = (RadListBox)e.Item.FindControl("RadListBoxTo");

                CagetypeServiceDAO cageservice = new CagetypeServiceDAO();
                foreach (RadListBoxItem rlbItem in rlbTo.Items)
                {
                    cageservice.AddServiceToCageType(cageType.ToUpper(), rlbItem.Value, User.Identity.Name);

                }
                CageTypesRadGrid.MasterTableView.IsItemInserted = true;
                DisplayMessage(false, "Cage Type " + cageType + " inserted");
                CageTypesRadGrid.Rebind();
            }
            catch (Exception ex)
            {
                string messageline = ex.Message.Split('\n')[0].Substring(10);
                DisplayMessage(true, messageline);
            }

        }
    }

    protected void CageTypesRadGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {

        Label txtCageType = (Label)e.Item.FindControl("lblCageType");
        RadTextBox txtCageTypeDescr = (RadTextBox)e.Item.FindControl("txtCageTypeDescr");
        RadComboBox rcbCarrier = (RadComboBox)e.Item.FindControl("rcbCarriers");
        RadComboBox rcbCntryGrps = (RadComboBox)e.Item.FindControl("rcbCntryGrps");
        CheckBox cbDespatchableInd = (CheckBox)e.Item.FindControl("cbDespatchableInd");
        RadComboBox rcbStoreDeliveryGroup = (RadComboBox)e.Item.FindControl("rcbStoreDeliveryGroup");

        if (txtCageType.Text.Length > 0 && txtCageTypeDescr.Text.Length > 0 && rcbCarrier.SelectedValue != "-1")
        {

            try
            {
                string cageType = txtCageType.Text;
                string cageTypeDescr = txtCageTypeDescr.Text;
                string carrierID = rcbCarrier.SelectedValue;
                string userLogin = User.Identity.Name;
                string carrierGrpId = rcbCntryGrps.SelectedValue == "-1" ? "" : rcbCntryGrps.SelectedValue;
                string despatchableInd = cbDespatchableInd.Checked == true ? "T" : "F";
                Int64? storeDeliveryGroup = Int64.Parse(rcbStoreDeliveryGroup.SelectedValue);
                if (storeDeliveryGroup < 0) storeDeliveryGroup = null;

                CagetypeDAO cage = new CagetypeDAO();
                cage.UpdateCageTypeDetails(cageType.ToUpper(), cageTypeDescr, carrierID, carrierGrpId, despatchableInd, storeDeliveryGroup, userLogin);

                RadListBox rlbTo = (RadListBox)e.Item.FindControl("RadListBoxTo");
                RadListBox rlbFrom = (RadListBox)e.Item.FindControl("RadListBoxFrom");

                CagetypeServiceDAO cageservice = new CagetypeServiceDAO();

                foreach (RadListBoxItem rlbItem in rlbFrom.Items)
                {
                    cageservice.RemvoveServiceToCageType(cageType.ToUpper(), rlbItem.Value, User.Identity.Name);
                }

                foreach (RadListBoxItem rlbItem in rlbTo.Items)
                {
                    cageservice.AddServiceToCageType(cageType.ToUpper(), rlbItem.Value, User.Identity.Name);

                }
                DisplayMessage(false, "Cage Type " + cageType + " updated");
                CageTypesRadGrid.Rebind();
            }
            catch (Exception ex)
            {
                string messageline = ex.Message.Split('\n')[0].Substring(10);
                DisplayMessage(true, messageline);
            }

        }

    }

  


    protected void CageTypesRadGrid_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CageTypesRadGrid.EditIndexes.Add(0);
            //CageTypesRadGrid.Rebind();
        }
    }

    protected void rcbCarriers2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        RadComboBox combobox = (RadComboBox)sender;
        GridEditFormItem edititem = (GridEditFormItem)combobox.NamingContainer;

        RadTextBox rtb = (RadTextBox)edititem.FindControl("txtCageType");

        RadListBox rlbFrom = (RadListBox)edititem.FindControl("RadListBoxFrom");
        RadListBox rlbTo = (RadListBox)edititem.FindControl("RadListBoxTo");

        Label lblCageType = (Label)edititem.FindControl("lblCageType");
        RadTextBox txtCageType = (RadTextBox)edititem.FindControl("txtCageType");

        rlbFrom.Items.Clear();
        CagetypeServiceDAO cts = new CagetypeServiceDAO();
        DataTable dtServices = cts.GetCarrierServices(combobox.SelectedValue).Tables[0];

        foreach (DataRow dr in dtServices.Rows)
        {
            rlbFrom.Items.Add(new RadListBoxItem(dr["carrier_service_descr"] as string, dr["carrier_service_id"] as string));

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
            CagetypeServiceDAO cts = new CagetypeServiceDAO();
            DataTable dtServices = cts.GetCarrierServices(combobox.SelectedValue).Tables[0];

            foreach (DataRow dr in dtServices.Rows)
            {
                rlbFrom.Items.Add(new RadListBoxItem(dr["carrier_service_descr"] as string,dr["carrier_service_id"] as string));

            }
        }
    }

    protected void CageTypesRadGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        string cagetype = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cage_type_id"].ToString();
        CagetypeDAO cage = new CagetypeDAO();
        try
        {
            cage.RemoveCageType(cagetype);
            DisplayMessage(false, "Cage Type " + cagetype + " removed");
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
