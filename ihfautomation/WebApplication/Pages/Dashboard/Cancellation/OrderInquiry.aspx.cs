using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects.Cancellation;
using System.Data;
using Telerik.Web.UI;
using IHF.BusinessLayer.Util;

namespace IHF.ApplicationLayer.Web.Pages.Cancellation
{
    public partial class OrderInquiry : System.Web.UI.Page
    {
        public OrderInquiry(){
            _orderId = 0;
            _orderDao = new OrderDAO();
        }
        private OrderDAO _orderDao;
        private int _orderId;

        private void DisplayMessage(string message, char messageType){
            Message.InnerHtml = message;
            Message.Style["color"] = messageType == 'S' ? "Green" : "Red";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Initialise();

            string I_message = null;

            if (Request.QueryString["ordernumber"] != null)
            {
                I_message = Request.QueryString["ordernumber"].ToString();
                orderNumber.Text = I_message;

                if (!IsPostBack)
                    orderItemGrid.Rebind();
                else
                {
                    try
                    {
                        _orderId = int.Parse(orderNumber.Text);
                    }
                    catch (Exception)
                    {
                        this.Master.ErrorMessage = "Invalid order number";
                    }

                    if (_orderId != 0)
                        LoadOrder();

                }
            }
            else
            {

                //Initialise();

                orderNumber.Attributes.Add("onfocus", "SetCursorToTextEnd(this.id)");

                if (!IsPostBack)
                    orderItemGrid.Rebind();
                else
                {
                    try
                    {
                        _orderId = int.Parse(orderNumber.Text);
                    }
                    catch (Exception)
                    {
                        this.Master.ErrorMessage = "Invalid order number";
                    }

                    if (_orderId != 0)
                        LoadOrder();

                }
            }


        }

        private void Initialise(){
            orderNumber.Focus();
            //orderItemGrid.Visible = false;
            NoRecords.Style["display"] = "None";
            order.InnerHtml = string.Empty;
            status.InnerHtml = string.Empty;
            noOfItems.InnerHtml = string.Empty;
            chute.InnerHtml = string.Empty;
            trolley.InnerHtml = string.Empty;
            workstation.InnerHtml = string.Empty;
            failedTote.InnerHtml = string.Empty;
        }

        private void LoadOrderItems(int type, int orderNumber){
            DataSet ds;
            if (_orderId == 0){
                ds = new DataSet();
                ds.Tables.Add(new DataTable());
            }
            else
                ds = _orderDao.GetOrderItems(type, orderNumber);

            orderItemGrid.DataSource = ds.Tables[0];
            //orderItemGrid.DataBind();
        }

        private void LoadOrder(){
            Int32.TryParse(orderNumber.Text, out _orderId);

            using (IDataReader dr = _orderDao.GetOrderDetails(_orderId)){
                try{
                    dr.Read();

                    order.InnerHtml = dr[0].ToString();
                    status.InnerHtml = dr[1].ToString();
                    noOfItems.InnerHtml = dr[6].ToString();
                    chute.InnerHtml = dr[4].ToString();
                    trolley.InnerHtml = dr[2].ToString();
                    workstation.InnerHtml = dr[5].ToString();
                    failedTote.InnerHtml = dr[3].ToString();
                    orderItemGrid.Visible = true;
                    //orderItemGrid.DataSource = null;
                    //orderItemGrid.DataBind();
                }
                catch (Exception){
                    //orderItemGrid.Visible = false;
                    this.Master.ErrorMessage = "Invalid order number";
                    //DisplayMessage("Invalid order number", 'E');
                    //NoRecords.InnerHtml = "Invalid order number.";
                    //NoRecords.Style["display"] = "Block";
                }
                //LoadOrderItems((int)ReasonCode.Nonreturns, _orderId);
                
            }
        }
        
        protected void orderItemGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e){
                
            LoadOrderItems((int)ReasonCode.Nonreturns, _orderId);

        }

        protected void orderItemGrid_ItemDataBound(object sender, GridItemEventArgs e){
            RadComboBox reasons = null;

            if (e.Item is GridEditableItem){
                GridEditableItem editedItem = e.Item as GridEditableItem;
                DataRowView r = (DataRowView)editedItem.DataItem;
                Label lbllastActivity = (Label)editedItem.FindControl("last_activity");
                reasons = (RadComboBox)editedItem.FindControl("Action_RadComboBox");

                if (r["last_changed_dtm"] != DBNull.Value){
                    DateTime statusChanged = Convert.ToDateTime(r["last_changed_dtm"]);
                    TimeSpan ts = DateTime.Now.Subtract(statusChanged);
                    string timeInStatus = "";
                    if (ts.Days == 0 && ts.Hours == 0)
                        timeInStatus = string.Format("{0} minutes", ts.Minutes);
                    else if (ts.Days == 0)
                        timeInStatus = string.Format("{0}h {1}m", ts.Hours, ts.Minutes);
                    else
                        timeInStatus = string.Format("{0}d {1}h {2}m", ts.Days, ts.Hours, ts.Minutes);

                    lbllastActivity.Text = timeInStatus;
                }

                reasons.Items.Insert(0, new RadComboBoxItem("Please select", "0"));

                if (r["editable"].ToString() == "N"){
                    ImageButton editLink = (ImageButton)editedItem["EditCommandColumn"].Controls[0];
                    editLink.Visible = false;

                    reasons.Visible = false;
                }
            }

            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode){

                GridEditableItem editedItem = e.Item as GridEditableItem;
                reasons = (RadComboBox)editedItem.FindControl("Action_RadComboBox");

                DataView reasonsSource = _orderDao.GetReasons((int)EventType.Cancellation);

                reasons.DataSource = reasonsSource;

                reasons.DataTextField = "short_description";

                reasons.DataValueField = "reason_id";

                reasons.DataBind();

                reasons.Items.Insert(0, new RadComboBoxItem("", "0"));
            }
        }

        protected void orderItemGrid_UpdateCommand(object sender, GridCommandEventArgs e){
            GridEditableItem editedItem = e.Item as GridEditableItem;

            string failedTote = String.Empty;

            int itemNumber = 0;

            //Item number
            itemNumber = int.Parse(
                                    editedItem
                                    .OwnerTableView
                                    .DataKeyValues[e.Item.ItemIndex]["itemnumber"]
                                    .ToString());

            int standardCancelCode = (int)ActionCode.NoStock;

            //Reasons
            RadComboBox reasons = (RadComboBox)editedItem
                                    .FindControl("Action_RadComboBox");

            string selectedReason = reasons.SelectedValue + 
                                    ":" + 
                                    reasons.SelectedItem.Text;

            failedTote = _orderDao.ProcessCancellation(
                         _orderId, 
                         itemNumber, 
                         standardCancelCode, 
                         selectedReason, 
                         User.Identity.Name
                         );

            if (failedTote != "F"){
                DisplayMessage("Order complete. Start packing from Failed Tote:" + failedTote,'S' );
            }

            LoadOrder();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //LoadOrder();
            orderItemGrid.Rebind();

        }
    }
}