// Name: CageCreate.aspx.cs
// Type: class file 
// Description: Code behind class for Cage Create screen
//
//$Revision:   1.24  $
//
// Version   Date        Author     Reason
//  1.0      20/06/11    J Watt     Initial Revision
//  1.1      20/06/11    J Watt     No Change.
//  1.2      21/06/11    J Watt     Cage Creation
//  1.3      22/06/11    J Watt     Checkin attempt - cage creation
//  1.4      22/06/11    J Watt     checking
//  1.5      22/06/11    J Watt     AA
//  1.6      23/06/11    J Watt     Caging
//  1.7      28/06/11    ITMK       no changes
//  1.8      02/08/11    ITAJ1      label
//  1.9      04/08/11    ITAJ1      label
//  1.10     23/08/11    ITMK       Despatch - Interim version
//  1.11     13/09/11    J Watt     Added cage created date.
//  1.12     14/09/11    J Watt     Display cage type in drop down
//  1.13     05/12/11    ITMK       no changes
//  1.14     05/12/11    ITMK       no changes
//  1.15     09/01/13    J Watt     No Change.
//  1.16     09/07/13    ITMK       Enabled LoadOnDemand for cage_type
//                                  Changed device type for printing cage label on 8 by 4
//  1.17     06/08/13    J Watt     Cage label printed to 8x4 label printer
//  1.18     20/08/13    J Watt     Validation on cage type
//  1.19     20/08/13    J Watt     Load on demand working correctly
//  1.20     07/01/14    S Green    Changes for multiple cage creation and printing
//  1.21     30/12/16    M Cackett  Disabled remove button for Click and Collect
//                                  Also added banner.  Better late than never!
//  1.22     03/01/17    M Cackett  Click and Collect changes when createForStore is true
//  1.23     10/01/17    M Cackett  QA Changes.
//  1.24     21/03/17    M Cackett  Add a delay to the tote label loop and put the data reader
//                                  in a using blockbecause of the connection timeout/memory leak bug.

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
    public partial class CageCreate : System.Web.UI.Page
    {
        private void DisplayMessage(bool isError, string text)
        {
            Label label = (isError) ? this.Label1 : this.Label2;
            label.Text = text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

  
        }


        private void DoCageCreate(string cageType, int numOfCages)
        {

            CageDAO cagedao = new CageDAO();

            DataSet cageDS = cagedao.CreateCages(numOfCages, cageType, User.Identity.Name);

            PrintService ps = new PrintService();

            //Print each of the labels
            foreach (DataRow cageRow in cageDS.Tables[0].Rows)
            {
                string machinename = Shared.UserHostName;
                string reportname = "5";
                string devicetype = "1";
                int cageid = int.Parse(cageRow["cage_id"].ToString());

                string test = ps.PrintLabel(reportname, machinename, devicetype, cageid, true);

            }

        }

        protected void CagesRadGrid_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int numOfCages = (int)((RadNumericTextBox)(e.Item.FindControl("numcages_radnumerictextbox"))).Value;
            string cageType = ((RadComboBox)e.Item.FindControl("cagetype_radcombobox")).SelectedValue;
            string createForStores = ((RadioButtonList)e.Item.FindControl("rblCreateForStores")).SelectedValue;
            bool storesFound = false;

            if (createForStores == "T")
            {
                CageDAO cagedao = new CageDAO();
                PrintService ps = new PrintService();
                SystemParamsDAO sysParam = new SystemParamsDAO();
                const Int32 toteDelayParamId = 24;
                const int labelsInBatch = 50;
                const int defaultDelay = 50;
                int delayCounter = 0;
                int delaySeconds;

                try
                {
                    if (!int.TryParse(sysParam.Get_param_value(toteDelayParamId), out delaySeconds))
                    {
                        delaySeconds = defaultDelay;  // if param not set then use default delay.
                    }
                }
                catch  // The params package doesn't handle parameter not found so need to catch error.
                {
                    delaySeconds = defaultDelay;  // if param not set then use default delay.
                }

                // Creating IDataReader in a using block should tidy up properly when we are finished with it or
                // in case of failure.
                using (IDataReader toteLabels = cagedao.GetToteLabels(numOfCages, cageType, User.Identity.Name))
                {
                    while (toteLabels.Read())
                    {
                        storesFound = true;

                        ps.PrintPackDocuments(Convert.ToDecimal(toteLabels["order_id"].ToString()),
                                                  true,
                                                  Shared.UserHostName,
                                                  "L",
                                                  Shared.CurrentUser);
                        delayCounter++;
                        if (delayCounter == labelsInBatch)
                        {
                            System.Threading.Thread.Sleep(delaySeconds * 1000);  // Sleep is in milliseconds
                            delayCounter = 0;
                        }
                    }
                }
                if (storesFound)
                {
                    DisplayMessage(false, "Tote Label(s) of type " + cageType + " created");
                }
                else
                {
                    DisplayMessage(true, "Error: No valid stores for tote labels found for cage type " + cageType);
                }

            }
            else
            {
                DoCageCreate(cageType, numOfCages); 
                DisplayMessage(false, numOfCages.ToString() + " cage(s) of type " + cageType + " created");
            }

        }


        protected void CagesRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                //GridEditCommandColumn editColumn = (GridEditCommandColumn)CagesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
                //editColumn.Visible = false;
            }
            else if (e.CommandName == RadGrid.UpdateCommandName)
            {
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else
            {
                //GridEditCommandColumn editColumn = (GridEditCommandColumn)CagesRadGrid.MasterTableView.GetColumn("EditCommandColumn");
                //if (!editColumn.Visible)
                //    editColumn.Visible = true;
            }

            if (e.CommandName == "print")
            {

                GridDataItem dataItem = (GridDataItem)e.Item;
                String scageid = dataItem.GetDataKeyValue("cage_id").ToString();
                Int32 icageid = Int32.Parse(scageid);



                string machinename = Shared.UserHostName; 
                string reportname = "5";
                string devicetype = "1";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype + icageid);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, icageid, true);
                //HttpContext.Current.Response.Write("after print" + test);

            }
        }

        protected void CagesRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if (e.Item is GridDataItem)
            {
                int cageid = int.Parse(dataItem.GetDataKeyValue("cage_id").ToString());
                Button button = dataItem["DeleteColumn"].Controls[0] as Button;
//  Temporary change to disable the "Remove" button for MVP for Click and Collect.
//  To re-enable the button, uncomment the below line and remove the line which sets Enabled property to false.
//                button.Attributes["onclick"] = "return confirm('Are you sure you want to remove cage " + cageid.ToString() + "?')";
                button.Enabled = false;
            }


            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {

                // Inserting Cage Type

                GridEditableItem editedItem = e.Item as GridEditableItem;

                //RadComboBox cagetypes = (RadComboBox)e.Item.FindControl("cagetype_radcombobox");

                //CagetypeDAO cagetypedao = new CagetypeDAO();

                //cagetypes.DataSource = cagetypedao.GetCageTypes();
                //cagetypes.DataValueField = "cage_type_id";
                //cagetypes.DataTextField = "cage_type_id";
                //cagetypes.DataBind();

            }

        }

        protected void CagesRadGrid_ItemDeleted(object sender, Telerik.Web.UI.GridDeletedEventArgs e)
        {

        }

        protected void CagesRadGrid_ItemEvent(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void CagesRadGrid_ItemInserted(object sender, Telerik.Web.UI.GridInsertedEventArgs e)
        {

        }

        protected void CagesRadGrid_ItemUpdated(object sender, Telerik.Web.UI.GridUpdatedEventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                CageDAO cage = new CageDAO();
                CagesRadGrid.DataSource = cage.GetCages();
            }
        }

        protected void CagesRadGrid_PreRender(object sender, EventArgs e)
        {
            //if (CagesRadGrid.EditItems.Count > 0)
            //{
            //    foreach (GridDataItem item in CagesRadGrid.MasterTableView.Items)
            //    {
            //        if (item != CagesRadGrid.EditItems[0])
            //        {
            //            item.Visible = false;
            //        }
            //    }
            //}

        }

        protected void CagesRadGrid_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnCreateCages_Click(object sender, EventArgs e)
        {
            

        }

        protected void CagesRadGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            int cageID = int.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["cage_id"].ToString());
            CageDAO cagedao = new CageDAO();
            cagedao.UpdateCageStatus(cageID, (int)CageStatus.Cancelled, User.Identity .Name );
            DisplayMessage(false, "Cage " + cageID.ToString() + " cancelled");

        }

        protected void cagetype_radcombobox_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
           int i = 1;

        }

        protected void cagetype_radcombobox_Load(object sender, EventArgs e)
        {
            RadComboBox cagetypes = (RadComboBox)sender; // e.Item.FindControl("cagetype_radcombobox");

            CagetypeDAO cagetypedao = new CagetypeDAO();

            cagetypes.DataSource = cagetypedao.GetCageTypes();
            cagetypes.DataValueField = "cage_type_id";
            cagetypes.DataTextField = "cage_type_id";
            cagetypes.DataBind();

        }

        


    }
}