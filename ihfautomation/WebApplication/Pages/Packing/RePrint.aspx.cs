// Name: RePrint.cs
// Type: Code Behind Class for Open Order.
// Description: Include local function and page 
//              events
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      29/07/11    MSalman   Initial Released
//  1.1      18/08/11    MSalman   Paging fixed.
//  1.2      09/09/11    MSalman   Message updated.        
//  1.3      27/09/11    MSalman   Sort type are added.        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IHF.BusinessLayer.DataAccessObjects.Packing;
using IHF.BusinessLayer.BusinessClasses.Packing;
using Telerik.Web.UI;

namespace IHF.ApplicationLayer.Web.Pages.Packing
{
    public partial class RePrint : System.Web.UI.Page
    {
        #region Local Memeber

        PackingDAO _pack = new PackingDAO();

        string _orderNo = string.Empty;

        string _parcelNo = string.Empty;

        List<PackOrder> lst = null;

        #endregion

        #region Page Events

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

                    string searchVal = this.txtOrderNo.Text;

                    if (!string.IsNullOrEmpty(searchVal))
                    {
                        if (isNumeric(searchVal))

                            _orderNo = searchVal;
                        else
                            _parcelNo = searchVal;

                    }


                    if (!string.IsNullOrEmpty(_orderNo) || !string.IsNullOrEmpty(_parcelNo))
                        SearchOrder(_orderNo, _parcelNo);

                }
                else if (evt == "PrintOrder")
                {
                    btnPrintDoc_click();
                    
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

                this.grdOrder.DataBind();
            }
        } 



        private void SearchOrder(string orderNo = "", string parcelNo = "")
        {
            lst = _pack.OpenOrderForPrint(orderNo, parcelNo);


            if (lst.Count > 0) ViewState["GridData"] = lst;

            this.grdOrder.DataSource = lst;

            this.grdOrder.ClientSettings.Selecting.AllowRowSelect = true;

            this.grdOrder.DataBind();
        }


        private string GetSelectedRowId()
        {

            string retVal = string.Empty;

            if (IsPostBack)
            {
                if (grdOrder.SelectedItems.Count != 0)
                {
                    GridItem item = this.grdOrder.SelectedItems[0];

                    if (item != null)
                    {
                        //orderNo
                        retVal = item.Cells[3].Text;
                    }
                }

            }

            return retVal;
        }


        protected void btnPrintDoc_click()
        {
            string orderNo = GetSelectedRowId();
            string docs = this.hdnOrder.Value;

            docs = docs.EndsWith(",")?docs.Substring(0,docs.LastIndexOf(',')):docs; 

            if (!string.IsNullOrEmpty(orderNo)&& (!string.IsNullOrEmpty(docs)))
            {

                _pack.RePrintDocs(orderNo, docs);



            }

        }


        private bool isNumeric(string val)
        {
            Double result;

            return Double.TryParse(val, out result);
        }

        #endregion
    }
}