<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Dashboard/IHFDashboard.Master" AutoEventWireup="true" CodeBehind="OrderInquiry.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Cancellation.OrderInquiry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Pages/Dashboard/IHFDashboard.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SetCursorToTextEnd(textControlID) {
            var text = document.getElementById(textControlID);
            if (text != null && text.value.length > 0) {
                if (text.createTextRange) {
                    var FieldRange = text.createTextRange();
                    FieldRange.moveStart('character', text.value.length);
                    FieldRange.collapse();
                    FieldRange.select();
                }
            }
        } 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>



    <div id="Cancellation" runat="server" clientidmode="Static" style="padding-top:0px;border-spacing:0px;">

        <table width="100%">
            <tr>
                <td class="childheader">Order Inquiry</td>
            </tr>
        </table>

        <div style="padding-left:3px;">
            <asp:Panel ID="submitPanel" runat="server" DefaultButton="Submit">
                <table>
                    <tr>
                        <td style="background-color:#C6DFFF;width:13%;">
                            Enter order #
                        </td>
                        <td style="width:10%;">
                            <asp:TextBox ID="orderNumber" runat="server" ClientIDMode="Static" />
                        </td>
                        <td style="width:5%;">
                            <asp:Button ID="Submit" Text="GO" runat="server" ClientIDMode="Static" 
                                onclick="Submit_Click" />
                        </td>
                        <td id="Message" runat="server" enableviewstate="false" clientidmode="Static" 
                            style="color:Green;font-size:20px;text-align:center;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <hr />

            <table style="width:75%;border-width:1px;border-style:groove;border: 1px Solid Black;">
                <tr>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Order</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Status</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">No. of Items</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Chute</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Trolley</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Workstation</td>
                    <td style="background-color:#C6DFFF;font-weight:bold;">Tote</td>
                </tr>
                <tr>
                    <td id="order" runat="server"></td>
                    <td id="status" runat="server"></td>
                    <td id="noOfItems" runat="server"></td>
                    <td id="chute" runat="server"></td>
                    <td id="trolley" runat="server"></td>
                    <td id="workstation" runat="server"></td>
                    <td id="failedTote" runat="server"></td>
                </tr>
            </table>
        </div>

        <table>
            
            <tr>
                <td id ="Grid" runat="server" clientidmode="Static">
                    <telerik:RadGrid 
                        ClientIDMode="Static"
                        ID="orderItemGrid" 
                        runat="server"
                        AutoGenerateColumns="False" 
                        AllowSorting="True" 
                        AllowPaging = "false"
                        Height="355px"
                        Font-Bold="False" Font-Italic="False" 
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                        GridLines="None" CellSpacing="0" 
                        onneeddatasource="orderItemGrid_NeedDataSource" 
                        onitemdatabound="orderItemGrid_ItemDataBound" onupdatecommand="orderItemGrid_UpdateCommand">
                    
                        <ClientSettings>
                            <Scrolling 
                                AllowScroll="true"
                                 ScrollHeight="355px" SaveScrollPosition="True"/>
                        </ClientSettings>
        
                        <AlternatingItemStyle Font-Bold="false" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Left" VerticalAlign="Bottom" Wrap="True" />
        
                        <MasterTableView 
                                DataKeyNames="itemnumber" 
                                AllowMultiColumnSorting="True" 
                                Width="100%"
                                CommandItemDisplay="Top"
                                EditMode="InPlace">

                                <CommandItemSettings
                                    ShowAddNewRecordButton="false" />
                                
                                <Columns>

                                    <telerik:GridEditCommandColumn 
                                        ButtonType="ImageButton" 
                                        UniqueName="EditCommandColumn" 
                                        HeaderText = "Cancel">
                        
                                        <HeaderStyle Width="50px"  HorizontalAlign="Center" />
                                        <ItemStyle Width="50px"  HorizontalAlign="Center" />
                                    
                                    </telerik:GridEditCommandColumn>

                                    
                                    
                                    <telerik:GridTemplateColumn
                                        AllowFiltering="false"
                                        HeaderText="Select Action">
                                        
                                        <ItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="Action_RadComboBox" 
                                                runat="server"
                                                Enabled="false"
                                                Width="125px">
                                            </telerik:RadComboBox>

                                        </ItemTemplate>

                                        <EditItemTemplate>
                            
                                            <telerik:RadComboBox 
                                                ID="Action_RadComboBox" 
                                                runat="server"
                                                Enabled="true"
                                                Width="125px">
                                            </telerik:RadComboBox>
                                            <br />
                                            <asp:RequiredFieldValidator
                                                runat="server"
                                                ID="RequiredFieldValidator1" 
                                                ControlToValidate="Action_RadComboBox"
                                                ErrorMessage="*Select Action"
                                                ForeColor="Red">

                                            </asp:RequiredFieldValidator>
                                                

                                        </EditItemTemplate>

                                        <HeaderStyle Width="125px" HorizontalAlign="Left" />
                                        <ItemStyle   Width="125px" HorizontalAlign="Left" />

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn 
                                        UniqueName="itemnumber" 
                                        AllowFiltering="false"
                                        SortExpression="itemnumber" 
                                        HeaderText="Item #"
                                        DataField="itemnumber"
                                        ReadOnly="true"
                                        AllowSorting="true">
                                    
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemStyle   Width="75px" HorizontalAlign="Left" />
                                    
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn 
                                        UniqueName="sku" 
                                        AllowFiltering="false"
                                        SortExpression="sku" 
                                        HeaderText="Sku #"
                                        DataField="sku"
                                        AllowSorting="true" 
                                        ReadOnly="true">
                                    
                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemStyle Width="75px" />

                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn 
                                        UniqueName="barcode" 
                                        AllowFiltering="false"
                                        SortExpression="barcode" 
                                        HeaderText="Barcode"
                                        DataField="barcode"
                                        AllowSorting="true" 
                                        ReadOnly="true">
                                    
                                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                        <ItemStyle Width="100px" />

                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn  
                                        DataField="status" 
                                        AllowFiltering="false"
                                        HeaderText="Status" 
                                        SortExpression="status" 
                                        UniqueName="status"
                                        ReadOnly = "true"
                                        AllowSorting="true"
                                        ItemStyle-HorizontalAlign="Left">
                                        
                                        <HeaderStyle Width="125px" HorizontalAlign="Center" />
                                        <ItemStyle Width="125px" />

                                    
                                    </telerik:GridBoundColumn >
                                    <telerik:GridBoundColumn  
                                        DataField="label" 
                                        AllowFiltering="false"
                                        HeaderText="In FT" 
                                        SortExpression="label" 
                                        UniqueName="label"
                                        ReadOnly = "true"
                                        AllowSorting="true"
                                        ItemStyle-HorizontalAlign="Left"
                                        >

                                        <HeaderStyle Width="50px"  HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" />

                                    </telerik:GridBoundColumn >

                                    <telerik:GridTemplateColumn
                                        AllowFiltering="false"
                                        HeaderText="Last Activity"
                                        UniqueName="last_changed_dtm1" 
                                        SortExpression="last_changed_dtm" 
                                        DataField="last_changed_dtm"
                                        ReadOnly="true"
                                        ItemStyle-HorizontalAlign="Left">
                                        
                                        <ItemTemplate>
                            
                                            <asp:Label 
                                                ID="last_activity" 
                                                runat="server">
                                            </asp:Label>

                                        </ItemTemplate>

                                        

                                        <HeaderStyle Width="75px" HorizontalAlign="Center" />
                                        <ItemStyle   Width="75px" HorizontalAlign="Center" />

                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn  
                                        DataField="description" 
                                        AllowFiltering="false"
                                        HeaderText="Description" 
                                        SortExpression="description" 
                                        UniqueName="description"
                                        ReadOnly = "true"
                                        AllowSorting="true"
                                        ItemStyle-HorizontalAlign="Left"
                                        >

                                        <HeaderStyle Width="175px" HorizontalAlign="left" />
                                        <ItemStyle   Width="175px" HorizontalAlign="left" />

                                   
                                    </telerik:GridBoundColumn >



                                </Columns>
                
                                <EditFormSettings
                                
                                    CaptionFormatString="Edit details for item number - {0}" 
                                    CaptionDataField="itemnumber" >
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
                            VerticalAlign="Bottom" Wrap="True" BackColor="#C6DFFF" ForeColor="Black" />

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
            <tr>
                <td id = "NoRecords" runat="server" enableviewstate="false">
                    No records to display.
                </td>
            </tr>
        </table>

    </div>
  
</asp:Content>
