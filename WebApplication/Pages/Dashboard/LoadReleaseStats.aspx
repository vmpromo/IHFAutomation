<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" 
CodeBehind="LoadReleaseStats.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Dashboard.LoadReleaseStats" %>
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
                LOAD RELEASE STATISTICS </b>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; color: Black; padding: 5px 5px 5px 5px; Font-Size: small;" >
                <table><tr><td>Load: </td><td>
                    <telerik:RadComboBox ID="RadComboLoad" Runat="server" AutoCompleteSeparator=";" 
                        MarkFirstMatch="True">
                    </telerik:RadComboBox>
                    </td><td>
                        <telerik:RadButton ID="RadBtnGo" runat="server" Text="GO" 
                            onclick="RadBtnGo_Click">
                        </telerik:RadButton>
                    </td></tr></table></td>
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td>
                <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
                    Text="Button" />--%>
                <telerik:RadGrid ID="RadGrid3" runat="server" 
                    onneeddatasource="RadGrid3_NeedDataSource" AutoGenerateColumns="False" 
                    CellSpacing="0" GridLines="None" AllowPaging="True" 
                    onitemcreated="RadGrid3_ItemCreated" PageSize="15" AllowSorting="True">
<MasterTableView AutoGenerateColumns="False" DataKeyNames="LoadNumber">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

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
        <telerik:GridBoundColumn DataField="interfacedatetime" 
            FilterControlAltText="Filter InterfaceTime column" HeaderText="Release Time" DataType="System.DateTime"
            UniqueName="InterfaceTime">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="singleorders" 
            FilterControlAltText="Filter column1 column" HeaderText="Single Orders" 
            UniqueName="singleorders" DataType="System.Int32">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="multiorders" 
            FilterControlAltText="Filter column2 column" HeaderText="Multi Orders" 
            UniqueName="multiorders">
        </telerik:GridBoundColumn>
<telerik:GridBoundColumn DataField="internationalorders" HeaderText="Int. Orders" 
            UniqueName="internationalorders" 
            FilterControlAltText="Filter internationalorders column"></telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="multiitems" 
            FilterControlAltText="Filter column3 column" HeaderText="Multi Items" 
            UniqueName="multiitems">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="percentagecomplete" 
            FilterControlAltText="Filter column4 column" 
            HeaderText="% Multi Orders Complete" UniqueName="percentagecomplete">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="missingitems" 
            FilterControlAltText="Filter column5 column" HeaderText="Missing Items" 
            UniqueName="missingitems">
        </telerik:GridBoundColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter LinkedUnreleased column" 
            HeaderText="Linked Unreleased" UniqueName="LinkedUnreleased">
            <ItemTemplate>
                <asp:ListBox ID="lbUnreleased" runat="server" Rows="2"></asp:ListBox>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
        <telerik:GridTemplateColumn FilterControlAltText="Filter LinkedReleased column" 
            HeaderText="Linked Released" UniqueName="LinkedReleased">
            <ItemTemplate>
                <asp:ListBox ID="lbReleased" runat="server" Rows="2"></asp:ListBox>
            </ItemTemplate>
        </telerik:GridTemplateColumn>
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

