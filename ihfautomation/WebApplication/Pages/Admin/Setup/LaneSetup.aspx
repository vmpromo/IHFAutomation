<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="LaneSetup.aspx.cs" Inherits="LaneSetup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />

	<telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager>

	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>
    <p id="divMsgs" runat="server">
        <asp:Label ID="Label1" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="#FF8080"></asp:Label>
        <asp:Label ID="Label2" runat="server" EnableViewState="False" Font-Bold="True" 
            ForeColor="#003399"></asp:Label>
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
        <telerik:RadGrid ID="LanesRadGrid" runat="server" 
            onneeddatasource="LanesRadGrid_NeedDataSource" 
            CellSpacing="0" GridLines="None" 
            ondetailtabledatabind="LanesRadGrid_DetailTableDataBind" 
            oneditcommand="LanesRadGrid_EditCommand" Skin="Default" 
            onitemcommand="LanesRadGrid_ItemCommand" 
            onitemdatabound="LanesRadGrid_ItemDataBound" 
            AutoGenerateColumns="False" ShowStatusBar="True" 
            AllowAutomaticDeletes="True" AllowAutomaticInserts="True" 
            AllowAutomaticUpdates="True" onitemevent="LanesRadGrid_ItemEvent" 
            oninsertcommand="LanesRadGrid_InsertCommand" 
            onitemdeleted="LanesRadGrid_ItemDeleted" 
            oniteminserted="LanesRadGrid_ItemInserted" 
            onitemupdated="LanesRadGrid_ItemUpdated" 
            onupdatecommand="LanesRadGrid_UpdateCommand" 
            onprerender="LanesRadGrid_PreRender" 
            ondeletecommand="LanesRadGrid_DeleteCommand" AllowPaging="True" AllowSorting="True" 
            PageSize="20" ShowFooter="True" onitemcreated="LanesRadGrid_ItemCreated" 
            Width="96%">
<MasterTableView 
                CommandItemDisplay="Top"
                DataKeyNames="lane_id" EditMode="PopUp" 
                Caption="Lanes" 
                NoDetailRecordsText="No services are assigned to cage type" 
                AllowAutomaticDeletes="False" AllowAutomaticInserts="False" 
                AllowAutomaticUpdates="False" 
                NoMasterRecordsText="No lane records are set up">
<CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton=false></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="lane_id" 
            FilterControlAltText="Filter column column" HeaderText="Lane Number" 
            UniqueName="Laneid_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="active_ind" 
            FilterControlAltText="Filter column1 column" HeaderText="Active" 
            UniqueName="Activeind_column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_type_descr" 
            FilterControlAltText="Filter column column" HeaderText="Assigned Cage Type" 
            UniqueName="cagetype_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_name" 
            FilterControlAltText="Filter column1 column" HeaderText="Attached Cage" 
            UniqueName="carrierdescr_column">
        </telerik:GridBoundColumn>
        <telerik:GridEditCommandColumn 
            FilterControlAltText="Filter EditCommandColumn column" 
            ButtonType="PushButton" EditText="Assign Cage Type" Visible="False">
        </telerik:GridEditCommandColumn>
        <telerik:GridButtonColumn CommandName="Delete" 
            FilterControlAltText="Filter DeleteColumn column" Text="Unassign Cage Type" 
            UniqueName="DeleteColumn" ButtonType="PushButton" Visible="False">
        </telerik:GridButtonColumn>
        <telerik:GridButtonColumn UniqueName="PrintColumn" CommandName="print" ButtonType="PushButton"
                                Text="Print" />
    </Columns>

<EditFormSettings EditFormType="Template" InsertCaption="Create Cage Type" 
        CaptionFormatString="Assign Cage Type To Lane">
<EditColumn FilterControlAltText="Filter EditCommandColumn column" 
        UniqueName="EditCommandColumn1" ButtonType="ImageButton"></EditColumn>
    <FormStyle BorderWidth="1px" />
    <FormTemplate>                    
                    <table><tr><td style="width: 10px"></td><td>
                     

                        <telerik:RadComboBox ID="cagetype_radcombobox" Runat="server" 
                            Label="Cage Type: ">
                        </telerik:RadComboBox>
                     

                    <br />                    
                                         <table><tr><td><asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Assign Cage Type" %>'
                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                </asp:Button>&nbsp;
<asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
CommandName="Cancel"></asp:Button></td></tr></table>
</td></tr></table>
    </FormTemplate>
    <PopUpSettings Height="100px" Modal="True" Width="300px" />
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
