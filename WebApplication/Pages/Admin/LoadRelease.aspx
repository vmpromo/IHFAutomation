<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/RI.Master"
    CodeBehind="LoadRelease.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.LoadRelease" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Styles/api.pack.css" rel="stylesheet" type="text/css" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <%--<div class="childdefault">
        <table>
            <tr>
                <td class="childheader">
                    Load Release
                </td>
            </tr>
        </table>--%>
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;">
                    Load Release
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <asp:Label ID="Error" runat="server" Text="Label"></asp:Label>
            </tr>
        </table>
        <%--<table>
            <tr>
                <td>--%>
        <table border="0">
            <tbody>
                <tr>
                    <td style="width: 100px">
                        Load Status
                    </td>
                    <td style="width: 200px">
                        <asp:DropDownList runat="server" ID="loadStatus">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnGetLoad" Text="Get Load(s)" OnClick="btnGetLoad_Click" />
                    </td>
                </tr>
            </tbody>
        </table>
        <%--</td>
            </tr>--%>
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadGrid ID="grdLoadRelease" runat="server" AllowPaging="True" ShowFooter="true"
                        ShowHeader="true" ShowStatusBar="true" AllowSorting="True" PageSize="10" GridLines="Both"
                        Width="100%" OnNeedDataSource="grdLoadRelease_NeedDataSource" OnItemDataBound="grdLoadRelease_ItemDataBound"
                        OnItemCreated="grdLoadRelease_ItemCreated" OnItemCommand="grdLoadRelease_ItemCommand">
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <MasterTableView DataKeyNames="pick_load_num" AutoGenerateColumns="false" AllowMultiColumnSorting="True"
                            Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="Server" ID="SelectRow" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="pick_load_num" SortExpression="pick_load_num"
                                    HeaderText="Load No" DataField="pick_load_num" ReadOnly="true" AllowFiltering="False"
                                    AllowSorting="true">
                                    <%--<HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="70px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="pick_load_status" SortExpression="pick_load_status"
                                    HeaderText="Status" DataField="pick_load_status" ReadOnly="true" AllowFiltering="False"
                                    AllowSorting="true">
                                    <%--<HeaderStyle Width="70px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="70px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="release_dtm" SortExpression="release_dtm" HeaderText="Release Date"
                                    DataField="release_dtm" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                    <%--<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="tot_single_orders" SortExpression="tot_single_orders"
                                    HeaderText="Single Orders" DataField="tot_single_orders" ReadOnly="true" AllowFiltering="False"
                                    AllowSorting="true">
                                    <%--<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="tot_multi_orders" SortExpression="tot_multi_orders"
                                    HeaderText="Multi Orders" DataField="tot_multi_orders" ReadOnly="true" AllowFiltering="False"
                                    AllowSorting="true">
                                    <%--<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="tot_multi_items" SortExpression="tot_multi_items"
                                    HeaderText="Multi Items" DataField="tot_multi_items" ReadOnly="true" AllowFiltering="False"
                                    AllowSorting="true">
                                    <%--<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="area_id" HeaderText="Release To">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList runat="server" ID="releaseTo" AutoPostBack="true"
                                        OnSelectedIndexChanged="release_to_DD_SelectedIndexChanged">                                            
                                        </asp:DropDownList>--%>
                                        <%--     <telerik:RadComboBox ID="releaseTo" runat="server" 
                                         OnSelectedIndexChanged="release_to_DD_SelectedIndexChanged">
                                        </telerik:RadComboBox>--%>
                                        <telerik:RadComboBox ID="releaseTo" runat="server" Height="200px" Width="200px" DropDownWidth="298px"
                                            EmptyMessage="Choose a Product" HighlightTemplatedItems="true" EnableLoadOnDemand="true"
                                            Filter="StartsWith" >
                                            <HeaderTemplate>
                                                <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            Area
                                                        </td>
                                                        <td style="width: 100px;">
                                                            Free Location
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 275px" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['free_loc']")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="releaseTo"
                                            ErrorMessage="!" CssClass="validator">
                                        </asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="release_status" HeaderText="Action">
                                    <ItemTemplate>
                                        <%--<asp:DropDownList runat="server" ID="release_action" >                                            
                                        </asp:DropDownList>--%>
                                        <telerik:RadComboBox ID="release_action" runat="server">
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="created_dtm" SortExpression="created_dtm" HeaderText="Created Date"
                                    DataField="created_dtm" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                    <%--<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />--%>
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="created_dtm" Display="true" HeaderText="Created Date" />--%>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input type="hidden" id="hdnOrder" />
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
