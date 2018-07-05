<%@ Page Language="C#" MasterPageFile="~/Pages/RI.Master" AutoEventWireup="true" CodeBehind="ClickCollectLateEmail.aspx.cs" Inherits="IHF.ApplicationLayer.Web.Pages.Admin.ClickCollectLateEmail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
<div class="childdefault">
<table>
<tr>
<td class="style1">&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td class="style1">&nbsp;</td>
<td>&nbsp;</td>
<td>Enter Store for and number of days for which van is late</td>
</tr>
<tr>
<td class="style1">&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td class="style1">Store:</td>
<td>&nbsp;</td>
<td>
          <telerik:RadComboBox runat="server" ID="rcbStore" Height="100px" EnableLoadOnDemand="true"
               ShowMoreResultsBox="true" EnableVirtualScrolling="true" 
              EmptyMessage="Type here ..." EnableAutomaticLoadOnDemand="True" 
              ItemsPerRequest="10" Width="228px">
          </telerik:RadComboBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="rcbStore" ErrorMessage="RequiredFieldValidator" 
              ForeColor="Red">Store Required</asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td class="style1">Number days Late:</td>
<td></td>
<td>
    <telerik:RadNumericTextBox ID="rnbNumDays" Runat="server" MaxValue="14" 
        MinValue="0" Width="60px">
        <NumberFormat DecimalDigits="0" />
    </telerik:RadNumericTextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="rnbNumDays" 
        ErrorMessage="Number of Days Late  Required" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td class="style1"></td>
<td></td>
<td>
    <telerik:RadButton ID="RadButton1" runat="server" onclick="RadButton1_Click" 
        Text="Send Emails">
    </telerik:RadButton>
    </td>
</tr>

<tr>
<td class="style1">&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>

<tr>
<td class="style1">&nbsp;</td>
<td>&nbsp;</td>
<td>
    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" 
        Text="No orders due for selected store" Visible="False"></asp:Label>
    </td>
</tr>

</table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 173px;
            text-align: right;
        }
    </style>
</asp:Content>
