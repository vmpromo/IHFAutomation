<%@ Page Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" CodeBehind="LoadDetail.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.LoadDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>    <table width="100%">
        <tr>
            <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px;" 
                class="style3" >
                <a href="LoadOverview.aspx">&lt;-- Back to Load Management</a></td>
        </tr>
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;" 
                class="style1" >
                Load Detail</td>
        </tr>
        <tr>
            <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <table><tr><td><i>Load #: </i> </td><td>
                    &nbsp;</td><td>
                        <asp:Label ID="lbLoadNumber" runat="server" Text="1231545"></asp:Label>
                    </td><td class="style2">
                        &nbsp;</td><td>
                        <i>Item Status:</i></td><td>
                        &nbsp;</td><td>
                        <asp:Label ID="lbItemStatus" runat="server" Text="ALL"></asp:Label>
                    </td></tr><tr><td>&nbsp;</td><td>
                        &nbsp;</td><td>
                            &nbsp;</td><td class="style2">
                            &nbsp;</td><td>
                            &nbsp;</td><td>
                            &nbsp;</td><td>
                            &nbsp;</td></tr><tr><td><i>Load Status:</i></td><td>
                        &nbsp;</td><td>
                            <asp:Label ID="lbLoadStatus" runat="server" Text="PACKED"></asp:Label>
                        </td><td class="style2">
                            &nbsp;</td><td>
                            <i>Order Type:<i></td><td>
                            &nbsp;</td><td>
                            <asp:Label ID="lbOrderType" runat="server" Text="ALL"></asp:Label>
                        </td></tr></table></td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td>
                <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
                    Text="Button" />--%>
                <telerik:RadGrid ID="RadGrid3" runat="server" 
                    onneeddatasource="RadGrid3_NeedDataSource" AutoGenerateColumns="False" 
                    CellSpacing="0" GridLines="None" AllowPaging="True" 
                    onitemcreated="RadGrid3_ItemCreated" PageSize="15" 
                    oncolumncreated="RadGrid3_ColumnCreated" onprerender="RadGrid3_PreRender">
                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" 
                        OpenInNewWindow="True">
                    </ExportSettings>
<MasterTableView AutoGenerateColumns="False" DataKeyNames="LoadNumber" 
                        AllowFilteringByColumn="True" AllowMultiColumnSorting="True" 
                        AllowSorting="True" CommandItemDisplay="TopAndBottom">
<CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False" 
        ShowExportToCsvButton="True" ShowExportToExcelButton="True"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="loadnumber" 
            FilterControlAltText="Filter column column" HeaderText="Load #" 
            UniqueName="LoadNumber">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="OrderDate" 
            FilterControlAltText="Filter orderdate column" HeaderText="Order Date" DataType="System.DateTime"
            UniqueName="OrderDate">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="CarrierServiceGroup" 
            FilterControlAltText="Filter service group column" HeaderText="Service Group" 
            UniqueName="CarrierServiceGroup">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ServiceGroupDescr" 
            FilterControlAltText="Filter service group descr column" HeaderText="Service Group Descr" 
            UniqueName="ServiceGroupDescr">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ServiceTypeDescr" HeaderText="Service Name" 
            UniqueName="ServiceTypeDescr" 
            FilterControlAltText="Filter service name">
            </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Sku" 
            FilterControlAltText="Filter sku column" HeaderText="Sku" 
            UniqueName="Sku">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="Barcode" 
            FilterControlAltText="Filter barcode" 
            HeaderText="Barcode" UniqueName="Barcode">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="SkuDescr" 
            FilterControlAltText="Filter sku descr" HeaderText="Sku Descr." 
            UniqueName="SkuDescr">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ItemStatus" 
            FilterControlAltText="Filter item status" HeaderText="Item Status" 
            UniqueName="ItemStatus">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="LastActionTime" 
            FilterControlAltText="Filter last action time" HeaderText="Last Action" DataType="System.DateTime"
            UniqueName="LastActionTime">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                </telerik:RadGrid>
                <br />

            </td>
        </tr>
    </table>
    
    
</asp:Content>

<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            font-size: large;
            font-weight: bold;
        }
        .style2
        {
            width: 92px;
        }
        .style3
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
</asp:Content>


