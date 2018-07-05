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

public partial class LaneSetup : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // CagetypeServiceDAO cts = new CagetypeServiceDAO();
       // this.RadListBox1.DataSource = cts.GetCageServices("A").Tables[0];
       // DataBind();
 
    }


    protected void LanesRadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            LaneDAO lane = new LaneDAO();
            LanesRadGrid.DataSource = lane.GetLanes();
        }
    }


    protected void LanesRadGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        CagetypeServiceDAO cts = new CagetypeServiceDAO ();
        string cageTypeId = dataItem.GetDataKeyValue("cage_type_id").ToString();
        e.DetailTableView.DataSource = cts.GetCageServices(cageTypeId).Tables[0];
    }

    protected void LanesRadGrid_EditCommand(object sender, GridCommandEventArgs e)
    {
    }

    protected void LanesRadGrid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
        {
            GridEditCommandColumn editColumn = (GridEditCommandColumn)LanesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
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
            GridEditCommandColumn editColumn = (GridEditCommandColumn)LanesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
            if (!editColumn.Visible)
                editColumn.Visible = false;
        }

        if (e.CommandName == "print")
        {

            GridDataItem dataItem = (GridDataItem)e.Item;
            String scageid = dataItem.GetDataKeyValue("lane_id").ToString();
            Int32 icageid = Int32.Parse(scageid);



            string machinename = Shared.UserHostName; 
            string reportname = "7";
            string devicetype = "6";
            //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype + icageid);

            PrintService ps = new PrintService();
            string test = ps.PrintLabel(reportname, machinename, devicetype, icageid, true);
            //HttpContext.Current.Response.Write("after print" + test);

        }

    }

    protected void LanesRadGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        Control cont = e.Item.NamingContainer;
        if (e.Item is GridDataItem  && !e.Item.Expanded)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            int laneid = int.Parse(dataItem.GetDataKeyValue("lane_id").ToString());

            Button button = dataItem["DeleteColumn"].Controls[0] as Button;
            button.Attributes["onclick"] = "return confirm('Are you sure you want to unassign cage type?')";
        }

        if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
        {

            if (e.Item.ItemIndex < 0)
            {
                // Inserting .. should never come here

            }
            else
            {
                // Editing Lane to associate a cage type


                GridEditableItem editedItem = e.Item as GridEditableItem;
                
                string laneid = editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["lane_id"].ToString();
                DataRowView row = (DataRowView)e.Item.DataItem;

                RadComboBox rcbCageTypes = (RadComboBox)e.Item.FindControl("cagetype_radcombobox");
                CagetypeDAO cagetypedao = new CagetypeDAO();
                rcbCageTypes.DataSource = cagetypedao.GetCageTypes();
                rcbCageTypes.DataTextField = "cage_type_descr";
                rcbCageTypes.DataValueField = "cage_type_id";
                if (row.Row["cage_type_id"] !=  System.DBNull.Value)
                {
                    rcbCageTypes.SelectedValue = (string)row.Row["cage_type_id"];
                }
                rcbCageTypes.DataBind();

            }
        }

    }

    protected void LanesRadGrid_ItemEvent(object sender, GridItemEventArgs e)
    {

    }

    protected void RadListBoxTo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void LanesRadGrid_ItemUpdated(object sender, GridUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
            DisplayMessage(true, "Unable to (re)assign cage type to lane " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["lane_id"] + ". Reason: " + e.Exception.Message);
        }
        else
        {
            DisplayMessage(false, "Lane " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["lane_id"] + " cage type assigned");
        }
    }

    protected void LanesRadGrid_ItemInserted(object sender, GridInsertedEventArgs e)
    {

    }

    protected void LanesRadGrid_ItemDeleted(object sender, GridDeletedEventArgs e)
    {

    }

    private void DisplayMessage(bool isError, string text)
    {
        Label label = (isError) ? this.Label1 : this.Label2;
        label.Text = text;
    }

    protected void LanesRadGrid_InsertCommand(object sender, GridCommandEventArgs e)
    {
    }

    protected void LanesRadGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {

        RadComboBox rcbCagetype = (RadComboBox)e.Item.FindControl("cagetype_radcombobox");

        GridEditableItem edititem = e.Item as GridEditableItem;

        int laneid = int.Parse(edititem.OwnerTableView.DataKeyValues[edititem.ItemIndex]["lane_id"].ToString());

        LaneDAO lanedao = new LaneDAO();

        string cagetype = rcbCagetype.SelectedValue;

        try
        {
            lanedao.AddCageTypeLane(laneid, cagetype, User.Identity.Name);

            DisplayMessage(false, "Cage type " + cagetype + " assigned to lane " + laneid.ToString());
        }
        catch (Exception ex)
        {
             string messageline = ex.Message.Split('\n')[0].Substring(10);
             DisplayMessage(true, messageline);
        }
    }

  

    protected void LanesRadGrid_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }


    protected void LanesRadGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        GridEditableItem editedItem = e.Item as GridEditableItem;

        int laneid = int.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["lane_id"].ToString());
        LaneDAO lanedao = new LaneDAO();
        lanedao.RemoveCageTypeLane(laneid, User.Identity.Name);
        DisplayMessage(false, "Cage type unassigned from lane " + laneid.ToString());
    }

    protected void LanesRadGrid_ItemCreated(object sender, GridItemEventArgs e)
    {

    }

}
