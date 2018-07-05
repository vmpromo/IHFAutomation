using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using Telerik.Web.UI;
using System.Data.OracleClient;
using IHF.BusinessLayer.Util;
using System.Drawing;
using System.Net;

namespace IHF.ApplicationLayer.Web.Administration.Setup
{
    public partial class WorkstationSetup : System.Web.UI.Page
    {
        private WorkstationDAO _workstationDAO = null;
        private Workstation _workstation = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void workstationGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            DataSet workstations = new DataSet();
            
            _workstationDAO = new PackstationDAO();
            
            workstations = _workstationDAO.GetWorkstations();

            workstationGrid.DataSource = workstations.Tables["mds_workstation"];
        }

        protected void workstationGrid_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            _workstationDAO = new PackstationDAO();
            _workstation = new Packstation();

            _workstation.ID = int.Parse(
                                (e.Item as GridDataItem)
                                .OwnerTableView
                                .DataKeyValues[e.Item.ItemIndex]["workstation_id"]
                                .ToString());

            _workstation.LastChangedBy = User.Identity.Name;

            int returnResult = _workstationDAO.Delete(_workstation);
            
            BindData();
        }

        protected void workstationGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            
            //type
            RadComboBox type = (RadComboBox)editedItem
                                    .FindControl("workstation_type_RadComboBox");

            SetInstance(type.SelectedItem.Text);



            _workstation.Type = decimal.Parse(type.SelectedValue);

            //id
            _workstation.ID = int.Parse(
                                    editedItem
                                    .OwnerTableView
                                    .DataKeyValues[e.Item.ItemIndex]["workstation_id"]
                                    .ToString());

            //status
            RadComboBox status = (RadComboBox)editedItem
                                    .FindControl("workstation_status_RadComboBox");

            _workstation.Status = decimal.Parse(status.SelectedValue);

            //workstation
            _workstation.WorkstationLabel = (editedItem["workstation_label"]
                                                .Controls[0] as TextBox).Text;


            /****/
            //trolley
            ////RadComboBox trolley = (RadComboBox)editedItem
            ////                        .FindControl("trolley_label_RadComboBox");

            _workstation.TrolleyID = -1;//decimal.Parse(trolley.SelectedValue);

            //international
            RadComboBox isInternational = (RadComboBox)editedItem
                                    .FindControl("international_ind_RadComboBox");

            _workstation.IsInternational = isInternational.SelectedValue;

            _workstation.LastChangedBy = User.Identity.Name;
            
            _workstationDAO.Modify(_workstation);

            BindData();
        }

        /*
        private void CreateTypeValidationScript(string workstationTypeCtlID, string trolleyCtlID)
        {
            ClientScript
                .RegisterClientScriptBlock(
                    this.GetType(),
                    "typeDropdownScript",
                    "function CheckType()" +
                    "{" +
                    "    if (document.getElementById('" +
                        workstationTypeCtlID + "').value != '" + WorkstationType.PS.ToString() + "')" +
                    "    {" +
                    "        document.getElementById('" +
                            trolleyCtlID + "').style.visibility = 'hidden';" +
                    "        var combo = $find('" + trolleyCtlID + "');" +
                    "        " +
                    "        var comboItem = combo.findItemByText('item');" +
                    "        if (comboItem){" +
                    "        }else{" +
                    "         comboItem= new Telerik.Web.UI.RadComboBoxItem();" +
                    "         comboItem.set_text('item');" +
                    "         comboItem.set_value('0');" +
                    "         combo.trackChanges();" +
                    "         combo.get_items().add(comboItem);" +
                    "         comboItem.select(); " +
                    "         combo.commitChanges();}" +
                    "    }" +
                    "    else" +
                    "    {" +
                    "          var combo = $find('" + trolleyCtlID + "');" +
                    "          var items = combo.get_items();" +
                    "          var comboItem = combo.findItemByText('item');" +
                    "          if (comboItem)" +
                    "          {" +
                    "              combo.trackChanges();" +
                    "              items.remove(comboItem);" +
                    "              combo.commitChanges();" +
                    "          }" +
                    "          document.getElementById('" +
                                trolleyCtlID + "').style.visibility = 'visible';" +
                    "    }" +
                    "}", true);
        }
        */
        protected void workstationGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {
                DataSet dropdownSource = new DataSet();
                
                GridEditableItem editedItem = e.Item as GridEditableItem;

                SNumCodeDAO sNumCodeDAO = new SNumCodeDAO();

                //workstation type
                dropdownSource = sNumCodeDAO
                                .GetCodesByType(SNumcodeType.MDS_WORKSTATION_TYPE.ToString());

                RadComboBox workstationType = (RadComboBox)editedItem
                                                .FindControl("workstation_type_RadComboBox");

                TextBox workstationLabel = (TextBox)editedItem.FindControl("workstation_label");
                
                workstationType
                    .DataSource = dropdownSource
                                    .Tables[SNumcodeType.MDS_WORKSTATION_TYPE.ToString()];
                
                workstationType.DataTextField = "char_short_translation";
                
                workstationType.DataValueField = "code";
                
                workstationType.DataBind();
                workstationType.Items.Insert(0, new RadComboBoxItem("", "0"));
                
                //workstation status
                dropdownSource = sNumCodeDAO
                                .GetCodesByType(SNumcodeType.MDS_WORKSTATION_STATUS.ToString());

                RadComboBox workstationStatus = (RadComboBox)editedItem
                                                    .FindControl("workstation_status_RadComboBox");

                workstationStatus.DataSource = dropdownSource
                                                   .Tables[SNumcodeType.MDS_WORKSTATION_STATUS.ToString()];

                workstationStatus.DataTextField = "char_short_translation";

                workstationStatus.DataValueField = "code";
                
                workstationStatus.DataBind();
                workstationStatus.Items.Insert(0, new RadComboBoxItem("", "0"));

                
                //is international
                RadComboBox isInternational = (RadComboBox)editedItem
                                                .FindControl("international_ind_RadComboBox");

                isInternational.Items.Add(new RadComboBoxItem("Yes","T"));
                isInternational.Items.Add(new RadComboBoxItem("No", "F"));


                isInternational.DataBind();
                isInternational.SelectedValue = "F";

                /****/
                ////trolley
                //TrolleyDAO trolleyDAO = new TrolleyDAO();
                //dropdownSource = trolleyDAO.Search_trolley();

                //RadComboBox trolley = (RadComboBox)editedItem
                //                        .FindControl("trolley_label_RadComboBox");

                //trolley.DataSource = dropdownSource.Tables[0];
                //trolley.DataTextField = "trolley_label";
                //trolley.DataValueField = "trolley_id";
                //trolley.DataBind();
                //trolley.Items.Insert(0, new RadComboBoxItem("", "0"));

                

                //Register script for status validation
                //CreateTypeValidationScript(workstationType.ClientID, trolley.ClientID);

                //In case is edit, 
                //pre populate the fields with the record being edited
                _workstation = null;
                if (e.Item.ItemIndex != -1)
                {
                    int workstationID = int.Parse(
                                        editedItem
                                        .OwnerTableView
                                        .DataKeyValues[e.Item.ItemIndex]["workstation_id"]
                                        .ToString());

                    _workstationDAO = new PackstationDAO();

                    _workstation = _workstationDAO.GetWorkstationByID(workstationID);


                    workstationType.SelectedValue = _workstation.Type.ToString();
                    workstationStatus.SelectedValue = _workstation.Status.ToString();
                    isInternational.SelectedValue = _workstation.IsInternational.ToString();

                    /****/
                    //trolley.SelectedValue = _workstation.TrolleyID.ToString();
                }
                else
                {
                    workstationLabel.Text = Shared.UserHostName.Substring(0, Shared.UserHostName.IndexOf('.') - 1);

                }
            }
        }

        protected void workstationGrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            
            //type
            RadComboBox type = (RadComboBox)insertedItem
                                    .FindControl("workstation_type_RadComboBox");

            SetInstance(type.SelectedItem.Text);

            _workstation.Type = decimal.Parse(type.SelectedValue);

            //status
            RadComboBox status = (RadComboBox)insertedItem
                                    .FindControl("workstation_status_RadComboBox");

            _workstation.Status = decimal.Parse(status.SelectedValue);

            //workstation
            _workstation.WorkstationLabel = (insertedItem["workstation_label"]
                                                .Controls[0] as TextBox).Text;


            /****/
            ////trolley
            //RadComboBox trolley = (RadComboBox)insertedItem
            //                        .FindControl("trolley_label_RadComboBox");

            //_workstation.TrolleyID = decimal.Parse(trolley.SelectedValue);

            //international
            RadComboBox isInternational = (RadComboBox)insertedItem
                                    .FindControl("international_ind_RadComboBox");

            _workstation.IsInternational = isInternational.SelectedValue;

            _workstation.CreatedBy = User.Identity.Name;

            
            _workstationDAO.Add(_workstation);

            BindData();
            
        }

        private void SetInstance(string workstationType)
        {
            _workstation = null;
            _workstationDAO = null;
            if (workstationType.CompareTo(WorkstationType.IS.ToString()) == 0)
            {
                _workstationDAO = new InductstationDAO();
                _workstation = new Inductstation();
            }
            else if (workstationType.CompareTo(WorkstationType.PS.ToString()) == 0)
            {
                _workstationDAO = new PackstationDAO();
                _workstation = new Packstation();
            }
            else
            {
                _workstationDAO = new SupervisorDAO();
                _workstation = new Supervisorstation();
            }
        }

        protected void workstationGrid_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor workstationEditor = (GridTextBoxColumnEditor)item
                                                    .EditManager
                                                    .GetColumnEditor("workstation_label");

                TableCell nameCell = (TableCell)workstationEditor.TextBoxControl.Parent;
                //nameCell.Text = Dns.GetHostName();

                RequiredFieldValidator workstationValidator = new RequiredFieldValidator();
                workstationEditor.TextBoxControl.ID = "workstation_label";
                workstationValidator.ControlToValidate = workstationEditor.TextBoxControl.ID;
                workstationValidator.ErrorMessage = "*Enter workstation label";
                workstationValidator.ForeColor = Color.Red;
                nameCell.Controls.Add(workstationValidator);
            }
        }

        protected void workstationGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("workstation_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "3";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);

            } 
        }

        
    }
}