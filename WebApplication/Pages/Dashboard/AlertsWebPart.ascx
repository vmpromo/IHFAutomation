<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlertsWebPart.ascx.cs"
    Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.AlertsWebPart" %>
<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upnlAlters">
    <ProgressTemplate>
        Loading...
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel runat="server" ID="upnlAlters">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkRefresh" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <div class="refreshAction">
            <asp:LinkButton runat="server" ID="lnkRefresh" OnClick="lnkRefresh_Click" Text="Refresh" />
        </div>
        <div class="">
            <asp:Repeater runat="server" ID="rptAlertView" 
                OnDataBinding="rptAlterView_OndataBinding" 
                onitemdatabound="rptAlertView_ItemDataBound">
                <HeaderTemplate>
                    <table border="0" width="100%">
                        <tbody>
                            <tr id="headerRow">
                                <td>
                                    Priority
                                </td>
                                <td>
                                    Error Type
                                </td>
                                <td>
                                    New
                                </td>
                                <td>
                                    Last Hour
                                </td>
                                <td>
                                    Total
                                </td>
                            </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Priority")%>
                        </td>
                        <td>
                            <a class="lnkErrorType" href="AlertDetails.aspx?et=<%# DataBinder.Eval(Container.DataItem, "ErrorType")%>&onlynew=false">
                                <%# DataBinder.Eval(Container.DataItem, "ErrorType")%>
                            </a>
                        </td>
                        <td>
                        <asp:PlaceHolder ID="PhNewErrors" runat="server">
                        <a class="lnkErrorType" href="AlertDetails.aspx?et=<%# DataBinder.Eval(Container.DataItem, "ErrorType")%>&onlynew=true">
                            <%# DataBinder.Eval(Container.DataItem, "NewErrors")%>
                            </a>
                            </asp:PlaceHolder>
                        <asp:PlaceHolder ID="PhNoErrors" runat="server">
                            <%# DataBinder.Eval(Container.DataItem, "NewErrors")%>
                            </asp:PlaceHolder>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "ErrorInLastHour")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Total")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody> </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="pages">
            <asp:LinkButton runat="server" ID="lnkPrev" Text="«" OnClick="lnkPrev_Click" Enabled="false" />
            &nbsp;<asp:Label runat="server" Text="" ID="lblCPage" Font-Size="8pt" />
            <asp:LinkButton runat="server" ID="lnkNext" Text="»" OnClick="lnkNext_Click" Enabled="false" />
        </div>
        <div class="dLastRefreshStatusBar">
            Last Refreshed:
            <asp:Label runat="server" ID="lblLastRefreshedAlerts" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
