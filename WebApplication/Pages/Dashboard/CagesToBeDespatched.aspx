﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true"
    CodeBehind="CagesToBeDespatched.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.CagesToBeDespatched" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 898px;
        }
    </style>
    <script type="text/javascript">
        function confirmthis() {
            var cagestring = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value;
            if (confirm(cagestring)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    
</asp:Content>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; font-size: small;">
                <b>Cages To Be Despatched </b>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server" ID="Panel">
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
        </Triggers>--%>

        <ContentTemplate>
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
                    <td>
                        
                        <asp:Button ID="Button1" runat="server" OnClick="Button2_Click" OnClientClick="return confirmthis()"
                            Text="Despatch Cages"  />
                        <asp:HiddenField ID="HiddenField1" runat="server" ></asp:HiddenField >
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
            
    <br /><br />
    <asp:UpdateProgress runat="server" ID="UpdateProgress1">
        <ProgressTemplate>
            <div style="width: 100%; height: 100%; text-align: center;">
                <asp:Image ID="Image1" runat="server" ImageUrl="../../images/progress.gif" ImageAlign="Middle" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <br />
    
            <table width="100%">
                <tr>
                    <td>
                        <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>--%>
                        <telerik:RadGrid ID="RadGrid2" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
                            PageSize="30" AllowSorting="True" AllowPaging="True" OnNeedDataSource="RadGrid2_NeedDataSource"
                            ShowStatusBar="True" AllowFilteringByColumn="true" OnItemCommand="RadGrid2_ItemCommand"
                            OnItemDataBound="RadGrid2_ItemDataBound" AutoGenerateDeleteColumn="False" AutoGenerateEditColumn="False">
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView DataKeyNames="cage_id" AllowMultiColumnSorting="True" Width="100%"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" />
                                <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="status" SortExpression="status" HeaderText="Cage Status"
                                        DataField="status" ReadOnly="true">
                                        <%--FilterControlWidth="60px"--%>
                                        <%--<HeaderStyle Width="80px" HorizontalAlign="Left" />
                                    <ItemStyle Width="80px" />--%>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="status_cd" SortExpression="status_cd" HeaderText="Cage Status"
                                        DataField="status_cd" ReadOnly="true" FilterControlWidth="60px" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="cage_id" SortExpression="cage_id" HeaderText="Cage Id"
                                        DataField="cage_id" ReadOnly="true" Visible="true" FilterControlWidth="25px">
                                        <HeaderStyle Width="45px" HorizontalAlign="Left" />
                                        <ItemStyle Width="45px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="cage_label" SortExpression="cage_label" HeaderText="Cage#"
                                        DataField="cage_label" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="carrier_id" SortExpression="carrier_id" HeaderText="Carrier"
                                        DataField="carrier_id" ReadOnly="true" Visible="true" FilterControlWidth="40px">
                                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="type" SortExpression="type" HeaderText="Cage Type"
                                        DataField="type" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="parcel_count" SortExpression="parcel_count"
                                        HeaderText="Parcel Count" DataField="parcel_count" ReadOnly="true" FilterControlWidth="30px">
                                        <HeaderStyle Width="50px" HorizontalAlign="Left" />
                                        <ItemStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="oldest_order_date" SortExpression="oldest_order_date"
                                        HeaderText="Earliest Order Date" DataField="oldest_order_date" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="collection_dtm" SortExpression="collection_dtm"
                                        HeaderText="Earliest Collection Window" DataField="collection_dtm" ReadOnly="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="ReadyForDespatch" HeaderText="Ready For Despatch"
                                        AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_ready" runat="server" OnClick="Button3_Click" Text="Ready For Despatch" width="130"/>
                                            <%--<asp:Label ID="itemslocated" runat="server" Style="font-weight: bold;" Text='<%# Eval("percent_located") %>' />--%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="130px" HorizontalAlign="Left" />
                                        <ItemStyle Width="130px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
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

            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--<asp:Button ID="Button1" runat="server" OnClick="Button2_Click" OnClientClick="return confirmthis()"
                            Text="Despatch Cages" CausesValidation="true" />--%>


    
</asp:Content>
