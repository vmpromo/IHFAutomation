// Name: Returns.aspx.cs
// Type: class file 
// Description: Code behind class for Returns screen
//
//$Revision:   1.5  $
//
// Version   Date        Author     Reason
//  1.0      07/12/12    J Watt     Initial Revision
//           10/12/12    J Watt     Intermediate checkin
//           11/12/12    J Watt     Intermediate checkin
//           19/12/12    J Watt     No Change
//           21/12/12    J Watt     No Change
//           07/01/13    J Watt     Changes after DC testing
//           17/01/13    J Watt     No Change
//           17/01/13    J Watt     No Change
//           17/01/13    J Watt     Dont allow unsaved changes
//           21/01/13    J Watt     No Change
//           22/01/13    J Watt     No Change
//           21/02/13    J Watt     No Change
//           26/07/13    J Watt     No Change
//           05/09/13    J Watt     Dont allow clicking out of scan box
//           05/09/13    J Watt     Dont allow clicking out of scan box
//           09/05/17    M Cackett  Cross border returns changes.
//           18/05/17    M Cackett  CBR call proc to process refund in store.
//           21/06/17    M Cackett  Allow DC users to change status from Refunded Return.
//           26/09/17    M Cackett  CBR Bug fix for Store id/ till id
//           11/10/17    M Cackett  Prevent store users from using CBR app to process
//                                  Store Orders.  Also fixed compiler warning message.
//  1.5     12/04/18    M Cackett  Reprint LPN for Supervisor
//                       S Remedios           


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IHF.BusinessLayer.DataAccessObjects.Returns;
using Telerik.Web.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System.Configuration;
using IHF.Security.UserManagement;

namespace IHF.ApplicationLayer.Web.Pages.Returns
{
    public partial class ReturnsDC : System.Web.UI.Page
    {
#region constants
        private const int    EDITCHECKBOXCELL = 0; // Grid cell for editing checkbox
        private const int    ORDERNUMBERCELL = 3;   // Cell number of getting order number in grid (Customer search results)
        private const int    ACTIONREASONCELL = 4; // Cell number for ddl list of selecing reason
        private const int    EDITICONCELL = 2;   // Cell in grid for edit icon
        private const int    SKUVALCELL = 1;     // Cell in grid for SKU value
        private const string REFUNDEDRETURN = "100";
        private const string CUSTOMERSERVICERTN = "70";
        private const string STOREORDERERRMSG = 
            "The order being returned is a Store Order. Please process this return as per the usual Store Order returns process.";
        private const string ORDERSOURCESTORE = "STORE";
#endregion

        private string _skuToFind = string.Empty;
        private int? _ordernumber= null;
        private DataTable _dt;
        private DataTable _actions;
        private DataTable _actionsCBR;  // Subset of _actions used for CBR return from store received in DC
        private Hashtable _itemHashTable = new Hashtable();
        private ReturnsDAO _dao = new ReturnsDAO();
        private int _focusindex = -1; // Index in table to set focus or -2 if the order number or -1 if the SKU scan.
        private int _alreadyCancelled = 0;
        private int _tabselected = 0;
        protected DataSet ds;

        // New members used by Cross Border Returns where screen is used from store till.
        private string _userID;
        private string _storeID;
        private string _tillID;
        private string _storeUser = "N";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            // This is really yuck but can't but this in the aspx page because a postback occurs when changing the client id mode to fixed
            // When a post back occurs this then fires the windows.beforeunload event which checks whether navigating away.
            //  The script tag should be necessary either but....
            //string s = 
            //    "<script type=\"text/javascript\">" +
            //"function handleMouseEvent()  {" +
            //"    var idSource = window.event.srcElement.id; " +
            ////"    alert(idSource); " + 
            //"    if (idSource == \"\" || idSource.indexOf('RadPageView') != -1) { " +
            //" var tb = document.getElementById( 'scanText'); " +
            //" tb.focus(); " +
            //"     } " +
            //"}" +"</script>";
            //string csname = "MouseHandlerScript";
            //Type cstype = this.GetType();

            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname))
            //{
            //    cs.RegisterClientScriptBlock(cstype, csname, s);
            //}

            
            // For Cross border returns, we need to read the user, store and till info from
            // the URL querystring.

            if (Request.QueryString["username"] != null)
            {
                _userID = Request.QueryString["username"];
                _storeUser = "Y";
                RadTabStrip1.Tabs[1].Visible = false;  // Store user has no access to customer search
                //Button4.Visible = false;               // or to the Print button (Button4).
            }

            if (Request.QueryString["device"] != null)
            {
                String[] deviceDetails = Request.QueryString["device"].Split('.');
                _storeID = deviceDetails[2];
                _tillID = deviceDetails[3];
            }


            PopulateActionsOnPage();
            

            _itemHashTable = (Hashtable)ViewState["itemsprocessed"];

            _tabselected = ViewState["tabselected"] == null ? 0 : (int)ViewState["tabselected"];

            RadTabStrip1.Tabs[_tabselected].Selected = true;
            RadMultiPage1.SelectedIndex = _tabselected;

            //lblMessage.Text = "";
            //lblMessage.Visible = false;
            //btnReject.Attributes.Add("onclick", "if(confirm('Are you sure?')){}else{return false}");


            if (ViewState["ordernumber"] != null) _ordernumber = (int)ViewState["ordernumber"];
            else _ordernumber = null;



        }

        private void PopulateActionsOnPage()
        {
            var scriptTemplate = @"
    <script>
        var refundParams = refundParams || {6}
            isStoreUser: {0},
            storeId: {1},
            tillId: {2},
            userId: {3},
            actions: {4},
            supervisor: {5}
            
        {7};
    </script>";

            var script = string.Format(
                scriptTemplate,
                (_storeUser == "Y").ToString().ToLower(),   //0
                NullOrQuotedValue(_storeID),                //1 
                NullOrQuotedValue(_tillID),                 //2
                NullOrQuotedValue(_userID),                 //3
                ComputeActions(),                           //4
                IsSupervisor().ToString().ToLower(),        //5
                "{",                                        //6
                "}"                                         //7
                );

            
            string actionsSetupScript = "ActionsSetup";
            Type pageType = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(pageType, actionsSetupScript))
            {
                cs.RegisterClientScriptBlock(pageType, actionsSetupScript, script);
            }
        }

        private Boolean IsSupervisor()
        {
            // Assume not supervisor
            Boolean supervisor = false;

            // Get the list of user groups which have access to the advanced packing funcionality
            string[] advancedPackingGroups = ConfigurationManager.AppSettings["AdvancedPackingGroups"].Split(',');

            IHFRoleProvider roleprovider = new IHFRoleProvider();

            // Now determine if one of these groups is 
            foreach (string grp in advancedPackingGroups)
            {
                if (roleprovider.IsUserInRole(User.Identity.Name, grp))
                {
                    supervisor = true;
                }
            }

            return supervisor;

        }




        private string NullOrQuotedValue(string value) {
            if(value == null)
            {
                return "null";
            }
            return string.Format("'{0}'", value);
        }


        private class __Action
        {
            public string Id;
            public string Description;
            public string[] DisableForStatus;
            public bool IsStoreAction;
        }

        private string ComputeActions()
        {
            var actionsDcDataSet      = _dao.getReturnActions("N").Tables[0];
            var actionsStoreDataSet = _dao.getReturnActions("Y").Tables[0];
            // Make a copy of the _actions data table for use by CBR
            // this version of the table will not include the
            // customer services option.
            //_actionsCBR = _actions.Clone();
            //foreach (DataRow dr in _actions.Rows)
            //{
            //    if (dr["actioncode"].ToString() != CUSTOMERSERVICERTN)
            //    {
            //        _actionsCBR.Rows.Add(dr.ItemArray);
            //    }
            //}
            Func<DataRow, bool, __Action> rowProcessFunction = (dataRow, isStoreAction) =>
            {
                var actionCode = dataRow["actioncode"].ToString();
                return new __Action
                {
                    Id = actionCode,
                    Description = dataRow["description"].ToString(),
                    DisableForStatus = actionCode == CUSTOMERSERVICERTN ? new[] { REFUNDEDRETURN } : new string[0],
                    IsStoreAction = isStoreAction
                };
            };

            

            var actionsDC = actionsDcDataSet.Rows.Cast<DataRow>().Select(row => rowProcessFunction(row, false)).ToList();
            var actionStore = actionsStoreDataSet.Rows.Cast<DataRow>().Select(row => rowProcessFunction(row, true)).ToList();

            return string.Format(
                "[{0}]",
                string.Join(
                    ",\n                ",
                    actionsDC.Union(actionStore).Select(action => "{ " + string.Format(
                        "Id: {0}, Description: '{1}', DisabledForStatus: [{2}], IsStoreAction: {3}",
                        action.Id,
                        action.Description,
                        string.Join(", ", action.DisableForStatus),
                        action.IsStoreAction.ToString().ToLower()
                    ) + " }")));
        }

        protected DataSet GetOrder(string ordernumberstring)
        {
            DataSet dsorder = _dao.getOrderDetails(ordernumberstring);

            return dsorder;
        }

        protected void populateOrderDetails(DataSet dsorder)
        {
            //gvItems.EditIndex = -1;
            //if (dsorder.Tables[0].Rows.Count > 0)
            //{
            //    DataRow dr = dsorder.Tables[0].Rows[0];
            //    _ordernumber = int.Parse(dr.Table.Rows[0]["ordernumber"].ToString());
            //    ViewState["parcelscannedind"] = dr.Table.Rows[0]["parcel_scanned_ind"].ToString();
            //    lblOrdernumber.Text = _ordernumber.ToString();
            //    lblOrderDate.Text = ((DateTime)dr.Table.Rows[0]["orderdate"]).ToString("dd-MMM-yyyy");
            //    lblCustomerUrn.Text = dr.Table.Rows[0]["customerurn"].ToString();
            //    lblTotalItems.Text = dr.Table.Rows[0]["totalitems"].ToString();
            //    lblCustomerName.Text = dr.Table.Rows[0]["customername"].ToString();
            //    lblPostCode.Text = dr.Table.Rows[0]["postcode"].ToString();
            //    lblMessage.Visible = false;
            //    lblMessage.Text = "";
 
            //}
            //else
            //{
            //    lblMessage.Text = "Order " + _ordernumber.ToString() + " not found";
            //    lblMessage.Visible = true;
            //}

        }


        protected void BindGrid()
        {
            //if (ViewState["dt"] == null)
            //{
            //    gvItems.DataSource = createDataTable();
            //    gvItems.DataBind();
            //}
            //else
            //{
            //    gvItems.DataSource = ViewState["dt"] as DataTable;
            //    gvItems.DataBind();
            //}
        }

        protected DataTable createDataTable()
        {
            if (_ordernumber != null)
            {                
                _dt = _dao.getItemsToReturn((int)_ordernumber).Tables[0];
            }

            ViewState["dt"] = _dt;
            return _dt;
        }


        protected void RadScriptManager1_PreRender(object sender, EventArgs e)
        {

        }

        protected void displayOrder()
        {
            //ReturnsDAO dao = new ReturnsDAO();
            //bool skuaAlreadyScanned = false;
            //string scannstring = Regex.Replace(tbPackageBarcode.Text, "[^.0-9]", "");
            //tbPackageBarcode.Text = "";
            //lblMessage.Visible = false;
            //lblItemsReturned.Text = "";
            //lblItemsReturned.Visible = false;

            //if (_ordernumber != null)
            //{
            //    try
            //    {
            //        _skuToFind = dao.getSKU(scannstring);
            //    }
            //    catch
            //    {
            //        // Not a SKU so if we are not yet editing an order - try and see if it is a valid order
            //        if (_itemHashTable == null || _itemHashTable.Count == 0)
            //        {
            //            DataSet dsorder = GetOrder(scannstring);
            //            if (dsorder.Tables[0].Rows.Count > 0)
            //            {
            //                // If we are called from a store, and the returned order is a store order then
            //                // display error message and don't allow the user to continue.
            //                if (_storeUser == "Y" && _dao.getOrderSource(scannstring) == ORDERSOURCESTORE)
            //                {
            //                    lblMessage.Text = STOREORDERERRMSG;
            //                    lblMessage.Visible = true;
            //                    tbPackageBarcode.Text = "";
            //                    return;
            //                }
            //                populateOrderDetails(dsorder);
            //                _focusindex = -1;
            //                tbPackageBarcode.Text = "";
            //                createDataTable();
            //                BindGrid();

            //                return;
            //            }
            //            else
            //            {
            //                lblMessage.Visible = true;
            //                lblMessage.Text = "Invalid item/order: " + scannstring;
            //                tbPackageBarcode.Text = "";
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Text = "Invalid item: " + scannstring;
            //            tbPackageBarcode.Text = "";
            //            return;
            //        }
            //    }
            //    int inx = 0;
            //    _itemHashTable = (Hashtable)ViewState["itemsprocessed"];

            //    foreach (GridViewRow gridrow in gvItems.Rows)
            //    {
            //        CheckBox cb = (CheckBox)gridrow.Cells[EDITCHECKBOXCELL].FindControl("cbEdited");
            //        bool cbenabled = cb != null && cb.Enabled ? true : false;
            //        if (gridrow.Cells[SKUVALCELL].Text == _skuToFind && cbenabled)
            //        {
            //            if (_itemHashTable == null || !_itemHashTable.Contains(inx))
            //            {
            //                gvItems.EditIndex = inx;
            //                _focusindex = inx;
            //                lblMessage.Text = "";
            //                lblMessage.Visible = false;
            //                BindGrid();
            //                return;
            //            }
            //            skuaAlreadyScanned = true;
            //        }
            //        inx++;
            //    }
            //    // If no SKUs have been processed and an order number has been entered then change to processing new order
            //    if (_itemHashTable == null || _itemHashTable.Count == 0)
            //    {
            //        DataSet dsorder = GetOrder(scannstring);
            //        if (dsorder.Tables[0].Rows.Count > 0)
            //        {
            //            // If we are called from a store, and the returned order is a store order then
            //            // display error message and don't allow the user to continue.
            //            if (_storeUser == "Y" && _dao.getOrderSource(scannstring) == ORDERSOURCESTORE)
            //            {
            //                lblMessage.Text = STOREORDERERRMSG;
            //                lblMessage.Visible = true;
            //                tbPackageBarcode.Text = "";
            //                return;
            //            }
            //            populateOrderDetails(dsorder);
            //            _focusindex = -1;
            //            tbPackageBarcode.Text = "";
            //            createDataTable();
            //            BindGrid();
            //            return;
            //        }
            //        else
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Text = skuaAlreadyScanned ? "SKU " + _skuToFind + " already scanned" : "SKU " + _skuToFind + " not found on order";
            //        }
            //    }
            //    else
            //    {
            //        lblMessage.Visible = true;
            //        lblMessage.Text = skuaAlreadyScanned ? "SKU " + _skuToFind + " already scanned" : "SKU " + _skuToFind + " not found on order";
            //    }
            //}
            //else
            //{
            //    // If we are called from a store, and the returned order is a store order then
            //    // display error message and don't allow the user to continue.
            //    if (_storeUser == "Y" && _dao.getOrderSource(scannstring) == ORDERSOURCESTORE)
            //    {
            //        lblMessage.Text = STOREORDERERRMSG;
            //        lblMessage.Visible = true;
            //        tbPackageBarcode.Text = "";
            //        return;
            //    }
            //    populateOrderDetails(GetOrder(scannstring));
            //    _focusindex = -1;
            //    tbPackageBarcode.Text = "";
            //    createDataTable();
            //    BindGrid();
            //}
         

        }
 
        protected void rtbPackageBarcode_TextChanged(object sender, EventArgs e)
        {
            displayOrder();
 
        }



        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    // Set row colour
            //    if (gvItems.EditIndex == e.Row.DataItemIndex)
            //    {   // Row being edited
            //        e.Row.BackColor = System.Drawing.Color.Azure;
            //    }
            //    else if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
            //    {  //Row with action 
            //        e.Row.BackColor = System.Drawing.Color.Beige;
            //    }
            //    else
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Bisque;
            //    }

            //    if (e.Row.RowState == DataControlRowState.Edit)
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Azure;
            //    }
            //    CheckBox cb = (CheckBox)e.Row.Cells[EDITCHECKBOXCELL].FindControl("cbEdited");
            //    CheckBox cb2 = (CheckBox)e.Row.Cells[EDITCHECKBOXCELL].FindControl("cbEditing");
            //    e.Row.Cells[EDITICONCELL].Controls[EDITCHECKBOXCELL].Visible = false;

            //    DataRowView drv = (DataRowView)e.Row.DataItem;
            //    string actioncode = drv.Row.ItemArray[4].ToString();
            //    if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
            //    {

            //        if (gvItems.EditIndex != e.Row.DataItemIndex)
            //            e.Row.Cells[EDITICONCELL].Controls[0].Visible = true;
            //        if (cb != null) cb.Checked = true;
            //    }
            //    else
            //    {
            //        if (cb != null)
            //        {
            //            cb.Checked = false;
            //        }

            //        // If we are being called from the DC (_storeUser == "N")
            //        // and this row has action code 100 (refunded return)
            //        // then allow the row to be edited so that DC user
            //        // can move the status on to saleable/mispick/damaged.
            //        if (actioncode == REFUNDEDRETURN && _storeUser == "N")
            //        {
            //            if (cb != null) cb.Enabled = true;
            //            e.Row.BackColor = System.Drawing.Color.Yellow;
            //        }
            //        // Not refunded retarun and/or not in DC
            //        // so disable the row if action code previouslyy selected.
            //        else if (actioncode != string.Empty)
            //        {
            //            _alreadyCancelled++;
            //            if (cb != null) cb.Enabled = false;
            //            e.Row.BackColor = System.Drawing.Color.Chocolate;
            //        }
            //    }

            //    Label lbl = (Label)e.Row.FindControl("lbAction");
            //    if (lbl != null)
            //    {
            //        if (_itemHashTable != null && _itemHashTable.Contains(e.Row.DataItemIndex))
            //        {
            //            string ss = _itemHashTable[e.Row.DataItemIndex].ToString();
            //            _ReturnAction act = (_ReturnAction)_itemHashTable[e.Row.DataItemIndex];

            //            lbl.Text = act.actionDescr;
            //        }
            //        else
            //        {
            //            DataRowView dr = (DataRowView)e.Row.DataItem;
            //            string actioncodedescr = dr.Row.ItemArray[6].ToString();
            //            lbl.Text = actioncodedescr;
            //        }
            //    }
            //    Control ctrl = e.Row.FindControl("ddlAct");
            //    if (ctrl != null)
            //    {

            //        DropDownList ddl = (DropDownList)ctrl;
            //        if (actioncode == REFUNDEDRETURN && _storeUser == "N")
            //        {
            //            ddl.DataSource = _actionsCBR;
            //        }
            //        else
            //        {
            //            ddl.DataSource = _actions;
            //        }

            //        ddl.DataTextField = "description";
            //        ddl.DataValueField = "actioncode";
            //        ddl.DataBind();
            //        ddl.Items.Add(new ListItem("Select Reason", "-1"));
            //        if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
            //        {
            //            ddl.SelectedValue = ((_ReturnAction)(_itemHashTable[e.Row.RowIndex])).actionCode;
            //            if (ddl.SelectedValue == "70")
            //            {
            //                ((TextBox)e.Row.FindControl("tbTaskDesc")).Text = ((_ReturnAction)(_itemHashTable[e.Row.RowIndex])).taskDescription;
            //                ((TextBox)e.Row.FindControl("tbTaskDesc")).Visible = true;
            //                ((Button)e.Row.FindControl("btnFinish")).Visible = true;
            //            }
            //        }
            //        else
            //            ddl.SelectedValue = "-1"; 

            //    }
            //    if (cb2 != null)
            //    {
            //        if (gvItems.EditIndex == e.Row.DataItemIndex)
            //        {
            //            cb2.Checked = true;
            //        }
            //    }
            //}
        }

        protected void gvItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvItems.EditIndex = e.NewEditIndex;
            //_focusindex = e.NewEditIndex;
            //BindGrid();

        }

        protected void gvItems_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
           
        }

        protected void gvItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvItems.EditIndex = -1;
            //_focusindex = -1;
            //tbPackageBarcode.Text = "";
            //BindGrid();
        }


        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            //ViewState["itemsprocessed"] = _itemHashTable;
            //ViewState["ordernumber"] = _ordernumber;
            //ViewState["itemactions"] = _actions;

            //if (gvItems.Rows.Count > 0)
            //{
            //    pnlOrder.Visible = true;
            //    lblScan.Text = "Scan Item/Order: ";
            //}
            //else
            //{
            //    pnlOrder.Visible = false;
            //    lblScan.Text = "Scan Order/Package: ";
            //}

            //if (_itemHashTable != null)
            //{
            //    lblItemsToProcess.Text = (int.Parse(lblTotalItems.Text) - _itemHashTable.Count).ToString();
            //    if (_itemHashTable.Count > 0)
            //    {
            //        btnAccept.Visible = true;
            //        btnReject.Visible = true;
            //    }
            //    else
            //    {
            //    }
            //}
            //else
            //{
            //    lblItemsToProcess.Text = lblTotalItems.Text;
            //    btnAccept.Visible = false;
            //    btnReject.Visible = false;
            //}


            //if (_focusindex == -1)
            //    tbPackageBarcode.Focus();
            //else
            //{
            //    if (gvItems.EditIndex > -1 && gvItems.Rows[gvItems.EditIndex].Cells[ACTIONREASONCELL].FindControl("ddlAct") != null)
            //    {
            //        gvItems.Rows[gvItems.EditIndex].Cells[ACTIONREASONCELL].FindControl("ddlAct").Focus();
            //    }
            //}

            _tabselected = ViewState["tabselected"] == null ? 0 : (int)ViewState["tabselected"];

            RadTabStrip1.Tabs[_tabselected].Selected = true;
            RadMultiPage1.SelectedIndex = _tabselected;

        }

        protected void gvItems_DataBinding(object sender, EventArgs e)
        {
           
        }

        protected void gvItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
         

        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Edit"))
            //{
            //    int inx = int.Parse((string)e.CommandArgument);
            //    GridViewRow row = gvItems.Rows[inx];
            //    gvItems.EditIndex = inx;
            //   DropDownList ddl = (DropDownList)row.FindControl("ddlAct");
            //    if (ddl != null)
            //    {
            //        ddl.DataSource = _actions;
            //        string sv = ddl.SelectedValue;
            //        if (_itemHashTable == null) _itemHashTable = new Hashtable();
            //        if (_itemHashTable.ContainsKey(inx))
            //        {
            //            _itemHashTable.Remove(inx);
            //        }
            //        _ReturnAction act = new _ReturnAction();
            //        act.actionCode = sv;
            //        act.actionDescr = ddl.SelectedItem.Text;
            //        _itemHashTable.Add(inx, act);
            //        ViewState["itemsprocessed"] = _itemHashTable;
            //    }
            //    gvItems.EditIndex = -1;
            //    _focusindex = -1;
            //    tbPackageBarcode.Text = "";
            //    BindGrid();
            //}
        }

        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //int rowinx = -1;

            //string sv = ddl.SelectedValue;

            //_actions = (DataTable)ViewState["itemactions"];

            //if (_actions != null)
            //{

            //    foreach (DataRow dr in _actions.Rows)
            //    {
            //        if (dr["actioncode"].ToString() == sv)
            //        {
            //            TextBox tb = (TextBox)ddl.NamingContainer.FindControl("tbTaskDesc");
            //            Button btn = (Button)ddl.NamingContainer.FindControl("btnFinish");
            //            if (tb != null)
            //            {
            //                if (dr["createtask"].ToString() == "1")
            //                {
            //                    tb.Visible = true;
            //                    btn.Visible = true;
            //                }
            //                else
            //                {
            //                    tb.Visible = false;
            //                    btn.Visible = false;

            //                    if (ddl.SelectedValue != "-1")
            //                    {
            //                        //Find which row this is
            //                        foreach (GridViewRow row in gvItems.Rows)
            //                        {

            //                            Control ctrl = (Control)row.Cells[ACTIONREASONCELL].Controls[1];
            //                            rowinx++;
            //                            if (ctrl is DropDownList)
            //                            {
            //                               if (_itemHashTable == null) _itemHashTable = new Hashtable();
            //                               if (_itemHashTable.ContainsKey(rowinx))
            //                               {
            //                                  _itemHashTable.Remove(rowinx);
            //                               }
            //                               gvItems.EditIndex = -1;
            //                                _focusindex = -1;
            //                                tbPackageBarcode.Text = "";
            //                                _ReturnAction act = new _ReturnAction();
            //                                act.actionCode = sv;
            //                                act.actionDescr = ddl.SelectedItem.Text;
            //                                act.taskDescription = "";
            //                                _itemHashTable.Add(rowinx, act);
            //                                ViewState["itemsprocessed"] = _itemHashTable;
            //                                BindGrid();
            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //    }
            //}
        }


        protected void gvItems_DataBound(object sender, EventArgs e)
        {

        }

        //Process all returns
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            //ReturnsDAO dao = new ReturnsDAO();

            //_focusindex = -1;

            //DataTable dt = (DataTable)ViewState["dt"];

            //if (dt != null)
            //{
            //    for (int inx = 0; inx < dt.Rows.Count; inx++)
            //    {
            //        if (_itemHashTable.Contains(inx))
            //        {
            //            DataRow dr = dt.Rows[inx];
            //            _ReturnAction ra = (_ReturnAction)_itemHashTable[inx];

            //            string parcelScannedInd = ViewState["parcelscannedind"] == null || ViewState["parcelscannedind"].ToString() == "F" ? "F" : "T";
            //            int itemnumber = int.Parse(dr.ItemArray[0].ToString());
            //            int ordernumber = int.Parse(lblOrdernumber.Text);
            //            string actioncode = ra.actionCode;
            //            string taskdescr = ra.taskDescription;
            //            string customerurn = lblCustomerUrn.Text;
            //            string sku = (string)dr.ItemArray[2];
            //            string loginname = User.Identity.Name;

            //            if (_storeUser == "N")
            //            {
            //                dao.ReturnItem(parcelScannedInd, itemnumber, ordernumber, actioncode, taskdescr, customerurn, sku, 
            //                               loginname, System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
            //            }
            //            else
            //            {
            //                dao.RefundItem(parcelScannedInd, itemnumber, ordernumber, actioncode, taskdescr, customerurn, sku,
            //                               _userID, _storeID, _tillID);

            //            }
            //        }
            //    }
            //}

            //ViewState["dt"] = null;
            //if (_itemHashTable != null && _itemHashTable.Count > 0)
            //{
            //    lblItemsReturned.Visible = true;
            //    lblItemsReturned.Text = _itemHashTable.Count.ToString() + " items returned";
            //}
            //_ordernumber = null;
            //_itemHashTable = null;
            //BindGrid();
        }

        // Reject all return actions
        protected void btnReject_Click(object sender, EventArgs e)
        {
            _ordernumber = null;
            _itemHashTable = null;
            ViewState["dt"] = null;
            BindGrid();
        }

        //Unselecting an item after having edited
        protected void cbEdited_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in gvItems.Rows)
            //{
            //    CheckBox cb = (CheckBox)row.Cells[EDITCHECKBOXCELL].FindControl("cbEdited");
            //    if (cb != null)
            //    {
            //        int inx = int.Parse(((HiddenField)row.FindControl("hidRowIndex")).Value);

            //        if (cb == (CheckBox)sender)
            //        {
            //            if (cb.Checked && (_itemHashTable == null || !_itemHashTable.Contains(inx)))
            //            {
            //                gvItems.EditIndex = inx;
            //            }
            //            else
            //            {
            //                if (_itemHashTable != null && _itemHashTable.Contains(inx))
            //                {
            //                    _itemHashTable.Remove(inx);
            //                }
            //                gvItems.EditIndex = -1;
            //                _focusindex = -1;
            //                tbPackageBarcode.Text = "";
            //            }
            //            ViewState["itemsprocessed"] = _itemHashTable;
            //            BindGrid();
            //            break;
            //        }
            //    }
            //}
        }


        //Deselecting item during editing
        protected void cbEditing_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb = (CheckBox)sender;

            //if (cb.Checked == false)
            //{
            //    gvItems.EditIndex = -1;
            //    BindGrid();
            //}
        }


        //Click on saving customer service action
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            //int inx = -1;
            //foreach (GridViewRow row in gvItems.Rows)
            //{

            //    Control ctrl = (Control)row.Cells[ACTIONREASONCELL].Controls[1];
            //    inx++;
            //    if (ctrl is DropDownList)
            //        break;
            //}

            //if (inx >= 0)
            //{

            //    GridViewRow row = gvItems.Rows[inx];
            //    //row.BackColor = System.Drawing.Color.White;
            //    gvItems.EditIndex = inx;
            //    DropDownList ddl = (DropDownList)row.FindControl("ddlAct");
            //    TextBox tb = (TextBox)row.FindControl("tbTaskDesc");
            //    if (ddl != null)
            //    {
            //        string sv = ddl.SelectedValue;
            //        if (_itemHashTable == null) _itemHashTable = new Hashtable();
            //        if (_itemHashTable.ContainsKey(inx))
            //        {
            //            _itemHashTable.Remove(inx);
            //        }
            //        _ReturnAction act = new _ReturnAction();
            //        act.actionCode = sv;
            //        act.actionDescr = ddl.SelectedItem.Text;
            //        act.taskDescription = tb.Text;
            //        _itemHashTable.Add(inx, act);
            //        ViewState["itemsprocessed"] = _itemHashTable;
            //    }
            //    gvItems.EditIndex = -1;
            //    _focusindex = -1;
            //    tbPackageBarcode.Text = "";
            //    BindGrid();
            //}
        }

        private void ExecuteSearch()
        {
            bool dosearch = (rtbAddress.Text != string.Empty) || (rtbAddress.Text != string.Empty) || (rtbPostCode.Text != string.Empty) || (rtbFirstName.Text != string.Empty) || (rtbLastName.Text != string.Empty) || (rtbEmail.Text != string.Empty);

            if (dosearch)
            {
                ReturnsDAO dao = new ReturnsDAO();
                ds = dao.searchCustomer(null, rtbLastName.Text, rtbFirstName.Text, rtbPostCode.Text, rtbAddress.Text, "", rtbEmail.Text, rtbTelephone.Text, "");
                rgCustomers.DataSource = ds.Tables[0];
            }
        }

        // Customer search
        protected void rgCustomers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                ExecuteSearch();
            }
        }

        //Display of orders/items in customer search screen
        protected void rgCustomers_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {

                case "orders":
                    {
                        string customerurn = dataItem.GetDataKeyValue("customerurn").ToString();
                        DataSet dsorders = dao.getCustomerOrders(customerurn);
                        e.DetailTableView.DataSource = dsorders;
                        break;
                    }

                case "items":
                    {
                        int ordernumber = int.Parse(dataItem.GetDataKeyValue("ordernumber").ToString());
                        DataSet dsitems = dao.getOrderItems(ordernumber);
                        e.DetailTableView.DataSource = dsitems;
                        break;
                    }

            }

        }

        // Click on customer search
        protected void rbSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
            rgCustomers.Rebind();
        }

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            RadTabStrip  rtabstrip = RadTabStrip1; 
            RadMultiPage rmultipg = rtabstrip.MultiPage;

            if (e.Tab.Text == "Search Customer")
            {
                ViewState["tabselected"] = 1;
            }
            else
                ViewState["tabselected"] = 0;


        }

        protected void rgCustomers_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        //This handles the order select from the customer search screen
        protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            cb.Checked = false;
            string naming = cb.NamingContainer.ToString();
            GridDataItem dataItem = (GridDataItem)cb.NamingContainer;

            string ord = dataItem.Cells[ORDERNUMBERCELL].Text;


            tbSearchByCustomerOrderNumber.Value = ord;
           

            // tbPackageBarcode.Text = ord;
            // displayOrder();
            ViewState["orderselected"] = ord;
            ViewState["tabselected"] = 0;
               
        }

        protected void RadTabStrip1_Init(object sender, System.EventArgs e)
        {

        }

        protected void RadAjaxPanel1_Load(object sender, System.EventArgs e)
        {

        }

        protected void RadPageView1_PreRender(object sender, System.EventArgs e)
        {
            //if (_focusindex == -1)
            //    tbPackageBarcode.Focus();
            //else
            //{
            //    if (gvItems.EditIndex > -1 && gvItems.Rows[gvItems.EditIndex].Cells[ACTIONREASONCELL].FindControl("ddlAct") != null)
            //    {
            //        gvItems.Rows[gvItems.EditIndex].Cells[ACTIONREASONCELL].FindControl("ddlAct").Focus();
            //    }
            //}

        }

        protected void Button1_Click(object sender, System.EventArgs e)
        {
            
        }


        protected void Button3_Click(object sender, System.EventArgs e)
        {
            rtbEmail.Text = "";
            rtbFirstName.Text = "";
            rtbLastName.Text = "";
            rtbPostCode.Text = "";
            rtbTelephone.Text = "";
            rtbAddress.Text = "";

            //shouldn't execute as part of Clear
            rgCustomers.DataSource = null;
            rgCustomers.DataBind();
        }
    }

    }

    // This class is stored int he Viewstate _itemHashtable to record each return before committing to the database.
    //[Serializable()]
    //public class _ReturnAction
    //{

    //    public string sku { get; set; }

    //    public string actionCode { get; set; }

    //    public string actionDescr { get; set; }

    //    public string taskDescription { get; set; }

    //}
