<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="TestLiveLoads.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.TestHarness.TestLiveLoads" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="2000">
    </telerik:RadScriptManager>
    <div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetLines" 
        TypeName="IHF.BusinessLayer.DataAccessObjects.TestLiveLoadsDAO">
    </asp:ObjectDataSource>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var currentLoadingPanel = null;
            var currentUpdatedControl = null;
            var btn1 = $find("<%= Button1.ClientID%>");

            function requestStart(sender, args) {
                currentLoadingPanel = $find("<%= RadAjaxLoadingPanel1.ClientID%>");
                btn1 = $find("<%= Button1.ClientID%>");

                if (args.get_eventTarget() == "<%= Button1.UniqueID %>") {
                    currentUpdatedControl = "<%= Panel1.ClientID %>";
                    //show the loading panel over the updated control   
                    currentLoadingPanel.show(currentUpdatedControl);
                }
            }
            function responseEnd() {
                //hide the loading panel and clean up the global variables
                if (currentLoadingPanel != null) {
                    currentLoadingPanel.hide(currentUpdatedControl);
                }
                currentUpdatedControl = null;
                currentLoadingPanel = null;
            }
        </script>
    </telerik:RadCodeBlock>

    <table width="100%">
        <tr>
            <td style="text-align:center;color:Black;padding:5px 5px 5px 5px;"><strong>Import Live Loads into UAT</strong></td>
        </tr>
    </table>
    <br />
    <br />
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
            DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Button1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lbmsg" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" 
                            LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
            <ClientEvents OnRequestStart="requestStart" OnResponseEnd="responseEnd"></ClientEvents>
        </telerik:RadAjaxManager>
            <asp:Panel ID="Panel1" runat="server">
    <div>
        <br />
        <br />
        <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Visible="False" 
            Width="500px"></asp:Label>
    </div>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>


    <asp:Label ID="Label1" runat="server" Text="Select Live pick load:"></asp:Label>
    <telerik:RadComboBox ID="RadComboBox1" Runat="server" 
        DataSourceID="ObjectDataSource1" DataTextField="PickLoadNum" 
        DataValueField="PickLoadNum" ClientIDMode="Static" 
        EnableAutomaticLoadOnDemand="True" EnableLoadOnDemand="True" 
        EnableVirtualScrolling="True" ItemsPerRequest="10" ShowMoreResultsBox="True">
    </telerik:RadComboBox>

    <br />
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Import Live Load" />
    </asp:Panel>
    </div>
    <br />
    <br />
</asp:Content>
