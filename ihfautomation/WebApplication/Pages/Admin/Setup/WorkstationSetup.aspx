<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="WorkstationSetup.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Administration.Setup.WorkstationSetup" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <div class="childdefault">

        <table>
            <tr>
                <td class="childheader">Workstation setup</td>
            </tr>
        </table>

        <table>
        
            <tr>
                <td>
                
                    <telerik:RadGrid 
                        ID="workstationGrid" 
                        runat="server"
                        AutoGenerateColumns="False" 
                        AllowSorting="True" 
                        AllowPaging="True" 
                        onneeddatasource="workstationGrid_NeedDataSource" 
                        ondeletecommand="workstationGrid_DeleteCommand" 
                        onitemdatabound="workstationGrid_ItemDataBound" 
                        onupdatecommand="workstationGrid_UpdateCommand" 
                        oninsertcommand="workstationGrid_InsertCommand"
                        Height="400px" Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        AllowFilteringByColumn="True" onitemcreated="workstationGrid_ItemCreated" 
                        onitemcommand="workstationGrid_ItemCommand">
        
                        <%--<ItemStyle BorderColor="Black" BorderStyle="Solid" />--%>

                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
        
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="True" />
        
                        <MasterTableView 
                                DataKeyNames="workstation_id" 
                                AllowMultiColumnSorting="True" 
                                Width="100%"
                                CommandItemDisplay="Top">
            
                                <CommandItemSettings 
                                    ExportToPdfText="Export to Pdf">
                                </CommandItemSettings>

                                <RowIndicatorColumn 
                                    FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>

                                <ExpandCollapseColumn 
                                    FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>

                                <Columns>
                                
                                    <telerik:GridBoundColumn 
                                        UniqueName="workstation_id" 
                                        SortExpression="workstation_id" 
                                        HeaderText="ID"
                                        DataField="workstation_id"
                                        ReadOnly="true"
                                        AllowFiltering="False"
                                        AllowSorting="true">
                        
                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle width="50px" BorderStyle="Solid" />
                        
                                    </telerik:GridBoundColumn>
                    
                                    <telerik:GridTemplateColumn 
                                        DataField="workstation_type" 
                                        FilterControlAltText="Filter WORKSTATION_TYPE_ID column" 
                                        HeaderText="Type" 
                                        SortExpression="workstation_type"
                                        UniqueName="workstation_type"
                                        FilterControlWidth="35px">
                        
                                        <%--<HeaderStyle Width="100px" />
                                        <ItemStyle Width = "100px" BorderStyle="Solid" />--%>

                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="workstation_type_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator1" 
                                                runat="server" 
                                                ControlToValidate="workstation_type_RadComboBox" 
                                                ErrorMessage="*Select workstation type">
                                            </asp:RequiredFieldValidator>


                                            <asp:CustomValidator 
                                                ID="CustomValidator1" 
                                                runat="server" 
                                                ClientValidationFunction="CheckType" 
                                                ControlToValidate="workstation_type_RadComboBox">
                                            </asp:CustomValidator>



                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="workstation_type_Label" 
                                                runat="server" 
                                                Text='<%# Eval("workstation_type") %>'>
                                            </asp:Label>
                            
                                        </ItemTemplate>

                                        <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="65px"></ItemStyle>

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn 
                                        DataField="workstation_status" 
                                        FilterControlAltText="Filter WORKSTATION_TYPE_ID column" 
                                        HeaderText="Status" 
                                        SortExpression="workstation_status" 
                                        UniqueName="workstation_status"
                                        AllowFiltering="False">
                        
                        
                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="workstation_status_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator2" 
                                                runat="server" 
                                                ControlToValidate="workstation_status_RadComboBox" 
                                                ErrorMessage="*Select workstation status">
                                            </asp:RequiredFieldValidator>


                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="workstation_status_Label" 
                                                runat="server" 
                                                Text='<%# Eval("workstation_status") %>'>
                                            </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="75px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="75px"></ItemStyle>

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn 
                                        UniqueName="barcode" 
                                        SortExpression="barcode" 
                                        HeaderText="Barcode"
                                        DataField="barcode" 
                                        ReadOnly="true"  
                                        AllowSorting="True"
                                        AllowFiltering="False">
                        
                                        <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="150px" Font-Names="Courier" ></ItemStyle>
                        
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn 
                                        UniqueName="workstation_label" 
                                        SortExpression="workstation_label" 
                                        HeaderText="Label"
                                        DataField="workstation_label"
                                         FilterControlWidth="90px" 
                                         >

                                         <%--<HeaderStyle Width="175px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="175px" Font-Names="Courier" ></ItemStyle>--%>

                                        <%--<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle></ItemStyle>--%>

                                    </telerik:GridBoundColumn>

                                    <%--<telerik:GridTemplateColumn 
                                        DataField="trolley_label" 
                                        FilterControlAltText="Filter WORKSTATION_TYPE_ID column" 
                                        HeaderText="Trolley" 
                                        SortExpression="trolley_label"
                                        UniqueName="trolley_label"
                                        ReadOnly="true">--%>
                        
                        
                                        <%--<EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="trolley_label_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>

                                            <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator3" 
                                                runat="server" 
                                                ControlToValidate="trolley_label_RadComboBox" 
                                                ErrorMessage="*Select trolley">
                                            </asp:RequiredFieldValidator>


                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="trolley_label_Label" 
                                                runat="server" 
                                                Text='<%# Eval("trolley_label") %>'>
                                            </asp:Label>

                                        </ItemTemplate>--%>

                                        <%--<HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="120px" ></ItemStyle>

                                    </telerik:GridTemplateColumn>--%>

                                    <telerik:GridBoundColumn 
                                        UniqueName="trolley_label" 
                                        SortExpression="trolley_label" 
                                        HeaderText="Trolley"
                                        DataField="trolley_label"
                                        ReadOnly="true"
                                        AllowFiltering="False"
                                        AllowSorting="true">

                                        
                        
                                        <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>

                                        <ItemStyle Width="100px" ></ItemStyle>
                        
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn 
                                        DataField="international_ind_description" 
                                        FilterControlAltText="Filter WORKSTATION_TYPE_ID column" 
                                        HeaderText="International" 
                                        SortExpression="international_ind_description" 
                                        UniqueName="international_ind_description"
                                        AllowFiltering="False">
                        
                        
                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="international_ind_RadComboBox" 
                                                runat="server">
                                            </telerik:RadComboBox>

                                        </EditItemTemplate>
                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="international_ind_Label" 
                                                runat="server" 
                                                Text='<%# Eval("international_ind_description") %>'>
                                            </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="100px" HorizontalAlign="Center" ></HeaderStyle>

                                        <ItemStyle Width="100px" HorizontalAlign="Center" ></ItemStyle>

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridEditCommandColumn 
                                        ButtonType="ImageButton" 
                                        UniqueName="EditCommandColumn" 
                                        HeaderText="Edit">
                        
                                        <%--<HeaderStyle 
                                            Width="20px">
                                        </HeaderStyle>
                                        <ItemStyle Width = "20px" BorderStyle="Solid" />--%>

                                        <HeaderStyle 
                                            Width="50px"
                                            HorizontalAlign="Center" />

                                        <ItemStyle 
                                            Width="50px"
                                            HorizontalAlign="Center"  />

                                    </telerik:GridEditCommandColumn>

                                    <telerik:GridButtonColumn 
                                        UniqueName="DeleteColumn"
                                        HeaderText="Delete" 
                                        CommandName="Delete" 
                                        ButtonType="ImageButton" 
                                        Text="Delete" ConfirmText="Are you sure you want to delete this record?" 
                                        ConfirmTitle="IHF Automation">  

                                        <HeaderStyle Width="50px" HorizontalAlign="Center"  />
                                        <ItemStyle Width = "50px" BorderStyle="Solid" HorizontalAlign="Center"  />


                                    </telerik:GridButtonColumn>

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
                
                                <EditFormSettings CaptionFormatString="Edit details for workstation with ID {0}" CaptionDataField="WORKSTATION_ID">
                                    <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                                    <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                                    <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                                    <EditColumn ButtonType="ImageButton" />
                                </EditFormSettings>
                                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
                                <AlternatingItemStyle BorderColor="Black" BorderStyle="Solid" />
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" 
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
