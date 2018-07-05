<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="StoreDelivGrpSetup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.StoreDelivGrpSetup" %>
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
        <telerik:RadGrid ID="StoreDelivGroupsRadGrid" runat="server" 
            onneeddatasource="RadGrid1_NeedDataSource" 
            CellSpacing="0" GridLines="None" 
            ondetailtabledatabind="StoreDelivGroupsRadGrid_DetailTableDataBind" 
            oneditcommand="StoreDelivGroupsRadGrid_EditCommand"
            onitemcommand="StoreDelivGroupsRadGrid_ItemCommand" 
            onitemdatabound="StoreDelivGroupsRadGrid_ItemDataBound" 
            AutoGenerateColumns="False" ShowStatusBar="True" 
            AllowAutomaticDeletes="True" AllowAutomaticInserts="True" 
            AllowAutomaticUpdates="True" onitemevent="StoreDelivGroupsRadGrid_ItemEvent" 
            oninsertcommand="StoreDelivGroupsRadGrid_InsertCommand" 
            onitemdeleted="StoreDelivGroupsRadGrid_ItemDeleted" 
            oniteminserted="StoreDelivGroupsRadGrid_ItemInserted" 
            onitemupdated="StoreDelivGroupsRadGrid_ItemUpdated" 
            onupdatecommand="StoreDelivGroupsRadGrid_UpdateCommand" 
            onprerender="StoreDelivGroupsRadGrid_PreRender" 
            ondeletecommand="StoreDelivGroupsRadGrid_DeleteCommand" 
            AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" 
            PageSize="20" ShowFooter="True" Width="96%">
<MasterTableView 
                CommandItemDisplay="Top" DataKeyNames="group_id,group_name" EditMode="PopUp" 
                Caption="Store Delivery Groups" 
                NoDetailRecordsText="No criteria are assigned to store delivery group" 
                AllowAutomaticDeletes="False" AllowAutomaticInserts="False" 
                AllowAutomaticUpdates="False" 
                NoMasterRecordsText="No store delivery groups are set up">
    <DetailTables>
        <telerik:GridTableView runat="server" DataKeyNames="group_id" 
            HierarchyDefaultExpanded="True" AllowFilteringByColumn="False" 
            AllowPaging="False">
            <NoRecordsTemplate>
                No Store Delivery Criteria Assigned.
            </NoRecordsTemplate>
            <CommandItemSettings ExportToPdfText="Export to PDF" />
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px" />
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px" />
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="group_id" 
                    FilterControlAltText="Filter column column" UniqueName="sdgroupid_column" 
                    Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="store_deliv_grp_map_id" 
                    FilterControlAltText="Filter column1 column" UniqueName="sdgroupmap_id" 
                    Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="criterion_type_code" 
                    FilterControlAltText="Filter column2 column" 
                    UniqueName="criteriontypecode_column" HeaderText="Type">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="criterion_value" 
                    FilterControlAltText="Filter criterionvalue_column column" HeaderText="Value" 
                    UniqueName="criterionvalue_column">
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
        <telerik:GridBoundColumn DataField="group_id" 
            FilterControlAltText="Filter sdgroupidcolumn column" 
            UniqueName="sdgroupid_column" Visible="False">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="group_name" 
            FilterControlAltText="Filter sdgroupname_column column" 
            HeaderText="Store Delivery Group Name" UniqueName="sdgroupname_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="description" 
            FilterControlAltText="Filter column1 column" HeaderText="Store Delivery Group Description" 
            UniqueName="sdgroupdescr_column">
        </telerik:GridBoundColumn>
        <telerik:GridButtonColumn CommandName="Delete" 
            FilterControlAltText="Filter DeleteColumn column" Text="Delete" 
            UniqueName="DeleteColumn" ButtonType="PushButton">
        </telerik:GridButtonColumn>
    </Columns>

<EditFormSettings EditFormType="Template" InsertCaption="Create Store Delivery Group" 
        CaptionFormatString="Edit Store Delivery Group">
<EditColumn FilterControlAltText="Filter EditCommandColumn column" 
        UniqueName="EditCommandColumn1" ButtonType="ImageButton"></EditColumn>
    <FormStyle BorderWidth="1px" />
    <FormTemplate>                    
                    <table style="height: 379px; width: 476px"><tr><td style="width: 10px"></td><td>
                    <table>
                       <tr>
                          <td><asp:label ID="txtStoreDeliveryGroupIdLabel" runat="server">Store Delivery Group: </asp:label></td>                             
                          <td>
                             <asp:Label ID="lblStoreDeliveryGroup" runat="server"></asp:Label>
                             <telerik:RadTextBox style="text-transform: uppercase;" ID="txtStoreDeliveryGroup" runat="server" MaxLength="10" Width="200px"></telerik:RadTextBox>
                          </td>
                       </tr>
                       <tr>
                          <td><asp:label ID="lblStoreDeliveryGroupName" runat="server" Text="Name: "></asp:label></td>
                          <td>
                             <telerik:RadTextBox ID="txtStoreDeliveryGroupName" runat = "server" Width="200px"></telerik:RadTextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStoreDeliveryGroupName" runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator>
                          </td>
                       </tr>
                       <tr>
                          <td><asp:label ID="lblStoreDeliveryGroupDescr" runat="server" Text="Description: "></asp:label></td>
                          <td>
                             <telerik:RadTextBox ID="txtStoreDeliveryGroupDescr" runat = "server" Width="200px"></telerik:RadTextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtStoreDeliveryGroupDescr" runat="server" ErrorMessage="*">*</asp:RequiredFieldValidator>
                          </td>
                       </tr>
                    </table>

                    <br />                    
                    <table><tr><td>
                        Select Criteria Type</td><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="rcbCriteriaType" Runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rcbCriteriaType_SelectedIndexChanged">
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
