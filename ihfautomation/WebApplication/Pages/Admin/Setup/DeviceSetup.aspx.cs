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

namespace IHF.ApplicationLayer.Web.Administration.Setup
{
    public partial class DeviceSetup : System.Web.UI.Page
    {
        private DeviceDAO _deviceDAO = new DeviceDAO();
        private Device _device = null;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void deviceGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            DataSet devices = new DataSet();
            
            devices = _deviceDAO.GetDevices();

            deviceGrid.DataSource = devices.Tables["mds_device"];
        }

        protected void deviceGrid_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            _device = new Device();
            _device.ID = int.Parse(
                                (e.Item as GridDataItem)
                                .OwnerTableView
                                .DataKeyValues[e.Item.ItemIndex]["device_id"]
                                .ToString());

            _device.LastChangedBy = User.Identity.Name;


            int returnResult = _deviceDAO.Delete(_device);

            BindData();
        }

        private void SetInstance(string deviceType)
        {
            _device = null;
            _deviceDAO = null;
            if (deviceType.CompareTo(DeviceType.HH.ToString()) == 0)
            {
                _deviceDAO = new HandheldDAO();
                _device = new IHF.BusinessLayer.BusinessClasses.Handheld();
            }
            else
            {
                _deviceDAO = new DeviceDAO();
                _device = new Device();
            }
        }


        protected void deviceGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            //device type
            RadComboBox type = (RadComboBox)editedItem
                                    .FindControl("device_type_RadComboBox");

            SetInstance(type.SelectedItem.Text);

            _device.Type = decimal.Parse(type.SelectedValue);

            //id
            _device.ID = int.Parse(
                                    editedItem
                                    .OwnerTableView
                                    .DataKeyValues[e.Item.ItemIndex]["device_id"]
                                    .ToString());

            //device name
            _device.DeviceName = ((TextBox)editedItem.FindControl("device_nameTextBox")).Text;

            //workstation
            RadComboBox workstation = (RadComboBox)editedItem
                                    .FindControl("workstation_label_RadComboBox");

            _device.WorkstationId = decimal.Parse(workstation.SelectedValue);


            //serial number
            //_device.SerialNumber = (editedItem["serial_number"]
            //                                    .Controls[0] as TextBox).Text;

            //barcode
            _device.Barcode = (editedItem["barcode"]
                                                .Controls[0] as TextBox).Text;

            //username
            //RadComboBox username = (RadComboBox)editedItem
            //                        .FindControl("username_RadComboBox");

            //_device.CurrentUser = username.SelectedValue;

            _device.LastChangedBy = User.Identity.Name;

            _deviceDAO.Modify(_device);

            BindData();
        }

        private void CreateStatusValidationScript(string devicetypeCtlID, string workstationCtlID)
        {
            ClientScript
                .RegisterClientScriptBlock(
                    this.GetType(),
                    "typeDropdownScript",
                    "function CheckStatus()" +
                    "{" +
                    "    if (document.getElementById('" +
                        devicetypeCtlID + "').value == '" + DeviceType.HH.ToString() + "')" +
                    "    {" +
                    "        document.getElementById('" +
                            workstationCtlID + "').style.visibility = 'hidden';" +
                    "        var combo = $find('" + workstationCtlID + "');" +
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
                    "          var combo = $find('" + workstationCtlID + "');" +
                    "          var items = combo.get_items();" +
                    "          var comboItem = combo.findItemByText('item');" +
                    "          if (comboItem)" +
                    "          {" +
                    "              combo.trackChanges();" +
                    "              items.remove(comboItem);" +
                    "              combo.commitChanges();" +
                    "          }" +
                    "          document.getElementById('" +
                                workstationCtlID + "').style.visibility = 'visible';" +
                    "    }" +
                    "}", true);
        }

        protected void deviceGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;

                //get all device types
                DataSet dropdownSource = new DataSet();
                
                SNumCodeDAO sNumCodeDAO = new SNumCodeDAO();

                
                dropdownSource = sNumCodeDAO
                                .GetCodesByType(SNumcodeType.MDS_DEVICE_TYPE.ToString());

                RadComboBox deviceType = (RadComboBox)editedItem
                                                .FindControl("device_type_RadComboBox");

                RadComboBox deviceName = (RadComboBox)editedItem.FindControl("rcbName");

                if (e.Item.ItemIndex < 0)
                {
                    DeviceDAO devDAO = new DeviceDAO();

                    DataTable dtTemp = devDAO.GetDevices().Tables[0].Select("device_type <> 'CT'").CopyToDataTable();

                    deviceName.DataTextField = "device_name";
                    deviceName.DataValueField = "device_name";

                    deviceName.DataSource = dtTemp;
                    deviceName.DataBind();
                }
           

                deviceType
                    .DataSource = dropdownSource
                                    .Tables[SNumcodeType.MDS_DEVICE_TYPE.ToString()];

                deviceType.DataTextField = "char_short_translation";

                deviceType.DataValueField = "code";

                deviceType.DataBind();

                deviceType.Items.Insert(0, new RadComboBoxItem("", "0"));
                

                //workstations
                WorkstationDAO deviceDAO = new PackstationDAO();

                dropdownSource = deviceDAO.GetWorkstations();

                RadComboBox workstationLabel = (RadComboBox)editedItem
                                                    .FindControl("workstation_label_RadComboBox");
                RequiredFieldValidator RequiredFieldValidator2 = (RequiredFieldValidator)editedItem
                                                    .FindControl("RequiredFieldValidator2");

                workstationLabel.DataSource = dropdownSource.Tables["mds_workstation"];

                workstationLabel.DataTextField = "workstation_label";

                workstationLabel.DataValueField = "workstation_id";

                workstationLabel.DataBind();

                workstationLabel.Items.Insert(0, new RadComboBoxItem("", "0"));
                
                //users
                //UserDAO userDAO = new UserDAO();
                
                //dropdownSource = userDAO.GetUsers();

                //RadComboBox username = (RadComboBox)editedItem
                //                                .FindControl("username_RadComboBox");

                //username.DataSource = dropdownSource.Tables["tblusers"];
                //username.DataTextField = "displayname";
                //username.DataValueField = "userid";
                //username.DataBind();
                //username.Items.Insert(0, new RadComboBoxItem("", "0"));
                

                //Register script for status validation
                CreateStatusValidationScript(deviceType.ClientID, workstationLabel.ClientID);
                
                _device = null;
                if (e.Item.ItemIndex != -1)
                {
                    int deviceID = int.Parse(
                                        editedItem
                                        .OwnerTableView
                                        .DataKeyValues[e.Item.ItemIndex]["device_id"]
                                        .ToString());

                    _deviceDAO = new DeviceDAO();
                    
                    _device = _deviceDAO.GetDeviceByID(deviceID); 


                    deviceType.SelectedValue = _device.Type.ToString();
                    workstationLabel.SelectedValue = _device.WorkstationId.ToString();
                    //username.SelectedValue = _device.CurrentUser.ToString();
                }
            }
        }

        protected void deviceGrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertedItem = (GridEditFormInsertItem)e.Item;

            //device type
            RadComboBox type = (RadComboBox)insertedItem
                                    .FindControl("device_type_RadComboBox");

            SetInstance(type.SelectedItem.Text);

            _device.Type = decimal.Parse(type.SelectedValue);

            RadComboBox name = (RadComboBox)insertedItem.FindControl("rcbName");

            //device name
            _device.DeviceName = name.SelectedValue; 

            //workstation
            RadComboBox workstation = (RadComboBox)insertedItem
                                    .FindControl("workstation_label_RadComboBox");

            _device.WorkstationId = decimal.Parse(workstation.SelectedValue);

            //serial number
            //_device.SerialNumber = (insertedItem["serial_number"]
            //                                    .Controls[0] as TextBox).Text;

            //barcode
            _device.Barcode = (insertedItem["barcode"]
                                                .Controls[0] as TextBox).Text;

            //username
            //RadComboBox username = (RadComboBox)insertedItem
            //                        .FindControl("username_RadComboBox");

            //_device.CurrentUser = username.SelectedValue;

            _device.CreatedBy = User.Identity.Name;

            _deviceDAO.Add(_device);

        }

        protected void deviceGrid_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //GridEditableItem item = e.Item as GridEditableItem;
                //GridTextBoxColumnEditor nameEditor = (GridTextBoxColumnEditor)item
                //                                    .EditManager
                //                                    .GetColumnEditor("device_name");

                //GridTextBoxColumnEditor serialNumberEditor = (GridTextBoxColumnEditor)item
                //                                    .EditManager
                //                                    .GetColumnEditor("serial_number");

                //TableCell nameCell = (TableCell)nameEditor.TextBoxControl.Parent;
                ////TableCell serialNumberCell = (TableCell)serialNumberEditor.TextBoxControl.Parent;

                //RequiredFieldValidator nameValidator = new RequiredFieldValidator();
                //nameEditor.TextBoxControl.ID = "device_name";
                //nameValidator.ControlToValidate = nameEditor.TextBoxControl.ID;
                //nameValidator.ErrorMessage = "*Enter device name";
                //nameValidator.ForeColor = Color.Red;
                //nameCell.Controls.Add(nameValidator);

                //RequiredFieldValidator serialNumberValidator = new RequiredFieldValidator();
                //serialNumberEditor.TextBoxControl.ID = "serial_number";
                //serialNumberValidator.ControlToValidate = serialNumberEditor.TextBoxControl.ID;
                //serialNumberValidator.ErrorMessage = "*Enter serial number";
                //serialNumberValidator.ForeColor = Color.Red;
                //serialNumberCell.Controls.Add(serialNumberValidator);
            }
        }

        protected void deviceGrid_EditCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        protected void deviceGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                String strolleyid = dataItem.GetDataKeyValue("device_id").ToString();
                Int32 itrolleyid = Int32.Parse(strolleyid);


                //HttpContext.Current.Response.Write("inside the btn_trolley_ps_Click");
                string machinename = Shared.UserHostName;
                string reportname = "4";
                string devicetype = "6";
                //HttpContext.Current.Response.Write("before calling webservice " + machinename + reportname + devicetype);

                PrintService ps = new PrintService();
                string test = ps.PrintLabel(reportname, machinename, devicetype, itrolleyid, true);
                //HttpContext.Current.Response.Write("after print" + test);


            }

        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            PrintService ps = new PrintService();

            ps.GetAvailablePrinters(Shared.CurrentUser);


        }
    }
}