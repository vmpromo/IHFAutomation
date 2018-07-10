﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true"
    CodeBehind="Adminreleasebarcode.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Adminreleasebarcode" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;">
                Admin Release Barcode Printing
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="96%" GridLines="None" AutoGenerateColumns="False"
                    PageSize="13" AllowSorting="True" AllowPaging="True" 
                    OnNeedDataSource="RadGrid1_NeedDataSource" ShowStatusBar="True" AllowFilteringByColumn="true"
                    OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand"
                    AutoGenerateDeleteColumn="False" AutoGenerateEditColumn="False">
                    <MasterTableView DataKeyNames="trolley_id" AllowMultiColumnSorting="True" Width="100%"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--<telerik:GridButtonColumn UniqueName="DeleteColumn" CommandName="Delete" ButtonType="PushButton"
                    Text="Delete" />--%>
                            <%--<telerik:GridEditCommandColumn ButtonType="PushButton" UniqueName="EditCommandColumn">
                                <HeaderStyle Width="85px"></HeaderStyle>
                            </telerik:GridEditCommandColumn>--%>
                            <telerik:GridBoundColumn UniqueName="load_id" SortExpression="load_id" HeaderText="Load ID"
                                DataField="load_id" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="load_status" SortExpression="load_status" HeaderText="Load Status"
                                DataField="load_status" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="trolley_id" SortExpression="trolley_id" HeaderText="Trolley ID"
                                DataField="trolley_id" ReadOnly="true" />
                            
                            <telerik:GridBoundColumn UniqueName="trolley_label" SortExpression="trolley_label"
                                HeaderText="Trolley Label" DataField="trolley_label" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="trolley_barcode" SortExpression="trolley_barcode"
                                HeaderText="Barcode" DataField="trolley_barcode" ReadOnly="true" />
                            
                            <telerik:GridButtonColumn UniqueName="Print" CommandName="Print" ButtonType="PushButton"
                                Text="Print" />
                        </Columns>
                        <%--<EditFormSettings CaptionFormatString="Edit details for trolley with ID {0}" CaptionDataField="trolley_id">
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
            </td>
        </tr>
    </table>
</asp:Content>
