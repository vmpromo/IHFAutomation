<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" 
CodeBehind="LoadOverview.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.LoadOverview" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <b>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                LOAD MANAGEMENT </b>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <table><tr><td>Load: </td><td>
                    <telerik:RadComboBox ID="RadComboLoad" Runat="server" AutoCompleteSeparator=";" 
                        MarkFirstMatch="True" 
                        onselectedindexchanged="RadComboLoad_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    </td><td>
                        <telerik:RadButton ID="RadBtnGo" runat="server" Text="GO" 
                            onclick="RadBtnGo_Click">
                        </telerik:RadButton>
                    </td></tr><tr><td>Load Status:</td><td>
                        <telerik:RadComboBox ID="rcbLoadStatus" Runat="server">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="All" />
                                <telerik:RadComboBoxItem runat="server" Text="Read For Release" Value="40" />
                                <telerik:RadComboBoxItem runat="server" Text="Released" Value="50" />
                                <telerik:RadComboBoxItem runat="server" Text="Sorting" Value="60" />
                                <telerik:RadComboBoxItem runat="server" Text="Sorted" Value="90" />
                                <telerik:RadComboBoxItem runat="server" Text="Locating" Value="100" />
                                <telerik:RadComboBoxItem runat="server" Text="Located" Value="100" />
                                <telerik:RadComboBoxItem runat="server" Text="Packing" Value="130" />
                                <telerik:RadComboBoxItem runat="server" Text="Packed" Value="150" />
                            </Items>
                        </telerik:RadComboBox>
                    </td><td>
                            &nbsp;</td></tr></table></td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="rgLoadOverview" runat="server" 
                    onneeddatasource="rgLoadOverview_NeedDataSource" AutoGenerateColumns="False" 
                    CellSpacing="0" GridLines="None" AllowPaging="True" 
                     PageSize="15" ShowFooter="True" 
                    onitemdatabound="rgLoadOverview_ItemDataBound">
                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" 
                        OpenInNewWindow="True">
                    </ExportSettings>
<MasterTableView AutoGenerateColumns="False" DataKeyNames="LoadNumber" AllowMultiColumnSorting="True" 
                        AllowSorting="True" CommandItemDisplay="TopAndBottom">
<CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False" 
        ShowExportToCsvButton="True" ShowExportToExcelButton="True" 
        ShowExportToPdfButton="True"></CommandItemSettings>

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
        <telerik:GridBoundColumn DataField="TimeReleased" 
            FilterControlAltText="Filter InterfaceTime column" 
            HeaderText="Release Time" DataType="System.DateTime"
            UniqueName="InterfaceTime">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="loadstatus" 
            FilterControlAltText="Filter column column" HeaderText="Load Status" 
            UniqueName="loadstatus">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter TotalItems column" 
            HeaderText="Total" UniqueName="TotalItems">
            <HeaderTemplate>
                <table width="100">
                       <tr>
                          <td colspan="3" align="center"><b>Total</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                           <td align="right">Total</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="100">
                        <tr>
                           <td align="right" width="33%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T"><%# DataBinder.Eval(Container.DataItem, "TotalMultis") %></a></td>
                           <td align="right" width="33%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F"><%# DataBinder.Eval(Container.DataItem, "TotalSingles") %></a></td>
                           <td align="right" width="34%"><a href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>"><%# DataBinder.Eval(Container.DataItem, "TotalItems") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter ReadyReleaseItems column" 
            HeaderText="Ready Release" UniqueName="ReadyReleaseItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Ready Release</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Ready+For+Release&ItemStatusCode=40"><%# DataBinder.Eval(Container.DataItem, "ReadyReleaseMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Ready+For+Release&ItemStatusCode=40"><%# DataBinder.Eval(Container.DataItem, "ReadyReleaseSingles")%></a> </td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totreadyreleasemultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                           <asp:Label ID="totreadyreleasesingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter ReleasedItems column" 
            HeaderText="Released" UniqueName="ReleasedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Released</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Released&ItemStatusCode=50"><%# DataBinder.Eval(Container.DataItem, "ReleasedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Released&ItemStatusCode=50"><%# DataBinder.Eval(Container.DataItem, "ReleasedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totreleasedmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totreleasedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter SortedItems column" 
            HeaderText="Sorted" UniqueName="SortedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Sorted</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Sorted&ItemStatusCode=90"><%# DataBinder.Eval(Container.DataItem, "SortedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Sorted&ItemStatusCode=0"><%# DataBinder.Eval(Container.DataItem, "SortedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totsortedmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totsortedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter LocatedItems column" 
            HeaderText="Located" UniqueName="LocatedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Located</b></td>
                       </tr>
                       <tr><td align="right" width="50%">Multi</td>
                           <td align="right" width="50%">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Located&ItemStatusCode=110"><%# DataBinder.Eval(Container.DataItem, "LocatedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Located&ItemStatusCode=110"><%# DataBinder.Eval(Container.DataItem, "LocatedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totlocatedmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totlocatedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter PackingItems column" 
            HeaderText="Sorted" UniqueName="PackingItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Packing</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Packing&ItemStatusCode=130"><%# DataBinder.Eval(Container.DataItem, "PackingMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Packing&ItemStatusCode=130"><%# DataBinder.Eval(Container.DataItem, "PackingSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totpackingmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totpackingsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>

        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter PackedItems column" 
            HeaderText="Sorted" UniqueName="PackedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Packed</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Packed&ItemStatusCode=150"><%# DataBinder.Eval(Container.DataItem, "PackedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Packed&ItemStatusCode=150"><%# DataBinder.Eval(Container.DataItem, "PackedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totpackedmultisLabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totpackedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter CagedItems column" 
            HeaderText="Sorted" UniqueName="CagedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Caged</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Caged&ItemStatusCode=170"><%# DataBinder.Eval(Container.DataItem, "CagedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Caged&ItemStatusCode=170"><%# DataBinder.Eval(Container.DataItem, "CagedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totcagedmultisLabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totcagedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter ReadyDespatchItems column" 
            HeaderText="Sorted" UniqueName="ReadyDespatchItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Ready Despatch</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Ready+For+Despatch&ItemStatusCode=180"><%# DataBinder.Eval(Container.DataItem, "ReadyDespatchMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Ready+For+Despatch&ItemStatusCode=180"><%# DataBinder.Eval(Container.DataItem, "ReadyDespatchSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totreadydespatchmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totreadydespatchsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter DespatchedItems column" 
            HeaderText="Sorted" UniqueName="DespatchedItems">
            <HeaderTemplate>
                <table width="75">
                       <tr>
                          <td colspan="2" align="center"><b>Despatched</b></td>
                       </tr>
                       <tr><td align="right">Multi</td>
                           <td align="right">Singles</td>
                       </tr> 
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <table width="75">
                        <tr>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=T&ItemStatus=Despatched&ItemStatusCode=190"><%# DataBinder.Eval(Container.DataItem, "DespatchedMultis") %></a></td>
                           <td align="right" width="50%"><a  href="LoadDetail.aspx?LoadStatus=<%# DataBinder.Eval(Container.DataItem, "loadstatus") %>&LoadNumber=<%# DataBinder.Eval(Container.DataItem, "loadnumber") %>&MultiInd=F&ItemStatus=Despatched&ItemStatusCode=190"><%# DataBinder.Eval(Container.DataItem, "DespatchedSingles") %></a></td>
                       </tr>
                </table>
            </ItemTemplate>
            <FooterTemplate>
                <table width="75" border="0">
                    <tr>
                        <td align="right" width="50%">
                           <asp:Label ID="totdespatchedmultislabel" runat="server"> </asp:Label>
                        </td>
                        <td align="right" width="50%">
                            <asp:Label ID="totdespatchedsingleslabel" runat="server"> </asp:Label>
                        </td>
                    </tr>
                </table>
            </FooterTemplate>
        </telerik:GridTemplateColumn>

        <telerik:GridBoundColumn DataField="TotReadyReleaseSingles" 
            FilterControlAltText="Filter TotReadyReleaseSingles column" 
            UniqueName="TotReadyReleaseSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotReadyReleaseMultis" 
            FilterControlAltText="Filter TotReadyReleaseMultis column" 
            UniqueName="TotReadyReleaseMultis" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotReleasedSingles" 
            FilterControlAltText="Filter TotReleasedSingles column" 
            UniqueName="TotReleasedSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotReleasedMultis" 
            FilterControlAltText="Filter TotReleasedMultis column" 
            UniqueName="TotReleasedMultis" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotSortedSingles" 
            FilterControlAltText="Filter TotSortedSingles column" 
            UniqueName="TotSortedSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotSortedMultis" 
            FilterControlAltText="Filter TotSortedMultis column" 
            UniqueName="TotSortedMultis" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotLocatedSingles" 
            FilterControlAltText="Filter TotLocatedSingles column" 
            UniqueName="TotLocatedSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotLocatedMultis" 
            FilterControlAltText="Filter TotLocatedMultis column" 
            UniqueName="TotLocatedMultis" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotPackingSingles" 
            FilterControlAltText="Filter TotPackingSingles column" 
            UniqueName="TotPackingSingles" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotPackingMultis" 
            FilterControlAltText="Filter TotPackingMultis column" 
            UniqueName="TotPackingMultis" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotPackedSingles" 
            FilterControlAltText="Filter TotPackedSingles column" 
            UniqueName="TotPackedSingles" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotReadyPackMultis" 
            FilterControlAltText="Filter TotReadyPackMultis column" 
            UniqueName="TotReadyPackMultis" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotReadyPackSingles" 
            FilterControlAltText="Filter TotReadyPackSingles column" 
            UniqueName="TotReadyPackSingles" Visible="False">
        </telerik:GridBoundColumn>


        <telerik:GridBoundColumn DataField="TotPackedMultis" 
            FilterControlAltText="Filter TotPackedMultis column" 
            UniqueName="TotPackedMultis" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotCagedSingles" 
            FilterControlAltText="Filter TotCagedSingles column" 
            UniqueName="TotCagedSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotCagedMultis" 
            FilterControlAltText="Filter TotCagedMultis column" 
            UniqueName="TotCagedMultis" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotReadyDespatchSingles" 
            FilterControlAltText="Filter TotReadyDespatchSingles column" 
            UniqueName="TotReadyDespatchSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotReadyDespatchMultis" 
            FilterControlAltText="Filter TotReadyDespatchMultis column" 
            UniqueName="TotReadyDespatchMultis" Visible="False">
        </telerik:GridBoundColumn>

       <telerik:GridBoundColumn DataField="TotDespatchedSingles" 
            FilterControlAltText="Filter TotDespatchedSingles column" 
            UniqueName="TotDespatchedSingles" Visible="False">
        </telerik:GridBoundColumn>

        <telerik:GridBoundColumn DataField="TotDespatchedMultis" 
            FilterControlAltText="Filter TotDespatchedMultis column" 
            UniqueName="TotDespatchedMultis" Visible="False">
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


