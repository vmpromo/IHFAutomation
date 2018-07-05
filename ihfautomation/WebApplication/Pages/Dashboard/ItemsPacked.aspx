<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true"
    CodeBehind="ItemsPacked.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.ItemsPacked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Styles/popcalendar.css" type="text/css" rel="stylesheet"></link>
    <script type="text/javascript" src="../../Scripts/popcalendar.js">
    </script>
    <style type="text/css">
        .style2
        {
            width: 60px;
        }
        .style4
        {
            width: 66px;
        }
        .style5
        {
            width: 149px;
        }
        .style6
        {
            width: 125px;
        }
        .style7
        {
            width: 59px;
        }
        .style8
        {
            width: 70px;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; font-size: small;">
                <b>ITEMS PACKED BY HOUR</b>
            </td>
        </tr>
    </table>
    <table style="width: 81%">
        <tr>
            <td align="left" class="style4">
                <asp:Label ID="lbl_stDate" Font-Bold="true" runat="server">Start Date</asp:Label>
            </td>
            <td align="left" class="style5">
                <asp:TextBox ID="txt_stDate" runat="server" Style="margin-left: 0px"></asp:TextBox>
            </td>
            <td class="style8">
                <asp:Image ID="imgCalendarst" runat="server" ImageUrl="../../Images/calendar.gif"
                    Style="margin-left: 0px"></asp:Image>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="regDate" runat="server" ValidationExpression="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"
                    ControlToValidate="txt_stDate" SetFocusOnError="True" ForeColor="#FF3300">Enter a Valid Start Date</asp:RegularExpressionValidator>
            </td>
            <td align="left" class="style2">
                <asp:Label ID="lbl_endDate" Font-Bold="true" runat="server">End Date</asp:Label>
            </td>
            <td align="left" class="style6">
                <asp:TextBox ID="txt_endDate" runat="server" Width="122px"></asp:TextBox>
            </td>
            <td class="style7">
                <asp:Image ID="imgCalendarend" runat="server" ImageUrl="../../Images/calendar.gif"
                    Style="margin-left: 2px"></asp:Image>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"
                    ControlToValidate="txt_endDate" SetFocusOnError="True" ForeColor="#FF3300">Enter a Valid End Date</asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:Button ID="btn_search" runat="server" OnClick="Button1_Click" Text="Search" />
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
                <telerik:RadGrid ID="RadGrid2" runat="server" Width="100%" AutoGenerateColumns="False"
                    PageSize="50" AllowPaging="True" 
                    OnNeedDataSource="RadGrid2_NeedDataSource" ShowStatusBar="True"
                    OnItemCommand="RadGrid2_ItemCommand" OnItemDataBound="RadGrid2_ItemDataBound"
                    CellSpacing="0" Skin="Outlook" GridLines="None">
                    <ExportSettings HideStructureColumns="true" />
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="Date" AllowMultiColumnSorting="True" Width="100%"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ShowAddNewRecordButton="false" />
                        <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="Date" SortExpression="Date" HeaderText="Date"
                                DataField="Date" ReadOnly="true" Visible="true" ItemStyle-Font-Size="Smaller">
                                <HeaderStyle Width="80px" HorizontalAlign="Left" />
                                <ItemStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="0" HeaderText="0" DataField="0" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="1" HeaderText="1" DataField="1" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="2" HeaderText="2" DataField="2" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="3" HeaderText="3" DataField="3" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="4" HeaderText="4" DataField="4" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="5" HeaderText="5" DataField="5" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="6" HeaderText="6" DataField="6" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="7" HeaderText="7" DataField="7" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="8" HeaderText="8" DataField="8" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="9" HeaderText="9" DataField="9" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="10" HeaderText="10" DataField="10" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="11" HeaderText="11" DataField="11" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="12" HeaderText="12" DataField="12" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="13" HeaderText="13" DataField="13" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="14" HeaderText="14" DataField="14" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="15" HeaderText="15" DataField="15" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="16" HeaderText="16" DataField="16" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="17" HeaderText="17" DataField="17" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="18" HeaderText="18" DataField="18" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="19" HeaderText="19" DataField="19" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="20" HeaderText="20" DataField="20" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="21" HeaderText="21" DataField="21" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="22" HeaderText="22" DataField="22" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="23" HeaderText="23" DataField="23" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="24" HeaderText="24" DataField="24" ReadOnly="true"
                                ItemStyle-Font-Size="Smaller" >
<ItemStyle Font-Size="Smaller"></ItemStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Total" SortExpression="Total" HeaderText="Total Items"
                                DataField="Total" ReadOnly="true" Visible="true" ItemStyle-Font-Size="Smaller">
                                <HeaderStyle Width="40px" HorizontalAlign="Left" />
                                <ItemStyle Width="40px" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
                <br />
                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click1" Text="Export to Excel" />
            </td>
            </tr>
    </table>
    <br />
    <br />
</asp:Content>
