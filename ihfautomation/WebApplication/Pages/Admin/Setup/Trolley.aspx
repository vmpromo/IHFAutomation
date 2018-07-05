﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true"
    CodeBehind="Trolley.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.Trolley" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;">
                Trolley setup
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False"
                    PageSize="13" AllowSorting="True" AllowPaging="True" OnUpdateCommand="RadGrid1_UpdateCommand"
                    OnNeedDataSource="RadGrid1_NeedDataSource" ShowStatusBar="True" OnInsertCommand="RadGrid1_InsertCommand"
                    AllowFilteringByColumn="true" OnEditCommand="RadGrid1_EditCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                    OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                    AutoGenerateDeleteColumn="False" AutoGenerateEditColumn="False">
                    <MasterTableView DataKeyNames="trolley_id" AllowMultiColumnSorting="True" Width="100%"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--<telerik:GridButtonColumn UniqueName="DeleteColumn" CommandName="Delete" ButtonType="PushButton"
                                Text="Delete" />--%>
                            <%--<telerik:GridEditCommandColumn ButtonType="PushButton" UniqueName="EditCommandColumn">
                                <HeaderStyle Width="85px"></HeaderStyle>
                            </telerik:GridEditCommandColumn>--%>
                            <telerik:GridBoundColumn UniqueName="trolley_id" SortExpression="trolley_id" HeaderText="Trolley ID"
                                DataField="trolley_id" ReadOnly="true" FilterControlWidth="30"/>
                            <telerik:GridBoundColumn UniqueName="trolley_label" SortExpression="trolley_label"
                                HeaderText="Label" DataField="trolley_label" ReadOnly="true"/>
                            <telerik:GridBoundColumn UniqueName="barcode" SortExpression="barcode" HeaderText="Barcode"
                                DataField="barcode" ReadOnly="true" ItemStyle-Font-Names="Courier" />
                            <telerik:GridTemplateColumn DataField="class_type" FilterControlAltText="Filter class_cd column"
                                HeaderText="Class Type" SortExpression="class_type" UniqueName="trolley_class" FilterControlWidth="25">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="trolleyclass_type_RadComboBox" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="trolleyclass_type_RadComboBox_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="trolleyclass_type_RadComboBox"
                                        ErrorMessage="*Select Trolley Class">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="trolleyclass_type_Label" runat="server" Text='<%# Eval("class_type") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px"></ItemStyle>
                                
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="trolley_type" FilterControlAltText="Filter class_cd column"
                                HeaderText="Trolley Type" SortExpression="trolley_type" UniqueName="trolley_typ" FilterControlWidth="25">
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="trolleytype_RadComboBox" runat="server">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="trolleytype_RadComboBox"
                                        ErrorMessage="*Select Trolley Type">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="trolleytype_Label" runat="server" Text='<%# Eval("trolley_type") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="trolley_status" SortExpression="trolley_status"
                                HeaderText="Trolley Status" DataField="trolley_status" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="class_type" SortExpression="class_type" HeaderText="Class Type"
                                DataField="class_type" Visible="false" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="trolley_type" SortExpression="trolley_type"
                                HeaderText="Trolley Type" DataField="trolley_type" Visible="false" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="class_cd" SortExpression="class_cd" HeaderText="class code"
                                DataField="class_cd" Visible="false" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="status_cd" SortExpression="status_cd" HeaderText="status cd"
                                DataField="status_cd" Visible="false" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="chute_id" SortExpression="chute_id" HeaderText="Chute ID"
                                DataField="chute_id" Visible="true" ReadOnly="true" FilterControlWidth="40" />
                            <telerik:GridBoundColumn UniqueName="chute_label" SortExpression="chute_label" HeaderText="Chute Label"
                                DataField="chute_label" Visible="true" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="type_id" SortExpression="type_id" HeaderText="type id"
                                DataField="type_id" Visible="false" ReadOnly="true" />
                            <telerik:GridBoundColumn UniqueName="trolley_locations" SortExpression="trolley_locations" HeaderText="Number of Locations"
                                DataField="trolley_locations" Visible="true" ReadOnly="true" FilterControlWidth="40" />
                            <%--<telerik:GridButtonColumn UniqueName="PrintColumn" CommandName="print" ButtonType="PushButton"
                                Text="Print" />--%>
                            <telerik:GridButtonColumn HeaderImageUrl="~/Images/print.gif" UniqueName="Print"
                                    CommandName="Print" ButtonType="ImageButton" Text="Print" ImageUrl="~/Images/print.gif"
                                    HeaderText="Print">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings CaptionFormatString="Edit details for trolley with ID {0}" CaptionDataField="trolley_id">
                            <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                            <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                            <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                            <EditColumn ButtonType="ImageButton" />
                        </EditFormSettings>
                    </MasterTableView>
                    <%--<ClientSettings>
            <ClientEvents OnRowDblClick="RowDblClick" />
        </ClientSettings>--%>
                    <%--<ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
     <ClientEvents OnRowDblClick="RowDblClick" />--%>
                    <%--</ClientSettings>--%>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    </HeaderContextMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>