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

namespace IHF.ApplicationLayer.Web.Pages.Returns
{
    public partial class ReturnItems : System.Web.UI.Page
    {
        private string _skuToFind = string.Empty;
        private int? _ordernumber= null;
        private DataTable _dt;
        private DataTable _actions;
        private Hashtable _itemHashTable = new Hashtable();
        private ReturnsDAO _dao = new ReturnsDAO();
        private int _focusindex = -1; // Index in table to set focus or -2 if the order number or -1 if the SKU scan.
        private int _alreadyCancelled = 0;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();

            _actions = dao.getReturnActions().Tables[0];
            _itemHashTable = (Hashtable)ViewState["itemsprocessed"];


            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.Visible = false;
                btnReject.Attributes.Add("onclick", "if(confirm('Are you sure?')){}else{return false}");
 

                if (Request.QueryString["ordernumber"] != null)
                {
                    _ordernumber = int.Parse(Request.QueryString["ordernumber"]);
                    
                    populateOrderDetails(GetOrder(_ordernumber.ToString()));
                    _focusindex = -1;
                }
                BindGrid();
            }
            else
            {
                if (ViewState["ordernumber"] != null) _ordernumber = (int)ViewState["ordernumber"];
                else _ordernumber = null;
            }

        }

        protected DataSet GetOrder(string ordernumberstring)
        {
            DataSet dsorder = _dao.getOrderDetails(ordernumberstring);

            return dsorder;
        }

        protected void populateOrderDetails(DataSet dsorder)
        {

            if (dsorder.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsorder.Tables[0].Rows[0];
                _ordernumber = int.Parse(dr.Table.Rows[0]["ordernumber"].ToString());

                lblOrdernumber.Text = _ordernumber.ToString();
                lblOrderDate.Text = ((DateTime)dr.Table.Rows[0]["orderdate"]).ToString("dd-MM-yyyy");
                lblCustomerUrn.Text = dr.Table.Rows[0]["customerurn"].ToString();
                lblTotalItems.Text = dr.Table.Rows[0]["totalitems"].ToString();
                lblCustomerName.Text = dr.Table.Rows[0]["customername"].ToString();
                lblMessage.Visible = false;
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "Order " + _ordernumber.ToString() + " not found";
                lblMessage.Visible = true;
            }
        }


        protected void BindGrid()
        {
            if (ViewState["dt"] == null)
            {
                gvItems.DataSource = createDataTable();
                gvItems.DataBind();
            }
            else
            {
                gvItems.DataSource = ViewState["dt"] as DataTable;
                gvItems.DataBind();
            }
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

 
        protected void rtbPackageBarcode_TextChanged(object sender, EventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();
            bool skuaAlreadyScanned = false;

            if (_ordernumber != null)
            {
                try
                {
                    _skuToFind = dao.getSKU(rtbPackageBarcode.Text);
                }
                catch
                {
                    DataSet dsorder = GetOrder(rtbPackageBarcode.Text);
                    if (dsorder.Tables[0].Rows.Count > 0)
                    {
                        populateOrderDetails(dsorder);
                        _focusindex = -1;
                        rtbPackageBarcode.Text = "";
                        createDataTable();
                        BindGrid();
                        return;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Invalid scan: " + rtbPackageBarcode.Text;
                        rtbPackageBarcode.Text = "";
                        return;
                    }
                }
                int inx = 0;
                _itemHashTable = (Hashtable)ViewState["itemsprocessed"];

                foreach (GridViewRow gridrow in gvItems.Rows)
                {
                    CheckBox cb = (CheckBox)gridrow.Cells[0].FindControl("cbEdited");
                    bool cbenabled = cb != null && cb.Enabled ? true : false;
                    if (gridrow.Cells[1].Text == _skuToFind  && cbenabled)
                    {
                        if (_itemHashTable == null || !_itemHashTable.Contains(inx))
                        {
                            gvItems.EditIndex = inx;
                            _focusindex = inx;
                            lblMessage.Text = "";
                            lblMessage.Visible = false;
                            BindGrid();
                            return;
                        }
                        skuaAlreadyScanned = true;
                    }
                    inx++;
                }
                // If no SKUs have been processed and an order number has been entered then change to processing new order
                if (_itemHashTable == null || _itemHashTable.Count > 0)
                {
                    DataSet dsorder = GetOrder(rtbPackageBarcode.Text);
                    if (dsorder.Tables[0].Rows.Count > 0)
                    {
                        populateOrderDetails(dsorder);
                        _focusindex = -1;
                        rtbPackageBarcode.Text = "";
                        createDataTable();
                        BindGrid();
                        return;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = skuaAlreadyScanned ? "SKU " + _skuToFind + " already scanned" : "SKU " + _skuToFind + " not found on order";
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = skuaAlreadyScanned ? "SKU " + _skuToFind + " already scanned" : "SKU " + _skuToFind + " not found on order";
                }
            }
            else
            {
                populateOrderDetails(GetOrder(rtbPackageBarcode.Text));
                _focusindex = -1;
                rtbPackageBarcode.Text = "";
                createDataTable();
                BindGrid();
            }
         
        }



        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Set row colour
                if (gvItems.EditIndex == e.Row.DataItemIndex)
                {   // Row being edited
                    e.Row.BackColor = System.Drawing.Color.Azure;
                }
                else if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
                {  //Row with action 
                    e.Row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Bisque;
                }

                if (e.Row.RowState == DataControlRowState.Edit)
                {
                    e.Row.BackColor = System.Drawing.Color.Azure;
                }
                CheckBox cb = (CheckBox)e.Row.Cells[0].FindControl("cbEdited");
                CheckBox cb2 = (CheckBox)e.Row.Cells[0].FindControl("cbEditing");
                e.Row.Cells[2].Controls[0].Visible = false;

                if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
                {

                    if (gvItems.EditIndex != e.Row.DataItemIndex)
                        e.Row.Cells[2].Controls[0].Visible = true;
                    if (cb != null) cb.Checked = true;
                }
                else
                {
                    if (cb != null)
                    {
                        cb.Checked = false;
                    }

                    DataRowView dr = (DataRowView)e.Row.DataItem;
                    string actioncode = dr.Row.ItemArray[4].ToString();
                    if (actioncode != string.Empty)
                    {
                        _alreadyCancelled++;
                        if (cb != null) cb.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.Chocolate;
                    }
                }

                Label lbl = (Label)e.Row.FindControl("lbAction");
                if (lbl != null)
                {
                    if (_itemHashTable != null && _itemHashTable.Contains(e.Row.DataItemIndex))
                    {
                        string ss = _itemHashTable[e.Row.DataItemIndex].ToString();
                        _ReturnAction act = (_ReturnAction)_itemHashTable[e.Row.DataItemIndex];

                        lbl.Text = act.actionDescr;
                    }
                    else lbl.Text = "";
                }
                Control ctrl = e.Row.FindControl("ddlAct");
                if (ctrl != null)
                {

                    DropDownList ddl = (DropDownList)ctrl;
                    ddl.DataSource = _actions;
                    ddl.DataTextField = "description";
                    ddl.DataValueField = "actioncode";
                    ddl.DataBind();
                    ddl.Items.Add(new ListItem("Select Action", "-1"));
                    if (_itemHashTable != null && _itemHashTable.Contains(e.Row.RowIndex))
                    {
                        ddl.SelectedValue = ((_ReturnAction)(_itemHashTable[e.Row.RowIndex])).actionCode;
                        if (ddl.SelectedValue == "70")
                        {
                            ((TextBox)e.Row.FindControl("tbTaskDesc")).Text = ((_ReturnAction)(_itemHashTable[e.Row.RowIndex])).taskDescription;
                            ((TextBox)e.Row.FindControl("tbTaskDesc")).Visible = true;
                            ((Button)e.Row.FindControl("btnFinish")).Visible = true;
                        }
                    }
                    else
                        ddl.SelectedValue = "-1"; 

                }
                if (cb2 != null)
                {
                    if (gvItems.EditIndex == e.Row.DataItemIndex)
                    {
                        cb2.Checked = true;
                    }
                }
            }
        }

        protected void gvItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvItems.EditIndex = e.NewEditIndex;
            _focusindex = e.NewEditIndex;
            BindGrid();

        }

        protected void gvItems_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
           
        }

        protected void gvItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvItems.EditIndex = -1;
            _focusindex = -1;
            rtbPackageBarcode.Text = "";
            BindGrid();
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            ViewState["itemsprocessed"] = _itemHashTable;
            ViewState["ordernumber"] = _ordernumber;
            ViewState["itemactions"] = _actions;

            if (gvItems.Rows.Count > 0)
            {
                pnlOrder.Visible = true;
                lblScan.Text = "Scan Item: ";
            }
            else
            {
                pnlOrder.Visible = false;
                lblScan.Text = "Scan Order/Package: ";
            }

            if (_itemHashTable != null)
            {
                lblItemsToProcess.Text = (int.Parse(lblTotalItems.Text) - _itemHashTable.Count).ToString();
                if (_itemHashTable.Count > 0)
                {
                    btnAccept.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                }
            }
            else
            {
                lblItemsToProcess.Text = lblTotalItems.Text;
                btnAccept.Visible = false;
                btnReject.Visible = false;
            }

            if (_focusindex == -1)
                rtbPackageBarcode.Focus();
            else
                gvItems.Rows[_focusindex].Cells[4].Focus();
        }

        protected void gvItems_DataBinding(object sender, EventArgs e)
        {
           
        }

        protected void gvItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
         

        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                int inx = int.Parse((string)e.CommandArgument);
                GridViewRow row = gvItems.Rows[inx];
                //row.BackColor = System.Drawing.Color.White;
                gvItems.EditIndex = inx;
               DropDownList ddl = (DropDownList)row.FindControl("ddlAct");
               // TextBox tb = (TextBox)row.FindControl("tbTaskDesc");
                if (ddl != null)
                {
                    ddl.DataSource = _actions;
                    string sv = ddl.SelectedValue;
                    if (_itemHashTable == null) _itemHashTable = new Hashtable();
                    if (_itemHashTable.ContainsKey(inx))
                    {
                        _itemHashTable.Remove(inx);
                    }
                    _ReturnAction act = new _ReturnAction();
                    act.actionCode = sv;
                    act.actionDescr = ddl.SelectedItem.Text;
                    _itemHashTable.Add(inx, act);
                    ViewState["itemsprocessed"] = _itemHashTable;
                }
                gvItems.EditIndex = -1;
                _focusindex = -1;
                rtbPackageBarcode.Text = "";
                BindGrid();
            }
            //else if (e.CommandName.Equals("Undo"))
            //{
            //    int inx = int.Parse((string)e.CommandArgument);
            //    if (_itemHashTable.Contains(inx))
            //    {
            //        _itemHashTable.Remove(inx);
            //    }
            //    gvItems.EditIndex = -1;
            //    _focusindex = -1;
            //    rtbPackageBarcode.Text = "";
            //    BindGrid();

            //}
        }

        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int rowinx = -1;

            string sv = ddl.SelectedValue;

            _actions = (DataTable)ViewState["itemactions"];

            if (_actions != null)
            {

                foreach (DataRow dr in _actions.Rows)
                {
                    if (dr["actioncode"].ToString() == sv)
                    {
                        TextBox tb = (TextBox)ddl.NamingContainer.FindControl("tbTaskDesc");
                        Button btn = (Button)ddl.NamingContainer.FindControl("btnFinish");
                        if (tb != null)
                        {
                            if (dr["createtask"].ToString() == "1")
                            {
                                tb.Visible = true;
                                btn.Visible = true;
                            }
                            else
                            {
                                tb.Visible = false;
                                btn.Visible = false;

                                if (ddl.SelectedValue != "-1")
                                {
                                    //Find which row this is
                                    foreach (GridViewRow row in gvItems.Rows)
                                    {

                                        Control ctrl = (Control)row.Cells[4].Controls[1];
                                        rowinx++;
                                        if (ctrl is DropDownList)
                                        {
                                           if (_itemHashTable == null) _itemHashTable = new Hashtable();
                                           if (_itemHashTable.ContainsKey(rowinx))
                                           {
                                              _itemHashTable.Remove(rowinx);
                                           }
                                           gvItems.EditIndex = -1;
                                            _focusindex = -1;
                                            rtbPackageBarcode.Text = "";
                                            _ReturnAction act = new _ReturnAction();
                                            act.actionCode = sv;
                                            act.actionDescr = ddl.SelectedItem.Text;
                                            act.taskDescription = "";
                                            _itemHashTable.Add(rowinx, act);
                                            ViewState["itemsprocessed"] = _itemHashTable;
                                            BindGrid();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }


        protected void gvItems_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ReturnsDAO dao = new ReturnsDAO();

            _focusindex = -1;

            DataTable dt = (DataTable)ViewState["dt"];

            if (dt != null)
            {
                for (int inx = 0; inx < dt.Rows.Count; inx++)
                {
                    if (_itemHashTable.Contains(inx))
                    {
                        DataRow dr = dt.Rows[inx];
                        _ReturnAction ra = (_ReturnAction)_itemHashTable[inx];

                        int itemnumber = int.Parse(dr.ItemArray[0].ToString());
                        int ordernumber = int.Parse(lblOrdernumber.Text);
                        string actioncode = ra.actionCode;
                        string taskdescr = ra.taskDescription;
                        string customerurn = lblCustomerUrn.Text;
                        string sku = (string)dr.ItemArray[2];
                        string loginname = User.Identity.Name;

                        dao.ReturnItem("F", itemnumber, ordernumber, actioncode, taskdescr, customerurn, sku, loginname, System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                    }
                }
            }

            _ordernumber = null;
            _itemHashTable = null;
            ViewState["dt"] = null;
            BindGrid();
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            _ordernumber = null;
            _itemHashTable = null;
            ViewState["dt"] = null;
            BindGrid();
        }

        protected void cbEdited_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvItems.Rows)
            {
                CheckBox cb = (CheckBox)row.Cells[0].FindControl("cbEdited");
                if (cb != null)
                {
                    int inx = int.Parse(((HiddenField)row.FindControl("hidRowIndex")).Value);

                    if (cb == (CheckBox)sender)
                    {
                        if (cb.Checked && (_itemHashTable == null || !_itemHashTable.Contains(inx)))
                        {
                            gvItems.EditIndex = inx;
                        }
                        else
                        {
                            if (_itemHashTable != null && _itemHashTable.Contains(inx))
                            {
                                _itemHashTable.Remove(inx);
                            }
                            gvItems.EditIndex = -1;
                            _focusindex = -1;
                            rtbPackageBarcode.Text = "";
                        }
                        ViewState["itemsprocessed"] = _itemHashTable;
                        BindGrid();
                        break;
                    }
                }
            }
        }


        protected void cbEditing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked == false)
            {
                gvItems.EditIndex = -1;
                BindGrid();
            }
        }


        protected void btnFinish_Click(object sender, EventArgs e)
        {
            int inx = -1;
            foreach (GridViewRow row in gvItems.Rows)
            {

                Control ctrl = (Control)row.Cells[4].Controls[1];
                inx++;
                if (ctrl is DropDownList)
                    break;
            }

            if (inx >= 0)
            {

                GridViewRow row = gvItems.Rows[inx];
                //row.BackColor = System.Drawing.Color.White;
                gvItems.EditIndex = inx;
                DropDownList ddl = (DropDownList)row.FindControl("ddlAct");
                TextBox tb = (TextBox)row.FindControl("tbTaskDesc");
                if (ddl != null)
                {
                    string sv = ddl.SelectedValue;
                    if (_itemHashTable == null) _itemHashTable = new Hashtable();
                    if (_itemHashTable.ContainsKey(inx))
                    {
                        _itemHashTable.Remove(inx);
                    }
                    _ReturnAction act = new _ReturnAction();
                    act.actionCode = sv;
                    act.actionDescr = ddl.SelectedItem.Text;
                    act.taskDescription = tb.Text;
                    _itemHashTable.Add(inx, act);
                    ViewState["itemsprocessed"] = _itemHashTable;
                }
                gvItems.EditIndex = -1;
                _focusindex = -1;
                rtbPackageBarcode.Text = "";
                BindGrid();
            }
        }
    }
/*
    [Serializable()]
    public class _ReturnAction
    {

        public string sku { get; set; }

        public string actionCode { get; set; }

        public string actionDescr { get; set; }

        public string taskDescription { get; set; }

    }
 * */
}