<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" 
CodeBehind="ChutesInArea.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.Setup.ChutesInArea" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: center; color: Black; padding: 5px 5px 5px 5px;" >
                    Chutes In Area
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lblArea" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="Back" onclick="Button1_Click" />
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadGrid ID="RadGrid1" 
                            runat="server" 
                            AllowPaging="True" 
                            ShowFooter="true"
                            ShowHeader="true" 
                            ShowStatusBar="true" 
                            AllowSorting="True" 
                            PageSize="10" 
                            GridLines="Both"
                            Width="100%" 
                            OnNeedDataSource="RadGrid1_NeedDataSource"
                            OnItemDataBound="RadGrid1_ItemDataBound"
                            OnItemCreated="RadGrid1_ItemCreated"
                            OnItemCommand="RadGrid1_ItemCommand">
                            
                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                        <MasterTableView DataKeyNames="chute_id" AutoGenerateColumns="false" AllowMultiColumnSorting="True" Width="100%">
                            <Columns>
                                
                                <telerik:GridBoundColumn UniqueName="chute_id" SortExpression="chute_id" HeaderText="chute_id"
                                    DataField="chute_id" ReadOnly="true" AllowFiltering="False" AllowSorting="true">                                    
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="chlabel" SortExpression="chlabel" HeaderText="Label"
                                    DataField="chlabel" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="statuscd_desc" SortExpression="statuscd_desc" HeaderText="Status"
                                    DataField="statuscd_desc" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn DataField="chutetyp_id" HeaderText="TypeID" Visible=false>
                                    <ItemTemplate>                                       
                                        <asp:Label ID="ch_type_lab" runat="server" Text='<%# Eval("chutetyp_id") %>' Visible=false>
                                        </asp:Label>                                  
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridBoundColumn UniqueName="chute_type" SortExpression="chute_type" HeaderText="Type"
                                    DataField="chute_type" ReadOnly="true" AllowFiltering="False" AllowSorting="true" >
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn UniqueName="enb_ind_desc" SortExpression="enb_ind_desc" HeaderText="Enabled"
                                    DataField="enb_ind_desc" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="int_ind_desc" SortExpression="int_ind_desc" HeaderText="International"
                                    DataField="int_ind_desc" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="trolley_type" SortExpression="trolley_type" HeaderText="Trolley Type"
                                    DataField="trolley_type" ReadOnly="true" AllowFiltering="False" AllowSorting="true">
                                </telerik:GridBoundColumn>

                                
                                <telerik:GridTemplateColumn DataField="seq_no" HeaderText="Sequence">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="ch_seq_TextBox" runat="server" Text='<%# Eval("seq_no") %>'/>
                                        
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                
                                
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>


                    
                </td>
            </tr>
            
        </table>
        <table width="100%">
           
            <tr>
                
                <td align="left">
                    <input type="hidden" id="Hidden2" />
                   
                    <asp:Label ID="Error" runat="server" Text="Label"></asp:Label>
                </td>
                
                <td align="right">
                    <input type="hidden" id="Hidden3" />
                   
                    <asp:Button runat="server" ID="Btn_chute" Text="Save Sequence" onclick="btnSave_Click" Width="132px"/>
                </td>
            </tr>

            <tr>
                
                <td align="left">
                    <input type="hidden" id="Hidden1" />
                   
                    <asp:Label ID="lbl_stack" runat="server" Text="Enter Start Stack Number"></asp:Label>
                    <asp:TextBox ID="tb_stack" runat="server" style="margin-left: 21px" 
                        Width="165px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_stack"
                                            ErrorMessage="* Enter Valid Stack " 
                        ForeColor="#CC3300"></asp:RequiredFieldValidator>
                </td>
                
                <td align="right">
                    <input type="hidden" id="Hidden4" />
                   
                    <asp:Button runat="server" ID="Btn_stack" Text="Manage Stack" Width="132px" 
                     onclick="Btn_stack_Click"   />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>