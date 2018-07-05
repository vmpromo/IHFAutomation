<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" CodeBehind="PackedToBeCaged.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.PackedToBeCaged" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; font-size: small;">
                <b>Packed To Be Caged</b>
            </td>
        </tr>
    </table>
    
            <table width="100%">
                <tr>
                    <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px;" clientidmode="Predictable"
                        class="style1">
                        &nbsp;<asp:Label ID="LBtrstatus" runat="server" Font-Bold="True" Text="Carrier"></asp:Label>
                        <asp:DropDownList ID="DD_carrier" runat="server" Height="20px" Style="margin-left: 25px"
                            Width="170px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_search" runat="server" OnClick="Button1_Click" Text="Search" />
                    </td>
                   
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            
    
    
            <table width="100%">
                <tr>
                    <td>
                        <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>--%>
                        <telerik:RadGrid ID="RadGrid2" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
                            PageSize="30" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid2_NeedDataSource"
                            ShowStatusBar="True" AllowFilteringByColumn="True" OnItemCommand="RadGrid2_ItemCommand"
                            OnItemDataBound="RadGrid2_ItemDataBound" CellSpacing="0">
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView DataKeyNames="order_no" AllowMultiColumnSorting="True" Width="100%"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn 
                                        UniqueName="order_no" 
                                        SortExpression="order_no" 
                                        HeaderText="Order#"
                                        DataField="order_no" 
                                        ReadOnly="true" 
                                        FilterControlWidth="40px">  
                                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemStyle Width="60px" />                                      
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn 
                                        UniqueName="order_status" 
                                        SortExpression="order_status" 
                                        HeaderText="Order Status"
                                        DataField="order_status" 
                                        ReadOnly="true"
                                        FilterControlWidth="40px">  
                                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemStyle Width="60px" />   
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn UniqueName="package_id" SortExpression="package_id" HeaderText="Parcel #"
                                        DataField="package_id" ReadOnly="true" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="barcode" SortExpression="barcode" HeaderText="Parcel Barcode"
                                        DataField="barcode" ReadOnly="true" Visible="true" FilterControlWidth="60px">
                                        <HeaderStyle Width="80px" HorizontalAlign="Left" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="status_cd" SortExpression="status_cd" HeaderText="Parcel Status code"
                                        DataField="status_cd" ReadOnly="true" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="package_status" SortExpression="package_status"
                                        HeaderText="Parcel Status" DataField="package_status" ReadOnly="true" FilterControlWidth="40px">
                                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="consignment_id" SortExpression="consignment_id"
                                        HeaderText="Consignment Id" DataField="consignment_id" ReadOnly="true" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="consignment_cd" SortExpression="consignment_cd"
                                        HeaderText="Consignment #" DataField="consignment_cd" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="deliv_store_num" 
                                        FilterControlAltText="Filter deliv_store_num column" HeaderText="Store" 
                                        UniqueName="deliv_store_num">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn 
                                        UniqueName="carrier_id" 
                                        SortExpression="carrier_id"
                                        HeaderText="Carrier" 
                                        DataField="carrier_id" 
                                        ReadOnly="true"
                                        FilterControlWidth="25px">
                                        <HeaderStyle Width="45px" HorizontalAlign="Left" />
                                        <ItemStyle Width="45px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="service_name" SortExpression="service_name"
                                        HeaderText="Service Name" DataField="service_name" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn 
                                        UniqueName="carr_service_id" 
                                        SortExpression="carr_service_id"
                                        HeaderText="Service" 
                                        DataField="carr_service_id" 
                                        ReadOnly="true"
                                        FilterControlWidth="40px">
                                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cage_name" 
                                        FilterControlAltText="Filter cage_name column" HeaderText="Cage" 
                                        UniqueName="cage_name">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn 
                                        UniqueName="packed_time" 
                                        SortExpression="packed_time" 
                                        HeaderText="Packed time"
                                        DataField="packed_time" 
                                        ReadOnly="true" 
                                        Visible="true" 
                                        FilterControlWidth="25px">
                                        <HeaderStyle Width="45px" HorizontalAlign="Left" />
                                        <ItemStyle Width="45px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn 
                                        UniqueName="collection_window" 
                                        SortExpression="collection_window"
                                        HeaderText="Collection Window" 
                                        DataField="collection_window" 
                                        ReadOnly="true"
                                        FilterControlWidth="25px">
                                        <HeaderStyle Width="45px" HorizontalAlign="Left" />
                                        <ItemStyle Width="45px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn 
                                        UniqueName="PrintColumn" 
                                        CommandName="print" 
                                        ButtonType="PushButton"
                                        Text="Print" />
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
                        
                        <br />
                    </td>
                </tr>
            </table>

            
       
    
</asp:Content>
