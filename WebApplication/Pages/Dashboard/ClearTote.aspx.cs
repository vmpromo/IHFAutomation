using System;
using System.Data;
using System.Web.UI.WebControls;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.Util;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.Dashboard
{
    public partial class ClearTote : System.Web.UI.Page
    {
        FailedtoteDAO tote = new FailedtoteDAO();

        private string _toteBarcode = string.Empty;
        private string _skuNumber   = string.Empty;

        IHF.BusinessLayer.DataAccessObjects.Cancellation.OrderDAO _orderDao = new IHF.BusinessLayer.DataAccessObjects.Cancellation.OrderDAO();

        private void DisplayMessage(string message, char messageType)
        {
            Message.InnerHtml = message;
            Message.Style["color"] = messageType == 'S' ? "Green" : "Red";
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                
                DataRowView r = (DataRowView)dataItem.DataItem;
                if (r != null)
                {
                    Label lbllastActivity = (Label)dataItem.FindControl("lblLastActivity");

                    if (r["last_changed_dtm"] != DBNull.Value)
                    {
                        DateTime statusChanged = Convert.ToDateTime(r["last_changed_dtm"]);
                        TimeSpan ts = DateTime.Now.Subtract(statusChanged);
                        string timeInStatus = "";
                        if (ts.Days == 0 && 
                            ts.Hours == 0)
                            timeInStatus = string.Format("{0} minutes", ts.Minutes);
                        else if (ts.Days == 0)
                            timeInStatus = string.Format("{0}h {1}m", ts.Hours, ts.Minutes);
                        else
                            timeInStatus = string.Format("{0}d {1}h {2}m", ts.Days, ts.Hours, ts.Minutes);

                        lbllastActivity.Text = timeInStatus;
                    }
                }
                
                
                DropDownList rad = (DropDownList)dataItem.FindControl("Action_RadComboBox");
                DataView reasonsSource = _orderDao.GetReasons((int)EventType.ClearTote);

                //to add a blank record in the drop down
                //DataRowView row = reasonsSource.
                // for now a known issue that once edit row cannot be rolled
                //DataRow row = reasonsSource.Table.NewRow();
                //row["reason_id"] = 0;
                //row["short_description"] = "Add Reason";
                //reasonsSource.Table.Rows.InsertAt(row,0);

                rad.DataSource = reasonsSource;

                rad.DataTextField = "short_description";

                rad.DataValueField = "reason_id";

                rad.DataBind();

                
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == string.Empty)
            {
                RadGrid1.Rebind();
            }
            string[] editedItemIds = e.Argument.Split(':');
            int i;
            for (i = 0; i <= editedItemIds.Length - 2; i++)
            {
                string[] skuFailedToteId = editedItemIds[i].Split('-');;
                GridDataItem updatedItem = RadGrid1.MasterTableView.FindItemByKeyValue("skufailedtoteid", skuFailedToteId[0]);

                UpdateValues(updatedItem, int.Parse(skuFailedToteId[0]), int.Parse(skuFailedToteId[1]));
            }

            string redirectUrl = FormatUrl();

            Response.Redirect(redirectUrl);
            
        }

        private string FormatUrl()
        {
            string redirectUrl = Request.RawUrl;
            int index = Request.RawUrl.IndexOf('?');
            if (index > -1)
            {
                redirectUrl = redirectUrl.Substring(0, index);
            }

            redirectUrl += "?tote=" + _toteBarcode +
                           "&sku=" + _skuNumber;
            return redirectUrl;
        }
        protected void UpdateValues(GridDataItem updatedItem, int skuFailedToteId, int reasonID)
        {
            //if user does not want to update the row, he sets it to 0
            // to be implemented later
            //if (reasonID != 0){
                tote.RemoveSku(skuFailedToteId, reasonID, User.Identity.Name);
            //}
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            LoadSkus(_toteBarcode, _skuNumber);
        }

        private void LoadSkus(string toteBarcode, string skuNumber)
        {
            DataSet ds;
            try
            {
                if (toteBarcode != "")
                {
                    ds = tote.GetSku(new object[] { toteBarcode, null });
                }
                else if (skuNumber != "")
                {
                    ds = tote.GetSku(new object[] { null, skuNumber });
                }
                else
                {
                    ds = tote.GetSku(new object[] { null, null });
                }

                RadGrid1.DataSource = ds.Tables[0];
            }
            catch (Exception exception)
            {
                int indx = exception.Message.IndexOf(":");
                string ErrorMessage = exception.Message.Substring((indx + 1),
                                                                         exception.Message.IndexOf("ORA", (indx + 1)) - (indx + 1));
                this.Master.ErrorMessage = ErrorMessage;
                RadGrid1.DataSource = new DataTable();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            toteBarcode.Focus();
            toteBarcode.Attributes.Add("onfocus", "SetCursorToTextEnd(this.id)");

            if (!IsPostBack)
            {
                if (Request.QueryString["tote"] != null && Request.QueryString["sku"] != null)
                {
                    if (Request.QueryString["tote"].ToString() != "")
                    {
                        _toteBarcode = Request.QueryString["tote"].ToString();
                        toteBarcode.Text = _toteBarcode;
                        LoadSkus(_toteBarcode, "");
                    }
                    else if (Request.QueryString["sku"].ToString() != "")
                    {
                        _skuNumber = Request.QueryString["sku"].ToString();
                        skuNumber.Text = _skuNumber;
                        LoadSkus("", _skuNumber);
                    }
                    else
                    {
                        LoadSkus("", "");
                    }
                }
                else
                    RadGrid1.DataSource = new DataTable();
            }
            else
            {
                _toteBarcode = toteBarcode.Text;
                _skuNumber = skuNumber.Text;
                RadGrid1.Rebind();
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            RadGrid1.Rebind();
        }
    }
}