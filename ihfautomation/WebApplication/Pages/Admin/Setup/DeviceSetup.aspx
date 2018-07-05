<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="DeviceSetup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Administration.Setup.DeviceSetup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadButton ID="RadButton1" runat="server" onclick="RadButton1_Click" 
        Text="Get New Printers">
    </telerik:RadButton>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <div class="childdefault">

        <table>
            <tr>
                <td class="childheader">Device setup</td>
            </tr>
        </table>

        <table>
        
            <tr>
                <td>
                    <telerik:RadGrid 
                        ID="deviceGrid" 
                        runat="server"
                        AutoGenerateColumns="False" 
                        AllowSorting="True" 
                        AllowPaging="True" 
                        onneeddatasource="deviceGrid_NeedDataSource" 
                        ondeletecommand="deviceGrid_DeleteCommand" 
                        onitemdatabound="deviceGrid_ItemDataBound" 
                        onupdatecommand="deviceGrid_UpdateCommand" 
                        oninsertcommand="deviceGrid_InsertCommand"
                        Height="400px" Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        GridLines="None" AllowFilteringByColumn="True" 
                        onitemcreated="deviceGrid_ItemCreated" 
                        oneditcommand="deviceGrid_EditCommand" 
                        onitemcommand="deviceGrid_ItemCommand" CellSpacing="0">
        
                    
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />

                        </ClientSettings>
        
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="True" />
        
                        <MasterTableView 
                                DataKeyNames="device_id" 
                                AllowMultiColumnSorting="True" 
                                Width="100%"
                                CommandItemDisplay="Top">
            
                                <CommandItemSettings 
                                    ExportToPdfText="Export to Pdf">
                                </CommandItemSettings>

                                <CommandItemSettings AddNewRecordText="Add device" />

                                <RowIndicatorColumn 
                                    FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>

                                <ExpandCollapseColumn 
                                    FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>

                                <Columns>

                                    <telerik:GridBoundColumn 
                                        UniqueName="device_id" 
                                        SortExpression="device_id" 
                                        HeaderText="ID"
                                        DataField="device_id"
                                        ReadOnly="true"
                                        AllowFiltering="False"
                                        AllowSorting="true">
                                    
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle   Width="50px" HorizontalAlign="Center" />
                                    
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn DataField="device_name" 
                                        FilterControlAltText="Filter device_name column" FilterControlWidth="160px" 
                                        HeaderText="Name" SortExpression="device_name" UniqueName="device_name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="device_nameTextBox" runat="server" ReadOnly="True" 
                                                Text='<%# Bind("device_name") %>' Width="350px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            <telerik:RadComboBox ID="rcbName" Runat="server" ClientIDMode="Static" 
                                                EnableVirtualScrolling="True" ItemRequestTimeout="10" ItemsPerRequest="15" 
                                                ShowMoreResultsBox="True" Width="350px">
                                            </telerik:RadComboBox>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="device_nameLabel" runat="server" 
                                                Text='<%# Eval("device_name") %>' Width="400px"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn 
                                        DataField="device_type" 
                                        FilterControlAltText="Filter device type" 
                                        HeaderText="Type" 
                                        SortExpression="device_type" 
                                        UniqueName="device_type"
                                        AllowFiltering="true"
                                        ShowFilterIcon="True"
                                        FilterControlWidth="45px">
                        
                                    
                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="device_type_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>


                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                ControlToValidate="device_type_RadComboBox" 
                                                ErrorMessage="*Select device type">
                                            </asp:RequiredFieldValidator>


                                            <asp:CustomValidator 
                                                ID="CustomValidator1" 
                                                runat="server" 
                                                ClientValidationFunction="CheckStatus" 
                                                ControlToValidate="device_type_RadComboBox">
                                            </asp:CustomValidator>


                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="device_type_Label" 
                                                runat="server" 
                                                Text='<%# Eval("device_type") %>'>
                                            </asp:Label>
                            
                                        </ItemTemplate>

                                        <HeaderStyle Width="75px"  HorizontalAlign="Center" />
                                        <ItemStyle Width="75px" />

                                    </telerik:GridTemplateColumn>
                    
                                    <telerik:GridTemplateColumn 
                                        DataField="workstation_label" 
                                        FilterControlAltText="Filter workstation" 
                                        HeaderText="Workstation" 
                                        SortExpression="workstation_label" 
                                        UniqueName="workstation_label"
                                        AllowFiltering="true"
                                        FilterControlWidth="60px">
                        
                                    
                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="workstation_label_RadComboBox" 
                                                runat="server" ChangeTextOnKeyBoardNavigation="False" 
                                                EnableLoadOnDemand="True" EnableVirtualScrolling="True" Filter="Contains" 
                                                ItemsPerRequest="10" MarkFirstMatch="True" MaxLength="10" 
                                                ShowMoreResultsBox="True">
                                            </telerik:RadComboBox>


                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator2" 
                                                runat="server" 
                                                ControlToValidate="workstation_label_RadComboBox" 
                                                ErrorMessage="*Select workstation">
                                            </asp:RequiredFieldValidator>


                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="workstation_label_Label" 
                                                runat="server" 
                                                Text='<%# Eval("workstation_label") %>'>
                                            </asp:Label>
                            
                                        </ItemTemplate>

                                        <HeaderStyle Width="90px"  HorizontalAlign="Center" />
                                        <ItemStyle Width="90px" />

                                    
                                    </telerik:GridTemplateColumn>

                                    <%--<telerik:GridBoundColumn 
                                        UniqueName="serial_number" 
                                        SortExpression="serial_number" 
                                        HeaderText="Serial number"
                                        DataField="serial_number"
                                        AllowFiltering="False"
                                        AllowSorting="true">
                                    
                                    
                                    </telerik:GridBoundColumn>--%>

                                    <telerik:GridBoundColumn 
                                        UniqueName="barcode" 
                                        SortExpression="barcode" 
                                        HeaderText="Barcode"
                                        DataField="barcode"
                                        ReadOnly="true"
                                        AllowFiltering="False"
                                        AllowSorting="true"
                                        ItemStyle-HorizontalAlign="Left"
                                        >
                                    
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Font-Names="Courier" />
                                    
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn 
                                        DataField="username" 
                                        FilterControlAltText="Filter user" 
                                        HeaderText="Current user" 
                                        SortExpression="username" 
                                        UniqueName="username"
                                        AllowFiltering="true"
                                        ReadOnly="true">
                        
                        
                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="username_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator3" 
                                                runat="server" 
                                                ControlToValidate="username_RadComboBox" 
                                                ErrorMessage="*Select user">
                                            </asp:RequiredFieldValidator>

                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="username_Label" 
                                                runat="server" 
                                                Text='<%# Eval("username") %>'>
                                            </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="120px" />
                                        <ItemStyle Width="120px" />
                                    
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridEditCommandColumn 
                                        ButtonType="ImageButton" 
                                        UniqueName="EditCommandColumn" 
                                        HeaderImageUrl = "~/Images/edit.gif">
                        
                                        <HeaderStyle Width="50px"  HorizontalAlign="Center" />
                                        <ItemStyle Width="50px"  HorizontalAlign="Center" />
                                    
                                    </telerik:GridEditCommandColumn>

                                    <telerik:GridButtonColumn 
                                        UniqueName="DeleteColumn"
                                        CommandName="Delete" 
                                        ButtonType="ImageButton" 
                                        HeaderImageUrl = "~/Images/delete.gif"
                                        ConfirmText="Are you sure you want to delete this record?" 
                                        ConfirmTitle="IHF Automation">  

                                        <HeaderStyle Width="50px"  HorizontalAlign="Center"/>
                                        <ItemStyle Width="50px"  HorizontalAlign="Center"/>
                                    </telerik:GridButtonColumn>

                                    <telerik:GridButtonColumn
                                        HeaderImageUrl="~/Images/print.gif" 
                                        UniqueName="Print" 
                                        CommandName="Print" 
                                        ButtonType="ImageButton"
                                        Text = "Print"
                                        ImageUrl = "~/Images/print.gif">
                                
                                    <HeaderStyle Width="50px"  HorizontalAlign="Center" />
                                    <ItemStyle Width="50px"  HorizontalAlign="Center"/>

                                    </telerik:GridButtonColumn>

                                </Columns>
                
                                <EditFormSettings 
                                    CaptionFormatString="Edit details for device with ID {0}" 
                                    CaptionDataField="device_id" >
                                    <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                                    <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                                    <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                    <EditColumn ButtonType="ImageButton" />
                                </EditFormSettings>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
                                <AlternatingItemStyle BorderColor="Black" BorderStyle="Solid" />
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" 
                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                    Font-Underline="False" Wrap="False" />
                                <CommandItemStyle BorderColor="Black" BorderStyle="Solid" />
                            </MasterTableView>

                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            VerticalAlign="Bottom" Wrap="True" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" Wrap="True" />

                        <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            VerticalAlign="Bottom" Wrap="True" />
                        <PagerStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Bottom" 
                            Wrap="True" />

                <FilterMenu EnableImageSprites="False"></FilterMenu>

                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                    </telerik:RadGrid>                            
                </td>
            </tr>
        </table>
    </div>

    
</asp:Content>
