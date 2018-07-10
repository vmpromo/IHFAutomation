<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/RI.Master"
    CodeBehind="AreaSetup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.AreaSetup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="childdefault">
        <table>
            <tr>
                <td class="childheader">
                    Area setup
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <asp:Label ID="Error" runat="server" Text="Label"></asp:Label>
            </tr>
        </table>
        <table>
            
            <tr>
                <td>
                    <telerik:RadGrid ID="grdAreaSetup" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                        AllowPaging="True" OnNeedDataSource="grdAreaSetup_NeedDataSource" OnDeleteCommand="grdAreaSetup_DeleteCommand"
                        OnItemDataBound="grdAreaSetup_ItemDataBound" OnUpdateCommand="grdAreaSetup_UpdateCommand"
                        OnInsertCommand="grdAreaSetup_InsertCommand" OnItemCreated="grdAreaSetup_ItemCreated"
                        OnItemCommand="grdAreaSetup_ItemCommand" Height="400px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" 
                        AllowCustomPaging="True" PageSize="20">
                        <%--<ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>--%>
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Bottom"
                            Wrap="True" />
                        <MasterTableView DataKeyNames="Area_id" AllowMultiColumnSorting="True" Width="100%"
                            CommandItemDisplay="Top" EditMode="PopUp" AllowCustomPaging="False">
                            <%--<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>--%>
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            </ExpandCollapseColumn>
                            <Columns>
                                <%-- ~/Pages/Dashboard/TrolleyDetail.aspx?trolley_id={0} --%>
                                <%--Edit button --%>
                                <telerik:GridBoundColumn UniqueName="Area_id" SortExpression="Area_id" HeaderText="Area_id"
                                    DataField="Area_id" ReadOnly="true" AllowFiltering="False" AllowSorting="true" Visible="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />
                                </telerik:GridBoundColumn>
                                <telerik:GridHyperLinkColumn
                                    HeaderText="Area ID" 
                                    UniqueName="areaid"
                                    DataTextField="Area_id"
                                    DataNavigateUrlFields="Area_id"
                                    AllowFiltering="false"
                                    SortExpression="Area_id"
                                    DataNavigateUrlFormatString="~/Pages/Admin/Setup/ChutesInArea.aspx?Area_id={0}">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" BorderStyle="Solid" />
                                </telerik:GridHyperLinkColumn> 
                                <telerik:GridTemplateColumn DataField="Area_type_id" 
                                    HeaderText="Type" SortExpression="Area_type_id" UniqueName="Area_type_id" AllowFiltering="false">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="Area_type_RadComboBox" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Area_type_RadComboBox"
                                            ErrorMessage="* Select Area type">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Area_type" runat="server" Text='<%# Eval("Area_type_id") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="75px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="65px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridTemplateColumn AllowFiltering="False" DataField="warehouse_name" 
                                    FilterControlAltText="Filter TemplateColumn4 column" HeaderText="Warehouse" 
                                    UniqueName="TemplateColumn4">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="rcbWarehouse" Runat="server">
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbWarehouse" runat="server"
                                            Text='<%# Eval("warehouse_name") %>'> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="65px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridTemplateColumn DataField="Area_code" HeaderText="Code" FilterControlWidth="35px"  AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArea_code" runat="server" Text='<%# Eval("Area_code") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="65px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="Area_desc" HeaderText="Description" 
                                    FilterControlWidth="65px"  AllowFiltering="false">
                                    <EditItemTemplate>
                                        <telerik:RadTextBox ID="Area_desc_TextBox" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Area_desc_TextBox"
                                            ErrorMessage="* Type Area description.">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Area_desc" runat="server" Text='<%# Eval("Area_desc") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="170px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="105px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridTemplateColumn DataField="Handle_Split_load" HeaderText="Handle Split Load"
                                    FilterControlWidth="50px"  AllowFiltering="false">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="handle_split_RadComboBox" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="handle_split_RadComboBox"
                                            ErrorMessage="* Select Handle Split.">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSplit_load" runat="server" Text='<%# Eval("Handle_Split_load") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="65px"></ItemStyle>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn DataField="Active_ind" HeaderText="Status"
                                    FilterControlWidth="35px" AllowFiltering="false">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="Act_ind_RadComboBox" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Act_ind_RadComboBox"
                                            ErrorMessage="* select Area Active Indicator.">
                                        </asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblArea_status" runat="server" Text='<%# Eval("Active_ind") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="65px"></ItemStyle>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" 
                                    DataField="allow_admin_release_ind" 
                                    FilterControlAltText="Filter TemplateColumn5 column" FilterControlWidth="50px" 
                                    HeaderText="Allow Admin Release" UniqueName="TemplateColumn5">
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="allow_admin_release_ind_RadComboBox" Runat="server">
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblallow_admin_release_ind" runat="server" 
                                            Text='<%# Eval("allow_admin_release_ind") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="95px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                    HeaderText="Edit">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </telerik:GridEditCommandColumn>
                                
                            </Columns>
                            <EditFormSettings CaptionFormatString="Edit details for Area with ID {0}" 
                                CaptionDataField="Area_id" InsertCaption="Add New Area">
                                <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                                <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                                <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                <EditColumn ButtonType="ImageButton" />
                                <PopUpSettings Height="300px" />
                            </EditFormSettings>
                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
                            <AlternatingItemStyle BorderColor="Black" BorderStyle="Solid" />
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <CommandItemStyle BorderColor="Black" BorderStyle="Solid" />
                        </MasterTableView>
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="True" />
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                        <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                        </HeaderContextMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
