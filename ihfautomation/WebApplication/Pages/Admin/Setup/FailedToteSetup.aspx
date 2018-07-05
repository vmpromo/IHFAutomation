﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="FailedToteSetup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Admin.Setup.FailedToteSetup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;">
                Failed Tote Printing
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                    PageSize="13" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource" ShowStatusBar="True" AllowFilteringByColumn="true"
                    OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand = "RadGrid1_ItemCommand"
                    AutoGenerateDeleteColumn="False" AutoGenerateEditColumn="False">
                    <MasterTableView DataKeyNames="tote_id" AllowMultiColumnSorting="True" Width="100%"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="tote_id" SortExpression="tote_id" HeaderText="Failed Tote ID"
                                DataField="tote_id" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="tote_label" SortExpression="tote_label" HeaderText="Label"
                                DataField="tote_label" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="barcode" SortExpression="barcode" HeaderText="Barcode"
                                DataField="barcode" ReadOnly="true" />                            
                            <telerik:GridBoundColumn UniqueName="order_id" SortExpression="order_id" HeaderText="Order Number"
                                DataField="order_id" ReadOnly="true" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="workstation" SortExpression="workstation" HeaderText="Workstation"
                                DataField="workstation" ReadOnly="true" Visible="true" />
                            <telerik:GridBoundColumn UniqueName="in_use" SortExpression="in_use" HeaderText="In Use"
                                DataField="in_use" ReadOnly="true" Visible="true" />
                            <telerik:GridButtonColumn UniqueName="Print" CommandName="Print" ButtonType="PushButton"
                                Text="Print" />
                            
                        </Columns>
                        <%--<EditFormSettings CaptionFormatString="Edit details for trolley with ID {0}" CaptionDataField="tote_id">
                            <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                            <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                            <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                            <EditColumn ButtonType="ImageButton" />
                        </EditFormSettings>--%>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
                <br />
                <br />
                <asp:Button ID="Btn_print_all" runat="server" onclick="Btn_print_all_Click" 
                    Text="Print All" />
            </td>
        </tr>
    </table>
</asp:Content>

