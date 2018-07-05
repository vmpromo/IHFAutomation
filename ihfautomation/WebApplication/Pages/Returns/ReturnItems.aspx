<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="ReturnItems.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Returns.ReturnItems" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server" 
        onprerender="RadScriptManager1_PreRender">
    </telerik:RadScriptManager>
 
   <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <br />
    <table><tr><td>
        <asp:Label ID="lblScan" runat="server" Text="Order Scan: "></asp:Label>
        </td><td class="style10">
    <telerik:RadTextBox ID="rtbPackageBarcode"  Runat="server" 
        ontextchanged="rtbPackageBarcode_TextChanged" Skin="Sitefinity" Width="200px" 
                AutoPostBack="True">
    </telerik:RadTextBox>


        </td><td class="style10">

            <asp:Button ID="Button1" runat="server" Text="Enter" />

            <input id="Text1" type="text" style="visibility:hidden"/></td><td class="style10">

                &nbsp;</td><td class="style10">

            <asp:Label ID="lblCustomerUrn" runat="server" Text="CustomerUrn" 
                Visible="False"></asp:Label>
        </td><td class="style14">

                <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
                    Text="Accept" Visible="False" />
        </td></tr>
    <tr><td></td><td class="style10" colspan="2">
        <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
        </td><td class="style10">&nbsp;</td><td class="style10">
        <asp:Label ID="Label1" runat="server" Text="lblCancelledCount" Visible="False"></asp:Label>
        </td><td class="style14">
            <asp:Button ID="btnReject" runat="server" Text="Reject" onclick="btnReject_Click" 
                    Visible="False" width="61px" />
        </td></tr>
    </table>
    <asp:Panel ID="pnlOrder" runat="server">
    <table frame="border" style="width:100%;">
        <tr>
            <td class="style1">
                <strong style="text-align: right">Order Details</strong></td>
            <td class="style2">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8">
                <em class="style9">Order Number:</em></td>
            <td class="style2">
                <asp:Label ID="lblOrdernumber" runat="server" Text="77777777777"></asp:Label>
            </td>
            <td class="style11">
                Order Date:</td>
            <td class="style3">
                <asp:Label ID="lblOrderDate" runat="server" Text="03 Sep 2012"></asp:Label>
            </td>
            <td class="style12">
                Total Items:</td>
            <td class="style13">
                <asp:Label ID="lblTotalItems" runat="server" Text="5"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Customer:</td>
            <td class="style2" colspan="3">
                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
            </td>
            <td class="style12">
                Items to Process:</td>
            <td class="style13">
                <asp:Label ID="lblItemsToProcess" runat="server" Text="5"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
    </table>


    <br />


	<asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
        onrowcancelingedit="gvItems_RowCancelingEdit" 
        onrowdatabound="gvItems_RowDataBound" onrowediting="gvItems_RowEditing" 
        onrowupdated="gvItems_RowUpdated" Width="923px" 
        ondatabinding="gvItems_DataBinding" 
        onrowcommand="gvItems_RowCommand" onrowupdating="gvItems_RowUpdating" 
        ondatabound="gvItems_DataBound">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:CheckBox ID="cbEditing" runat="server" AutoPostBack="True" 
                        oncheckedchanged="cbEditing_CheckedChanged" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbEdited" runat="server" 
                        oncheckedchanged="cbEdited_CheckedChanged" AutoPostBack="True" /><asp:Hiddenfield runat="server" id="hidRowIndex" value='<%#Container.DataItemIndex %>' />
                </ItemTemplate>
                <ItemStyle Width="20px" />
            </asp:TemplateField>
            <asp:BoundField DataField="sku" HeaderText="SKU" ReadOnly="True" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="80px" />
            </asp:BoundField>
            
            <asp:ButtonField ButtonType="Image" CommandName="Edit" 
                ImageUrl="~/Images/Edit.GIF" Text="Edit" >
            
            <ItemStyle Width="20px" />
            </asp:ButtonField>
            
            <asp:BoundField DataField="skudescr" HeaderText="Description" ReadOnly="True">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="450px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAct" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlAct_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:TextBox ID="tbTaskDesc" runat="server" Height="75px" TextMode="MultiLine" 
                        Visible="False" Width="390px"></asp:TextBox>
                    <asp:Button ID="btnFinish" runat="server" onclick="btnFinish_Click" 
                        Text="Done" Visible="False" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbAction" runat="server"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            
            <asp:BoundField DataField="action" HeaderText="actioncode" Visible="False" />
            
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    </asp:Panel>





	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>

	</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 132px;
            text-align: right;
        }
        .style2
        {
        }
        .style3
        {
            width: 111px;
        }
        .style4
        {
            width: 111px;
            text-align: left;
        }
        .style5
        {
            width: 132px;
            text-align: right;
            font-size: x-small;
        }
        .style6
        {
            width: 110px;
            text-align: right;
        }
        .style8
        {
            text-align: right;
            font-size: x-small;
        }
        .style9
        {
            font-style: normal;
        }
        .style10
        {
        }
        .style11
        {
            width: 111px;
            text-align: right;
            font-size: x-small;
        }
        .style12
        {
            width: 110px;
            text-align: right;
            font-size: x-small;
        }
        .style13
        {
            width: 52px;
        }
        .newStyle1
        {
            visibility: hidden;
        }
        .style14
        {
            width: 275px;
            text-align: right;
        }
    </style>
</asp:Content>

