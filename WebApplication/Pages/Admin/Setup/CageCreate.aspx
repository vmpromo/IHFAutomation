<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="CageCreate.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.CageCreate" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />

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
        <telerik:RadScriptManager ID="RadScriptManager2" Runat="server">
        </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
	<div>
        <telerik:RadGrid ID="CagesRadGrid" runat="server" 
            onneeddatasource="RadGrid1_NeedDataSource" 
            CellSpacing="0" GridLines="None" 
            onitemcommand="CagesRadGrid_ItemCommand" 
            onitemdatabound="CagesRadGrid_ItemDataBound" 
            AutoGenerateColumns="False" ShowStatusBar="True" onitemevent="CagesRadGrid_ItemEvent" 
            oninsertcommand="CagesRadGrid_InsertCommand" 
            onitemdeleted="CagesRadGrid_ItemDeleted" 
            oniteminserted="CagesRadGrid_ItemInserted" 
            onitemupdated="CagesRadGrid_ItemUpdated" 
            onupdatecommand="CagesRadGrid_UpdateCommand" 
            onprerender="CagesRadGrid_PreRender" AllowFilteringByColumn="True" 
            ondeletecommand="CagesRadGrid_DeleteCommand" style="text-align: center" 
            Width="96%">
<MasterTableView AllowPaging="True" AllowSorting="True" 
                CommandItemDisplay="Top"  DataKeyNames="cage_id" 
                Caption="Cages" NoMasterRecordsText="No cages created" EditMode="PopUp" 
                AllowMultiColumnSorting="True">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="cage_barcode" 
            FilterControlAltText="Filter column column" HeaderText="Barcode" 
            UniqueName="barcode_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_name" 
            FilterControlAltText="Filter column1 column" HeaderText="Cage Name" 
            UniqueName="cagename_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_type_id" 
            FilterControlAltText="Filter column column" HeaderText="Cage Type" 
            UniqueName="cagetype_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="carrier_descr" 
            FilterControlAltText="Filter column1 column" HeaderText="Carrier Description" 
            UniqueName="cagedescr_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="cage_status_descr" 
            FilterControlAltText="Filter column2 column" HeaderText="Cage Status" 
            UniqueName="cagestatus_column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="last_changed_dtm" 
            FilterControlAltText="Filter column2 column" HeaderText="Last Changed Date" 
            UniqueName="last_changed_dtm_column" DataFormatString="{0:dd/M/yy H:mm}" 
            DataType="System.DateTime">
        </telerik:GridBoundColumn>

        <telerik:GridButtonColumn ButtonType="PushButton" CommandName="Delete" 
            FilterControlAltText="Filter Deletecolumn column" HeaderButtonType="PushButton" 
            Text="Remove" UniqueName="DeleteColumn">
        </telerik:GridButtonColumn>
        <telerik:GridButtonColumn UniqueName="PrintColumn" CommandName="print" ButtonType="PushButton"
                                Text="Print" />
    </Columns>

<EditFormSettings EditFormType="Template" InsertCaption="Create Cages" 
        CaptionFormatString="Edit Cage Type">
<EditColumn FilterControlAltText="Filter EditCommandColumn column" 
        UniqueName="EditCommandColumn1"></EditColumn>
    <FormStyle BorderWidth="1px" />
    <FormTemplate>
    <table><tr><td style="width: 10px"></td><td class="style2"> 
    <table style="width: 505px">
    <tr><td class="style5"></td><td class="style6"></td>
        <td class="style9">
        </td>
        </tr>
    <tr><td></td><td class="style11">
        Number of Cages:</td>
        <td class="style10">
            <telerik:RadNumericTextBox ID="numcages_radnumerictextbox" Runat="server" 
                Label="" MaxValue="20" MinValue="0" Width="147px">
                <NumberFormat DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="numcages_radnumerictextbox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
        </tr>
    <tr>
    <td></td><td class="style11">
        </td>
        <td class="style10">
        </td>
        </tr>
        <tr>
            <td>
                </td>
            <td class="style11">
                Cage Type:</td>
            <td class="style10">
                <telerik:RadComboBox ID="cagetype_radcombobox" Runat="server" 
                    ClientIDMode="Static" EnableLoadOnDemand="True"  
                    Label="" MaxHeight="100px" EnableAutomaticLoadOnDemand="True" 
                    EnableVirtualScrolling="True" ItemsPerRequest="10" 
                    ShowMoreResultsBox="True" ShowDropDownOnTextboxClick="true" 
                    onitemsrequested="cagetype_radcombobox_ItemsRequested" 
                    onload="cagetype_radcombobox_Load">
                </telerik:RadComboBox>
                <script type="text/javascript">

                    function validateCombo(source, args) {
                        args.IsValid = false;
                        var combo = $find("cagetype_radcombobox"); 
                        var text = combo.get_text();
                        if (text.length < 1) {
                            args.IsValid = false;
                        }
                        else {
                            var node = combo.findItemByText(text);
                            if (node) {
                                args.IsValid = true;
                            }
                            else {
                                args.IsValid = false;
                            }
                        }
                    }
                    //Put your JavaScript code here.
    </script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="cagetype_radcombobox" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ClientValidationFunction="validateCombo" 
                    ErrorMessage="Invalid Cage Type Entered" ForeColor="Red"></asp:CustomValidator>
            </td>
            <tr>
                <td>
                    </td>
                <td class="style12">
                    </td>
                <td class="style10">
                </td>
            </tr>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style11">
                Create for Stores:</td>
            <td class="style10">
                <asp:RadioButtonList ID="rblCreateForStores" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="T">Yes</asp:ListItem>
                    <asp:ListItem Value="F">No</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td class="style12">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr><td></td><td class="style12"></td>
            <td class="style10">
                <asp:Button ID="btnCreateCages" runat="server" 
                    CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
                    onclick="btnCreateCages_Click" Text="Create Cage(s)" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                    CommandName="Cancel" Text="Cancel" />
            </td>
        </tr>
    </table>
    </tr><td class="style1"></td> </table>                  
    </FormTemplate>
    <PopUpSettings Height="200px" Modal="True" Width="500px" />
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
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 148px;
        }
        .style2
        {
            width: 298px;
        }
        .style5
        {
            height: 26px;
        }
        .style6
        {
            width: 226px;
            height: 26px;
        }
        .style9
        {
            width: 729px;
            height: 26px;
            text-align: left;
        }
        .style10
        {
            width: 729px;
            text-align: left;
        }
        .style11
        {
            width: 226px;
            text-align: right;
        }
        .style12
        {
            width: 226px;
        }
    </style>
</asp:Content>

