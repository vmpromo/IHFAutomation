<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicedGroupCounts.ascx.cs"
    Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.ServicedGroupCounts" %>
<div class="InfoBlock">
    <div class="InfoBlockTitle">
        Operational Overview
    </div>
    <div class="InfoBlockContent">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlOperationalOverview">
            <ProgressTemplate>
                Loading...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="upnlOperationalOverview">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Go" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="WebPartSmall">
                    <div id="criteriafields">
                        Service Group
                        <asp:DropDownList runat="server" ID="ddlServiceGroup">
                        </asp:DropDownList>
                        <asp:Button runat="server" Text="GO" ID="btn_Go" OnClick="btnGo_Click" />
                    </div>
                    <asp:Repeater ID="rptOperationalView" runat="server" OnItemDataBound="rptOperationalView_ItemCommand">
                        <HeaderTemplate>
                            <table width="100%" cellpadding="4" cellspacing="0" id="tblDataList">
                                <tbody>
                                    <tr id="headerRow">
                                        <td colspan="2" align="center">
                                        </td>
                                        <td colspan="2" align="center">
                                            Multi
                                        </td>
                                        <td>
                                            Single
                                        </td>
                                        <td colspan="2" align="center">
                                            Total
                                        </td>
                                        <td align="center" style="width: 100px">
                                            KPI
                                        </td>
                                    </tr>
                                    <tr id="headerRow">
                                        <td>
                                            Status
                                        </td>
                                        <td>
                                            Load
                                        </td>
                                        <td>
                                            Orders
                                        </td>
                                        <td>
                                            Items
                                        </td>
                                        <td>
                                            Orders
                                        </td>
                                        <td>
                                            Orders
                                        </td>
                                        <td>
                                            Items
                                        </td>
                                        <td style="width: 30px">
                                            Earliest Order
                                        </td>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="ItemRow">
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"StatusDescription")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("LoadNumber") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("MultiOrders") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("MultiOrderitems") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("SingleOrders") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("TotalOrders") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <asp:Label runat="server" ID="lblTotalMultiAndSignleItems" />
                                </td>
                                </td>
                                <td style="width: 40px">
                                    <%# DataBinder.Eval(Container.DataItem,"EarliestOrderDateTime","{0:HH:mm  dd/MM/yyyy}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id="alternativeItemRow">
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"StatusDescription")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("LoadNumber") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("MultiOrders")??"0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("MultiOrderItems") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("SingleOrders") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <%# (Convert.ToInt32(Eval("TotalOrders") ?? "0")).ToString("#,##0")%>
                                </td>
                                <td style="text-align:right">
                                    <asp:Label runat="server" ID="lblTotalMultiAndSignleItems" />
                                </td>
                                </td>
                                <td style="width: 40px">
                                    <%# DataBinder.Eval(Container.DataItem,"EarliestOrderDateTime","{0:HH:mm  dd/MM/yyyy}")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr id="rooterRow">
                                <td>
                                    Total
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblLoadNumber" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblMultiOrderTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblMultiOrderItemTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblSingleOrderTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblOrderTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblOrderItemTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>                               
                                <td style="width: 40px">
                                </td>
                            </tr>
                            <tr id="trcanc">
                                <td colspan="2">
                                Cancelled
                                   
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblMultiOrderCancel" Text="ggg" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblMultiOrderCancelItem" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblSingleOrderCancel" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblCancelOrderTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>
                                <td style="text-align:right">
                                    <asp:Label ID="lblCancelItemTotal" Text="" Font-Size="8pt" runat="server" />
                                </td>                               
                                <td style="width: 40px">
                                </td>
                            </tr>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="dLastRefreshStatusBar">
                    Last Refreshed:
                    <asp:Label runat="server" ID="lblOpViewLastRefreshed" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
