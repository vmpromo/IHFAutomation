<%@ Page Title="Select Order" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="SelectOrder.aspx.cs" Inherits="PackingMock.SelectOrder" %>
    <%@ MasterType VirtualPath="~/Pages/RI.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/api.pack.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function GetSelectedOrder() {
            var grid = $find("<%=grdOrder.ClientID%>");
            var MasterTable = grid.get_masterTableView();
            var selectedRows = MasterTable.get_selectedItems();

            if (selectedRows.length !== 0) {
                //for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[0];
                var cell = MasterTable.getCellByColumnUniqueName(row, "OrderNo")
                var val = cell.firstChild.data;

                setValue(val);

                //}
            }
            else {

                alert("Please select order to open.");
            }


        }


        function setValue(v) {
            $("#hdnOrder").val(v);
        }


        $(document).ready(function () {

            $("#txtOrderNo").focus();

            $("form").submit(function () {

                var arg = $("#__EVENTTARGET").val();
                var q = $("#txtOrderNo").val();

                if ((arg === undefined || arg === "") && (q !== ""))
                    $("#__EVENTTARGET").val("SearchOrder");

            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table width="100%" border="0">
        <tbody>
            <tr>
                <td>
                    <table>
                        <tbody>
                            <tr style="height: 70px;">
                                <td>
                                    <div id="actionCtrls">
                                        <div style="float: left">
                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="txtOrderNoExt" TargetControlID="txtOrderNo"
                                                ServicePath="~\Resources\LookUpService.asmx" ServiceMethod="GetPackOrders" MinimumPrefixLength="1"
                                                EnableCaching="true" CompletionInterval="1" CompletionListCssClass="AutocompleteCompletionListElement"
                                                UseContextKey="false" />
                                            <asp:TextBox runat="server" ID="txtOrderNo" CssClass="oSearch" ClientIDMode="Static" />
                                        </div>
                                        <div style="float: right; padding-left: 20px;" runat="server" onclick="javascript:__doPostBack('SearchOrder','')">
                                            <div class="actionButton">
                                                Search Order
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <%--       <asp:GridView ID="grdOrder" runat="server" Scrolling="Auto" Width="100%" Height="100%"
                        AllowPaging="True" PageSize="12" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerSettings PageButtonCount="5" FirstPageText="First" LastPageText="Last" NextPageText=""
                            Position="Top" PreviousPageText="" />
                        <PagerStyle BorderStyle="None" BackColor="#284775" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField AccessibleHeaderText="Select" HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="SelectOrder" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OrderNo" HeaderText="Order No" SortExpression="OrderNo" />
                            <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" SortExpression="OrderStatus" />
                            <asp:BoundField DataField="LastAction" HeaderText="Last Action" SortExpression="LastAction" />
                            <asp:BoundField DataField="User" HeaderText="User" SortExpression="User" />
                            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" SortExpression="OrderDate" />
                            <asp:BoundField DataField="ServiceGroupId" HeaderText="Service Group ID" SortExpression="ServiceGroupId" />
                            <asp:BoundField DataField="ServiceGroupName" HeaderText="Service Group Name" SortExpression="ServiceGroupName" />
                            <asp:BoundField DataField="CollectionWindow" HeaderText="Collection Window" SortExpression="CollectionWindow" />
                            <asp:BoundField DataField="OrderItem" HeaderText="Order Item" SortExpression="OrderItem" />

                        </Columns>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                       
                    </asp:GridView>--%>
                    <telerik:RadGrid ID="grdOrder" runat="server" AllowPaging="True" ShowFooter="true"
                        ShowHeader="true" ShowStatusBar="true" AllowSorting="True" PageSize="15" GridLines="Both"
                        Width="100%"  OnNeedDataSource="grdOrder_NeedDataSource">
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <MasterTableView AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                <telerik:GridBoundColumn DataField="OrderNo" Display="true" HeaderText="Order No"  DataType="System.Int32"/>
                                <telerik:GridBoundColumn DataField="OrderStatus" Display="true" HeaderText="Order Status" />
                                <telerik:GridBoundColumn DataField="LastAction" Display="true" HeaderText="Last Action" DataType="System.DateTime"/>
                                <telerik:GridBoundColumn DataField="User" Display="true" HeaderText="User" />
                                <telerik:GridBoundColumn DataField="Location" Display="true" HeaderText="Location" />
                                <telerik:GridBoundColumn DataField="OrderDate" Display="true" HeaderText="Order Date"  DataType="System.DateTime" />
                                <telerik:GridBoundColumn DataField="ServiceGroupId" Display="true" HeaderText="Service Group ID" />
                                <telerik:GridBoundColumn DataField="ServiceGroupName" Display="true" HeaderText="Service Group Name" />
                                <telerik:GridBoundColumn DataField="CollectionWindow" Display="true" HeaderText="Collection Window" />
                                <telerik:GridBoundColumn DataField="OrderItem" Display="true" HeaderText="OrderItem" DataType="System.Int32" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="hdnOrder" />
                    <div style="padding-top: 10px;">
                        <div style="float: right; padding-left: 20px;" onclick="javascript:__doPostBack('OpenOrder','')">
                            <div class="actionButton">
                                Open Order
                            </div>
                        </div>
                    </div>
                    <%--<asp:Button runat="server" ID="btnOpenOrder" Text="Open" OnClick="btnOpenOrder_Click" />--%>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
