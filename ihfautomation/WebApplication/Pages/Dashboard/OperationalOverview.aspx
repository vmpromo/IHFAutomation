<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationalOverView.aspx.cs"
    Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.OperationalView" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" width="100%">
            <tbody>
                <tr>
                    <td>
                        <div>
                            <table width="100%" border="0">
                                <tbody>
                                    <tr>
                                        <td valign="top" style="width:60%;padding-top:0px;">
                                    <table width="100%" border="0" style="vertical-align"><tbody ><tr><td style="padding-top:0px;">
                                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="LoadingPanel1">
                                            
                                            <asp:Panel ID="Panel1" runat="server">
                                            </asp:Panel>
                                            </telerik:RadAjaxPanel>
                                            </td></tr>
                                            <tr><td>
                                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="LoadingPanel2">
                                           
                                            <asp:Panel ID="Panel2" runat="server">
                                            </asp:Panel>
                                             </telerik:RadAjaxPanel>
                                            
                                            </td></tr></tbody>
                                            </table>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
