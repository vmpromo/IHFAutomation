<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreManifest.aspx.cs" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.StoreManifest" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <b>CREATE STORE MANIFEST </b>
            </td>
        </tr>
    </table>
<table width="100%">
<tr><td class="style1"></td></tr>
<tr>
<td class="style1">
          <telerik:RadComboBox runat="server" ID="rcbVanRun" Height="100px" EnableLoadOnDemand="true"
               ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
              EmptyMessage="Type here ..." EnableAutomaticLoadOnDemand="True" 
              ItemsPerRequest="10" Width="228px" Label="Enter Van Run: ">
          </telerik:RadComboBox>
          <telerik:RadButton ID="RadButton1" runat="server" 
              onclick="CreateManifestVanRun_Click" Text="Create Manifest">
          </telerik:RadButton>
    </td><td>
        <asp:Label ID="lbMsg2" runat="server" ForeColor="Blue"></asp:Label>
</td>
</tr>
<tr>
<td class="style1">
          <telerik:RadComboBox runat="server" ID="rcbStore" Height="19px" EnableLoadOnDemand="true"
               ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
              EmptyMessage="Type here ..." EnableAutomaticLoadOnDemand="True" 
              ItemsPerRequest="10" Width="365px" Label="Enter Store: ">
          </telerik:RadComboBox>
          <telerik:RadButton ID="tbCreateManifest" runat="server" 
              onclick="CreateManifestStore_Click" Text="Create Manifest">
          </telerik:RadButton>
    </td><td>
        <asp:Label ID="lbMsg" runat="server" ForeColor="Blue"></asp:Label>
</td>
</tr>
</table>
<table width="100%">
<tr>
<td>
    <telerik:RadGrid ID="rgManifestList" runat="server" 
        AllowFilteringByColumn="True" AllowSorting="True" AutoGenerateColumns="False" 
        CellSpacing="0" GridLines="None" 
        onitemcommand="rgManifestList_ItemCommand" Width="100%" AllowPaging="True" 
        PageSize="20">
<MasterTableView 
            AutoGenerateColumns="False" DataKeyNames="manifest_id">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridBoundColumn DataField="Manifest_id" DataType="System.Decimal" 
            FilterControlAltText="Filter column column" HeaderText="Manifest ID" 
            UniqueName="column" AllowFiltering="False">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="storename" 
            FilterControlAltText="Filter column1 column" HeaderText="Store" 
            UniqueName="column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="rte_name" 
            FilterControlAltText="Filter column4 column" HeaderText="Van Run" 
            UniqueName="column4">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn AllowFiltering="False" DataField="manifestdate" 
            DataType="System.DateTime" FilterControlAltText="Filter column3 column" 
            HeaderText="Manifest Date" UniqueName="column3">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="bagcount" DataType="System.Decimal" 
            FilterControlAltText="Filter column2 column" HeaderText="Bag Count" 
            UniqueName="column2" AllowFiltering="False">
        </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn
                                        HeaderImageUrl="~/Images/print.gif" 
                                        UniqueName="Print" 
                                        CommandName="Print" 
                                        ButtonType="ImageButton"
                                        Text = "Print"
                                        ImageUrl = "~/Images/print.gif">
                                
                                    <HeaderStyle 
                                        Width="50px"
                                        HorizontalAlign="Center"  />
                                    <ItemStyle 
                                        Width="50px" 
                                        HorizontalAlign="Center" />

                                    </telerik:GridButtonColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
    </telerik:RadGrid>
</td>
</tr>
</table>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
        width: 577px;
    }
    </style>
</asp:Content>
