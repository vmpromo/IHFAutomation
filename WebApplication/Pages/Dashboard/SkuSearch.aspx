﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true"
    CodeBehind="SkuSearch.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.SkuSearch" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
    <table width="100%">
        <tr>
            <td style="text-align: right; color: Black; padding: 5px 5px 5px 5px;">
                <b>SKU Search</b>
            </td>
            <td style="text-align: right;">
                <asp:Button ID="BtnClear" runat="server" onclick="BtnClear_Click" 
                    Text="Clear Search" BackColor="Silver" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Enter SKU/Barcode" BackColor="#99CCFF"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:TextBox ID="TB_sku" runat="server" onFocus="this.select()" Width="165px" BackColor="Silver"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Select Area                   "
                    BackColor="#99CCFF"></asp:Label>
                <asp:DropDownList ID="DD_area" runat="server" Height="21px" Style="margin-left: 34px"
                    Width="170px">
                </asp:DropDownList>
                <%--<table width="100%">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>--%>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Enter Load Number" BackColor="#99CCFF"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TB_load" runat="server" Width="167px" BackColor="Silver" Style="margin-left: 0px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go" 
                    Style="margin-left: 0px" BackColor="Silver" Width="37px" />&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                
            </td>
        </tr>
    </table>
    <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
                <telerik:RadGrid 
                        ID="RadGrid1" 
                        runat="server" 
                        Width="100%" 
                        GridLines="None" 
                        AutoGenerateColumns="False"
                        PageSize="20" 
                        AllowSorting="True" 
                        AllowPaging="True" 
                        OnNeedDataSource="RadGrid1_NeedDataSource"
                        ShowStatusBar="true" 
                        AllowFilteringByColumn="false" 
                        OnItemCommand="RadGrid1_ItemCommand"
                        OnItemDataBound="RadGrid1_ItemDataBound" 
                        AutoGenerateDeleteColumn="False" 
                        AutoGenerateEditColumn="False">
                    <MasterTableView DataKeyNames="sku_upc" AllowMultiColumnSorting="False" Width="100%"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ShowAddNewRecordButton="false" />
                        <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
                        <%--<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>--%>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="sku" HeaderText="SKU" DataField="sku" SortExpression="sku"
                            ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="sku_desc" HeaderText="SKU Description" DataField="sku_desc" 
                            SortExpression="sku_desc"
                                ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="sku_upc" HeaderText="SKU UPC" DataField="sku_upc" SortExpression="sku_upc"
                                ReadOnly="true" />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <%--<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>--%>
                                
                <telerik:RadGrid ID="RadGrid2" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
                    PageSize="20" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid2_NeedDataSource"
                    ShowStatusBar="True" AllowFilteringByColumn="True" OnItemCommand="RadGrid2_ItemCommand"
                    OnItemDataBound="RadGrid2_ItemDataBound" CellSpacing="0">
                    <MasterTableView DataKeyNames="sku_upc" AllowMultiColumnSorting="True" Width="100%"
                        CommandItemDisplay="Top">
                        <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
                        <CommandItemSettings ShowAddNewRecordButton="false" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--<telerik:GridBoundColumn UniqueName="ordernumber" SortExpression="ordernumber" HeaderText="Order #"
                                DataField="ordernumber" ReadOnly="true" />--%>
                            <telerik:GridBoundColumn UniqueName="sku" SortExpression="sku" HeaderText="SKU" DataField="sku"
                                ReadOnly="true" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="sku_upc" SortExpression="sku_upc" HeaderText="SKU UPC"
                                DataField="sku_upc" ReadOnly="true" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="sku_desc" SortExpression="sku_desc" HeaderText="SKU Description"
                                DataField="sku_desc" ReadOnly="true" Visible="false" />
                            <telerik:GridBoundColumn UniqueName="itemstatus" SortExpression="itemstatus" HeaderText="Item Status"
                                DataField="itemstatus" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="itemnumber" SortExpression="itemnumber" HeaderText="Item #"
                                DataField="itemnumber" ReadOnly="true" />
                            <telerik:GridHyperLinkColumn
                                    HeaderText="Order #" 
                                    UniqueName="ordernumber"
                                    DataTextField="ordernumber"
                                    DataNavigateUrlFields="ordernumber"
                                    AllowFiltering="false"
                                    SortExpression="ordernumber"
                                    DataNavigateUrlFormatString="~/Pages/Dashboard/Cancellation/OrderInquiry.aspx?ordernumber={0}">
                            </telerik:GridHyperLinkColumn> 
                            <telerik:GridBoundColumn UniqueName="orderplaced" SortExpression="orderplaced" HeaderText="Order Placed"
                                DataField="orderplaced" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="lastaction" SortExpression="lastaction" HeaderText="Last Action"
                                DataField="lastaction" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="load" SortExpression="load" HeaderText="Load"
                                DataField="load" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="location" SortExpression="location" HeaderText="Location"
                                DataField="location" ReadOnly="true" />
                             <telerik:GridBoundColumn UniqueName="Area" SortExpression="Area" HeaderText="Area"
                                DataField="load_area" ReadOnly="true" />
                             <telerik:GridBoundColumn DataField="in_tote_ind" 
                                FilterControlAltText="Filter In Tote column" HeaderText="In FT" 
                                ReadOnly="True" SortExpression="In FT" UniqueName="in_tote_ind">
                            </telerik:GridBoundColumn>
                        </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
