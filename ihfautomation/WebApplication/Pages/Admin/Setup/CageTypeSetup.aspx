<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="CageTypeSetup.aspx.cs" Inherits="CageTypeSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>
 
   <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />

	<script type="text/javascript">
		//Put your JavaScript code here.
    </script>
    <p id="divMsgs" runat="server">
        <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF8080"></asp:Label>
        <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" 
            ForeColor="#000099"></asp:Label>
    </p>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divMsgs" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
	<div>
        <telerik:RadGrid ID="CageTypesRadGrid" runat="server" 
            onneeddatasource="RadGrid1_NeedDataSource" 
            CellSpacing="0" GridLines="None" 
            ondetailtabledatabind="CageTypesRadGrid_DetailTableDataBind" 
            oneditcommand="CageTypesRadGrid_EditCommand" Skin="Default"
            onitemcommand="CageTypesRadGrid_ItemCommand" 
            onitemdatabound="CageTypesRadGrid_ItemDataBound" 
            AutoGenerateColumns="False" ShowStatusBar="True" 
            AllowAutomaticDeletes="True" AllowAutomaticInserts="True" 
            AllowAutomaticUpdates="True" onitemevent="CageTypesRadGrid_ItemEvent" 
            oninsertcommand="CageTypesRadGrid_InsertCommand" 
            onitemdeleted="CageTypesRadGrid_ItemDeleted" 
            oniteminserted="CageTypesRadGrid_ItemInserted" 
            onitemupdated="CageTypesRadGrid_ItemUpdated" 
            onupdatecommand="CageTypesRadGrid_UpdateCommand" 
            onprerender="CageTypesRadGrid_PreRender" 
            ondeletecommand="CageTypesRadGrid_DeleteCommand" 
            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" 
            PageSize="20" ShowFooter="True" Width="96%">
<MasterTableView 
                CommandItemDisplay="Top" DataKeyNames="cage_type_id" EditMode="PopUp" 
                Caption="Cage Types" 
                NoDetailRecordsText="No services are assigned to cage type" 
                AllowAutomaticDeletes="False" AllowAutomaticInserts="False" 
                AllowAutomaticUpdates="False" 
                NoMasterRecordsText="No cage types are set up">
    <DetailTables>
        <telerik:GridTableView runat="server" DataKeyNames="cage_type_id" 
            HierarchyDefaultExpanded="True" AllowFilteringByColumn="False" 
            AllowPaging="False">
            <NoRecordsTemplate>
                No Carrier Services Assigned.
            </NoRecordsTemplate>
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px" />
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px" />
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="cage_type_id" 
                    FilterControlAltText="Filter column column" UniqueName="column" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="carrier_service_id" 
                    FilterControlAltText="Filter column1 column" UniqueName="column1" HeaderText="Carrier Service ID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="carrier_service_descr" 
                    FilterControlAltText="Filter column2 column" UniqueName="column2" HeaderText="Service Description">
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                </EditColumn>
            </EditFormSettings>
        </telerik:GridTableView>
    </DetailTables>
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
        Visible="True">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridEditCommandColumn 
            FilterControlAltText="Filter EditCommandColumn column" 
            ButtonType="PushButton">
        </telerik:GridEditCommandColumn>
        <telerik:GridBoundColumn DataField="cage_type_id" 
            FilterControlAltText="Filter column column" HeaderText="Cage Type" 
            UniqueName="cagetype_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_type_descr" 
            FilterControlAltText="Filter column1 column" HeaderText="Cage Type Description" 
            UniqueName="cagetypedescr_column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="carrier_id" 
            FilterControlAltText="Filter column column" HeaderText="Carrier Id" 
            UniqueName="carrierid_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="carrier_descr" 
            FilterControlAltText="Filter column1 column" HeaderText="Carrier Description" 
            UniqueName="carrierdescr_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="country_group_id" 
            FilterControlAltText="Filter column column" HeaderText="Country Grp Id" 
            UniqueName="countrygrpid_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="country_group_descr" 
            FilterControlAltText="Filter column1 column" HeaderText="Country Grp Description" 
            UniqueName="countrygrpdescr_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="despatchable_ind" 
            FilterControlAltText="Filter despatchableind_column column" 
            HeaderText="Despatchable" UniqueName="despatchableind_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="sd_group_name" 
            FilterControlAltText="Filter sdgroupname_column column" 
            HeaderText="Store Delivery Group Name" UniqueName="sdgroupname_column">
        </telerik:GridBoundColumn>
        <telerik:GridButtonColumn CommandName="Delete" 
            FilterControlAltText="Filter DeleteColumn column" Text="Delete" 
            UniqueName="DeleteColumn" ButtonType="PushButton">
        </telerik:GridButtonColumn>
        <telerik:GridBoundColumn DataField="sd_group_id" 
            FilterControlAltText="Filter sdgroupidcolumn column" 
            UniqueName="sdgroupidcolumn" Visible="False">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings EditFormType="Template" InsertCaption="Create Cage Type" 
        CaptionFormatString="Edit Cage Type">
<EditColumn FilterControlAltText="Filter EditCommandColumn column" 
        UniqueName="EditCommandColumn1" ButtonType="ImageButton"></EditColumn>
    <FormStyle BorderWidth="1px" />
    <FormTemplate>                    
                    <table style="height: 379px; width: 476px"><tr><td style="width: 10px"></td><td>
                     <table>
                      <tr><td><asp:label ID="txtCageTypeLabel" runat="server">Cage Type: </asp:label></td>                             
                          <td><asp:Label ID="lblCageType" runat="server"></asp:Label>
                              <telerik:RadTextBox style="text-transform: uppercase;" ID="txtCageType" 
                                  runat="server" MaxLength="10" Width="200px"></telerik:RadTextBox>
                              </td>
                      </tr>
                      <tr><td><asp:label ID="lblCageTypeDescr" runat="server" Text="Description: "></asp:label></td>
                          <td><telerik:RadTextBox ID="txtCageTypeDescr" runat = "server" Width="200px"></telerik:RadTextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCageTypeDescr" runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator></td>
</td></tr>
                      <tr><td><asp:label ID="lblCarriers" runat="server" Text="Primary Carrier: "></asp:label></td>
                           <td><telerik:RadComboBox ID="rcbCarriers" Runat="server">
                     </telerik:RadComboBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rcbCarriers" runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator></td></tr>
                        <tr>
                            <td>
                                Despatchable:</td>
                            <td>
                                <asp:CheckBox ID="cbDespatchableInd" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCarriers0" runat="server" Text="Country Grp: "></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbCntryGrps" Runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Store Delivery Grp:</td>
                            <td>
                                <telerik:RadComboBox ID="rcbStoreDeliveryGroup" Runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                     </table>

                    <br />                    
                    <table><tr><td>
                        Select Available Services From</td><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="rcbCarriers2" Runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rcbCarriers2_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Available Services
                                <telerik:RadListBox ID="RadListBoxFrom" runat="server" AllowTransfer="True" 
                                    DataTextField="carrier_service_descr" DataValueField="carrier_service_id" 
                                    Height="170px" TransferToID="RadListBoxTo" Width="240px">
                                </telerik:RadListBox>
                            </td>
                            <td>
                                Assigned Services
                                <telerik:RadListBox ID="RadListBoxTo" runat="server" 
                                    DataTextField="carrier_service_descr" DataValueField="carrier_service_id" 
                                    Height="170px" onselectedindexchanged="RadListBoxTo_SelectedIndexChanged" 
                                    Width="240px">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                                         <table><tr><td><asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'> 
                                </asp:Button>&nbsp;
<asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
CommandName="Cancel"></asp:Button></td></tr></table>
</td></tr></table>
    </FormTemplate>
    <PopUpSettings Height="475px" Modal="True" Width="550px" />
</EditFormSettings>
</MasterTableView>
<ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
     <ClientEvents OnRowDblClick="RowDblClick" />
</ClientSettings>
<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
        </telerik:RadGrid>
	</div>
  </asp:Content>
