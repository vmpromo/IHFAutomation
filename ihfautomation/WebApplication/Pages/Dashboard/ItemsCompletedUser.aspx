<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" CodeBehind="ItemsCompletedUser.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.ItemsCompletedUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <Link href="../../Styles/popcalendar.css" type="text/css" rel="stylesheet"></Link>
    
    <script type="text/javascript" 
        src="../../Scripts/popcalendar.js">
    </script>
    <style type="text/css">
        .style2
        {
            width: 60px;
        }
        .style3
        {
            width: 128px;
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
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <b>ITEMS PACKED BY HOUR PER USER</b>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            
            <td align="left" class="style4"><asp:label id="lbl_stDate" Font-Bold="True" 
                    runat="server">Date</asp:label></td>
		    <td align="left" class="style5"><asp:textbox id="txt_stDate" runat="server" 
                    style="margin-left: 0px" ></asp:textbox></td>
		    <td class="style3"><asp:image id="imgCalendarst" runat="server" ImageUrl="../../Images/calendar.gif" 
                style="margin-left: 0px"></asp:image></td>
            
		    
            <td><asp:Button ID="btn_search" runat="server" onclick="Button1_Click" 
                    Text="Search" /></td>
            <td>
                <asp:RegularExpressionValidator ID="regDate" runat="server" ValidationExpression="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"
                    ControlToValidate="txt_stDate" SetFocusOnError="True" ForeColor="#FF3300">Enter a Valid Date</asp:RegularExpressionValidator>
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
                <telerik:RadGrid ID="RadGrid2" 
                                runat="server" 
                                Width="100%"
                                
                                AutoGenerateColumns="False"
                                PageSize="20" 
                                AllowPaging="True" 
                                OnNeedDataSource="RadGrid2_NeedDataSource" 
                                ShowStatusBar="True" 
                                OnItemCommand="RadGrid2_ItemCommand" 
                                OnItemDataBound="RadGrid2_ItemDataBound" CellSpacing="0" 
                    Skin="Outlook" >
                    
                    <ExportSettings HideStructureColumns="true" />                   


                    <MasterTableView 
                        DataKeyNames="User" 
                        AllowMultiColumnSorting="True" 
                        Width="100%"
                        CommandItemDisplay="Top">

                        <CommandItemSettings ShowAddNewRecordButton="false" />
                        <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>

                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="User" SortExpression="User" HeaderText="User"
                                DataField="User" ReadOnly="true" Visible="true" ItemStyle-Font-Size="Smaller">
                                <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                <ItemStyle Width="60px" />
                            </telerik:GridBoundColumn>
                                
                            
                            <telerik:GridBoundColumn UniqueName="0" HeaderText="0" DataField="0" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="1" HeaderText="1" DataField="1" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="2" HeaderText="2" DataField="2" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="3" HeaderText="3" DataField="3" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="4" HeaderText="4" DataField="4" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="5" HeaderText="5" DataField="5" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="6" HeaderText="6" DataField="6" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="7" HeaderText="7" DataField="7" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="8" HeaderText="8" DataField="8" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="9" HeaderText="9" DataField="9" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="10" HeaderText="10" DataField="10" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="11" HeaderText="11" DataField="11" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="12" HeaderText="12" DataField="12" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="13" HeaderText="13" DataField="13" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="14" HeaderText="14" DataField="14" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="15" HeaderText="15" DataField="15" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="16" HeaderText="16" DataField="16" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="17" HeaderText="17" DataField="17" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="18" HeaderText="18" DataField="18" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="19" HeaderText="19" DataField="19" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="20" HeaderText="20" DataField="20" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="21" HeaderText="21" DataField="21" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="22" HeaderText="22" DataField="22" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="23" HeaderText="23" DataField="23" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="24" HeaderText="24" DataField="24" ReadOnly="true" ItemStyle-Font-Size="Smaller"/>
                            <telerik:GridBoundColumn UniqueName="Total" SortExpression="Total" HeaderText="Total Items"
                                DataField="Total" ReadOnly="true" Visible="true" ItemStyle-Font-Size="Smaller">
                                <HeaderStyle Width="40px" HorizontalAlign="Left" />
                                <ItemStyle Width="40px" />
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
                
                <br />
                <asp:Button ID="Button2" runat="server" onclick="Button1_Click1" 
                    Text="Export to Excel" />

            </td>
        </tr>
    </table>
    
    <br />
    <br />
        
</asp:Content>
