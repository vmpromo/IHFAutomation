<%@ Page Language="C#" AutoEventWireup="true" Title="Alert Details" CodeBehind="AlertDetails.aspx.cs"
    Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.AlertDetails" MasterPageFile="~/Pages/RI.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="ScriptManager1" runat="server" 
        EnableTheming="True">
    </telerik:RadScriptManager>
    <div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grdAlertDetails" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:Button Text="Acknowledge Alert(s)" ID="btnAcknowledge" runat="server" 
                            onclick="btnAcknowledge_Click" />
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <telerik:RadGrid ID="grdAlertDetails" runat="server" AllowPaging="True" AllowMultiRowSelection="True"
                                ShowFooter="True" ShowStatusBar="True" AllowSorting="True" PageSize="15"
                                Width="100%" OnNeedDataSource="grdOrder_NeedDataSource" 
                                AutoGenerateColumns="False" CellSpacing="0" 
                                onitemevent="grdAlertDetails_ItemEvent" GridLines="None" 
                                onupdatecommand="grdAlertDetails_UpdateCommand" 
                                onitemcreated="grdAlertDetails_ItemCreated" 
                                oncolumncreated="grdAlertDetails_ColumnCreated">
                                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="logid">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                    <Columns>
                                                           <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                            <HeaderTemplate>
                             <asp:CheckBox id="headerChkbox" oncheckedchanged="ToggleSelectedState" AutoPostBack="true" runat="server"></asp:CheckBox>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox id="CheckBox1" oncheckedchanged="ToggleRowSelection" AutoPostBack="true" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn DataField="logid" 
                                            FilterControlAltText="Filter column column" UniqueName="logid" 
                                            DataType="System.Int32">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName= "ErrorTime" DataField="ErrorTime" Display="true" HeaderText="Error Time" />
                                        <telerik:GridBoundColumn DataField="Priority" Display="true" HeaderText="Priority" />
                                        <telerik:GridBoundColumn DataField="ErrorType" Display="true" HeaderText="Error Type" />
                                        <telerik:GridBoundColumn DataField="ErrorDetail" Display="true" HeaderText="Error Detail" />
                                        <telerik:GridBoundColumn UniqueName = "AcknowledgedBy" DataField="AcknowledgedBy" Display="true" HeaderText="Acknowledged By" />
                                        <telerik:GridBoundColumn DataField="AcknowledgedTime" Display="true" HeaderText="Acknowledged Time" />
                                    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
                                </MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
