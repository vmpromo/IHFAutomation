<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" CodeBehind="ClearTote.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.ClearTote" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Pages/Dashboard/IHFDashboard.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SetCursorToTextEnd(textControlID) {
            var text = document.getElementById(textControlID);
            if (text != null && text.value.length > 0) {
                if (text.createTextRange) {
                    var FieldRange = text.createTextRange();
                    FieldRange.moveStart('character', text.value.length);
                    FieldRange.collapse();
                    FieldRange.select();
                }
            }
        } 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">            
        <!--
            //Custom js code section used to edit records, store changes and switch the visibility of column editors

            //global variables for edited cell and edited rows ids
            var editedCell;
            var arrayIndex = 0;
            var editedItemsIds = [];
            var editedDDIds = [];

            function RowCreated(sender, eventArgs) {
                var dataItem = eventArgs.get_gridDataItem();

                //traverse the cells in the created client row object and attach dblclick handler for each of them
                var cell = dataItem.get_element().cells[6];
                if (cell) {
                    $addHandler(cell, "dblclick", Function.createDelegate(cell, ShowColumnEditor));
                }
            }
            //detach the ondblclick handlers from the cells on row disposing
            function RowDestroying(sender, eventArgs) {
                if (eventArgs.get_id() === "") return;
                var row = eventArgs.get_gridDataItem().get_element();
                var cells = row.cells;
                for (var j = 0, len = cells.length; j < len; j++) {
                    var cell = cells[j];
                    if (cell) {
                        $clearHandlers(cell);
                    }
                }
            }

            function RowClick(sender, eventArgs) {
                if (editedCell) {
                    //if the click target is table cell or span and there is an edited cell, update the value in it
                    //skip update if clicking a span that happens to be a form decorator element (having a class that starts with "rfd")
                    if ((eventArgs.get_domEvent().target.tagName == "TD") ||
                        (eventArgs.get_domEvent().target.tagName == "SPAN" && !eventArgs.get_domEvent().target.className.startsWith("rfd"))) {
                        UpdateValues(sender);
                        editedCell = false;
                    }
                }
            }
            function ShowColumnEditor() {
                editedCell = this;

                //hide text and show column editor in the edited cell
                if (this.getElementsByTagName("span") != "undefined")
                    var cellText = this.getElementsByTagName("span")[0];
                cellText.style.display = "none";

                //display the span which wrapps the hidden checkbox editor
                if (this.getElementsByTagName("span")[1]) {
                    this.getElementsByTagName("span")[1].style.display = "";
                }
                var colEditor = this.getElementsByTagName("input")[0] || this.getElementsByTagName("select")[0];
                //if the column editor is a form decorated select dropdown, show it instead of the original
                if (colEditor.className == "rfdRealInput" && colEditor.tagName.toLowerCase() == "select") colEditor = Telerik.Web.UI.RadFormDecorator.getDecoratedElement(colEditor);
                colEditor.style.display = "";
                colEditor.focus();
            }
            function StoreEditedItemId(editCell, dropdownValue) {
                //get edited row key value and add it to the array which holds them
                var gridRow = $find(editCell.parentNode.id);
                var rowKeyValue = gridRow.getDataKeyValue("skufailedtoteid");
                Array.add(editedItemsIds, rowKeyValue);
                //drop down value
                Array.add(editedDDIds, dropdownValue);
            }
            function HideEditor(editCell, editorType) {
                //get reference to the label in the edited cell
                var lbl = editCell.getElementsByTagName("span")[0];

                switch (editorType) {
                    case "textbox":
                        var txtBox = editCell.getElementsByTagName("input")[0];
                        if (lbl.innerHTML != txtBox.value) {
                            lbl.innerHTML = txtBox.value;
                            editCell.style.border = "1px dashed Red";

                            StoreEditedItemId(editCell);
                        }
                        txtBox.style.display = "none";
                        break;
                    case "checkbox":
                        var chkBox = editCell.getElementsByTagName("input")[0];
                        if (lbl.innerHTML.toLowerCase() != chkBox.checked.toString()) {
                            lbl.innerHTML = chkBox.checked;
                            editedCell.style.border = "1px dashed";

                            StoreEditedItemId(editCell);
                        }
                        chkBox.style.display = "none";
                        editCell.getElementsByTagName("span")[1].style.display = "none";
                        break;
                    case "dropdown":
                        var ddl = editCell.getElementsByTagName("select")[0];
                        var selectedValue = ddl.options[ddl.selectedIndex].value;
                        var selectedText = ddl.options[ddl.selectedIndex].text;
                        if (lbl.innerHTML != selectedText) {
                            lbl.innerHTML = selectedText;
                            editCell.style.border = "1px dashed Red";

                            StoreEditedItemId(editCell, selectedValue);
                        }
                        //if the form decorator was enabled, hide the decorated dropdown instead of the original.
                        if (ddl.className == "rfdRealInput") ddl = Telerik.Web.UI.RadFormDecorator.getDecoratedElement(ddl);
                        ddl.style.display = "none";
                    default:
                        break;
                }
                lbl.style.display = "inline";
            }
            function UpdateValues(grid) {
                //determine the name of the column to which the edited cell belongs
                var tHeadElement = grid.get_element().getElementsByTagName("thead")[0];
                var headerRow = tHeadElement.getElementsByTagName("tr")[0];
                var colName = grid.get_masterTableView().getColumnUniqueNameByCellIndex(headerRow, editedCell.cellIndex);

                //based on the column name, extract the value from the editor, update the text of the label and switch its visibility with that of the column
                //column. The update happens only when the column editor value is different than the non-editable value. We also set dashed border to indicate
                //that the value in the cell is changed. The logic is isolated in the HideEditor js method
                switch (colName) {
                    case "Action_RadComboBox":
                        HideEditor(editedCell, "dropdown");
                        break;
                    default:
                        break;
                }
            }
            function CancelChanges() {
                if (editedItemsIds.length > 0) {
                    $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest("");
                }
                else {
                    alert("No pending changes to be discarded");
                }
                editedItemsIds = [];
                //Clear dropdown IDs
                editedDDIds = [];
            }
            function ProcessChanges() {

                //extract edited rows ids and pass them as argument in the ajaxRequest method of the manager
                if (editedItemsIds.length > 0) {
                    if (window.confirm("Are you sure you want to process changes?")) {

                        var Ids = "";
                        for (var i = 0; i < editedItemsIds.length; i++) {
                            Ids = Ids + editedItemsIds[i] + "-" + editedDDIds[i] + ":";
                        }
                        $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest(Ids);
                    }
                    else
                        $find("<%=RadAjaxManager1.ClientID %>").ajaxRequest("");
                }
                else
                    alert("No pending changes to be processed");

                editedItemsIds = [];
                editedDDIds = [];
            }

            function RadGrid1_Command(sender, eventArgs) {
                //Note that this code has to be executed if you postback from external control instead from the grid (intercepting its onclientclick handler for this purpose),
                //otherwise the edited values will be lost or not propagated in the source
                if (editedItemsIds.length > 0) {
                    if (eventArgs.get_commandName() == "Sort" || eventArgs.get_commandName() == "Page" || eventArgs.get_commandName() == "Filter") {
                        if (confirm("Any unsaved edited values will be lost. Choose 'OK' to discard the changes before proceeding or 'Cancel' to abort the action and process them.")) {
                            editedItemsIds = [];
                            editedDDIds = [];
                        }
                        else {
                            //cancel the chosen action
                            eventArgs.set_cancel(true);

                            //process the changes
                            ProcessChanges();
                            editedItemsIds = [];
                            editedDDIds = [];
                        }
                    }
                }
            }
            window.onunload = function () {
                //this code should also be executed on postback from external control (which rebinds the grid) to process any unsaved data
                if (editedItemsIds.length > 0) {
                    if (confirm("Any unsaved edited values will be lost. Choose 'OK' to discard the changes before proceeding or 'Cancel' to abort the action and process them.")) {
                        editedItemsIds = [];
                        editedDDIds = [];
                    }
                    else {
                        //process the changes
                        ProcessChanges();
                        editedItemsIds = [];
                        editedDDIds = [];
                    }
                }
            };
	    -->	
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default,Select,Textbox"
        EnableRoundedCorners="false" />

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                    <telerik:AjaxUpdatedControl ControlID="Label1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                    <telerik:AjaxUpdatedControl ControlID="Label1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />

    <telerik:RadInputManager ID="RadInputManager1" EnableEmbeddedBaseStylesheet="false"
        Skin="" runat="server">
        
        <telerik:TextBoxSetting BehaviorID="StringBehavior" InitializeOnClient="true" EmptyMessage="type here" />
        <telerik:NumericTextBoxSetting BehaviorID="CurrencyBehavior"  EmptyMessage="type.."
            Type="Currency" Validation-IsRequired="true" MinValue="1" InitializeOnClient="true" MaxValue="100000" />
        <telerik:NumericTextBoxSetting InitializeOnClient="true" BehaviorID="NumberBehavior"  EmptyMessage="type.."
            Type="Number" DecimalDigits="0" MinValue="0" MaxValue="100" />
    </telerik:RadInputManager>

    <div style="padding-left:3px;">
            <asp:Panel ID="submitPanel" runat="server" DefaultButton="Submit">
                <table>
                    <tr>
                        <td style="background-color:#C6DFFF;width:13%;text-align:right;">
                            Enter Tote Barcode:
                        </td>
                        <td style="width:10%; margin-left: 40px;">
                            <asp:TextBox ID="toteBarcode" width="200px" runat="server" 
                                ClientIDMode="Static" TabIndex="1" />
                        </td>
                        <td style="width:5%;text-align:center;" rowspan="2">
                        <span>OR</span>
                        </td>
                        <td>
                            <asp:Button ID="Submit" Text="GO" runat="server" BackColor="#C6DFFF" 
                                BorderStyle="Inset" Font-Bold="true" Width="100px" ClientIDMode="Static" 
                                onclick="Submit_Click" TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color:#C6DFFF;width:13%;text-align:right;">
                            Enter Sku Number:
                        </td>
                        <td style="width:10%;">
                            <asp:TextBox ID="skuNumber" width="200px" runat="server" ClientIDMode="Static" 
                                TabIndex="2" />
                        </td>
                        <td style="width:5%;">
                        </td>
                        <td id="Message" runat="server" enableviewstate="false" clientidmode="Static" 
                            style="color:Green;font-size:20px;text-align:center;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />
        </div>

    <telerik:RadGrid ID="RadGrid1" Width="100%" ShowStatusBar="true"
        AllowSorting="True" PageSize="10" GridLines="None" AllowPaging="True" runat="server"
        AutoGenerateColumns="False" OnItemCreated="RadGrid1_ItemCreated" 
        onneeddatasource="RadGrid1_NeedDataSource" TabIndex="4">
        <MasterTableView TableLayout="Fixed" DataKeyNames="skufailedtoteid" EditMode="InPlace"
            ClientDataKeyNames="skufailedtoteid" CommandItemDisplay="Bottom">
            <CommandItemTemplate>
                <div style="height: 30px; text-align: right;">
                    <asp:Image ID="imgCancelChanges" runat="server" ImageUrl="~/Images/cancel.gif"
                        AlternateText="Cancel changes" ToolTip="Cancel changes" Height="24px" Style="cursor: pointer;
                        margin: 2px 5px 0px 0px;" onclick="CancelChanges();" />
                    <asp:Image ID="imgProcessChanges" runat="server" ImageUrl="~/Images/ok.gif"
                        AlternateText="Process changes" ToolTip="Process changes" Height="24px" Style="cursor: pointer;
                        margin: 2px 5px 0px 0px;" onclick="ProcessChanges();" />
                </div>
            </CommandItemTemplate>
            <Columns>
                <%--<telerik:GridBoundColumn UniqueName="skufailedtoteid" DataField="skufailedtoteid" HeaderText="skufailedtoteid"
                    ReadOnly="True" HeaderStyle-Width="10%" >
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn UniqueName="label" DataField="label" HeaderText = "Failed Tote" 
                    ReadOnly="true" SortExpression="label" HeaderStyle-Width="10%">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="sku" DataField="sku" HeaderText = "Sku" 
                    ReadOnly="true" SortExpression="sku" HeaderStyle-Width="10%">
                </telerik:GridBoundColumn>

                <%--<telerik:GridBoundColumn UniqueName="Barcode" DataField="Barcode" HeaderText = "Barcode" 
                    ReadOnly="true" SortExpression="Barcode" HeaderStyle-Width="10%">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="BarcodeCount" DataField="BarcodeCount" HeaderText = "*" 
                    ReadOnly="true" SortExpression="BarcodeCount" HeaderStyle-Width="2%">
                </telerik:GridBoundColumn>--%>

                <telerik:GridBoundColumn UniqueName="description" DataField="description" HeaderText = "Description" 
                    ReadOnly="true" SortExpression="description" HeaderStyle-Width="30%">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn UniqueName="workstation_label" DataField="workstation_label" HeaderText = "Workstation" 
                    ReadOnly="true" SortExpression="workstation_label" HeaderStyle-Width="15%">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="last_changed_by"  DataField="last_changed_by" HeaderText = "Changed By" 
                    ReadOnly="true" SortExpression="last_changed_by" HeaderStyle-Width="10%">
                </telerik:GridBoundColumn>

                <%--<telerik:GridBoundColumn UniqueName="last_changed_dtm"  DataField="last_changed_dtm" HeaderText = "Last Activity" 
                    ReadOnly="true" SortExpression="last_changed_dtm" HeaderStyle-Width="10%">
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridTemplateColumn UniqueName="last_changed_dtm" HeaderText="Last Activity" SortExpression="last_changed_dtm"
                    HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblLastActivity" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Action_RadComboBox" HeaderText="Reason" SortExpression="Action_RadComboBox"
                    HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblReason" runat="server" Text="Add Reason" />
                        <asp:DropDownList ID="Action_RadComboBox" runat="server" Style="display: none;width:100%;" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

            </Columns>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents OnRowCreated="RowCreated" OnRowClick="RowClick" OnCommand="RadGrid1_Command"
                OnRowDestroying="RowDestroying" />
        </ClientSettings>
    </telerik:RadGrid>

    <br />

  
</asp:Content>
