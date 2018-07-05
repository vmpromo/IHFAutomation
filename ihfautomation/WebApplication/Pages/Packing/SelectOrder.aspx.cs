// Name: SelectOrder.cs
// Type: Code Behind Class for Open Order.
// Description: Include local function and page 
//              events
//
//$Revision:   1.16  $
//
// Version   Date        Author    Reason
//  1.0      14/07/11    MSalman   Initial Released
//  1.1      04/08/11    MSalman   Mode and d type updated.
//  1.2      08/08/11    MSalman   field focus updated.   
//  1.3      12/08/11    MSalman   Query string removed.
//  1.4      18/08/11    MSalman   Paging fixed 
//  1.5      24/08/11    MSalman   New Field added.
//  1.6      25/08/11    MSalman   Activity log added.    
//  1.7      14/09/11    MSalman   Exception Handling to prevent Crashing.         
//  1.8      27/09/11    MSalman   Added sorting for List.   
//  1.9      19/10/11    MSalman   Validation added open order.         
//  1.16     20/03/12    M Khan    New session added for user option.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IHF.BusinessLayer.DataAccessObjects.Packing;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.Util;
using Telerik.Web.UI;



namespace PackingMock
{
    public partial class SelectOrder : System.Web.UI.Page
    {

        #region Local Memeber

        PackingDAO _pack = new PackingDAO();

        string _dtype = string.Empty;

        string _pmode = string.Empty;

        string _toteid = string.Empty;

        string _containerlabel = string.Empty;

        List<PackOrder> lst = null;

        ActivityLogDAO ac = new ActivityLogDAO();


        #endregion

        #region Page Events

        protected void page_init(object sender, EventArgs e)
        {

            ShowError(string.Empty);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SearchOrder(string.Empty);
            }
            else
            {

                string evt = Request.Form["__EVENTTARGET"].ToString();

                if (evt == "SearchOrder")
                {

                    string orderNo = this.txtOrderNo.Text;

                    if (!string.IsNullOrEmpty(orderNo))
                    {
                        SearchOrder(orderNo);
                    }
                }
                else if (evt == "OpenOrder")
                {
                    btnOpenOrder_Click();
                }

            }



        }

        #endregion

        #region Local Functions

        protected void grdOrder_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["GridData"] != null)
            {
                this.grdOrder.DataSource = (List<PackOrder>)ViewState["GridData"];

            }
        }


        private void SearchOrder(string orderNo = "")
        {

            try
            {

                this.grdOrder.MasterTableView.DataKeyNames = new String[] { "DestinationType", "ProcessMode", "ToteId", "ContainerLabel" };

                lst = _pack.GetPackOrder(orderNo);

                if (lst.Count > 0) ViewState["GridData"] = lst;

                this.grdOrder.DataSource = lst;

                this.grdOrder.ClientSettings.Selecting.AllowRowSelect = true;

                this.grdOrder.DataBind();
            }
            catch (Exception e)
            {


            }
        }

        private string GetSelectedRowId()
        {

            string retVal = string.Empty;

            if (IsPostBack)
            {
                if (grdOrder.SelectedItems.Count != 0)
                {

                    GridDataItem item = (GridDataItem)this.grdOrder.SelectedItems[0];


                    _dtype = item.GetDataKeyValue("DestinationType").ToString();

                    _pmode = item.GetDataKeyValue("ProcessMode").ToString();

                    _toteid = item.GetDataKeyValue("ToteId").ToString();

                    _containerlabel = item.GetDataKeyValue("ContainerLabel").ToString();


                    if (item != null)
                    {
                        //orderNo
                        retVal = item.Cells[3].Text;
                    }
                }

            }

            return retVal;
        }

        protected void btnOpenOrder_Click()
        {
            string orderNo = GetSelectedRowId();

            if (!string.IsNullOrEmpty(orderNo))
            {

                if (true == _pack.OpenForRePack(orderNo))
                {

                    string val = "ord=" + orderNo + "&dt=" + _dtype + "&pm=" + _pmode + "&tt=" + _toteid + "&cl=" + _containerlabel;

                    HttpContext.Current.Session["OpenOrderVal"] = val;

                    //user option PackFromFT as defined in api.Pack.helper.js file
                    val = "3";
                    HttpContext.Current.Session["UserOption"] = val;

                    RecordActivity(orderNo);

                    Response.Redirect("Pack.aspx");
                }
                else
                    ShowError("Invalid order status.");

            }

        }

        private void RecordActivity(string orderNo)
        {

            ac.SaveUserActivity(new UserActivity
            {
                AppSystem = (int)ActivityLogEnum.AppSystem.IHF,
                ApplicationId = (int)ActivityLogEnum.ApplicationID.Pack,
                EventDateTime = DateTime.Now,
                EventType = (int)EventType.OpenforPackOrderSelection,
                ModuleId = (int)ActivityLogEnum.ModuleID.OpenForPack,
                OrderNumber = Convert.ToInt32(orderNo),
                UserId = Shared.CurrentUser,
                TerminalId = Shared.UserHostName
            });

        }


        private void ShowError(string msg)
        {

            if (!string.IsNullOrEmpty(msg))

                this.Master.ErrorMessage = msg;
            else
                this.Master.ErrorMessage = string.Empty;

        }

        #endregion
    }
}